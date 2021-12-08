var app = angular.module('recycleInfoEditApp', []);
  app.config(['$locationProvider', function($locationProvider){
    $locationProvider.html5Mode(true);    
  }]);
  app.controller('RecycleInfoEditController', function($scope, $timeout ,$http, $window, $location) {
    $ctrl = this;

    if($scope.param = $location.search().recycleId){
      $scope.recycleURL = 'https://inthebagapi-dev.azurewebsites.net/api/getSpecRecycleInfo?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      $scope.recycleInfo = [];
      $scope.data = 
      {
        "id":$scope.param
      }
      $http.post($scope.recycleURL,$scope.data).then(function(success){
        $scope.recycleInfo = success.data;
        var replace = $scope.recycleInfo[0].accepted;
        $scope.recycleInfo[0].accepted = replace.replace(/;/g, "\n");
        var replace = $scope.recycleInfo[0].rejected;
        $scope.recycleInfo[0].rejected = replace.replace(/;/g, "\n");
      });
  }
  $scope.editRecycleInfo = function(){
    if($scope.param = $location.search().recycleId){
      $scope.recycleEditURL = 'https://inthebagapi-dev.azurewebsites.net/api/setRecycleInfo?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      
      var acceptedlocal = document.getElementById('accepted').value;
      acceptedlocal = acceptedlocal.replace(/\n/g, ";");
      var rejectedlocal = document.getElementById('rejected').value;
      rejectedlocal = rejectedlocal.replace(/\n/g, ";");

      $scope.data = 
      {
        "id":parseInt($scope.param),
        "question":document.getElementById("questionName").value,
        "accepted":acceptedlocal,
        "rejected":rejectedlocal
      }
      $http.post($scope.recycleEditURL,$scope.data).then(function(success){
        $scope.status = success.status
        $window.location.href = "/recycleInfo.html";
      });
    } else{
      $scope.recycleEditURL = 'https://inthebagapi-dev.azurewebsites.net/api/addRecycleInfo?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      
      var acceptedlocal = document.getElementById('accepted').value;
      acceptedlocal = acceptedlocal.replace(/\n/g, ";");
      var rejectedlocal = document.getElementById('rejected').value;
      rejectedlocal = rejectedlocal.replace(/\n/g, ";");

      $scope.data = 
      {
        "question":document.getElementById("questionName").value,
        "accepted":acceptedlocal,
        "rejected":rejectedlocal
      }
      $http.post($scope.recycleEditURL,$scope.data).then(function(success){
        $scope.status = success.status
        $window.location.href = "/recycleInfo.html";
      });
    }
    
  }
  });