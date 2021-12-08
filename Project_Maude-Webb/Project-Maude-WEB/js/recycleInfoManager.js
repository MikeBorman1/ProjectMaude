angular.module('recycleInfoApp', [])
  .controller('recycleInfoController', function($scope, $timeout ,$http, $window) {
    $ctrl = this;
    
    $scope.recycleAPI = 'https://inthebagapi-dev.azurewebsites.net/api/getRecycleInfo?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.delAPI = 'https://inthebagapi-dev.azurewebsites.net/api/delRecycleInfo?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.recycleInfo = [];



    $http.post($scope.recycleAPI).then(function(success){
        $scope.recycleInfo = success.data;
        for(question in $scope.recycleInfo){
          var replace = $scope.recycleInfo[question].accepted;
          $scope.recycleInfo[question].accepted = replace.replace(/;/g, "\n");
          var replace = $scope.recycleInfo[question].rejected;
          $scope.recycleInfo[question].rejected = replace.replace(/;/g, "\n");
        }
    });

    $scope.delete = function(recycleInfoId){
      if($window.confirm('Warning this will delete this question from the database!')){
          data = 
          {
          "id":recycleInfoId
          };
          $http.post($scope.delAPI, data);
      
          $scope.recycleInfo = $scope.recycleInfo.filter(function (item) {
              return item.recycleInfoId != recycleInfoId;
          });
      }
      
  };
});