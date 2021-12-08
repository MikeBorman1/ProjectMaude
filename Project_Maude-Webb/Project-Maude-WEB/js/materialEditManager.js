var app = angular.module('materialEditApp', []);
  app.config(['$locationProvider', function($locationProvider){
    $locationProvider.html5Mode(true);    
  }]);
  app.controller('MaterialEditController', function($scope, $timeout ,$http, $location, $window) {
    $ctrl = this;
    if($scope.param = $location.search().materialId){
      $scope.materialId = $location.search().materialId;
      $scope.materialURL = 'https://inthebagapi-dev.azurewebsites.net/api/getMaterial?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
      $scope.material = [];
      $scope.data = 
      {
        "materialId":$scope.materialId
      }
      $http.post($scope.materialURL,$scope.data).then(function(success){
        $scope.material = success.data
        $scope.isRecyclable = $scope.material.isRecyclable;
        $scope.matWarnings = []
        for(warnings in $scope.material.warnings){
          var matWarn = $scope.material.warnings[warnings].warning
          $scope.matWarnings.push(matWarn)
        }
      });
    }else{
      $scope.material = []
      $scope.material.recycleBin = ""
      $scope.material.materialImageId = ""
      $scope.matWarnings = []
    }
    $scope.materialsURL = 'https://inthebagapi-dev.azurewebsites.net/api/getMaterials?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.materials = [];
    $http.post($scope.materialsURL).then(function(success){
      $scope.materials = success.data
      
    });

    $scope.warningsURL = 'https://inthebagapi-dev.azurewebsites.net/api/getWarnings?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==';
    $scope.warnings = [];
    $http.post($scope.warningsURL).then(function(success){
      $scope.warnings = success.data
      $scope.warnList = []
      for(warningnumb in $scope.warnings){
        var newwarn = $scope.warnings[warningnumb].warning
        $scope.warnList.push(newwarn)
      }
    });

    $scope.recycleURL = "https://inthebagapi-dev.azurewebsites.net/api/getRecycleBin?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
    $scope.recycleBins = [];
    $http.post($scope.recycleURL).then(function(success){
      $scope.recycleBins = success.data
    });

    function uuidv4() {
      return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
      });
    }

    $scope.editMaterial = function(){
      if($scope.param = $location.search().materialId){
        var checkboxes = document.getElementsByName('warnings[]');
        var warningvals = [];
        for (var i=0, n=checkboxes.length;i<n;i++) 
        {
            if (checkboxes[i].checked) 
            {
                warningvals.push(parseInt(checkboxes[i].value));
            }
        }

        var radios = document.getElementsByName('recycleBin');
        for (var i = 0, length = radios.length; i < length; i++)
        {
        if (radios[i].checked)
          {
            
            var recycleCodeId = radios[i].value;

            break;
          }
        }
        if(document.getElementById("recyclable").checked = "true"){
          var recyclable = 1;
        } else{
          var recyclable = 0;
        }
        var editMaterialURL = "https://inthebagapi-dev.azurewebsites.net/api/setMaterial?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
        var data = 
        {
          "material":document.getElementById("materialName").value,
          "materialImageId":$scope.material.materialImageId.toLowerCase(),
          "isRecyclable": recyclable,
          "recycleCodeID": parseInt(recycleCodeId),
          "WarningId": warningvals,
          "materialid": parseInt($scope.param)
        }
        $http.post(editMaterialURL,data).then(function(success){
          $scope.status = success.status;
          $window.location.href = "/materials.html";
        });
      }else{
        var checkboxes = document.getElementsByName('warnings[]');
        var warningvals = [];
        for (var i=0, n=checkboxes.length;i<n;i++) 
        {
            if (checkboxes[i].checked) 
            {
                warningvals.push(parseInt(checkboxes[i].value));
            }
        }

        var radios = document.getElementsByName('recycleBin');
        for (var i = 0, length = radios.length; i < length; i++)
        {
        if (radios[i].checked)
          {
            
            var recycleCodeId = radios[i].value;

            break;
          }
        }
        if(document.getElementById("recyclable").checked = "true"){
          var recyclable = 1;
        } else{
          var recyclable = 0;
        }

        var sGuid="";
        for (var i=0; i<32; i++)
        {
          sGuid+=Math.floor(Math.random()*0xF).toString(0xF);
        }

        var editMaterialURL = "https://inthebagapi-dev.azurewebsites.net/api/addMaterial?code=Zctfp7TjcCWXheQtJ1nunpJMIgPaXY4ytOoH47GGZYs02WwznvoAng==";
        var data = 
        {
          "material":document.getElementById("materialName").value,
          "materialImageId":uuidv4(),
          "isRecyclable": recyclable,
          "recycleCodeID": parseInt(recycleCodeId),
          "WarningId": warningvals
        }
        $http.post(editMaterialURL,data).then(function(success){
          $scope.status = success.status;
          $window.location.href = "/materials.html";
        });
      }
    }

    $scope.recycleBinLink = function recycleBinLink(){
      if($scope.material.recycleBin != ""){
        document.getElementById($scope.material.recycleBin).checked = "true";

        for(i=0; i<$scope.matWarnings.length; i++){
          document.getElementById($scope.matWarnings[i]).checked = "true";
        }
      } 
    }
  });