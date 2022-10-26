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
	//****
	// driver variables for pocessing chart creation
	// countiesDiv -
	// chartArea -
	// aggregateDataFull -
	// aggregateDataSelected -
	// listItems -
	// dotColorArray -
	// aggregateDisplay -
	// tempHide -
	// showStateData -
	// chartName -
	// dataHolder -
	// maxValue -
	// minValue -
	// year -
	// healthAttribute -
	// healthAttributeData -
	// selectedCounties -
	// marks -
	// plot -
	// regionData -
	// selectedRegions -
	//****

	data: {
		countiesDiv: document.getElementById('Counties'),
		chartArea: document.getElementById("ChartArea"),
		aggregateDataFull: '',
		aggregateDataSelected: '',
		listItems: [],
		dotColorArray: [],
		aggregateDisplay: false,
		displayFilter: false,
		filterItems: ["All", "Raw", "Numerator", "Denominator"],
		filterSelect: 'All',
		tempHide: false,
		showStateData: false,
		bigData: [],
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
		regionData: undefined,
		selectedRegions: []
	},
	methods: {
		/**
		* 
		* 
		*/
		calculatePercentage(dataset) {



			
			let count = 0;
			let posArray = [];
			let indexMatchArray = [];
			// Builds an array of positive and negative numbers where a positive number means a match was found
			for (let i = 0; i < Object.keys(dataset[0]).length; i++) {
				let pos = Object.keys(dataset[1])[i].search("numerator");
				posArray.push(pos)
			}

			// Builds an index array with only positive matching values
			for (let i = 0; i < posArray.length; i++) {
				if (posArray[i] >= 0) {
					indexMatchArray.push(count);
					//console.log(Object.keys(dataset[0])[count + 1]);
				}
				count++;
			}
			console.log(indexMatchArray)

/*			let ratioArray = [];
			for (let i = 0; i < Object.keys(dataset[0]).length; i++) {
				let pos = Object.keys(dataset[1])[i].search("numerator");
				posArray.push(pos)
				if (pos >= 0) {
					if (!isNaN(parseFloat(Object.values(dataset[0])[i])) && !isNaN(parseFloat(Object.values(dataset[0])[i + 1]))) {
						ratioArray.push(parseFloat(Object.values(dataset[0])[i]) / parseFloat(Object.values(dataset[0])[i + 1]))
					} else {
						ratioArray.push(0)
                    }
				}
			}*/



			


        },
		/**
		* 
	    * This fuction handles the hiding of the attribute filter items
	    */
		displayFilterToogle() {
			this.displayFilter = true;
        },

		/** 
		*  
	    * this function handles the swtiching states and counties
		*/
		countyStateToggle() {
			// Negates the boolean
			this.showStateData = !this.showStateData;
			this.resetCounties()
		},
		/** 
		*
		* This function clears and resets the county select display list
		*/
		resetCounties() {
			// Gets the county div
			let countiesDiv = document.getElementById("Counties");

			// Sets the arrays to empty
			this.selectedCounties = [];
			this.listItems = [];

			// removes the counties from the UL
			this.removeAllChildNodes(countiesDiv);

			// Gets the county list
			let counties = this.getCountyList(this.bigData);

			// populates the county div
			this.addDataToUL(this.bigData, counties, countiesDiv);
        },
		/**
		 * @param {any} arrayOfObjects
		* Receives an array of objects, and populates an array with the counties found within
		*/
		createLegendList(arrayOfObjects) {
			this.listItems = [];
			for (let i = 0; i < arrayOfObjects.length; i++) {
				this.listItems.push(arrayOfObjects[i].info[0][1] + ", " + arrayOfObjects[i].info[0][2] + " || " + parseInt(arrayOfObjects[i].percentileInfo[0]));
			}
			return this.listItems;
        },
		/**
		 * @param {object} dataObject
		 * @param {bool} fullColumns
		 * If fullColumns is true, this function will attempt to aggregate a full column based on the data found within a passed healthAttributeData object
		 * if fullColumns is false, this function will attempt to aggregate the selected columns based on the data found within a passed createInfoObjects object
		 */
		columnMath(dataObject, fullColumns) {
			// Checks if the array is empty. If found true, returns an empty array
			// An empty array often occuers when a county is unselected 
			if (dataObject.length === 0) {
				let emptyArray = [];
				return emptyArray;
			};

			// Creates variables that will be populated in the following loop
			let columnSum = 0;
			let columnNumericDataArray = [];
			let rowValue = 0;

			// Populate a numeric array from a column within the dataset and sum its values
			for (let i = 0; i < dataObject.length; i++) {
				if (fullColumns === true && this.dataHolder[i][4] != "0") {
					rowValue = parseFloat(dataObject[i][1]);
					if (!isNaN(rowValue) && rowValue > 0) {
						columnNumericDataArray.push(rowValue);
						columnSum += rowValue;
					}
				} else if (fullColumns === false) {
					rowValue = parseFloat(dataObject[i].percentileInfo[1]);
					if (!isNaN(rowValue) && rowValue > 0) {
						columnNumericDataArray.push(rowValue);
						columnSum += rowValue;
					}
				}
			};
			// Creates an array to hold the titles of the aggregate data
			let headerArray = ["Total", "Mean", "Min", "Max", "Range", "Count"];

			// Checks the condition of fullColumns and applies the required header to the array
			if (fullColumns === true) {

				headerArray.unshift(`${this.healthAttribute}`)
			} else if (fullColumns === false) {
				headerArray.unshift(`${this.healthAttribute}`)
			}

			// Creates an array to hold the aggregate data
			let mathArray = [0];
			// Adds the sum of the data to the array
			mathArray.push(columnSum);
			// Adds the mean of the data to the array
			mathArray.push(Math.round((columnSum / columnNumericDataArray.length) * 100) / 100);
			// Adds the min of the data to the array
			mathArray.push(Math.min(...columnNumericDataArray));
			// Adds the max of the data to the array
			mathArray.push(Math.max(...columnNumericDataArray));
			// Adds the range of the data to the array
			mathArray.push(Math.max(...columnNumericDataArray) - Math.min(...columnNumericDataArray));
			// Adds the count to the array
			mathArray.push(columnNumericDataArray.length);

			// Pairs the parallel arrays into a single array that can be returned to the caller.
			let tempArray = [];
			let resultArray = [];
			for (let i = 0; i < headerArray.length; i++) {
				tempArray.push(headerArray[i]);
				tempArray.push(mathArray[i]);
				resultArray.push(tempArray);
				tempArray = [];
			}
			return resultArray;
			},
		/**
		 * Displays a health attribute's full aggregate data on the chart page
		 * @param {array} dataArray
		 */
		async displayAggregateDataFull(dataArray) {
			// Populates the aggregateDisplay element
			this.aggregateDataFull = (`${dataArray[0][0]}\n`)
			for (let i = 1; i < dataArray.length; i++) {
				this.aggregateDataFull += (`${dataArray[i][0]} : ${dataArray[i][1]}\n`)
			};

			// Sets the aggregateDisplay element visible
			this.aggregateDisplay = true;
			return this.aggregateDataFull;
		},
		/**
		 * Displays a health attribute's selected aggregate data on the chart page
		 * @param {array} dataArray
		 */
		async displayAggregateDataSelected(dataArray) {
			// Checks if the array is empty. If found true, returns an empty string
			// An empty array often occuers when a county is unselected 
			if (dataArray.length === 0) {
				this.aggregateDataSelected = "";
				return this.aggregateDataSelected;
			}

			// Populates the aggregateDisplay element
			this.aggregateDataSelected = (`${dataArray[0][0]}\n`)
			for (let i = 1; i < dataArray.length; i++) {
				this.aggregateDataSelected += (`${dataArray[i][0]} : ${dataArray[i][1]}\n`)
			};
			return this.aggregateDataSelected;
		},
		/**
		 * 
		 * @param {HtmlNode} year
		 */
		async setChartAttributes(year) {
			this.year = year.target.value;
			await this.setRegionData(this.year);

			//Get the columns div
			let healthAttrs = document.getElementById("HealthAttrs");
			let countiesDiv = document.getElementById("Counties");
			let regionsDiv = document.getElementById("Regions");
			let healthAttrsFieldIsEmpty = this.checkIfNodeIsEmpty(healthAttrs);
			let countiesFieldIsEmpty = this.checkIfNodeIsEmpty(countiesDiv);
			let regionsFieldIsEmpty = this.checkIfNodeIsEmpty(regionsDiv);

			if (countiesFieldIsEmpty === false) {
				this.removeAllChildNodes(countiesDiv);
			}

			if (healthAttrsFieldIsEmpty === false) {
				this.removeAllChildNodes(healthAttrs);
			}

			if (regionsFieldIsEmpty === false) {
				this.removeAllChildNodes(regionsDiv);
			}

			let regionNames = await this.getRegionNames(this.year);

			await d3.csv(`../uploads/${year.target.value}.csv`)
				.then((data) => {

					this.bigData = data;
					let counties = this.getCountyList(data);
					
					// Add to the html list.
					this.addDataToUL(data, data.columns, healthAttrs, "radio"); // data.columns are the health attributes from the csv file.
					this.addDataToUL(data, regionNames, regionsDiv);
					this.addDataToUL(data, counties, countiesDiv);
				})
				.catch((error) => {
					console.error("Getting selected year from the CSV directory failed.");
					console.error(error);
				});
		},
		/**
		*
	    *
	    */
		async setRegionData() {
			let regionData = await fetch('/Dashboard/ReadAllRegions')
				.then((response) => { return response.json(); })
				.then(data => { return data.filter(each => each.year === this.year) })
				.catch(error => {
					console.error(error);
				});

			this.regionData = regionData;
		},
		/**
		*
		*
		*/
		async getRegionNames(year) {
			let regionNames = await fetch('/Dashboard/ReadAllRegions')
				.then((response) => { return response.json(); })// handle the response
				.then(data => { return data.filter(each => each.year === this.year).map(x => x.name) })// then read the data
				.catch(error => {
					console.error(error);
				});	
			return regionNames;
        },
		/**
		 * @param {Array} dataset
		 * @param {Array} data
		 * @param {string} ulId UL = Unordered list in HTML
		 * @param {string} inputType
		 */
		addDataToUL(dataset, data, ulId, inputType = "checkbox") {
			// Searchs the dataset for the County FIPS Code index and saves that value to countyFIPS
			let countyFIPS = -70;
			for (let i = 0; i < data.length; i++) {
				if (Object.keys(dataset[0])[i] == "County FIPS Code") {
					countyFIPS = i;
				};
			};
			// Loops through and builds the html controls
			for (let i = 0; i < data.length; i++) {

				if (this.filterSelect != "All") {
					// Removes all item from the Health Attribute list that do not contain the word "raw"
					if (ulId.id === "HealthAttrs") {
						let pos = data[i].search(this.filterSelect.toLowerCase())
						if (pos < 0) {
							continue;
						}
					};
				}

				// Checks the state of showStateData and skips the unwanted data
				if (ulId.id === "Counties") {
					// DEBUG
					//console.log(`${Object.keys(dataset[i])[countyFIPS]}: ${Object.values(dataset[i])[countyFIPS]} ${Object.keys(dataset[i])[4]}: ${Object.values(dataset[i])[4]}`)
					// TODO: fix this if statement
					if (this.showStateData) {
						if (Object.values(dataset[i])[countyFIPS] != "0") {
							continue;
						};
					} else {
						if (Object.values(dataset[i])[countyFIPS] === "0") {
							continue;
						};
                    }
                }

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

				// Displays the filter dropdown
				this.displayFilterToogle()
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
			return node.childNodes.length === 0;
		},
		readRegionCheckbox(event) {
			if (clickEvent["target"].checked) {

				// Removing make sure this doesn't blow up.
				//let countyAndState = this.parseCountyAndStateName(clickEvent["target"].value);
				this.selectedRegions.push(clickEvent["target"].value);
				return;
			}

			//if (!clickEvent["target"].checked) {
			let indexOfItemToRemove = this.selectedRegions.indexOf(clickEvent["target"].value);

			// as long as the item is found in the array, continue. 
			if (indexOfItemToRemove > -1) {
				// splice the item from the array to remove it. 
				this.selectedRegions.splice(indexOfItemToRemove, indexOfItemToRemove);
			}

			if (indexOfItemToRemove === 0) {
				this.selectedRegions.shift();
			}
			//}
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
		 * TODO: This might need to be removed. 
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

				// push the object to an array
				countyStateArray.push(newObject);
            }
			//return the array of count state object information. 
			return countyStateArray;
		},
		/**
		 * 
		 * @param {Array} arrayOfObjects
		 * Returns a marks array for the Plot object
		 */
		createPlotMarksArray(arrayOfObjects) {
			let marksArray = [Plot.ruleY([0]),Plot.ruleX([0]),Plot.line(this.healthAttributeData)];
			for (let a = 0; a < arrayOfObjects.length; a++) {
				// push plot dots to marks array
				marksArray.push(this.createPlotDots(arrayOfObjects[a]));
				// push plot text to marks array
				//marksArray.push(this.createPlotText(arrayOfObjects[a]));
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
			this.plot = this.createPlot(plotMarksArray);
			

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
		},
		createPlot(plotMarksArray = []) {
			if (typeof (plotMarksArray[3]) != "undefined") {
				// Creates an array that holds the X and Y values for the plot marks
				let slicedArray = plotMarksArray.slice(3, plotMarksArray.length);
				let dotArray = [];
				for (let i = 0; i < slicedArray.length; i++) {
					dotArray.push(slicedArray[i].data);
				};

				this.dotColorArray = [];
				// Create a parallel color array for the dotArray
				let count = 0;
				for (let i = 0; i < dotArray.length; i++) {
					if (count === 0) {
						this.dotColorArray.push("red");
					} else if (count === 1) {
						this.dotColorArray.push("green");
					} else if (count === 2) {
						this.dotColorArray.push("blue");
					} else if (count === 3) {
						this.dotColorArray.push("DarkGray")
					} else if (count === 4) {
						this.dotColorArray.push("SaddleBrown");
					} else {
						this.dotColorArray.push("black");
					};

					// Resets the count to zero upon reaching the set limit
					count++
					if (count > 4) {
						count = 0;
					}
				};

				// DEBUG
				//console.log("_________________________________________________")
				//console.log(dotArray)
				//console.log(this.dotColorArray)
				//console.log(plotMarksArray)
				//console.log("_________________________________________________")

				return Plot.plot({
					margin: 80,
					grid: true,
					height: 700,
					style: {
						fontSize: "16px"
					},
					x: {
						ticks: 10,
						label: "Percentile →",
					},
					y: {
						label: `↑ ${this.healthAttribute}`
					},
					marks: [
						Plot.ruleY(plotMarksArray[0].data),
						Plot.ruleX(plotMarksArray[1].data),
						Plot.line(plotMarksArray[2].data),
						Plot.dot(dotArray, { fill: this.dotColorArray })
					]
				});
			} else {
				return Plot.plot({
					margin: 80,
					grid: true,
					height: 700,
					style: {
						fontSize: "16px"
					},
					x: {
						ticks: 10,
						label: "Percentile →",
					},
					y: {
						label: `↑ ${this.healthAttribute}`
					},
					marks: [
						Plot.ruleY(plotMarksArray[0]),
						Plot.ruleX(plotMarksArray[1]),
						Plot.line(this.healthAttributeData),
					]
				});

            }
        }
	},
	compute: {

    },
	watch: {
		/**
		 * TODO: what does this do>
		 * */
		async healthAttribute() {

			// needed to re-fetch the area to draw the chart - #ChartArea.
			this.chartArea = document.getElementById("ChartArea");

			// TODO: fix the removal of the counties on the chart when the health attribute is changed.
			this.removeAllChildNodes(this.chartArea);

			// TODO: uncheck selected counties in the html

			// set the selected counties array to empty
			if (this.healthAttribute === null) {
				console.error("Health Attribute is null.");
				return;
            }

			// read the health attribute from the csv correct year. 
			this.dataHolder = await d3.csv(`../uploads/${this.year}.csv`)
				.then((data) => {
					return data.map((x) => [Number(x[this.healthAttribute]), x["Name"], x["State Abbreviation"], x["5-digit FIPS Code"], x["County FIPS Code"]]);
				})
				.catch((error) => {
					console.error("Data mapping failed.");
					console.error(error);
				});

			this.dataHolder.sort((a, b) => a[0] - b[0]);

			// element[0] is the health attribute number/data-point. This also serves as an index into a parallel array of the dataHolder property. 
			this.healthAttributeData = this.dataHolder.map((element, index) => ([(index / this.dataHolder.length * 100), element[0]]));

			// TODO: what does this do?
			this.plot = this.createPlot([
				Plot.ruleY([0]),
				Plot.ruleX([0]),
				Plot.line(this.healthAttributeData),
			]);

			//this.plot.style({fontSize: 25});
			// Insert content into the #ChartArea Element.
			this.chartArea.appendChild(this.plot);

			// Creates and populates an array to display the chart's aggregate data
			let aggregateArrayFull = [];
			aggregateArrayFull = this.columnMath(this.healthAttributeData, true);
			this.displayAggregateDataFull(aggregateArrayFull);

			this.calculatePercentage(this.bigData);
		},
		/**
		* 
		*/
		selectedCounties() {
			let parsedArray = this.parseSelectedCountyStateArray(); // loop through all the selected counties and split the county and state names into an array: [["Washington County", "TN"], ["Sullivan County", "TN"]];

			//create object with information to be plotted. 
			let arrayOfObjects = this.createInfoObjects(parsedArray);

			// Populates the legend
			this.createLegendList(arrayOfObjects);

			// Creates and populates an array to display the selected counties aggregate data
			let aggregateArraySelected = [];
			aggregateArraySelected = this.columnMath(arrayOfObjects, false);
			this.displayAggregateDataSelected(aggregateArraySelected);

			// create the plot marks: Dots and Text.
			let plotMarksArray = this.createPlotMarksArray(arrayOfObjects);

			// Redraw the chart
			this.redrawChart(plotMarksArray);
		},

		filterSelect() {
			let healthAttrs = document.getElementById("HealthAttrs");
			this.removeAllChildNodes(healthAttrs);
			this.addDataToUL(this.bigData, this.bigData.columns, healthAttrs, "radio");
        }

	}
})