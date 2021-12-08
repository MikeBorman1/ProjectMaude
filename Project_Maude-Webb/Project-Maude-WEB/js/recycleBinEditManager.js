var app = angular.module('recycleBinEditApp', []);
  app.config(['$locationProvider', function($locationProvider){
    $locationProvider.html5Mode(true);    
  }]);
  app.controller('RecycleBinEditController', function($scope, $timeout ,$http, $window, $location) {
    $ctrl = this;

    if($scope.param = $location.search().recycleBinId){
      $scope.recycleBinURL = 'https://inthebagapi-dev.azurewebsites.net/api/getRecycleCode?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      $scope.bin = [];
      $scope.data = 
      {
        "id":parseInt($scope.param)
      }
      $http.post($scope.recycleBinURL,$scope.data).then(function(success){
        $scope.bin = success.data;
        $scope.isFlagged = $scope.bin[0].isBin;
      });
  }
  $scope.editRecycleBin = function(){
    if($scope.param = $location.search().recycleBinId){
      $scope.recycleBinEditURL = 'https://inthebagapi-dev.azurewebsites.net/api/setRecycleCode?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    

      $scope.data = 
      {
        "id":parseInt($scope.param),
        "recycleBin":document.getElementById("recycleBin").value,
        "isBin":document.getElementById('flagged').checked
      }
      $http.post($scope.recycleBinEditURL,$scope.data).then(function(success){
        $scope.status = success.status
        $window.location.href = "/recycleBin.html";
      });
    } else{
      $scope.recycleBinEditURL = 'https://inthebagapi-dev.azurewebsites.net/api/addRecycleCode?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    

      $scope.data = 
      {
        "recycleBin":document.getElementById("recycleBin").value,
        "isBin":document.getElementById('flagged').checked
      }
      $http.post($scope.recycleBinEditURL,$scope.data).then(function(success){
        $scope.status = success.status
        $window.location.href = "/recycleBin.html";
      });
    }

    
   
  }
  });