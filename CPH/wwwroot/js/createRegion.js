const RegionCreator = new Vue({
    el: '#Region',
    data: {
		counties: [],
		FIPS: [],
        selectedCounties: [],
        name: '',
        year: 0,

    },
	methods: {
		async saveRegion(event) {
			let regionName = document.getElementById("Name").value;
			
			// check to make sure the region is named

			// check to make sure a year has been selected

			// check to make sure that at least two counties have been selected
			if (this.selectedCounties.length <= 2 || regionName === "" || this.year === 0) {
				alert("Please complete the form before submitting.");
				return;
            }
			// if all check are ok, async call the server and attempt to save the region
			this.FIPS = this.getFIPSOfSelectedCounties();
			let region = {Name: this.name, Year: this.year, FIPS: this.FIPS.toString() };

			let stringifyRegion = JSON.stringify(region);

			await fetch('/Dashboard/SaveRegion', {
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: stringifyRegion,
			})
				.then(response => {
					console.log(response);
					if (response.ok) {
						alert("Region Saved");
						location.reload();
					} else {
						console.error(response);
						alert("For some reason your region did not save. Please try again.");
                    }
				})
			.catch(error => {
				console.error(error);
			});
        },
        async setRegionList(event) {
            this.year = event.target.value;
			let countiesDiv = document.getElementById("Counties");

			await d3.csv(`../uploads/${this.year}.csv`)
				.then((data) => {
					this.counties = this.getCountyList(data);
					this.FIPS = this.getFIPSCodesList(data);

					this.addDataToUL(this.counties, countiesDiv);
				}).catch((error) => {
					console.error("Getting selected year from the CSV directory failed.");
					console.error(error);
				});
		},
		getFIPSOfSelectedCounties() {
			let fipsArray = [];
			for (var i = 0; i < this.selectedCounties.length; i++) {

				// get the index of a selected county
				let countyIndex = this.counties.indexOf(this.selectedCounties[i]);

				// then, get its FIPS code with the index value
				let fipsCode = this.FIPS[countyIndex];

				fipsArray.push(fipsCode);
			}
			return fipsArray;
        },
		parseSelectedCountyStateArray() {
			let countyStateArray = [];
			for (var i = 0; i < this.selectedCounties.length; i++) {
				let parsed = this.parseCountyAndStateName(this.selectedCounties[i]);
				countyStateArray.push([parsed]);
			}

			return countyStateArray;
		},
		readCountyCheckbox(clickEvent) {

			if (clickEvent["target"].checked) {

				// Removing make sure this doesn't blow up.
				//let countyAndState = this.parseCountyAndStateName(clickEvent["target"].value);
				this.selectedCounties.push(clickEvent["target"].value);
				return;
			}

			//if (!clickEvent["target"].checked) {
			let indexOfItemToRemove = this.selectedCounties.indexOf(clickEvent["target"].value);

			// as long as the item is found in the array, continue. 
			if (indexOfItemToRemove > -1) {
				// splice the item from the array to remove it. 
				this.selectedCounties.splice(indexOfItemToRemove, indexOfItemToRemove);
			}

			if (indexOfItemToRemove === 0) {
				this.selectedCounties.shift();
			}
			//}
		},
		getCountyList(data) {
			let listOfCounties = [];
			for (var i = 0; i < data.length; i++) {
				var countyWithState = `${data[i]["Name"]}, ${data[i]["State Abbreviation"]}`;
				listOfCounties.push(countyWithState);
			}
			return listOfCounties;
		},
		getFIPSCodesList(data) {
			let listOfFIPS = [];
			for (var i = 0; i < data.length; i++) {
				let fips = `${data[i]["5-digit FIPS Code"]}`;
				listOfFIPS.push(fips);
			}
			return listOfFIPS;
        },
        addDataToUL(data, ulId, inputType = "checkbox") {
			for (let i = 0; i < data.length; i++) {

				//Create list item for the input and label to be inserted into
				let liNode = document.createElement("li");

				liNode.classList = ["form-check"];

				//Create input node
				let nodeInput = document.createElement("input");

				nodeInput.type = inputType;
				nodeInput.value = data[i];
				nodeInput.id = data[i];
				nodeInput.classList = ["form-check-input"];
				nodeInput.name = ulId;

				//Label for the checkboxes
				let label = document.createElement('label');

				label.htmlFor = data[i];

				// append the created text to the created label tag
				label.appendChild(document.createTextNode(`${data[i]}`));

				// append the li to the ul div
				ulId.appendChild(liNode);

				// append the checkbox and label to the li's
				liNode.appendChild(nodeInput);
				liNode.appendChild(label);


			}
		},
		removeAllChildNodes(parent) {
			while (parent.firstChild) {
				parent.removeChild(parent.firstChild);
			}
		},

    },
    watch: {
		selectedCounties() {
			let selectedCountiesDiv = document.getElementById("SelectedCounties");

			this.removeAllChildNodes(selectedCountiesDiv);
			
			//selectedCountiesDiv.innerHTML = this.selectedCounties.toString();
			for (var i = 0; i < this.selectedCounties.length; i++) {
				let liNode = document.createElement("li");
				liNode.classList = ["form-check"];
				liNode.innerText = this.selectedCounties[i];

				selectedCountiesDiv.appendChild(liNode);
            }
        }
    }
})