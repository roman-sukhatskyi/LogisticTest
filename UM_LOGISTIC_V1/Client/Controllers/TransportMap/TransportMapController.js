mainModule.controller('TransportMapController', function ($rootScope, $scope, $location, TransportationService, SessionService, moduleConstants, NotificationService) {

    //variables 
    $scope.key = moduleConstants.googleMapsKey;
    //variables

    //methods
    $scope.initMap = function () {

    }

    $scope.initialize = function () {

        /*var map = new google.maps.Map(document.getElementById('mapsContainer'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
        });*/
    }

    //google.maps.event.addDomListener(window, 'load', $scope.initialize);
    //init
    $scope.initialize();
    //
});