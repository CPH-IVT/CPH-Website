import { Chart } from '../js/charts.js';
import * as Plot from "https://cdn.skypack.dev/@observablehq/plot@0.4";


const ChartAttributes = new Vue({
	el: '#Chart',
	data: {
		countiesDiv: document.getElementById('Counties'),
		chartName: null,
		dataHolder: null,
		maxValue: 0,
		minValue: 0,
		year: 0,
		healthAttribute: null,
		healthAttributeData: [],
		selectedCounties: [],
		marks: [],
		plot: undefined,
	},
	methods: {
		/**
		 * 
		 * @param {number} year
		 */
		setChartAttributes(year) {
			this.year = year.target.value;
			//Get the columns div
			let healthAttrs = document.getElementById("HealthAttrs");
			let countiesDiv = document.getElementById("Counties");
			let checkHealthAttrs = this.checkIfNodeIsEmpty(healthAttrs);
			let checkCounties = this.checkIfNodeIsEmpty(countiesDiv);

			this.chart = Chart;


			if (checkCounties === false) {
				this.removeAllChildNodes(countiesDiv);
			}

			if (checkHealthAttrs === false) {
				this.removeAllChildNodes(healthAttrs);
			}

			d3.csv(`../uploads/${year.target.value}.csv`).then((data) => {

				let counties = this.getCountyList(data);

				//Place the csv data into a holder for later consumption 
				// this.dataHolder = data;

				this.addDataToUL(data.columns, healthAttrs, "radio"); // data.columns are the health attributes from the csv file.

				this.addDataToUL(counties, countiesDiv);
			});
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
		getCountyList(data) {
			let listOfCounties = [];
			for (var i = 0; i < data.length; i++) {
				var countyWithState = `${data[i]["Name"]}, ${data[i]["State Abbreviation"]}`;
				listOfCounties.push(countyWithState);
			}
			return listOfCounties;
		},
		removeAllChildNodes(parent) {
			while (parent.firstChild) {
				parent.removeChild(parent.firstChild);
			}
		},
		checkIfNodeIsEmpty(node) {
			if (node.childNodes.length > 0) {
				return false;
			}
			return true;
		},
		async readHealthAttribute(clickEvent) {
			if (clickEvent["target"].nodeName === "LABEL") {

				this.healthAttribute = clickEvent["target"].textContent;

			} else if (clickEvent["target"].nodeName === "INPUT") {

				this.healthAttribute = clickEvent["target"].value;

			} else {

				console.error("The click event did not have a health attribute. Check the readHealthAttribute method.");
			}
		},
		readCountyCheckbox(clickEvent) {

			if (clickEvent["target"].checked) {
				let countyAndState = this.parseCountyAndStateName(clickEvent["target"].value);
				this.selectedCounties.push(clickEvent["target"].value);
			}

			if (!clickEvent["target"].checked) {
				let indexOfItemToRemove = this.selectedCounties.indexOf(clickEvent["target"].value);

				// as long as the item is found in the array, continue. 
				if (indexOfItemToRemove > -1) {
					// splice the item from the array to remove it. 
					this.selectedCounties.splice(indexOfItemToRemove, indexOfItemToRemove);
				}

				if (indexOfItemToRemove === 0) {
					this.selectedCounties.shift();
				}
			}
		},
		getCountStateIndex() {
			let test = this.dataHolder.filter(
				function findCountState(row) {

					if (row[1] == "Washington County" && row[2] == "TN") {
						return row;
					}

				}
			);
			console.log(test);

			let index = this.dataHolder.indexOf(test);
			console.log(index);
        },
		parseCountyAndStateName(countyState) {
			var split = countyState.split(",");

			split[0] = split[0].trim();
			split[1] = split[1].trim();

			return split;
		}
	},
	watch: {
	   async healthAttribute() {

			if (this.healthAttribute !== null) {

				this.dataHolder = await d3.csv(`../uploads/${this.year}.csv`)
					.then((data) => {
						return data.map((x) => [Number(x[this.healthAttribute]), x["Name"], x["State Abbreviation"], x["5-digit FIPS Code"]]);
					});
				console.log(this.dataHolder);

				//let index = this.dataHolder.indexOf((d) => d[1] == "Washington", d[2] == "TN");

				this.dataHolder.sort((a, b) => a[0] - b[0]);

				this.healthAttributeData = this.dataHolder.map((element, index) => ([(index / this.dataHolder.length * 100), element[0]]));

				let chartArea = document.getElementById("ChartArea");

				console.log(this.healthAttributeData);

				this.plot = Plot.plot({
					x: {
						label: "Percentile →"
					},
					y: {
						label: `↑ ${this.healthAttribute}`
					},
					marks: [
						Plot.line(this.healthAttributeData),
						Plot.dot([3.131850923896023, 4037.62], { x: 3.131850923896023, y: 4037.62 }),
						Plot.dot([93.95552771688067, 12212.33], { x: 93.95552771688067, y: 12212.33 }),
						Plot.text([3.131850923896023, 4037.62], { x: 3.131850923896023, y: 4037.62, text: ["testing"], dy: -8 }),
						Plot.text([93.95552771688067, 12212.33], { x: 93.95552771688067, y: 12212.33, text: ["testing"], dy: -8 }),
					]
				});

				chartArea.appendChild(this.plot);
			}
		},
		selectedCounties() {
			console.log(this.selectedCounties);
			this.getCountStateIndex();
			
		}
	}
})