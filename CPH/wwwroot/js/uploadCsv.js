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
        // TODO: probably better to upload this 'blacklist' data from an authority like the ISO lists of state codes
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
            // Create a temporary array of keys for rows that aggregate data from multiple counties - i.e., states and the U.S. as a whole
            let nonCountyBlacklistArray = this.nonCountyBlacklist.slice();

            // Create an index that is decremented from within the following loop
            let rowIndex = rows.length;
            while (rowIndex-- > 0) {
                // Holds the iterated row data
                let row = rows[rowIndex];
                // Holds iterated county column value data
                let thisKey = row.split(",")[4] === undefined ? undefined : row.split(",")[4].toUpperCase();
                // Loops through nonCountyBlacklistArray looking for matching values with the iterated column value
                nonCountyBlacklistArray.forEach(function (value) {
                    if (thisKey == value) {
                        //console.log(`Removed Row Index: ${rowIndex} - Non-county Value: ${thisKey}`); ////Debug Code: Consoles out the indices and values of the rows that were removed.
                        // Removes the matched non-county data from the CSV array
                        rows.splice(rowIndex, 1);
                        // Removes the item from the blacklist, assuming that items key this list
                        if (thisKey !== undefined) nonCountyBlacklistArray.splice(nonCountyBlacklistArray.indexOf(value), 1);
                        return;
                    }
                })
            };
            // returns the array
            console.log("State data removed from the county column");
            return rows;
        },
        /**
         * TODO: Remove this
        *NOTE: Unused at the moment
        * @param {Array} rows
        */
        columnMath(rows) {      
            // Populate a numeric array from a cloumn within the dataset and sum its values
            let columnSum = 0;
            let columnNumericDataArray = [];
            rows.forEach(function (row) {
                let rowValue = parseFloat(row.split(",")[8]);
                if (!isNaN(rowValue)) {
                    columnNumericDataArray.push(rowValue);
                    columnSum += rowValue
                }              
            });

            // console the results
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

