var app = angular.module('collectionInfoEditApp', []);
  app.config(['$locationProvider', function($locationProvider){
    $locationProvider.html5Mode(true);    
  }]);
  app.controller('CollectionInfoEditController', function($scope, $timeout ,$http, $window, $location) {
    $ctrl = this;

    if($scope.param = $location.search().collectionId){
      $scope.locationURL = 'https://inthebagapi-dev.azurewebsites.net/api/getLocation?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      $scope.locations = [];
      $scope.data = 
      {
        "id":$scope.param
      }
      $http.post($scope.locationURL,$scope.data).then(function(success){
        $scope.locations = success.data;
      });
  }
  $scope.editCollectionInfo = function(){
    if($scope.param = $location.search().collectionId){
      $scope.collectionEditURL = 'https://inthebagapi-dev.azurewebsites.net/api/setLocation?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    

      $scope.data = 
      {
        "id":parseInt($scope.param),
        "locationName":document.getElementById("location").value,
        "latitude":parseFloat(document.getElementById("latitude").value),
        "longitude":parseFloat(document.getElementById("longitude").value)
      }
      $http.post($scope.collectionEditURL,$scope.data).then(function(success){
        $scope.status = success.status
        $window.location.href = "/collectioninfo.html";
      });
    } else{
      $scope.collectionEditURL = 'https://inthebagapi-dev.azurewebsites.net/api/addLocation?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      

      $scope.data = 
      {
        "locationName":document.getElementById("location").value,
        "latitude":parseFloat(document.getElementById("latitude").value),
        "longitude":parseFloat(document.getElementById("longitude").value)
      }
      $http.post($scope.collectionEditURL,$scope.data).then(function(success){
        $scope.status = success.status
        $window.location.href = "/collectioninfo.html";
      });
    }

    
   
  }
  });