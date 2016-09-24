var app = angular.module("app", []);

app.service("HomeService", ["$http", function($http) {
    
    var homeService = {},
        query = {
            getClassData: "/api/home/getclassdata"
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

    return homeService;
}])
.controller('HomeCtrl', ["$scope", "HomeService", function($scope, homeService) {
    $scope.getClassData = function() {
        homeService.getClassData().then(function(data) {
            $scope.classes = data.data;
        });
    };

    $scope.showInfo = function(index) {
        $scope.showEnrolments = index;
    };

    $scope.getClassData();
}]);