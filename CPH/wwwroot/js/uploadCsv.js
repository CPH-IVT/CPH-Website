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
    },
    methods: {

        /**
         *
         *
         */
        calculatePercentage(rows) {
            //let count = 0;
            let tempValue = 0;
            let posArray = [];
            let indexMatchArray = [];
            let ratioArray = [];
            let attributeNameArray = [];
            let countyFIPSArray = [];
            let IndexConutyFIPS = 0;

            let newStr = ",Hello World"
            //console.log(typeof(rows))
            //console.log(typeof (rows[0]))
            //rows[0] = rows[0].concat(newStr)
            //console.log(rows[0])

            //console.log(rows[0].indexOf("numerator"))
            //console.log(rows[0][157])
            //console.log(rows[0].split(",")[157 -1])


            // Gets the string index of each occurrence of the word numerator
            var regex = /numerator/g, result, strIndices = [];
            while ((result = regex.exec(rows[0]))) {
                strIndices.push(result.index);
            }

            // Gets the index of each occurrence of commas based on a range of between 0 and the numerator string index
            commaIndices = [];
            for (let i = 0; i < strIndices.length; i++) {
                commaIndices.push((rows[0].substring(0, strIndices[i]).match(/,/g) || []).length)
            }

            // Builds an array with the healthattribute names
            for (let i = 0; i < commaIndices.length; i++) {
                attributeNameArray.push(rows[0].split(",")[commaIndices[i]].replace('numerator', 'ratio'))
            }


            let strHeader = "";
            for (let i = 0; i < attributeNameArray.length; i++) {
                strHeader = strHeader.concat(`,${attributeNameArray[i]}`)
            }


            rows[0] = rows[0].concat(strHeader)
            console.log(rows[0])


/*
            let numeratorArray = [];
            let strTemp = '';
            // Gets all rows and columns
            for (let row = 0; row < rows.length; row++) {

                if (row === 0) {

                    strTemp = 

                } else {

     
                    for (let column = 0; column < commaIndices.length; column++) {
                        //numeratorArray.push(rows[row].split(",")[commaIndices[column]]);
                    }
                }


 
            }*/


        // Builds an array of the aggregated values and the parallel FIPS codes
  
/*            for (let i = 0; i < rows.length; i++) {

                tempValue = (row / parseFloat(Object.values(dataset[i])[indexMatchArray[indexPosition] + 1]));

                // Replaces NaN values with 0
                if (isNaN(tempValue)) {
                    ratioArray.push(0);
                } else {
                    ratioArray.push(tempValue);
                }
            }*/



/*            // Creates an object with the aggregated data, column headings, and FIPS codes
            let objArray = []
            for (let i = 0; i < dataset.length; i++) {
                let obj = {
                    "County FIPS Code": countyFIPSArray[0],
                    [input]: ratioArray[0]
                }
                objArray.push(obj)
            }*/





/*
            // Testing
            let testArray = []
            let obj2 = {}
            for (let i = 0; i < attributeNameArray.length; i++) {
                let tempObj = {
                    [attributeNameArray[i]]: parseFloat((i + 2) * 77)
                }
                Object.assign(obj2, tempObj);
            }
            //console.log(obj2);

            for (let i = 0; i < 3193; i++) {
                testArray.push(obj2)
            }
            //console.log(testArray);

            return testArray;*/


			//console.log(this.bigData)
			//console.log(objArray)




        /*			for (let i = 0; i < attributeNameArray.length; i++) {
                        if (attributeNameArray[i] === testStr) {
                            console.log(attributeNameArray[i])
                        }
                    }*/



			//console.log(attributeNameArray)
			//console.log(numeratorArray)


        /*			let numeratorArray = [];
                    // Gets all rows and columns
                    for (let row = 0; row < dataset.length; row++) {
                        for (let column = 0; column < indexMatchArray.length; column++) {
                            numeratorArray.push(Object.values(dataset[row])[indexMatchArray[column]]);
                        }
                    }
    
                    console.log(numeratorArray)*/


					// TODOL GET THIS WORKING!
        /*					let test = this.calculatePercentage(this.bigData)
                            for (let i = 0; i < data.length; i++) {
                                Object.assign(data[i], test[0]);
                            }
                            console.log(data)*/
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
        * 
        */
        createNewCsv() {
            if (this.originalCsv === undefined) {
                console.log("Internal Error: createNewCsv Failed to load");
                return;
            }               
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

                //
                this.calculatePercentage(rows)

                // Get the year of the file
                this.year = rows[2].split(",")[5];

                // create a csv blob
                let csvBlob = new Blob([rows], { type: 'text/csv' });

                this.alteredCsv = new File([csvBlob], `${this.year}.csv`, { type: "text/csv", lastModified: new Date().getTime() });
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

