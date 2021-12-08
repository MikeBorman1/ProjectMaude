angular.module('warningApp', [])
  .controller('warningController', function($scope, $timeout ,$http, $window) {
    $ctrl = this;
    
    $scope.searchAPI = 'https://inthebagapi-dev.azurewebsites.net/api/getWarnings?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.delAPI = 'https://inthebagapi-dev.azurewebsites.net/api/delWarning?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.warnings = [];



    $http.post($scope.searchAPI).then(function(success){
        $scope.warnings = success.data;
    });

    $scope.delete = function(warningId){
      if($window.confirm('Warning this will delete this warning from the database!')){
          data = 
          {
          "warningId":warningId
          };
          $http.post($scope.delAPI, data);
      
          $scope.warnings = $scope.warnings.filter(function (item) {
              return item.warningId != warningId;
          });
      }
      
  };
});