

// set the dimensions and margins of the graph
var margin = { top: 10, right: 40, bottom: 30, left: 30 },
    width = document.getElementById("ChartArea").offsetWidth /*- margin.left - margin.right*/,
    height = 400 /*- margin.top - margin.bottom*/;
console.log(`Height: ${height}`);
// Line testing
var lineData = [{ x: 10, y: height }, { x: 150, y: 150 }, { x: 300, y: 100 }, { x: 450, y: 20 }, { x: 1100, y: 130 }]
var percentileLineData; 
// Percentile Y Axis
var percentileBottomAxisLine = ["0", "5th", "10th", "15th", "20th", "25th", "30th", "35th", "40th", "45th", "50th", "55th", "60th", "65th", "70th", "75th", "80th", "85th", "90th", "95th", "100th"];

var tickSpacing = [0, (width - (width * 0.95)), (width - (width * 0.90)), (width - (width * 0.85)),(width - (width * 0.80)), (width - (width * 0.75)), (width - (width * 0.70)), (width - (width * 0.65)), (width - (width * 0.60)), (width - (width * 0.55)), (width - (width * 0.50)), (width - (width * 0.45)), (width - (width * 0.40)), (width - (width * 0.35)), (width - (width * 0.30)), (width - (width * 0.25)), (width - (width * 0.20)), (width - (width * 0.15)), (width - (width * 0.10)), (width - (width * 0.05)), width];


// append the svg object to the body of the page
var sVg = d3.select("#ChartArea")
    .append("svg")
    .attr("width", width + margin.left + margin.right)
    .attr("height", height + margin.top + margin.bottom)
    // translate this svg element to leave some margin.
    .append("g")
    .attr("id", "InsideChart")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");


// X scale and Axis
//var x = d3.scaleLinear()
//    .domain([0, 20])         // This is the min and the max of the data: 0 to 100 if percentages
//    .range([0, width]);       // This is the corresponding value I want in Pixel

const xScale = d3.scaleOrdinal()
    .domain(percentileBottomAxisLine)
    .range(tickSpacing);

sVg
    .append('g')
    .attr("transform", "translate(0," + height + ")")
    .call(d3.axisBottom(xScale));

// Y scale and Axis
var y = d3.scaleLinear()
    .domain([0, 100])         // This is the min and the max of the data: 0 to 100 if percentages
    .range([height, 0]);       // This is the corresponding value I want in Pixel
sVg
    .append('g')
    .call(d3.axisLeft(y));

var insideChart = d3.select("#InsideChart").append("svg").attr("width", width).attr("height", height);

var curveFunc = d3.line()
    .curve(d3.curveBasis)
    .x(function (d) { return d.x })
    .y(function (d) { return d.y });

insideChart.append('path')
    .attr('d', curveFunc(lineData))
    .attr('stroke', 'black')
    .attr('fill', 'none');



function yAxis() {
    // Y scale and Axis
    var y = d3.scaleLinear()
        .domain(percentileBottomAxisLine)         // This is the min and the max of the data: 0 to 100 if percentages
        .range([height, 0]);       // This is the corresponding value I want in Pixel
    sVg
        .append('g')
        .call(d3.axisLeft(y));

   
}

function xAxis(xAxisValues) {
    // X scale and Axis
    var x = d3.scaleLinear()
        .domain(xAxisValues)         // This is the min and the max of the data: 0 to 100 if percentages
        .range([0, width]);       // This is the corresponding value I want in Pixel
    sVg
        .append('g')
        .attr("transform", "translate(0," + height + ")")
        .call(d3.axisBottom(x));

   
}

function csv() {
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

        console.log("Print data[3001]:")
        console.log(data[3001]);

        for (var i = 0; i < 10; i++) {

            // console.log(data);
        }

        console.log("Print data.columns:")
        console.log(data.columns);
    });
}

function getCSVDatapoints() {

}