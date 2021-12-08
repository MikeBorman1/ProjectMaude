var app = angular.module('productEditApp', ['ngFileUpload']);
  app.config(['$locationProvider', function($locationProvider){
    $locationProvider.html5Mode(true);    
  }]);
  app.controller('ProductEditController', function($scope, $timeout ,$http, $location, $window, Upload) {
    $ctrl = this;
    $scope.changedImage = false;
    if($scope.param = $location.search().productId){
      $scope.productURL = 'https://inthebagapi-dev.azurewebsites.net/api/getProduct?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      $scope.product = [];
      $scope.data = 
      {
        "productId":$scope.param
      }
      $http.post($scope.productURL,$scope.data).then(function(success){
        $scope.product = success.data
        $scope.isflagged = $scope.product.flag;
        $scope.productImageUrl = "https://inthebag.blob.core.windows.net/app-images/products/".concat( $scope.product.productPhotoId);
        $scope.product.mats = $scope.product.mats.split(",");
      });
    } else{
      $scope.product = [];
      $scope.product.mats = "";
    }
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
    
    function flag(){
      var flagged = document.getElementById('flagged').checked;
      if(flagged !=true && $scope.product.flag==true){
        var setFlag0URL = 'https://inthebagapi-dev.azurewebsites.net/api/setFlag0?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
        var data =
        {
          "productId":$scope.param
        }
      } else{
        setFlag0URL = "https://inthebagapi-dev.azurewebsites.net/api/getProduct?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
        data =
        {
          "productId":$scope.param
        }
      }
      $http.post(setFlag0URL,data).then(function(success){
        $scope.statusFlag = success.status;
        $window.location.href = "/index.html";
        
      });
    }
    $scope.setFile = function setFile(){
      var sGuid="";
      for (var i=0; i<32; i++)
       {
         sGuid+=Math.floor(Math.random()*0xF).toString(0xF);
       }
      $scope.changedImage = true;
      $scope.product.productImageId = sGuid; 
    }
    
     
      
    

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

      

      if($scope.changedImage = true){
         $scope.file = document.querySelector('input[type=file]').files[0];
        var uploadFileURL = "https://inthebagapi-dev.azurewebsites.net/api/setProductLinkMaterial?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
        var data = 
        {
            "photo": $scope.file,
            "guid:": $scope.productImageId
        }
        $http.post(uploadFileURL, data).then(function(success){
          $scope.status = success.status;
        });
      }
      var editProductURL = "https://inthebagapi-dev.azurewebsites.net/api/setProductLinkMaterial?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
      var data = 
      {
        "productName":document.getElementById('productName').value,
        "productPhotoId":$scope.product.productPhotoId.toLowerCase(),
        "barcode":document.getElementById('barcode').value,
        "materials":vals
      }
      $http.post(editProductURL,data).then(function(success){
        $scope.status = success.status;
        flag();
      });

      
    }
    
    $scope.materialLink = function materialLink(productMaterials, allMaterials){
      for(i=0; i<productMaterials.length; i++){
        if(allMaterials.includes(productMaterials[i])){
          var propertyid = productMaterials[i];
          document.getElementById(propertyid).checked = 'true';
        }
      }
    }

    $scope.changedImage = function(){
      $scope.changedImage =  true;

  }

  $scope.uploadImages = function(){
    var uploadObj = JSON.stringify({
        guid: $scope.product.productPhotoId,
        photo: $scope.photoUpload
    });

    var xhr = new XMLHttpRequest();
    var url = "https://inthebagapi-dev.azurewebsites.net/api/uploadImage";
    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var json = JSON.parse(xhr.responseText);
        }
    };
    var data = uploadObj;
    xhr.send(data);
  }

  Dropzone.autoDiscover = false;

  $timeout(function () { //Delay configuring the dropzone until the modal has been opened by angular.

      var config = {
          url: "nope",
          paramName: "file", // The name that will be used to transfer the file
          maxFilesize: 200, // MB
          maxFiles: 1,
          uploadMultiple: false
      };

      if (document.getElementsByClassName('dropzone') !== null) {
        var uploadDropzone = new Dropzone($(".dropzone").get(0), config);
        

        //This Overrides the upload function
        uploadDropzone.uploadFiles = function (files) {
          var self = this;

          var file = files[0];

          $scope.$apply(function () {
              $scope.photoUpload = file;
          });

          file.status = Dropzone.SUCCESS;
          self.emit("success", file, 'success', null);
          self.emit("complete", file);
          self.processQueue();
        };
      }
  });
    
    
  });