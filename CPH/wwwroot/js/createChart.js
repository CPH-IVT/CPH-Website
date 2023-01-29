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
	//---------------------------------------
	// countiesDiv - Holds the current HTML county element
	// chartArea - Holds the current HTML CharArea element
	// aggregateDataFull - Holds the aggregate value of all counties
	// aggregateDataSelected - Holds the aggregate value of the selected counties
	// listItems - Holds the list of items for the legend
	// dotColorArray - This string holds the current dot color for use on the chart
	// aggregateDisplay - This boolean is responsible for hiding/showing the aggregate display elements
	// displayFilter - This boolean handles the hiding/showing of the attribute filter elements
	// displayHealthAttribute - This boolean handles the hiding/showing of the health attribute list
	// filterItems - Holds the filter option
	// filterSelect - Holds the defualt and the selected filter's state
	// tempHide - DEBUG hide element
	// showStateData - This boolean handles the switch toggling of states and counties
	// fullRawData - Holds the seclected file's full data
	// chartName - Holds the chart's name --UNUSED--
	// dataHolder - Holds the selected file's identifying data
	// maxValue - Holds the selected column's max value
	// minValue - Holds the selected column's minimum value
	// year - Holds the selected file's year
	// healthAttribute - holds the user selected health attribute
	// healthAttributeData - Holds the data values and percentiles of the selected health attribute
	// selectedCounties - Holds the user selected counties
	// marks - Holds the charts draw data
	// plot - Holds the plot
	// regionData - Holds the selected region --UNUSED--
	// selectedRegions - Holds the selected region value
	//---------------------------------------
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
		displayHealthAttribute: false,
		filterItems: ["All", "Raw", "Numerator", "Denominator", "Ratio"],
		filterSelect: 'All',
		tempHide: false,
		showStateData: false,
		fullRawData: [],
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
		 * Searchs the dataset for the County FIPS Code column index and returns that value
		 * @param {any} dataset
		 */
		getCountyFIPSIndex(dataset) {

			let countyFIPS = 0;
			const columnPositionFIPS = 0;

			for (let i = 0; i < this.fullRawData.length; i++) {
				if (Object.keys(dataset[columnPositionFIPS])[i] == "County FIPS Code") {
					countyFIPS = i;
				};
			};
			return countyFIPS;
		},

		/**
		* This fuction handles the hiding of the health attribute list
		*/
		displayHealthAttributeToggle() {
			this.displayHealthAttribute = true;
		},

		/**
		* This fuction handles the hiding of the attribute filter items
		*/
		displayFilterToogle() {
			this.displayFilter = true;
		},

		/** 
		* this function handles the swtiching states and counties
		*/
		countyStateToggle() {
			// Negates the boolean
			this.showStateData = !this.showStateData;
			this.resetCountiesStateList();
			this.clearlegend();
		},
		/**
		* Clears the chart area
		*/
		clearChartArea() {
			this.aggregateDisplay = false;
			this.healthAttribute = null;
		},
		/**
		* Clears the Legend
		*/
		clearlegend() {
			this.listItems = [];
		},

		/**
		* Clears the aggregate data fields
		*/
		clearAggregateData() {
			this.aggregateDataFull = '';
			this.aggregateDataSelected = '';
		},

		/** 
		* This function clears and resets the county select display list
		*/
		resetCountiesStateList() {
			// Sets the arrays to empty, thus removing the chart dots
			this.selectedCounties = [];

			// Gets the county div
			let countiesDiv = document.getElementById("Counties");

			// removes the counties from the UL
			this.removeAllChildNodes(countiesDiv);

			// Gets the county list
			let counties = this.getCountyList(this.fullRawData);

			// populates the county div, thus resting the county/state list
			this.addDataToUL(this.fullRawData, counties, countiesDiv);
		},
		/**
		 * @param {any} arrayOfObjects
		* Receives an array of objects, and populates an array with the counties found within
		*/
		createLegendList(arrayOfObjects) {
			this.listItems = [];
			const infoIndex = 0;
			const percentileIndex = 0;
			const columnPositionCounty = 1;
			const columnPositionState = 2;

			//Builds a list of strings that contain the legend information
			for (let i = 0; i < arrayOfObjects.length; i++) {
				this.listItems.push(arrayOfObjects[i].info[infoIndex][columnPositionCounty] + ", " + arrayOfObjects[i].info[infoIndex][columnPositionState] + " || " + parseInt(arrayOfObjects[i].percentileInfo[percentileIndex]));
			}

			return this.listItems;
		},
		/**
		 * @param {object} dataObject
		 * @param {boolean} isFullColumn
		 * This function aggregates the passed data.
		 */
		columnMath(dataObject, isFullColumn) {
			// Checks if the array is empty, and returns an empty array if true. An empty array often occurs when a county is unselected 
			if (dataObject.length === 0) {
				let emptyArray = [];
				return emptyArray;
			};

			const valueIndex = 1;
			const columnPositionCountyRanked = 4;
			let columnSum = 0;
			let columnNumericDataArray = [];
			let rowValue = 0;
			// Populate a numeric array from a column within the dataset aggregate values
			for (let i = 0; i < dataObject.length; i++) {
				// Checks that a full column is selected and that it has been ranked. if true, sum the column values. else sum only the selected values
				if (isFullColumn === true && this.dataHolder[i][columnPositionCountyRanked] != "0") {
					rowValue = parseFloat(dataObject[i][valueIndex]);
					if (!isNaN(rowValue) && rowValue > 0) {
						columnNumericDataArray.push(rowValue);
						columnSum += rowValue;
					}
				} else if (isFullColumn === false) {
					rowValue = parseFloat(dataObject[i].percentileInfo[valueIndex]);
					if (!isNaN(rowValue) && rowValue > 0) {
						columnNumericDataArray.push(rowValue);
						columnSum += rowValue;
					}
				}
			};

			// Creates header
			let headerArray = ["Total", "Mean", "Min", "Max", "Range", "Count"];
			headerArray.unshift(`${this.healthAttribute}`)

			// Pushes aggregate data to the math array
			let mathArray = [0];
			mathArray.push(columnSum); // Sum
			mathArray.push(Math.round((columnSum / columnNumericDataArray.length) * 100) / 100); // Mean
			mathArray.push(Math.min(...columnNumericDataArray)); // Min
			mathArray.push(Math.max(...columnNumericDataArray)); // Max
			mathArray.push(Math.max(...columnNumericDataArray) - Math.min(...columnNumericDataArray)); // Range
			mathArray.push(columnNumericDataArray.length); // Count

			// Pairs the parallel arrays into a single array that can be returned to the caller.
			let tempArray = [];
			let resultArray = [];
			for (let i = 0; i < headerArray.length; i++) {
				tempArray.push(headerArray[i]);
				tempArray.push(mathArray[i]);
				resultArray.push(tempArray);
				tempArray = [];
			}

			let returnArray = [resultArray, isFullColumn];
			return returnArray;
		},
		/**
		 * @param {any} dataArray
		 *  Displays a health attribute's aggregate data on the chart page
		 */
		async displayAggregateData(dataArray) {

			const isFullColumnFlag = 1;
			const dataIndex = 0;
			const attributeHeader = 0;
			const nameIndex = 0;
			const valueIndex = 1;

			if (dataArray[isFullColumnFlag] === true) {
				// Populates the aggregateDisplay element
				this.aggregateDataFull = (`${dataArray[dataIndex][attributeHeader][attributeHeader]}\n`);
				for (let i = 1; i < dataArray[dataIndex].length; i++) {
					this.aggregateDataFull += (`${dataArray[dataIndex][i][nameIndex]} : ${dataArray[dataIndex][i][valueIndex]}\n`);
				};
				// Sets the aggregateDisplay element visible
				this.aggregateDisplay = true;
				return this.aggregateDataFull;
			} else if (dataArray[isFullColumnFlag] == false) {
				// Checks if the array is empty, and returns an empty string if true. An empty array often occurs when a county is unselected 
				if (dataArray[dataIndex].length === 0) {
					this.aggregateDataSelected = "";
					return this.aggregateDataSelected;
				}
				// Populates the aggregateDisplay element
				this.aggregateDataSelected = (`${dataArray[dataIndex][attributeHeader][attributeHeader]}\n`);
				for (let i = 1; i < dataArray[dataIndex].length; i++) {
					this.aggregateDataSelected += (`${dataArray[dataIndex][i][nameIndex]} : ${dataArray[dataIndex][i][valueIndex]}\n`);
				};
				return this.aggregateDataSelected;
            }
        },
		/**
		 * @param {HtmlNode} year
		 * This function fires on year selection, and reads the selected file
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

			// Reads the selected file into the "data" variable
			await d3.csv(`../uploads/${year.target.value}.csv`)
				.then((data) => {

					this.fullRawData = data;
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

			// Displays the filter dropdown
			this.displayFilterToogle();

			// Displays the health attribute list
			this.displayHealthAttributeToggle();
		},
		/**
		* Sets the region data - TODO: UNUSED 
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
		* @param {int} year
		* Gets the region data - TODO: UNUSED
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
		 * Populates the list elements
		 */
		addDataToUL(dataset, data, ulId, inputType = "checkbox") {

			// Searchs the dataset for the County FIPS Code index and saves that value to countyFIPS
			let countyFIPS = this.getCountyFIPSIndex(dataset)

			// Loops through and builds the html controls
			for (let i = 0; i < data.length; i++) {

				// Checks the filterSelect global and if it does not equal "All", filters the heath attribute list based upon the passed filter string
				if (this.filterSelect != "All") {
					if (ulId.id === "HealthAttrs") {
						let pos = data[i].search(this.filterSelect.toLowerCase())
						if (pos < 0) {
							continue;
						}
					};
				}

				// Checks the state of showStateData and skips the unwanted data
				if (ulId.id === "Counties") {
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
			}
		},
		/**
		 * @param {Array} data
		 * Returns a list of counties
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
		 * @param {HtmlNode} parent
		 * Removes all child nodes
		 */
		removeAllChildNodes(parent) {
			while (parent.firstChild) {
				parent.removeChild(parent.firstChild);
			}
		},
		/**
		 * @param {HtmlNode} node
		 * Checks if a node is empty
		 */
		checkIfNodeIsEmpty(node) {
			return node.childNodes.length === 0;
		},
		/**
		 * @param {any} event
		 * Handles the reading of the selected region checkbox - TODO: UNUSED
		 */
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
		 * This function handles the health attribute click event logic
		 * @param {Event} clickEvent
		 */
		async readHealthAttribute(clickEvent) {
			if (clickEvent["target"].nodeName === "LABEL") {

				this.healthAttribute = clickEvent["target"].textContent;		// This logic will execute if a user clicks a health attribute label \\

			} else if (clickEvent["target"].nodeName === "INPUT") {

				this.healthAttribute = clickEvent["target"].value;		// This logic will execute if a user clicks a health attribute input \\

			} else {

				console.error("The click event did not have a health attribute. Check the readHealthAttribute method.");
			}
		},
		/**
		 * This function adds and removes items from the selectedCounties global, based upon the results of a clickEvent
		 * @param {Event} clickEvent
		 */
		readCountyCheckbox(clickEvent) {

			// If a box is checked, run this code
			if (clickEvent["target"].checked == true) {
				this.selectedCounties.push(clickEvent["target"].value);
			}

			// If a box is enchecked, run this code
			if (clickEvent["target"].checked == false) {

				// Gets the index of the item to be removed
				let indexOfItemToRemove = this.selectedCounties.indexOf(clickEvent["target"].value);

				// If the index value is greater or equal to 0, splice the item from the array to remove it.
				if (indexOfItemToRemove >= 0) {
					this.selectedCounties.splice(indexOfItemToRemove, 1);
				}
			}
		},
		/**
		 * @param {Array} parsedCountStateArray
		 *  Creates an object that holds identifying and percentile/value information
		 */
		createInfoObjects(parsedCountStateArray) {
			const columnCountyName = 0;
			let countyStateArray = [];
			// for each parsed county state
			for (let a = 0; a < parsedCountStateArray.length; a++) {
				//get the county state index
				let index = this.getCountStateIndex(parsedCountStateArray[a][columnCountyName]);

				// get the county state information
				let info = this.getCountyInformation(parsedCountStateArray[a][columnCountyName]);

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
		 * Returns a marks array for the Plot object
		 * @param {Array} arrayOfObjects
		 */
		createPlotMarksArray(arrayOfObjects) {
			let marksArray = [Plot.ruleY([0]), Plot.ruleX([0]), Plot.line(this.healthAttributeData)];
			for (let a = 0; a < arrayOfObjects.length; a++) {
				// push plot dots to marks array
				marksArray.push(this.createPlotDots(arrayOfObjects[a]));
				// push plot text to marks array - TODO: See if this can be brought back has hovor text
				//marksArray.push(this.createPlotText(arrayOfObjects[a]));
			}

			return marksArray;
		},
		/**
		 * @param {Object} countyStateObject
		 * Plots grapth dots
		 */
		createPlotDots(countyStateObject) {
			// Plot.dot([93.95552771688067, 12212.33], { x: 93.95552771688067, y: 12212.33 })
			return Plot.dot([countyStateObject.percentileInfo[0], countyStateObject.percentileInfo[1]], { x: countyStateObject.percentileInfo[0], y: countyStateObject.percentileInfo[1] });
		},
		/**
		 * @param {Object} countyStateObject
		 * Plots graph Text - TODO: See if this can be brought back has hovor text
		 */
		createPlotText(countyStateObject) {
			// Plot text example: Plot.text([93.95552771688067, 12212.33], { x: 93.95552771688067, y: 12212.33, text: ["testing"], dy: -8 })
			return Plot.text([countyStateObject.percentileInfo[0], countyStateObject.percentileInfo[1]], { x: countyStateObject.percentileInfo[0], y: countyStateObject.percentileInfo[1], text: [`${countyStateObject.info[0][1]} ${countyStateObject.info[0][2]} ${countyStateObject.info[0][3]}`], dy: -8 });
		},
		/**
		 * @param {Array} countyStateArray
		 * Returns the county state information
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
		 * @param {Array} countyStateArray
		 * Returns the county state index
		 */
		getCountStateIndex(countyStateArray) {
			let index = this.dataHolder.findIndex(x => x[1] == countyStateArray[0] && x[2] == countyStateArray[1]);
			return index;
		},
		/**
		 * Loops through all the selected counties and split the county and state names into an array: [["Washington County", "TN"], ["Sullivan County", "TN"]];
		*/
		parseSelectedCountyStateArray() {
			let countyStateArray = [];
			for (var i = 0; i < this.selectedCounties.length; i++) {
				let parsed = this.parseCountyAndStateName(this.selectedCounties[i]);
				countyStateArray.push([parsed]);
			}

			return countyStateArray;
		},
		/**
		 * @param {Array} plotMarksArray
		 * Redraws the chart
		 */
		redrawChart(plotMarksArray) {
			this.removeAllChildNodes(this.chartArea);
			this.plot = this.createPlot(plotMarksArray);
			this.chartArea.appendChild(this.plot);
		},
		/**
		 * @param {Number} indexOfCountyState
		 * This function returns the county state percentile information
		 */
		getCountyStateDatapointPercentile(indexOfCountyState) {
			return this.healthAttributeData[indexOfCountyState];
		},
		/**
		 * @param {string} countyState
		 * Splits the counties and states on ","
		 */
		parseCountyAndStateName(countyState) {
			var split = countyState.split(",");

			split[0] = split[0].trim();
			split[1] = split[1].trim();

			return split;
		},
		/** 
		 * @param {any} plotMarksArray
		 * this function populates the plot's lines and dots
		 */
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

				// Returns the plot to the calling function when a county is clicked
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
				// Returns the plot to the calling function when a health attribute is clicked
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
		 * This Function watches for changes to the healthAttribute variable, and executes the following code
		 * */
		async healthAttribute() {

			// needed to re-fetch the area to draw the chart - #ChartArea.
			this.chartArea = document.getElementById("ChartArea");

			// Clears the old chart from the display
			this.removeAllChildNodes(this.chartArea);

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
			let aggregateArray = [];
			aggregateArray = this.columnMath(this.healthAttributeData, true);
			this.displayAggregateData(aggregateArray);

			// Resets the selected counties on the chart and within the list
			this.resetCountiesStateList()

		},
		/**
		* This function watches for changes to the selectedCounties variable, and executes the following code
		*/
		selectedCounties() {

			let parsedArray = this.parseSelectedCountyStateArray();

			//create object with information to be plotted. 
			let arrayOfObjects = this.createInfoObjects(parsedArray);

			// Populates the legend
			this.createLegendList(arrayOfObjects);

			// Creates and populates an array to display the selected counties aggregate data
			let aggregateArray = [];
			aggregateArray = this.columnMath(arrayOfObjects, false);
			this.displayAggregateData(aggregateArray);

			// create the plot marks: Dots and Text.
			let plotMarksArray = this.createPlotMarksArray(arrayOfObjects);

			// Redraw the chart
			this.redrawChart(plotMarksArray);
		},

		/**
		* This function watches for changes to the filterSelect variable, and executes the following code
		*/
		filterSelect() {
			let healthAttrs = document.getElementById("HealthAttrs");
			this.removeAllChildNodes(healthAttrs);
			this.addDataToUL(this.fullRawData, this.fullRawData.columns, healthAttrs, "radio");

			this.resetCountiesStateList();
			this.clearChartArea();
			this.clearlegend();

		},
		/**
		* This function watches for changes to the year variable, and executes the following code
	    */
		year() {
			this.clearChartArea();
        }

	}
})