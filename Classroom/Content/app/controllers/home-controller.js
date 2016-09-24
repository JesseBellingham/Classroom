var app = angular.module("app", ['ui.bootstrap']);

app.service("HomeService", ["$http", function($http) {
    
    var homeService = {},
        query = {
            getClassData: "/api/home/getclassdata",
            createNewClass: "/api/home/createnewclass",
            getStudentsForEnrolment: "api/home/getstudentsforenrolment/",
            enrolStudent: "api/home/enrolstudent"
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
    };

    homeService.getStudentsForEnrolment = function(classId) {
        return $http({
            method: "GET",
            url: query.getStudentsForEnrolment + classId
        }).success(function(data) {
            return data;
        }).error(function() {
            console.log("error in " + query.getStudentsForEnrolment);
        })
    };

    homeService.enrolStudent = function(studentId, classId) {
        return $http({
            method: "POST",
            url: query.enrolStudent,
            data: {
                studentId: studentId,
                classId: classId
            }
        }).success(function(data) {
            return data;
        }).error(function() {
            console.log("error in " + query.enrolStudent);
        })
    };

    return homeService;
}])
.controller('HomeCtrl', ["$scope", "$uibModal", "HomeService", function($scope, $uibModal, homeService) {
    $scope.getClassData = function() {
        homeService.getClassData().then(function(data) {
            $scope.classes = data.data;
        });
    };

    // $scope.showInfo = function(index) {
    //     $scope.showEnrolments = index;
    // };

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
    };

    $scope.addEnrolments = function(classId) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Content/template/add-enrolments.html',
            controller: 'AddEnrolmentsCtrl',
            resolve: {
                classId: function() {
                    return classId;
                }
            }
        });

        modalInstance.result.then(function(result) {
            // if (result) {
            //     $scope.classes.push(result);
            // }
        });
    };

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
}])
.controller('AddEnrolmentsCtrl', ["$scope", "$uibModalInstance", "HomeService", "classId", function($scope, $uibModalInstance, homeService, classId) {
    
    $scope.students = [];

    $scope.close = function() {
        $uibModalInstance.dismiss('cancel');
    };

    // $scope.createNewClass = function() {
    //     homeService.createNewClass($scope.newClass).then(function(data) {
    //         if (data.data) {
    //             $scope.newClass.classId = data.data;
    //             $uibModalInstance.close($scope.newClass);
    //         }
    //     })
    // }

    $scope.enrolStudent = function(studentId) {
        homeService.enrolStudent(studentId, classId).then(function(data) {
            if (data.data) {
                var index = $scope.enrollableStudents.indexOf(data.data);
                if (index > 1) {
                    $scope.enrollableStudents.splice(index, 1);
                    $scope.existingStudents.push(data.data);
                }
            }
        });
    };

    homeService.getStudentsForEnrolment(classId).then(function(data) {
        if (data.data) {
            $scope.enrollableStudents = data.data.enrollableStudents;
            $scope.existingStudents = data.data.existingStudents;
        }
    });
}]);