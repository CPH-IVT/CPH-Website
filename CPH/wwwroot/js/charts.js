
const Chart = {
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
    set setSvg(value) {
        this.sVg = value;
    },
    xScale: undefined,
    get getXScale() {
        return this.xScale;
    },
    set setXScale(value) {
        this.xScale = value;
    },
    yScale: undefined,
    get getYScale() {
        return this.yScale;
    },
    set setYScale(value) {
        this.yScale = value;
    },
    curveFunc: undefined,
    get getCurveFunction() {
        return this.curveFunc;
    },
    set setCurveFunction(value) {
        this.curveFunc = value;
    },
    setChartSizeProperties: function (margin, width, height) {
        this.setMargin = margin;
        this.setWidth = width;
        this.setHeight = height;

        
    },
    buildTickSpacingArray: function () {
        return [0, (this.width - (this.width * 0.95)), (this.width - (this.width * 0.90)), (this.width - (this.width * 0.85)), (this.width - (this.width * 0.80)), (this.width - (this.width * 0.75)), (this.width - (this.width * 0.70)), (this.width - (this.width * 0.65)), (this.width - (this.width * 0.60)), (this.width - (this.width * 0.55)), (this.width - (this.width * 0.50)), (this.width - (this.width * 0.45)), (this.width - (this.width * 0.40)), (this.width - (this.width * 0.35)), (this.width - (this.width * 0.30)), (this.width - (this.width * 0.25)), (this.width - (this.width * 0.20)), (this.width - (this.width * 0.15)), (this.width - (this.width * 0.10)), (this.width - (this.width * 0.05)), this.width];
    },
    csv: function () {
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