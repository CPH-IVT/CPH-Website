const csvSelector = new Vue({
    el: '#upload-form',
    // ****
    // driver variables for pocessing uploads
    // originalCsv - what's obtained from CHR
    // alteredCsv - original, minus internal table header, redundant content
    // fileReader - method for uploading file from CHR
    // year - year for CHR health data - CHR collects data annually
    // formData - 
    // userId - 
    // uploadDate - when originalCsv was originally uploaded
    // hashCodeMatch - a hack - result of a test that checks if a newly uploaded file is the same as the previous; REALLY should be a character-by-character comparison
    // fileYearMatch - 
    // overrideDuplicate - something to do with tracking whether user has said to overwrite an existing CHR data file??
    // nonCountyBlacklist - keys for items in a CHR dataset that aggregate data from multiple counties: i.e., states and the overall US data
    // ****
    data: {
        originalCsv: {},
        alteredCsv: {},
        fileReader: new FileReader(),
        year: 0,
        formData: new FormData(),
        userId: document.getElementById("UserIdentity").value,
        uploadDate: document.getElementById("UploadDate").value,
        hashCodeMatch: null,
        fileYearMatch: null,
        uploadSuccess: null,
        overrideDuplicate: null,
        chartUploadStatusDisplay: false,
        chartUploadStatusState: false,
        chartUploadStatusText: ''
    },
    methods: {
        /**
         * 
         * Displays the loading status
         */
        chartUploadStatusDisplayToggle() {
            this.chartUploadStatusDisplay = true;
        },
        /**
         * 
         * Updates the loading status text
         */
        chartLoadingText() {

            if (this.chartUploadStatusState === false) {
                this.chartUploadStatusText = "Uploading"
                this.chartUploadStatusState = true
            } else if (this.chartUploadStatusState === true) {
                this.chartUploadStatusText = "Uploading Complete"
                this.chartUploadStatusState = false
            } else {
                this.chartUploadStatusText = "Error"
                this.chartUploadStatusState = false
            }
        },


        /**
         * Creates new ratio columns based upon the numerator and denominator column found within the data
         * @param {Object} rows
         */
        calculatePercentage(rows) {

            // Gets the string index of each occurrence of the word numerator
            var regex = /numerator/g, result, strIndices = [];
            while ((result = regex.exec(rows[0]))) {
                strIndices.push(result.index);
            }

            // Gets the index of each occurrence of commas based on a range of between 0 and the numerator string index
            commaIndices = [];
            for (let i = 0; i < strIndices.length; i++) {
                commaIndices.push((rows[0].substring(0, strIndices[i]).match(/,/g) || []).length);
            }

            // Builds an array with the healthattribute names
            let attributeNameArray = [];
            for (let i = 0; i < commaIndices.length; i++) {
                attributeNameArray.push(rows[0].split(",")[commaIndices[i]].replace('numerator', 'ratio'));
            }

            // Creates a header string
            let strHeader = "";
            for (let i = 0; i < attributeNameArray.length; i++) {
                strHeader = strHeader.concat(`,${attributeNameArray[i]}`);
            }
            rows[0] = rows[0].concat(strHeader);

            // Aggregate the numerator and denominator column, and concats them to each string row
            for (let rowNum = 0; rowNum < rows.length; rowNum++) {
                if (rowNum === 0) {
                    continue;
                } else {
                    let strNum = '';
                    for (let i = 0; i < commaIndices.length; i++) {
                        if (isNaN(parseFloat(rows[rowNum].split(",")[commaIndices[i]]) / parseFloat(rows[rowNum].split(",")[commaIndices[i] + 1]))) {
                            strNum = strNum.concat(`,`);
                        } else {
                            strNum = strNum.concat(`,${(parseFloat(rows[rowNum].split(",")[commaIndices[i]]) / parseFloat(rows[rowNum].split(",")[commaIndices[i] + 1])).toFixed(5)}`);
                        }          
                    };
                    rows[rowNum] = rows[rowNum].concat(strNum);
                }
            };

            return rows;
        },

        /**
        *
        * @param {Array} rows
        */
        removeUSTotal(rows) {
            // Loops through the rows looking for a matching value with the iterated column value
            // Create an index that is decremented from within the following loop
            let rowIndex = rows.length;
            while (rowIndex-- > 0) {
                // Holds the iterated row data
                let row = rows[rowIndex];
                // Holds iterated county column value data
                let thisKey = row.split(",")[4] === undefined ? undefined : row.split(",")[4].toUpperCase();
                if (thisKey == "UNITED STATES") {
                    // Removes the matched non-county data from the CSV array
                    rows.splice(rowIndex, 1);
                    // Removes the item from the blacklist, assuming that items key this list
                    if (thisKey !== undefined){
                    }
                }
            };
            return rows;
        },
        /**
        * 
        * @param {any} event
        */
        getSelectedCsv(event) {
            let csvFile = event.target.files[0];
            
            if (!csvFile) {
                console.error("No file has been selected");
                return undefined;
            }
            return csvFile;
        },
        /**
        * 
        * Creats the new CSV file
        */
        createNewCsv() {
            if (this.originalCsv === undefined) {
                console.log("Internal Error: createNewCsv Failed to load");
                return;
            }     

            // Display the loading status
            this.chartUploadStatusDisplayToggle();
            this.chartLoadingText();

            this.fileReader.onload = (csvToRead) => {
        
                // get the content of the original csv that the user selected
                let contents = csvToRead.target.result;

                // split the csv on \r rows
                let rows = contents.split("\r");

                // Remove the second row of the CSV
                rows.splice(1, 1);

                // Removes the last the last item in the array
                //rows = rows.pop();

                // Removes the U.S from the dataset
                this.removeUSTotal(rows);

                // Adds ratio columns to the dataset
                this.calculatePercentage(rows)

                // Get the year of the file
                this.year = rows[2].split(",")[5];

                // create a csv blob
                let csvBlob = new Blob([rows], { type: 'text/csv' });

                this.alteredCsv = new File([csvBlob], `${this.year}.csv`, { type: "text/csv", lastModified: new Date().getTime() });

                // update the loading status
                this.chartLoadingText();
            }
            this.fileReader.readAsBinaryString(this.originalCsv); 
        },
        setAlteredCsv(event) {
            this.originalCsv = this.getSelectedCsv(event);
            this.alteredCsv = this.createNewCsv();
        },
        createForm() {
            // Populate the form with uploaded data and other nodes within the upload-form div.
            this.formData.append("AlteredFile", this.alteredCsv);
            this.formData.append("OriginalFile", this.originalCsv);
            this.formData.append("UserIdentity", this.userId);
            this.formData.append("UploadDate", this.uploadDate);
        },
        async validateAndUploadCsv() {
            this.createForm();
            await fetch('/Dashboard/ValidateAndUploadCSV', {
                method: 'POST',
                body: this.formData,
            })
            .then(response => response.json())
            .then(data => {
                if (data["HashCodeMatch"]) {
                    this.hashCodeMatch = true
                } else if (data["FileYearMatch"]) {
                    this.fileYearMatch = true
                } else if (data["UploadSuccessful"]) {
                    this.uploadSuccess = true
                } else {
                    // should try harder to diagnose the error!
                    console.error("Bad server response!")
                }
            });
        },
        overrideCsvYear() {
           fetch("/Dashboard/OverrideCsvYear", {
                method: 'POST',
                body: this.formData
            })
            .then(response => response.json())
            .then(data => {
                if (data["FileUploaded"]) {
                    alert("File has been overridden.");
                    location.reload();
                }
            }).catch(error => {
                    console.error('Error: ', error)
            })
        }
    },
    watch: {
        hashCodeMatch(value) {
            if (value) {
                alert("This file has already been uploaded.")
            }
            location.reload()

        },
        fileYearMatch(value) {
            if (value) {
                // if the user agrees to override the current year, submit it to the server.
                this.overrideDuplicate = confirm("There is a duplicate year. Do you want to override the file on the server?");
            }
        },
        overrideDuplicate(value) {
            if (value) {
                this.overrideCsvYear()

            }
        },
        uploadSuccess(value) {
            if (value) {
                alert("File was uploaded successfully")
                location.reload();
            }
        }
    }
})

