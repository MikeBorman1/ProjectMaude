angular.module('materialApp', [])
  .controller('MaterialsController', function($scope, $timeout ,$http) {
    $ctrl = this;
    $scope.materialsURL = 'https://inthebagapi-dev.azurewebsites.net/api/getMaterials?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    
    $scope.materials= [];

    $http.post($scope.materialsURL).then(function(success){

        $scope.materials = success.data
    });  
  });