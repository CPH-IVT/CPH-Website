import { Chart } from '../js/charts.js';

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
        chart: Chart,
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

            Chart.InitializeChart


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

            if (this.healthAttribute !== null) {

                let csvData = await d3.csv(`../uploads/${this.year}.csv`)
                    .then((data) => {
                        return data.map((x) => x[this.healthAttribute]);
                    });

                this.healthAttributeData = csvData.map(Number);


                //this.chart.setHealthIndicatorMax = Math.max(...this.healthAttributeData);
                //this.chart.setHealthIndicatorMax = Math.min(...this.healthAttributeData);
                this.maxValue = Math.max(...this.healthAttributeData);
                this.minValue = Math.min(...this.healthAttributeData);
                this.healthAttributeData.sort((a, b) => a - b);

                console.log(this.healthAttributeData.length);

                let testData = this.healthAttributeData.map((element, index) => ({ y: element, x: (index / this.healthAttributeData.length * 100) }));

                //testData.sort((a, b) => b - a);

                console.log(testData);
                //this.chart.setYScale = this.healthAttributeData;
                //this.chart.setLineData = testData;
                this.chart.createLine(testData);
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
        parseCountyAndStateName(countyState) {
            var split = countyState.split(",");

            split[0] = split[0].trim();
            split[1] = split[1].trim();

            return split;
        }
    },
    watch: {
        maxValue() {
            if (!this.chart.getIsInitialized) {
                this.chart.InitializeChart({ top: 10, right: 40, bottom: 30, left: 40 }, document.getElementById("ChartArea").offsetWidth, this.maxValue, "#ChartArea");
            }
        },
        selectedCounties() {
            this.chart.setCountyNodes = this.selectedCounties;
        }
    }
})


function BuildChart(data, {
    x = ([x]) => x,
    y = ([, y]) => y,
    curve = d3.curveMonotoneX,
    marginTop = 20, // top margin, in pixels
    marginRight = 30, // right margin, in pixels
    marginBottom = 30, // bottom margin, in pixels
    marginLeft = 40, // left margin, in pixels
    width = 640, // outer width, in pixels
    height = 400, // outer height, in pixels
    xDomain, // [xmin, xmax]
    xRange = [marginLeft, width - marginRight], // [left, right]
    yDomain, // [ymin, ymax]
    yRange = [height - marginBottom, marginTop], // [bottom, top]
} = {}) {
    const X = d3.map(data, x);
    const Y = d3.map(data, y);
    const I = d3.range(X.length);
}