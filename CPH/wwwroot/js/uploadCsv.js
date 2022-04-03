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
        overrideDuplicate: null
    },
    methods: {

        /**
        *
        * @param {Array} rows
        */
        removeStates(rows) {

            // Creates an arry of states that is used to remove rows from the dataset
            let removeCountyListArray = [
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
            ];

            // Creates and populates an array with the indices of the rows to be removed
            let countiesToRemove = [];
            rows.forEach(function (row) {
                removeCountyListArray.forEach(function (value) {
                    if (row.split(",")[4].toUpperCase() === value) {
                        //console.log(`Removed Row Index: ${rows.indexOf(row)} - County Value: ${row.split(",")[4]}`);
                        countiesToRemove.push(rows.indexOf(row));
                        //rows.splice(rows.indexOf(row), rows.indexOf(row));
                        return
                    }
                })
            });

            // Removes the rows by indices
            countiesToRemove.reverse().forEach(function (index) {
                //console.log(`Removed Row Index: ${index} - County Value: ${rows[index].split(",")[4]}`);
                rows.splice(index, 1);
            });

            // returns the array
            return rows;
        },
        /**
        *
        * @param {Array} rows
        */
        removeBlankRows(rows) {
            // Creates and populates an array with the indices of the rows to be removed
            let blankToRemove = [];
            rows.forEach(function (row) {
                if (row.split(",")[7] == "") {
                    //console.log(row.split(",")[7])
                    blankToRemove.push(rows.indexOf(row));
                }
            });

            // Removes the rows by indices
            blankToRemove.reverse().forEach(function (index) {
                //console.log(`Removed Row Index: ${index} - County Value: ${rows[index].split(",")[7]}`);
                rows.splice(index, 1);
            });
            //console.log(`Total Blank Rows Removed: ${blankToRemove.length}`)
        },
        /**
        *
        * @param {Array} rows
        */
        rowMath(rows) {

            //let dataRows = rows;
            //dataRows = Array.from(rows);
            //dataRows.shift();


            let dataMean = 0;

            //
            let columnMean = [];
            rows.forEach(function (row) {

                //console.log(`Index: ${dataRows.indexOf(row)} - Value: ${dataRows.split(",")[8]}`)
                //dataMean += parseFloat(dataRows.split(",")[8]);

                columnMean.push(row.split(",")[8]);
            });


            columnMean.shift();
            columnMean.forEach(function (value) {
                dataMean += parseFloat(value);
            })

            //
            console.log(dataMean);


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
            if (this.originalCsv !== undefined) {

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
                    this.removeStates(rows);

                    // Removes blank rows from the dataset
                    this.removeBlankRows(rows);

                    //
                    //this.rowMath(rows);

                    // Get the year of the file
                    this.year = rows[2].split(",")[5];

                    // create a csv blob
                    let csvBlob = new Blob([rows], { type: 'text/csv' });

                    this.alteredCsv = new File([csvBlob], `${this.year}.csv`, { type: "text/csv", lastModified: new Date().getTime() });
                }

                this.fileReader.readAsBinaryString(this.originalCsv);

                
            }
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

