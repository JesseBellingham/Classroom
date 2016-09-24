var app = angular.module("app", ['ui.bootstrap']);

app.service("HomeService", ["$http", function($http) {
    
    var homeService = {},
        query = {
            getClassData: "/api/home/getclassdata",
            createNewClass: "/api/home/createnewclass"
        };

    homeService.getClassData = function() {
        return $http({
            method: "GET",
            url: query.getClassData
        }).success(function(data) {
            return data;
        }).error(function() {
            console.log("error in " + query.getClassData);
        });
    };

    homeService.createNewClass = function(newClass) {
        return $http({
            method: "POST",
            url: query.createNewClass,
            data: {
                className: newClass.className,
                location: newClass.location,
                teacherName: newClass.teacherName
            }
        }).success(function(data) {
            return data;
        }).error(function() {
            console.log("error in " + query.createNewClass);
        })
    }

    return homeService;
}])
.controller('HomeCtrl', ["$scope", "$uibModal", "HomeService", function($scope, $uibModal, homeService) {
    $scope.getClassData = function() {
        homeService.getClassData().then(function(data) {
            $scope.classes = data.data;
        });
    };

    $scope.showInfo = function(index) {
        $scope.showEnrolments = index;
    };

    $scope.createNewClass = function() {
        var modalInstance = $uibModal.open({
            templateUrl: '/Content/template/create-class.html',
            controller: 'CreateClassCtrl'
        });

        modalInstance.result.then(function(result) {
            if (result) {
                $scope.classes.push(result);
            }
        });
    }

    $scope.getClassData();
}])
.controller('CreateClassCtrl', ["$scope", "$uibModalInstance", "HomeService", function($scope, $uibModalInstance, homeService) {
    $scope.newClass = {
        className: null,
        location: null,
        teacherName: null
    };

    $scope.close = function() {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.createNewClass = function() {
        homeService.createNewClass($scope.newClass).then(function(data) {
            if (data.data) {
                $scope.newClass.classId = data.data;
                $uibModalInstance.close($scope.newClass);
            }
        })
    }
}]);