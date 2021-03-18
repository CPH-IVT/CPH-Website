// Set the dimensions and margins of the graph
var margin = { top: 10, right: 100, bottom: 30, left: 30 },
    width = 460 - margin.left - margin.right,
    height = 400 - margin.top - margin.bottom;

// Append the svg object to the body of the page
var svg = d3.select("#my_dataviz")
    .append("svg")
    //.attr("width" < width + margin.left + margin.right)
    //.attr("height", height + margin.top + margin.bottom)
    .append("g")
    .attr("transform", "translate(" + margin.left + ", " + margin.top + ")");

        // Read the data


function csv() {
    d3.csv('csv/analytic_data2010.csv').then(function (data) {
        var col1 = data.map(function (d) { return d['Teen births raw value'] });
        console.log(d3.sum(col1));
        var test = [];
        var length = data.length;
        for (var a = 0; a < length; a++) {
            // Getting all of the counties that aree washington
            if (data[a]['Name'].includes('Washington')) {
                test.push(data[a]);
            }
        }
        console.log(test);
        console.log(data[3001]);
        for (var i = 0; i < 10; i++) {
            console.log(data);
            // console.log(data);
        }
        console.log(data.columns);
    });
}