function initMap() {
    initialize();
}
function pinSymbol(color) {
    return {
        path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z M -2,-30 a 2,2 0 1,1 4,0 2,2 0 1,1 -4,0',
        fillColor: color,
        fillOpacity: 1,
        strokeColor: '#000',
        strokeWeight: 1,
        scale: 1
    };
}
function initialize() {
    var midlat = $('#midLat').val();
    var midlng = $('#midLng').val();
    var milesSelected = $('#milesSelected').val();

    if (midlat) {
        //GeoLocation is not supported by this browser.
        var mapOptions = {
            center: new google.maps.LatLng(midlat, midlng),
            zoom: ZoomMap(milesSelected),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_canvas"),
            mapOptions);
        // create a marker
        var latlng = new google.maps.LatLng(midlat, midlng);
        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            icon: pinSymbol("#0F0"),
            title: 'Midpoint Location'
        });
        var businesses = JSON.parse($('#Businesses').val());
        for (x = 0; x < businesses.length; x++)
        {
            latlng = new google.maps.LatLng(businesses[x].coordinates.latitude, businesses[x].coordinates.longitude);
            marker = new google.maps.Marker({
                position: latlng,
                map: map,
                title: businesses[x].name,
                url:businesses[x].url
            });
            google.maps.event.addListener(marker, 'click', function () {
                window.open(this.url);
            });
        }
    }
    else if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;

            var mapOptions = {
                center: new google.maps.LatLng(lat, lon),
                zoom: ZoomMap(milesSelected),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"),
                mapOptions);
            // create a marker
            var latlng = new google.maps.LatLng(lat, lon);
            var marker = new google.maps.Marker({
                position: latlng,
                map: map,
                title: 'Current Location'
            });
        });
    } else {
        //GeoLocation is not supported by this browser.
        var mapOptions = {
            center: new google.maps.LatLng(6.9167, 79.8473),
            zoom: ZoomMap(milesSelected),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_canvas"),
            mapOptions);
        // create a marker
        var latlng = new google.maps.LatLng(6.9167, 79.8473);
        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            title: 'Default Location'
        });
    }
    function ZoomMap(miles) {
        var zoom = 10;
        switch(miles) {
            case "1":
            case "2": 
            case "3": 
            case "4": 
            case "5": 
                zoom = 15;
                break;
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
            case "11":
            case "12":
            case "13":
            case "14":
            case "15":
            case "16": 
            case "17": 
            case "18": 
            case "19": 
            case "20":
                zoom = 13;
                break;
            case "21": 
            case "22": 
            case "23": 
            case "24": 
            case "25":
                zoom = 11;
                break;
            default:
                break;
        }
        return zoom;
    }
}