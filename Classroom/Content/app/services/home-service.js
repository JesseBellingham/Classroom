// this file is not used

var app = angular.module("app", []);

app.service("HomeService", ["$http", function($http) {
    
    var homeService = {},
        query = {
            getClassData: "api/getclassdata"
        };

    homeService.getClassData = function() {
        return
        $http.get(query.getClassData)
        .success(function(data) {
            return data;
        }).error(function() {
            console.log("error in " + query.getClassData);
        });
    };

    return homeService;
}]);


// Todo - Come back to this -- doesn't seem to like it being in a separate file for some reason