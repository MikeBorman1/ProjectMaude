angular.module('recycleBinApp', [])
  .controller('recycleBinController', function($scope, $timeout ,$http, $window) {
    $ctrl = this;
    
    $scope.recycleBinAPI = 'https://inthebagapi-dev.azurewebsites.net/api/getRecycleCodes?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.delAPI = 'https://inthebagapi-dev.azurewebsites.net/api/delRecycleCode?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.bins = [];



    $http.post($scope.recycleBinAPI).then(function(success){
        $scope.bins = success.data;
    });

    $scope.delete = function(recycleBinId){
      if($window.confirm('Warning this will delete this recycle bin from the database!')){
          data = 
          {
            "id":recycleBinId
          };
          $http.post($scope.delAPI, data);
      
          $scope.bins = $scope.bins.filter(function (item) {
              return item.recycleBinId != recycleBinId;
          });
      }
      
  };
});