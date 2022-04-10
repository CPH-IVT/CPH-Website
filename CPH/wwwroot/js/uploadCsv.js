const csvSelector = new Vue({
    el: '#upload-form',
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
        nonCountyBlacklist: [
            "UNITED STATES",
            "ALABAMA",
            "ALASKA",
            "ARIZONA",
            "ARKANSAS",
            "CALIFORNIA",
            "COLORADO",
            "CONNECTICUT",
            "DELAWARE",
            "DISTRICT OF COLUMBIA",
            "FLORIDA",
            "GEORGIA",
            "HAWAII",
            "IDAHO",
            "ILLINOIS",
            "INDIANA",
            "IOWA",
            "KANSAS",
            "KENTUCKY",
            "LOUISIANA",
            "MAINE",
            "MARSHALL ISLANDS",
            "MARYLAND",
            "MASSACHUSETTS",
            "MICHIGAN",
            "MINNESOTA",
            "MISSISSIPPI",
            "MISSOURI",
            "MONTANA",
            "NEBRASKA",
            "NEVADA",
            "NEW HAMPSHIRE",
            "NEW JERSEY",
            "NEW MEXICO",
            "NEW YORK",
            "NORTH CAROLINA",
            "NORTH DAKOTA",
            "OHIO",
            "OKLAHOMA",
            "OREGON",
            "PENNSYLVANIA",
            "RHODE ISLAND",
            "SOUTH CAROLINA",
            "SOUTH DAKOTA",
            "TENNESSEE",
            "TEXAS",
            "UTAH",
            "VERMONT",
            "VIRGIN ISLANDS",
            "VIRGINIA",
            "WASHINGTON",
            "WEST VIRGINIA",
            "WISCONSIN",
            "WYOMING"
        ]
    },
    methods: {

        /**
        *
        * @param {Array} rows
        */
        removeNonCountyEntriesInDataset(rows) {
            // Creates an expendable array of state names that is used to identify non-county entries in the dataset
            let nonCountyBlacklistArray = this.nonCountyBlacklist;

            // Creates and populates an array with the indices of the rows containing state namnes
            let indicesOfNonCountiesToRemove = [];
            rows.forEach(function (row) {
                nonCountyBlacklistArray.forEach(function (value) {
                    if (row.split(",")[4].toUpperCase() === value) {
                        //console.log(`Removed Row Index: ${rows.indexOf(row)} - County Value: ${row.split(",")[4]}`); //Debug Code: Consoles out the indices and values of the rows to be removed.
                        indicesOfNonCountiesToRemove.push(rows.indexOf(row));
                        nonCountyBlacklistArray.splice(value, 1);
                        return;
                    }
                })
            });

            // Removes the rows by indices
            indicesOfNonCountiesToRemove.reverse().forEach(function (index) {
                //console.log(`Removed Row Index: ${index} - County Value: ${rows[index].split(",")[4]}`); ////Debug Code: Consoles out the indices and values of the rows that were removed.
                rows.splice(index, 1);
            });
            // returns the array
            console.log("State data removed from the county column");
            return rows;
        },
        /**       
        *NOTE: Unused at the moment
        * @param {Array} rows       
        */
        removeBlankRows(rows) {
            // Creates and populates an array with the indices of the rows to be removed
            let blankToRemove = [];
            rows.forEach(function (row) {
                if (row.split(",")[7] == "") {
                    blankToRemove.push(rows.indexOf(row));
                }
            });
            // Removes the rows by indices
            blankToRemove.reverse().forEach(function (index) {
                //console.log(`Removed Row Index: ${index} - County Value: ${rows[index].split(",")[4]}`); //Debug Code: Shows the removed index and county.
                rows.splice(index, 1);
            });
            //console.log(`Total Blank Rows Removed: ${blankToRemove.length}`) //Debug Code: Shows the amount of rows removed
        },
        /**
        *NOTE: Unused at the moment
        * @param {Array} rows
        */
        columnMath(rows) {      
            // Creates and populates a numeric array from a cloumn within the dataset
            let columnNumericDataArray = [];
            rows.forEach(function (row) {
                if (!isNaN(parseFloat(row.split(",")[8]))) {
                columnNumericDataArray.push(parseFloat(row.split(",")[8]));
                }              
            });
            // Sums the data within the array
            let columnSum = 0;
            columnNumericDataArray.forEach(function (value) {
                columnSum += value;
            })
            // Consoles out the data results
            console.log(`Column: ${rows[0].split(",")[8]}\nTotal: ${columnSum}\nMean: ${Math.round((columnSum / columnNumericDataArray.length) * 100) / 100}\nMin: ${Math.min(...columnNumericDataArray)}\nMax: ${Math.max(...columnNumericDataArray)}\nRange: ${Math.max(...columnNumericDataArray) - Math.min(...columnNumericDataArray)}`);
        },
        getSelectedCsv(event) {
            let csvFile = event.target.files[0];
            
            if (!csvFile) {
                console.error("No file has been selected");
                return undefined;
            }
            return csvFile;
        },
        createNewCsv() {
            if (this.originalCsv === undefined) {
                console.log("Internal Error: createNewCsv Failed to load")
            }
            this.fileReader.onload = (csvToRead) => {
                // get the content of the original csv that the user selected
                let contents = csvToRead.target.result;

                // split the csv on \r rows
                let rows = contents.split("\r");

                // Remove the second row of the CSV
                rows.splice(1, 1);

                // Removes the last row of the CSV
                rows.pop();

                // Removes the state totals from the dataset
                this.removeNonCountyEntriesInDataset(rows);

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

