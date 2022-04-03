import { Chart } from '../js/charts.js';
import * as Plot from "https://cdn.skypack.dev/@observablehq/plot@0.4";

/**
 * 
 * Vue.js V2
 * https://v2.vuejs.org/v2/api/
 * 
 * */
const ChartAttributes = new Vue({
	el: '#Chart',
	data: {
		countiesDiv: document.getElementById('Counties'),
		chartArea: document.getElementById("ChartArea"),
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
		 * @param {HtmlNode} year
		 */
		async setChartAttributes(year) {
			this.year = year.target.value;

			//Get the columns div
			let healthAttrs = document.getElementById("HealthAttrs");
			let countiesDiv = document.getElementById("Counties");
			let healthAttrsFieldIsEmpty = this.checkIfNodeIsEmpty(healthAttrs);
			let countiesFieldIsEmpty = this.checkIfNodeIsEmpty(countiesDiv);

			// Might blowup but removing. 
			//this.chart = Chart;

			if (countiesFieldIsEmpty === false) {
				this.removeAllChildNodes(countiesDiv);
			}

			if (healthAttrsFieldIsEmpty === false) {
				this.removeAllChildNodes(healthAttrs);
			}

			await d3.csv(`../uploads/${year.target.value}.csv`)
				.then((data) => {

					let counties = this.getCountyList(data);

					// Add to the html list.
					this.addDataToUL(data.columns, healthAttrs, "radio"); // data.columns are the health attributes from the csv file.

					this.addDataToUL(counties, countiesDiv);
				})
				.catch((error) => {
					console.error("Getting selected year from the CSV directory failed.");
					console.error(error);
				});
		},
		/**
		 * 
		 * @param {Array} data
		 * @param {string} ulId UL = Unordered list in HTML
		 * @param {string} inputType
		 */
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
		/**
		 * 
		 * @param {Array} data
		 */
		getCountyList(data) {
			let listOfCounties = [];
			for (var i = 0; i < data.length; i++) {
				var countyWithState = `${data[i]["Name"]}, ${data[i]["State Abbreviation"]}`;
				listOfCounties.push(countyWithState);
			}
			return listOfCounties;
		},
		/**
		 * 
		 * @param {HtmlNode} parent
		 */
		removeAllChildNodes(parent) {
			while (parent.firstChild) {
				parent.removeChild(parent.firstChild);
			}
		},
		/**
		 * 
		 * @param {HtmlNode} node
		 */
		checkIfNodeIsEmpty(node) {
			// if stuff blows up check
			//if (node.childNodes.length > 0) {
			//	return false;
			//}
			//return true;

			return node.childNodes.length === 0;
		},
		/**
		 * 
		 * @param {Event} clickEvent
		 */
		async readHealthAttribute(clickEvent) {
			if (clickEvent["target"].nodeName === "LABEL") {

				this.healthAttribute = clickEvent["target"].textContent;

			} else if (clickEvent["target"].nodeName === "INPUT") {

				this.healthAttribute = clickEvent["target"].value;

			} else {

				console.error("The click event did not have a health attribute. Check the readHealthAttribute method.");
			}
		},
		/**
		 * This might need to be removed. 
		 * @param {Event} clickEvent
		 */
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
		/**
		 * 
		 * @param {Array} parsedCountStateArray
		 */
		createInfoObjects(parsedCountStateArray) {

			let countyStateArray = [];

			// for each parsed county state
			for (let a = 0; a < parsedCountStateArray.length; a++) {
				//get the county state index
				let index = this.getCountStateIndex(parsedCountStateArray[a][0]);

				// get the county state information
				let info = this.getCountyInformation(parsedCountStateArray[a][0]);

				// get the county state percentile information
				let percentileInfo = this.getCountyStateDatapointPercentile(index);

				// create an object with collected information
				let newObject = { info, percentileInfo };
				console.log(newObject);

				// push the object to an array
				countyStateArray.push(newObject);
				console.log(`countStateArrayObject:`, countyStateArray);
            }


			//return the array of count state object information. 
			return countyStateArray;
		},
		/**
		 * 
		 * @param {Array} arrayOfObjects
		 */
		createPlotMarksArray(arrayOfObjects) {
			let marksArray = [Plot.line(this.healthAttributeData)];
			for (let a = 0; a < arrayOfObjects.length; a++) {
				// push plot dots to marks array
				marksArray.push(this.createPlotDots(arrayOfObjects[a]));
				// push plot text to marks array
				marksArray.push(this.createPlotText(arrayOfObjects[a]));
			}

			return marksArray;
		},
		/**
		 * 
		 * @param {Object} countyStateObject
		 */
		createPlotDots(countyStateObject) {
			// Plot.dot([93.95552771688067, 12212.33], { x: 93.95552771688067, y: 12212.33 })
			return Plot.dot([countyStateObject.percentileInfo[0], countyStateObject.percentileInfo[1]], { x: countyStateObject.percentileInfo[0], y: countyStateObject.percentileInfo[1] });
		},
		/**
		 * 
		 * @param {Object} countyStateObject
		 */
		createPlotText(countyStateObject) {
			// Plot text example: Plot.text([93.95552771688067, 12212.33], { x: 93.95552771688067, y: 12212.33, text: ["testing"], dy: -8 })
			return Plot.text([countyStateObject.percentileInfo[0], countyStateObject.percentileInfo[1]], { x: countyStateObject.percentileInfo[0], y: countyStateObject.percentileInfo[1], text: [`${countyStateObject.info[0][1]} ${countyStateObject.info[0][2]} ${countyStateObject.info[0][3]}`], dy: -8 });
		},
		/**
		 * 
		 * @param {Array} countyStateArray
		 */
		getCountyInformation(countyStateArray) {
			let countyStateInformation = this.dataHolder.filter(
				function findCountState(row) {
					if (row[1] === countyStateArray[0] && row[2] === countyStateArray[1]) {
						return row;
					}
				}
			);
			//console.log(countyStateInformation);
			return countyStateInformation;
		},
		/**
		 * 
		 * @param {Array} countyStateArray
		 */
		getCountStateIndex(countyStateArray) {
			let index = this.dataHolder.findIndex(x => x[1] == countyStateArray[0] && x[2] == countyStateArray[1]);
			return index;
		},
		/**
		 * 
		 * 
		 * */
		parseSelectedCountyStateArray() {
			let countyStateArray = [];
			for (var i = 0; i < this.selectedCounties.length; i++) {
				let parsed = this.parseCountyAndStateName(this.selectedCounties[i]);
				countyStateArray.push([parsed]);
			}

			return countyStateArray;
		},
		/**
		 * 
		 * @param {Array} plotMarksArray
		 */
		redrawChart(plotMarksArray) {
			this.removeAllChildNodes(this.chartArea);

			this.plot = Plot.plot({
				x: {
					label: "Percentile →"
				},
				y: {
					label: `↑ ${this.healthAttribute}`
				},
				marks: plotMarksArray
			});

			this.chartArea.appendChild(this.plot);
		},
		/**
		 * 
		 * @param {Number} indexOfCountyState
		 */
		getCountyStateDatapointPercentile(indexOfCountyState) {
			return this.healthAttributeData[indexOfCountyState];
		},
		/**
		 * 
		 * @param {string} countyState
		 */
		parseCountyAndStateName(countyState) {
			var split = countyState.split(",");

			split[0] = split[0].trim();
			split[1] = split[1].trim();

			return split;
		}
	},
	watch: {
		/**
		 * Does await timeout and if so how long and can it be set? 
		 * 
		 * */
		async healthAttribute() {

			// needed to re-fetch the area to draw the chart - #ChartArea.
			this.chartArea = document.getElementById("ChartArea");


			this.removeAllChildNodes(this.chartArea);

			// uncheck selected counties in the html

			// set the selected counties array to empty

			if (this.healthAttribute === null) {
				console.error("Health Attribute is null.");
				return;
            }

			// read the health attribute from the csv correct year. 
			this.dataHolder = await d3.csv(`../uploads/${this.year}.csv`)
				.then((data) => {
					return data.map((x) => [Number(x[this.healthAttribute]), x["Name"], x["State Abbreviation"], x["5-digit FIPS Code"]]);
				})
				.catch((error) => {
					console.error("Data mapping failed.");
					console.error(error);
				});

			this.dataHolder.sort((a, b) => a[0] - b[0]);

			// element[0] is the health attribute number/data-point. This also serves as an index into a parallel array of the dataHolder property. 
			this.healthAttributeData = this.dataHolder.map((element, index) => ([(index / this.dataHolder.length * 100), element[0]]));

			// Need to remove this and hope it works! 
			//this.chartArea = document.getElementById("ChartArea");

			this.plot = Plot.plot({
				x: {
					label: "Percentile →"
				},
				y: {
					label: `↑ ${this.healthAttribute}`
				},
				marks: [
					Plot.line(this.healthAttributeData),
				]
			});

			// Insert content into the #ChartArea Element.
			this.chartArea.appendChild(this.plot);
			
		},
		selectedCounties() {
			let parsedArray = this.parseSelectedCountyStateArray(); // loop through all the selected counties and split the county and state names into an array: [["Washington County", "TN"], ["Sullivan County", "TN"]];

			//create object with information to be plotted. 
			let arrayOfObjects = this.createInfoObjects(parsedArray);

			// create the plot marks: Dots and Text.
			let plotMarksArray = this.createPlotMarksArray(arrayOfObjects);

			// Redraw the chart
			this.redrawChart(plotMarksArray);
		}
	}
})