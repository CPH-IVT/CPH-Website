const RegionCreator = new Vue({
    el: '#Region',
    data: {
        counties: [],
        selectedCounties: [],
        name: '',
        year: 0,

    },
    methods: {
        async setRegionList(event) {
            this.year = event.target.value;
			let countiesDiv = document.getElementById("Counties");

			await d3.csv(`../uploads/${this.year}.csv`)
				.then((data) => {
					let counties = this.getCountyList(data);


					this.addDataToUL(counties, countiesDiv);
				}).catch((error) => {
					console.error("Getting selected year from the CSV directory failed.");
					console.error(error);
				});
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