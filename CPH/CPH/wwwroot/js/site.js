let map;

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

                for (i = 0; i < data.features.length; i++) {
                    //console.log(data.features[i].properties);

                    console.log(data.features[i].properties.state);
                    console.log(event.feature.j.NAME);

                    if (data.features[i].properties.state === event.feature.j.NAME) {
                        console.log(data.features[i].geometry.coordinates);
                        var lat, lng, cord, contentString, infoWindow, marker;

                        lng = data.features[i].geometry.coordinates[0];
                        lat = data.features[i].geometry.coordinates[1];
                        console.log(lat);
                        console.log(lng);
                        cord = { lat: lat, lng: lng };
                        console.log(cord);

                        contentString = `Hey, what's up. ${cord.lat}`;

                        //map = new google.maps.Map(document.getElementById("map"), {
                        //    zoom: 5,
                        //    center: cord,
                        //});

                        //map.center = cord;

                        infoWindow = new google.maps.InfoWindow({
                            content: contentString,
                        });

                        marker = new google.maps.Marker({
                            position: cord,
                            map: map,
                            title: "Popup Title, Heeeeyyyy!"
                        });

                        infoWindow.open(map, marker);


                    }
                }


                //console.log(data);
                //console.log(event);
                
            }
        });


        // DONT DELET
        //console.log(event.feature.j.NAME);
        //console.log(event.feature);


        //        map.data.setStyle({});

        //map = new google.maps.Map(document.getElementById("map"), {
        //    zoom: 8,
        //    center: { lat: 35.860119, lng: -86.660156 },
        //    // mapTypeId: "terrain",
        //});

        //map.data.loadGeoJson("../GeoJson/us-county-boundaries-tn.json");

        //// Set the global styles.
        //map.data.setStyle({
        //    fillColor: 'green',
        //    strokeWeight: 5
        //});
        //map.data.setStyle((feature) => {
        //    var testing = feature.getProperty("NAME");

            
           
        //});
    });
}



// Loop through the results array and place a marker for each
// set of coordinates.
//const eqfeed_callback = function (results) {
//    for (let i = 0; i < results.features.length; i++) {
//        const coords = results.features[i].geometry.coordinates;
//        const latLng = new google.maps.LatLng(coords[1], coords[0]);
//        new google.maps.Marker({
//            position: latLng,
//            map: map,
//        });
//    }
//};
