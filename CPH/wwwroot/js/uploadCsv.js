const csvSelector = new Vue({
    el: '#upload-form',
    // ****
    // driver variables for pocessing uploads
    //---------------------------------------
    // originalCsv - what's obtained from CHR
    // alteredCsv - original, minus internal table header, redundant content
    // fileReader - method for uploading file from CHR
    // year - year for CHR health data - CHR collects data annually
    // formData - Populated with the uploaded data and other nodes within the upload-form div - Unused?
    // userId - Holds the user ID
    // uploadDate - when originalCsv was originally uploaded
    // hashCodeMatch - Holds a boolean value that indicates if a matching hash code was found
    // fileYearMatch - Holds a boolean value that indicates if a matching year was found
    // uploadSuccess - Holds a boolean value that indicates if the upload was a success
    // overrideDuplicate - something to do with tracking whether user has said to overwrite an existing CHR data file??
    // chartParsingStatusDisplay - Holds a boolean value that controls the hiding/showing of the parsing status display
    // chartParsingStatusState - Holds a boolean value that indicates the state of the file parsing
    // chartParsingStatusText - Holds a text string used to communicate the state of paring to the user
    //---------------------------------------
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
        chartParsingStatusDisplay: false,
        chartParsingStatusState: false,
        chartParsingStatusText: ''
    },
    methods: {
        /**
         * Displays the loading status
         */
        chartParsingStatusDisplayToggle() {
            this.chartParsingStatusDisplay = true;
        },
        /**
         * Updates the loading status text
         */
        chartLoadingText() {
            if (this.chartParsingStatusState === false) {
                this.chartParsingStatusText = "Parsing"
                this.chartParsingStatusState = true
            } else if (this.chartParsingStatusState === true) {
                this.chartParsingStatusText = "Parsing Complete"
                this.chartParsingStatusState = false
            } else {
                this.chartParsingStatusText = "Error"
                this.chartParsingStatusState = false
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
        * Removes the U.S Total column from the dataset
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
        * Retrieves the user uploaded file, and validates that it is a csv file
        * @param {any} event
        */
        getSelectedCsv(event) {
            let csvFile = event.target.files[0];

            let fileExtension = event.target.files[0].name.slice(-4).toLowerCase();

            // Checks to see if the uploaded file is a CSV file
            if (fileExtension != ".csv") {
                alert("Incorrect file type has been selected");
                document.getElementById('OriginalFile').value = null;
                throw 'Incorrect file type has been selected';
            }

            // Checks to see if the uploaded file exist
            if (!csvFile) {
                alert("No file has been selected");
                throw 'No file has been selected';
            }
            return csvFile;
        },
        /**
        * Creates the new CSV file
        */
        createNewCsv() {
            if (this.originalCsv === undefined) {
                console.log("Internal Error: createNewCsv Failed to load");
                return;
            }     

            // Display the loading status
            this.chartParsingStatusDisplayToggle();
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
        /**
        * This function is called by the html when selecting a year. This function then populates the originalCsv and alteredCsv by calling the getSelectedCsv and createNewCsv functions
        * @param {any} event
        */
        setAlteredCsv(event) {
            // Checks to see if the uploaded file exist. This check is only relevant when selecting no file after a file was previously selected
            if (event.target.files.length < 1) {
                console.log("No file has been selected");
                location.reload();
            }

            this.originalCsv = this.getSelectedCsv(event);
            this.alteredCsv = this.createNewCsv();
        },
        /**
        * Populates the form with uploaded data and other nodes within the upload-form div.
        */
        createForm() {
            this.formData.append("AlteredFile", this.alteredCsv);
            this.formData.append("OriginalFile", this.originalCsv);
            this.formData.append("UserIdentity", this.userId);
            this.formData.append("UploadDate", this.uploadDate);
        },
        /**
        * This function is called by the html on the usage of the Upload button. This function validates if a file has been selected, has a matching year or hash with previous uploaded file, and returns success status.
        */
        async validateAndUploadCsv() {
            // checks to see if originalCsv is undefined. if undefined, throw error
            if (this.originalCsv === undefined) {
                alert("Attempted to upload incorrect file type");
                throw 'Attempted to upload incorrect file type';
            }

            // checks to see if originalCsv.name is populated. if not populated, throw error
            if (this.originalCsv.name === undefined) {
                alert("No file was selected")
                throw 'No file was selected';
            };
            this.createForm();
            await fetch('/Dashboard/ValidateAndUploadCSV', {
                method: 'POST',
                body: this.formData
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
                    // TODO: An improved error diagnose might be needed here
                    console.error("Bad server response!")
                }
            });
        },
        /**
        * This function overrides an older uploaded file
        */
        // TODO: Does this function even work?
        overrideCsvYear() {

            //DEBUG
            console.log("***In overrideCsvYear***")

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
        /**
        * This watch function returns an error message when hashCodeMatch is evaluated as true
        * @param {boolean} value
        */
        hashCodeMatch(value) {
            if (value) {
                alert("This file has already been uploaded.")
            }
            location.reload()
        },
        /**
        * This watch function returns an error message when fileYearMatch is evaluated as true
        * @param {boolean} value
        */
        fileYearMatch(value) {
            if (value) {
                // if the user agrees to override the current year, submit it to the server.
                this.overrideDuplicate = confirm("There is a duplicate year. Do you want to override the file on the server?");
            }
        },
        /**
        * This watch function calls the overrideCsvYear when overrideDuplicate is evaluated as true
        * @param {boolean} value
        */
        // TODO: Is this ever used?
        overrideDuplicate(value) {
            if (value) {
                this.overrideCsvYear()

            }
        },
        /**
        * This watch function returns a success message when uploadSuccess is evaluated as true
        * @param {boolean} value
        */
        uploadSuccess(value) {
            if (value) {
                alert("File was uploaded successfully")
                location.reload();
            }
        }
    }
})

