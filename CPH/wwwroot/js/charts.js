const CountyNode = {
    stateName: '',
    get getStateName() {
        return this.stateName;
    },
    set setStateName(name) {
        this.stateName = name;
    },
    countyName: '',
    get getCountyName() {
        return this.countyName;
    },
    set setCountyName(name) {
        this.countyName = name;
    },
    healthAttribute: '',
    get getHealthAttribute() {
        return this.healthAttribute;
    },
    set setHealthAttribute(attributeName) {
        this.healthAttribute = attributeName;
    }

}

/**
 * A chart is an object with the following attributes: margin, width, height, lineData, percentileBottomAxisLine, tickSpacing, sVg, xScale, yScale, curveFunc.
 * */
const Chart = {
    isInitialized: false,
    get getIsInitialized() {
        return this.isInitialized;
    },
    set setIsInitialized(bool) {
        this.isInitialized = bool;
    },
    countyNodes: undefined,
    get getCountyNodes() {
        return this.countyNodes;
    },
    set setCountyNodes(countyNodeArray) {
        this.countyNodes = countyNodeArray;
    },
    margin: {},
    get getMargin() {
        return this.margin;
    },
    set setMargin(value) {
        this.margin = value;
    },
    width: undefined,
    get getWidth() {
        return this.width;
    },
    set setWidth(value) {
        this.width = value;
    },
    height: 0,
    get getHeight() {
        return this.height;
    },
    set setHeight(value) {
        this.height = value;
    },
    lineData: [],
    get getLineData() {
        return this.lineData;
    },
    set setLineData(value) {
        this.lineData = value;
    },
    percentileBottomAxisLine: ["0", "5th", "10th", "15th", "20th", "25th", "30th", "35th", "40th", "45th", "50th", "55th", "60th", "65th", "70th", "75th", "80th", "85th", "90th", "95th", "100th"],
    get getPercentileBottomAxisLine() {
        return this.percentileBottomAxisLine;
    },
    tickSpacing: [],
    get getTickSpacing() {
        return this.tickSpacing;
    },
    set setTickSpacing(value) {
        this.tickSpacing = value;
    },
    sVg: undefined,
    get getSvg() {
        return this.sVg;
    },
    set setSvg(chartAreaDiv) {

        this.sVg = d3.select(chartAreaDiv)
            .append("svg")
            .attr("width", this.getWidth + this.getMargin.left + this.getMargin.right)
            .attr("height", this.getHeight + this.getMargin.top + this.getMargin.bottom)
            // translate this svg element to leave some margin.
            .append("g")
            .attr("id", "InsideChart")
            .attr("transform", "translate(" + this.getMargin.left + "," + this.getMargin.top + ")");
    },
    xScale: undefined,
    get getXScale() {
        if (this.xScale === undefined) {
            this.xScale = d3.scaleOrdinal()
                .domain(this.getPercentileBottomAxisLine)
                .range(this.getTickSpacing);
        }
        return this.xScale;
    },
    set setXScale(value) {

    },
    yScale: undefined, // Y axis is the vertical axis that as the numeric value that represents the health attribute. 
    get getYScale() {
        if (this.yScale === undefined) {
            this.yScale = d3.scaleLinear().domain([0, this.getHeight]).range([this.getHeight, 0]);
        }
        return this.yScale;
    },
    set setYScale(data) {
        this.yScale = d3.scaleLinear().domain([0, d3.max(data, (d) => d.height)]).range([this.getHeight, 0]);
    },
    curveFunc: undefined,
    get getCurveFunction() {
        return this.curveFunc;
    },
    set setCurveFunction(value) {
        this.curveFunc = value;
    },
    InitializeChart(margin, width, height, chartDivName) {

        this.setChartSizeProperties(margin, width, height);
        this.defineTheAreaToDisplayChart(chartDivName);
        this.createTheBottomAxis();
        this.createYAxis();
        this.setIsInitialized = true;

    },
    yAxisCall(numberOfTicks) {
        return d3.axisLeft(this.getYScale)
            .ticks(numberOfTicks)
            .tickFormat((d) => d);
    },
    createYAxis() {
        this.getSvg.append("g").attr("class", "y axis").call(this.yAxisCall(3));
    },
    createTheBottomAxis() {
       

        this.getSvg.append('g')
            .attr("transform", "translate(0," + this.getHeight + ")")
            .call(d3.axisBottom(this.getXScale));
    },
    setChartSizeProperties(margin, width, height) {
        this.setMargin = margin;
        this.setWidth = width;
        this.setHeight = height;
        this.setTickSpacing = this.buildTickSpacingArray();
    },
    buildTickSpacingArray() {  
        return [0, (this.width * 0.05), (this.width * 0.10), (this.width * 0.15), (this.width * 0.20), (this.width * 0.25), (this.width * 0.30), (this.width * 0.35), (this.width * 0.40), (this.width * 0.45), (this.width * 0.50), (this.width * 0.55), (this.width * 0.60), (this.width * 0.65), (this.width * 0.70), (this.width * 0.75), (this.width * 0.80), (this.width * 0.85), (this.width * 0.90), (this.width * 0.95), this.width];
    },
    defineTheAreaToDisplayChart(chartAreaDiv) {

        this.setSvg = chartAreaDiv;
    },
    csv() {
        d3.csv('csv/analytic_data2010.csv').then(function (data) {

            var col1 = data.map(function (d) { return d['Teen births raw value'] });

            console.log(d3.sum(col1));

            console.log("Print data:")
            console.log(data);
            console.log([1, 2, 3].map(n => n * 10));

            var test = [];
            var length = data.length;

            for (var a = 0; a < length; a++) {
                // Getting all of the counties that aree washington
                if (data[a]['Name'].includes('Washington')) {
                    test.push(data[a]);
                }
            }
            console.log("Print test: ");
            console.log(test);

            console.log("Print data[3001]:");
            console.log(data[3001]);

            for (var i = 0; i < 10; i++) {

                // console.log(data);
            }

            console.log("Print data.columns:")
            console.log(data.columns);
        });
    }
}

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
        setChartAttributes(year) {
            this.year = year.target.value;
            //Get the columns div
            let healthAttrs = document.getElementById("HealthAttrs");
            let countiesDiv = document.getElementById("Counties");
            let checkHealthAttrs = this.checkIfNodeIsEmpty(healthAttrs);
            let checkCounties = this.checkIfNodeIsEmpty(countiesDiv);

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
                this.maxValue = Math.max(...this.healthAttributeData);
                this.minValue = Math.min(...this.healthAttributeData);
                this.healthAttributeData.sort((a, b) => a - b);
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
        healthAttribute() {
            if (!this.chart.getIsInitialized) {
                this.chart.InitializeChart({ top: 10, right: 40, bottom: 30, left: 30 }, document.getElementById("ChartArea").offsetWidth, 150, "#ChartArea");
            }
        },
        selectedCounties() {
            this.chart.setCountyNodes = this.selectedCounties;
        }
    }
})



export { Chart };