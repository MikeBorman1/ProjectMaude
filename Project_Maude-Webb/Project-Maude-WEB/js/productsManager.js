angular.module('productApp', [])
  .controller('ProductsController', function($scope, $timeout ,$http, $window) {
    $ctrl = this;
    
    $scope.prodAPI = 'https://inthebagapi-dev.azurewebsites.net/api/getProducts?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.delAPI = 'https://inthebagapi-dev.azurewebsites.net/api/delProduct?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.verifyAPI = 'https://inthebagapi-dev.azurewebsites.net/api/setVerify?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.products = [];



    $http.post($scope.prodAPI).then(function(success){
        $scope.products = success.data;
    });

    $scope.delete = function(productId){
        if($window.confirm('Warning this will delete this product from the database!')){
            data = 
            {
            "productId":productId
            };
            $http.post($scope.delAPI, data);
        
            $scope.products = $scope.products.filter(function (item) {
                return item.productId != productId;
            });
        }
        
    };

    $scope.verify = function(productId){
        data = 
        {
            "productId":productId
        };
        $http.post($scope.verifyAPI, data);
  
        for (var i in $scope.products) {
            if ($scope.products[i].productId == productId) {
                $scope.products[i].isVerified = true;
                break;
            }
        }
    }; 

    $scope.rowClass = function(product){
        if(product.flag){
            return "Flagged";
        }else if(product.isVerified){
            return "Verified";
        }else{
            return "notVerified"
        }
    };  
});