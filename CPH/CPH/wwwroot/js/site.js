let map;
let marker;
let markers = [];
let usStates = [
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

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 5,
        center: { lat: 35.860119, lng: -86.660156 },
       // mapTypeId: "terrain",
    });
    // Create a <script> tag and set the USGS URL as the source.
    //const script = document.createElement("script");
    // This example uses a local copy of the GeoJSON stored at
    // http://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/2.5_week.geojsonp
   // script.src =

    map.data.loadGeoJson("../GeoJson/us-state-outlines.json");

    // Set the global styles.
    map.data.setStyle({
        fillColor: 'green',
        strokeWeight: 5
    });


    map.data.addListener('click', function (event) {

        $.ajax({
            method: "GET",
            url: "../GeoJson/state-capital-geojson.json",
            dataType: "json",
            success: function _success(data) {

                var i;
                var dataLength = data.features.length;

                for (i = 0; i < dataLength; i++) {


                    if (data.features[i].properties.state === event.feature.j.NAME) {

                        deleteMarkers();

                        var lat, lng, cord, contentString, infoWindow;

                        lng = data.features[i].geometry.coordinates[0];
                        lat = data.features[i].geometry.coordinates[1];
 
                        cord = { lat: lat, lng: lng };


                        contentString =
                            `<h1>${event.feature.j.NAME}</h1> <br> 
                            <a href="">View Charts</a><br>
                            <a href="">View State Counties</a><br>
                            <a href="">Select Counties</a><br>
                            ${cord.lat}`;

                        getStateCounties(event.feature.j.NAME);

                        infoWindow = new google.maps.InfoWindow({
                            content: contentString,
                        });

                        marker = new google.maps.Marker({
                            position: cord,
                            map: map,
                            title: `${event.feature.j.NAME}`
                        });

                        addMarker(marker);

                        infoWindow.open(map, marker);

                    }
                }
                
            }
        });



    });
}

function getStateCounties(state) {


    var stateAbb, i;
    var stateListLength = usStates.length;

    for (i = 0; i < stateListLength; i++) {
        if (usStates[i].name.toLowerCase() === state.toLowerCase()) {
           
            stateAbb = usStates[i].abbreviation;
        }
    }


            map.data.setStyle({});

    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 8,
        center: { lat: 35.860119, lng: -86.660156 },
        // mapTypeId: "terrain",
    });

    map.data.loadGeoJson(`../GeoJson/us-county-boundaries-${stateAbb}.json`);

    // Set the global styles.
    map.data.setStyle({
        fillColor: 'green',
        strokeWeight: 5
    });
    map.data.setStyle((feature) => {
        var testing = feature.getProperty("NAME");
    });
}

// Adds a marker to the map and push to the array.
function addMarker(marker) {
    //const marker = new google.maps.Marker({
    //    position: location,
    //    map: map,
    //});
    markers.push(marker);
}



// Sets the map on all markers in the array.
function setMapOnAll(map) {
    var a;
    var markerLength = markers.length;

    for (a = 0; a < markerLength; a++) {
        markers[a].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}
