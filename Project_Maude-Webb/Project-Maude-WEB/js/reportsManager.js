angular.module('reportApp', [])
  .controller('ReportsController', function($scope, $timeout ,$http, $window) {
    $ctrl = this;
    $scope.delAPI = 'https://inthebagapi-dev.azurewebsites.net/api/delReport?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.reportsURL ="https://inthebagapi-dev.azurewebsites.net/api/getReports?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
    $scope.reports = [];

    $http.post($scope.reportsURL).then(function(success){
        $scope.reports = success.data
    });

    $scope.delete = function(reportId){
      if($window.confirm('Warning this will delete this Report from the database!')){
          data = 
          {
          "reportId":reportId
          };
          $http.post($scope.delAPI, data);
      
          $scope.reports = $scope.reports.filter(function (item) {
              return item.reportId != reportId;
          });
      }
    }
  });