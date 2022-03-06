
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
    healthIndicatorMax: 0,
    get getHealthIndicatorMax() {
        return this.healthIndicatorMax;
    },
    set setHealthIndicatorMax(value) {
        this.healthIndicatorMax = value;
    },
    healthIndicatorMin: 0,
    get getHealthIndicatorMin() {
        return this.healthIndicatorMin;
    },
    set setHealthIndicatorMin(value) {
        this.healthIndicatorMin = value;
    },
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
    xScaleLinear: undefined,
    get getXScaleLinear() {
        if (this.xScaleLinear === undefined) {

            this.xScaleLinear = d3.scaleLinear()
                .domain([0, this.getHeight])
                .range([0, this.getWidth]);

        }
        return this.xScaleLinear;
    },
    set setXScaleLinear(value) {
        this.xScaleLinear = value;
    },
    xScale: undefined, // x is the percentile
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
    /** 
     *  Y axis is the vertical axis that as the numeric value that represents the health attribute.
     *  @type {d3.ScaleLinear}
     ***/
    yScale: undefined,
    /**
     * testing
     **/
    get getYScale() {
        
        if (this.yScale === undefined) {

            this.yScale = d3.scaleLinear()
                .domain([0, this.getHeight])
                .range([this.getHeight, 0]);

        }
        return this.yScale;
    },
    set setYScale(data) {
        this.yScale = d3.scaleLinear()
            .domain([0, Math.max(...data)])
            .range([this.getHeight, 0]);
    },
    curveFunc: undefined,
    get getCurveFunction() {
        return this.curveFunc;
    },
    set setCurveFunction(value) {
        this.curveFunc = value;
    },
    /**
     * This thing does something.
     * @param {number} margin
     * @param {number} width
     * @param {number} height
     * @param {string} chartDivName
     */
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
    createLine(data) {
        const line = d3.line().x(d => this.getXScaleLinear(d.x)).y(d => this.getYScale(d.y));

        this.createLineData(data);

        this.getSvg
            .append('path') // add a path to the existing svg
            .attr('stroke', 'black')
            .attr('fill', 'none')
            .attr('d', line);
    },
    calculatePercentile(indexOfitem, sizeOfArray) {
        return (indexOfitem + 1) / sizeOfArray * 100;
    },
    createLineData(data) {
        let length = data.length;

        for (var x = 0; x < length; x++) {
            this.getLineData.push({ x: this.calculatePercentile(x, length), y: data[x] });
        }
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

export { Chart };