var app = angular.module('reportEditApp', []);
  app.config(['$locationProvider', function($locationProvider){
    $locationProvider.html5Mode(true);    
  }]);
  app.controller('ReportEditController', function($scope, $timeout ,$http, $location, $window) {
    $ctrl = this;

    $scope.param = $location.search().reportId;
    $scope.reportURL = 'https://inthebagapi-dev.azurewebsites.net/api/getReport?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.report = [];
    $scope.data = 
    {
      "reportId":$scope.param
    }
    $http.post($scope.reportURL,$scope.data).then(function(success){
      $scope.report = success.data
    });

    $scope.materialsURL = 'https://inthebagapi-dev.azurewebsites.net/api/getMaterials?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.materials = [];
    $http.post($scope.materialsURL).then(function(success){
      $scope.materials = success.data
      $scope.matList = []
      for(materialnumb in $scope.materials){
        var newmat = $scope.materials[materialnumb].material
        $scope.matList.push(newmat)
      }
    });

    $scope.editProduct = function(){
      var checkboxes = document.getElementsByName('materials[]');
      var vals = "";
      for (var i=0, n=checkboxes.length;i<n;i++) 
      {
          if (checkboxes[i].checked) 
          {
              vals += ","+checkboxes[i].value;
          }
      }
      if (vals) vals = vals.substring(1);
      var editProductURL = "https://inthebagapi-dev.azurewebsites.net/api/setProductLinkMaterial?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
      var data = 
      {
        "productName":document.getElementById('productName').value,
        "productPhotoId":$scope.report.reportImageUrl.toLowerCase(),
        "barcode":document.getElementById('barcode').value,
        "materials":vals
      }
      $http.post(editProductURL,data).then(function(success){
        $scope.status = success.status;
        $window.location.href = "/reports.html";
      });
    }
  });