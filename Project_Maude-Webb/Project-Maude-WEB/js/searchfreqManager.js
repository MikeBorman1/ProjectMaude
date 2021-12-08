angular.module('searchfreqApp', [])
  .controller('searchfreqController', function($scope, $timeout ,$http) {
    $ctrl = this;
    
    $scope.searchAPI = 'https://inthebagapi-dev.azurewebsites.net/api/getSearchFrequencies?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
   
    $scope.terms = [];



    $http.post($scope.searchAPI).then(function(success){
        $scope.terms = success.data;
    });
});