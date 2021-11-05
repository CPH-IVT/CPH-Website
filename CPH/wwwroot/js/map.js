
usStates = [
    { name: 'ALABAMA', abbreviation: 'AL' },
    { name: 'ALASKA', abbreviation: 'AK' },
    { name: 'AMERICAN SAMOA', abbreviation: 'AS' },
    { name: 'ARIZONA', abbreviation: 'AZ' },
    { name: 'ARKANSAS', abbreviation: 'AR' },
    { name: 'CALIFORNIA', abbreviation: 'CA' },
    { name: 'COLORADO', abbreviation: 'CO' },
    { name: 'CONNECTICUT', abbreviation: 'CT' },
    { name: 'DELAWARE', abbreviation: 'DE' },
    { name: 'DISTRICT OF COLUMBIA', abbreviation: 'DC' },
    { name: 'FEDERATED STATES OF MICRONESIA', abbreviation: 'FM' },
    { name: 'FLORIDA', abbreviation: 'FL' },
    { name: 'GEORGIA', abbreviation: 'GA' },
    { name: 'GUAM', abbreviation: 'GU' },
    { name: 'HAWAII', abbreviation: 'HI' },
    { name: 'IDAHO', abbreviation: 'ID' },
    { name: 'ILLINOIS', abbreviation: 'IL' },
    { name: 'INDIANA', abbreviation: 'IN' },
    { name: 'IOWA', abbreviation: 'IA' },
    { name: 'KANSAS', abbreviation: 'KS' },
    { name: 'KENTUCKY', abbreviation: 'KY' },
    { name: 'LOUISIANA', abbreviation: 'LA' },
    { name: 'MAINE', abbreviation: 'ME' },
    { name: 'MARSHALL ISLANDS', abbreviation: 'MH' },
    { name: 'MARYLAND', abbreviation: 'MD' },
    { name: 'MASSACHUSETTS', abbreviation: 'MA' },
    { name: 'MICHIGAN', abbreviation: 'MI' },
    { name: 'MINNESOTA', abbreviation: 'MN' },
    { name: 'MISSISSIPPI', abbreviation: 'MS' },
    { name: 'MISSOURI', abbreviation: 'MO' },
    { name: 'MONTANA', abbreviation: 'MT' },
    { name: 'NEBRASKA', abbreviation: 'NE' },
    { name: 'NEVADA', abbreviation: 'NV' },
    { name: 'NEW HAMPSHIRE', abbreviation: 'NH' },
    { name: 'NEW JERSEY', abbreviation: 'NJ' },
    { name: 'NEW MEXICO', abbreviation: 'NM' },
    { name: 'NEW YORK', abbreviation: 'NY' },
    { name: 'NORTH CAROLINA', abbreviation: 'NC' },
    { name: 'NORTH DAKOTA', abbreviation: 'ND' },
    { name: 'NORTHERN MARIANA ISLANDS', abbreviation: 'MP' },
    { name: 'OHIO', abbreviation: 'OH' },
    { name: 'OKLAHOMA', abbreviation: 'OK' },
    { name: 'OREGON', abbreviation: 'OR' },
    { name: 'PALAU', abbreviation: 'PW' },
    { name: 'PENNSYLVANIA', abbreviation: 'PA' },
    { name: 'PUERTO RICO', abbreviation: 'PR' },
    { name: 'RHODE ISLAND', abbreviation: 'RI' },
    { name: 'SOUTH CAROLINA', abbreviation: 'SC' },
    { name: 'SOUTH DAKOTA', abbreviation: 'SD' },
    { name: 'TENNESSEE', abbreviation: 'TN' },
    { name: 'TEXAS', abbreviation: 'TX' },
    { name: 'UTAH', abbreviation: 'UT' },
    { name: 'VERMONT', abbreviation: 'VT' },
    { name: 'VIRGIN ISLANDS', abbreviation: 'VI' },
    { name: 'VIRGINIA', abbreviation: 'VA' },
    { name: 'WASHINGTON', abbreviation: 'WA' },
    { name: 'WEST VIRGINIA', abbreviation: 'WV' },
    { name: 'WISCONSIN', abbreviation: 'WI' },
    { name: 'WYOMING', abbreviation: 'WY' }
]

var margin = {
    top: 10,
    bottom: 10,
    left: 10,
    right: 10
}, width = parseInt(d3.select('#map').style('width'))
    , width = width - margin.left - margin.right
    , mapRatio = 0.5
    , height = width * mapRatio;
//    , active = d3.select(null);


function maptest() {

}

function loadJson() {


}

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

function GetStateAbv(state) {

    var i;
    var stateListLength = usStates.length;

    for (i = 0; i < stateListLength; i++) {
        if (usStates[i].name.toLowerCase() === state.toLowerCase()) {

            return usStates[i].abbreviation;
        }
    }
}
let countyCart = [];

const projection = d3.geoAlbersUsa();
const path = d3.geoPath().projection(projection);

// Define the map area
const svg = d3.select("#map")
    .append("svg")
    .attr("width", width)
    .attr("height", height);

// Define zoom
const zoom = d3.zoom()
    .scaleExtent([1, 8])
    .on("zoom", zoomed);

// Append the g tag to manipulate the map zoom
const g = svg.append("g");

let states;

// Call the zoom function for use
svg.call(zoom);

// Define the how the zoom event works. 
function zoomed(event) {
    const { transform } = event;
    g.attr("transform", transform);
    g.attr("stroke-width", 1 / transform.k);
}



async function StartMap() {
    //Choose to only use the United States
    //var projection = d3.geoAlbersUsa().translate([width / 2, height / 2]).scale(width);

    // how the geojson will be converted to an svg path s
    //var path = d3.geoPath().projection(projection);

    //var svg = d3.select("#map")
    //    .append("svg")
    //    .attr("width", width)
    //    .attr("height", height);

    await Promise.resolve(d3.json("GeoJson/us-state-outlines.json")).then(DisplayStates);
    //const request = async () => {
    //    try {
    //        const data = await d3.json("GeoJson/stat-capital-geojson.json");
    //        return data.features;
    //    } catch (error) {
    //        console.error(error);
    //        throw Error("Failed to load the data.");
    //    }
    //}

    //console.log(request);
}

async function DisplayStates(geoJson) {

    console.log(geoJson.features);

    states = g.selectAll("path")
        .data(geoJson.features)
        .enter()
        .append("path")
        .attr("cursor", "pointer")
        .attr("fill", "#b8b8b8")
        .style("stroke", "#fff")
        .style("stroke-width", "1")
        .on("click", ClickState)
        .attr("d", path);
}

async function ClickState(event, d) {

    // Get the states abbreviation.
    var stateAbv = GetStateAbv(event["explicitOriginalTarget"]["__data__"]["properties"].NAME);

    await Promise.resolve(d3.json(`GeoJson/us-county-boundaries-${stateAbv}.json`)).then(ClickCounty);

    const [[x0, y0], [x1, y1]] = path.bounds(d);

    event.stopPropagation();

    states.transition().style("fill", null);

    //d3.select(this).transition().style("fill", "red");

    svg.transition().duration(750).call(
        zoom.transform,
        d3.zoomIdentity
            .translate(width / 2, height / 2)
            .scale(Math.min(8, 0.9 / Math.max((x1 - x0) / width, (y1 - y0) / height)))
            .translate(-(x0 + x1) / 2, -(y0 + y1) / 2),
        d3.pointer(event, svg.node()));
}

function ClickCounty(event) {

    g.append("g")
        .attr("id", "counties")
        .selectAll("path")
        .data(event.features)
        .enter()
        .append("path")
        .attr("cursor", "pointer")
        .attr("d", path)
        .attr("class", "county-boundary")
        .on("click", AddCountToCart);


}

function AddCountToCart(event) {
    console.log(event);

    countyCart.push(event["explicitOriginalTarget"]);

    console.log(countyCart);
}
function DISStartMap() {

    // Mak change
    //Width and height of map
    //var width = 960;
    //var height = 500;

    //Choose to only use the United States
    var projection = d3.geoAlbersUsa().translate([width / 2, height / 2]).scale(width);

    // how the geojson will be converted to an svg path s
    var path = d3.geoPath().projection(projection);

    console.log(path);
    var svg = d3.select("#map")
        .append("svg")
        .attr("width", width)
        .attr("height", height);

    var div = d3.select("#map")
        .append("div")
        .attr("class", "tooltip")
        .style("opacity", 0);

    var results = [];
    var testing = d3.json("GeoJson/us-state-outlines.json").then(function (value) {
        console.log(value.features);


        svg.selectAll("path")
            .data(value.features)
            .enter()
            .append("path")
            .attr("d", path)
            .attr("fill", "#b8b8b8")
            .style("stroke", "#fff")
            .style("stroke-width", "1")
            .on("click", function (event) {
                var g = svg.append("g")
                    .attr('class', 'center-container center-items us-state')
                    .attr('transform', 'translate(' + margin.left + ',' + margin.top + ')')
                    .attr('width', width + margin.left + margin.right)
                    .attr('height', height + margin.top + margin.bottom);


                var stateAbv = GetStateAbv(event["explicitOriginalTarget"]["__data__"]["properties"].NAME);

                var counties = d3.json(`GeoJson/us-county-boundaries-${stateAbv}.json`).then(function (countyLines) {


                    g.append("g")
                        .attr("id", "counties")
                        .selectAll("path")
                        .data(countyLines.features)
                        .enter()
                        .append("path")
                        .attr("d", path)
                        .attr("class", "county-boundary")
                        .on("click", reset);
                });
                console.log(counties);


                var capitals = d3.json("GeoJson/state-capital-geojson.json").then(function (data) {

                    var i;
                    var dataLength = data.features.length;

                    for (i = 0; i < dataLength; i++) {


                        if (data.features[i].properties.state === event["explicitOriginalTarget"]["__data__"]["properties"].NAME) {
                            console.log(data.features[i].properties.state);

                            var lat, lng, cord, contentString, infoWindow;

                            lng = data.features[i].geometry.coordinates[0];
                            lat = data.features[i].geometry.coordinates[1];

                            cord = { lat: lat, lng: lng };

                            var marker = svg.append("svg:path")
                                .attr("class", "marker")
                                .attr("d", "M0,0l-8.8-17.7C-12.1-24.3-7.4-32,0-32h0c7.4,0,12.1,7.7,8.8,14.3L0,0z")
                                //.attr("cx", function (d) {
                                //    return projection([cord.lng, cord.lat]);
                                //})
                                //.attr("cy", function (d) {
                                //    return projection([cord.lng, cord.lat]);
                                //})
                                .transition()
                                .delay(400)
                                .duration(800)
                                .attr("transform", `translate(${projection([cord.lng, cord.lat])}) scale(1)`)
                                //.on('mouseover', function(d){})
                                ;
                        }
                    }
                });

                console.log(event["explicitOriginalTarget"]["__data__"]["properties"].NAME);
                // console.log();
            });

    });

    //console.log(testing);

    //var capitals = d3.json("GeoJson/state-capital-geojson.json").then(function (data) {

    //                    var lat, lng, cord, contentString, infoWindow;

    //                    lng = data.features[i].geometry.coordinates[0];
    //                    lat = data.features[i].geometry.coordinates[1];

    //                    cord = { lat: lat, lng: lng };

    //    var marker = svg.append("svg:path")
    //        .attr("class", "marker")
    //        .attr("d", "M0,0l-8.8-17.7C-12.1-24.3-7.4-32,0-32h0c7.4,0,12.1,7.7,8.8,14.3L0,0z")
    //        .attr("transform", "translate(" + x + "," + y + ") scale(0)")
    //        .transition()
    //        .delay(400)
    //        .duration(800)
    //        .ease("elastic")
    //        .attr("transform", "translate(" + x + "," + y + ") scale(.75)")
    //        //.on('mouseover', function(d){})
    //        ;
    //})


}


function clicked(d) {
    if (d3.select('.background').node() === this) return reset();

    if (active.node() === this) return reset();

    active.classed("active", false);
    active = d3.select(this).classed("active", true);

    var bounds = path.bounds(d),
        dx = bounds[1][0] - bounds[0][0],
        dy = bounds[1][1] - bounds[0][1],
        x = (bounds[0][0] + bounds[1][0]) / 2,
        y = (bounds[0][1] + bounds[1][1]) / 2,
        scale = .9 / Math.max(dx / width, dy / height),
        translate = [width / 2 - scale * x, height / 2 - scale * y];

    g.transition()
        .duration(750)
        .style("stroke-width", 1.5 / scale + "px")
        .attr("transform", "translate(" + translate + ")scale(" + scale + ")");
}

function reset() {
    active.classed("active", false);
    active = d3.select(null);

    g.transition()
        .delay(100)
        .duration(750)
        .style("stroke-width", "1.5px")
        .attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');

}
