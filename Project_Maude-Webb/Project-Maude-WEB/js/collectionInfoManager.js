angular.module('collectionInfoApp', [])
  .controller('collectionInfoController', function($scope, $timeout ,$http, $window) {
    $ctrl = this;
    
    $scope.collectionAPI = 'https://inthebagapi-dev.azurewebsites.net/api/getLocations?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.delAPI = 'https://inthebagapi-dev.azurewebsites.net/api/delLocation?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.locations = [];



    $http.post($scope.collectionAPI).then(function(success){
        $scope.locations = success.data;
    });

    $scope.delete = function(locationId){
      if($window.confirm('Warning this will delete this warning from the database!')){
          data = 
          {
            "id":locationId
          };
          $http.post($scope.delAPI, data);
      
          $scope.locations = $scope.locations.filter(function (item) {
              return item.locationId != locationId;
          });
      }
      
  };
});