var app = angular.module('warningEditApp', []);
  app.config(['$locationProvider', function($locationProvider){
    $locationProvider.html5Mode(true);    
  }]);
  app.controller('WarningEditController', function($scope, $timeout ,$http, $window) {
    $ctrl = this;

    $scope.editWarning = function(){
      var editWarningURL = "https://inthebagapi-dev.azurewebsites.net/api/setWarning?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
      var data = 
      {
        "warning":document.getElementById('warning').value
      }
      $http.post(editWarningURL,data).then(function(success){
        $scope.status = success.status;
        $window.location.href = "/warnings.html";
      });
    }
  });