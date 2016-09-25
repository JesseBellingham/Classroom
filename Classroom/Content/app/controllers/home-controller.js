// the services and controllers should be in their own files, but I didn't want to spend to much time on this
// and I thought you'd be more interested in what I can do with the code, rather than where I put it

var app = angular.module("app", ['ui.bootstrap']);

app.service("HomeService", ["$rootScope", "$http", function($rootScope, $http) {
    
    var homeService = {},
        query = {
            getClassData: "/api/class/get",
            createNewClass: "/api/class/create",
            getStudentsForEnrolment: "api/studentenrolment/get/",
            enrolStudent: "api/enrolment/create",
            createNewStudent: "api/student/create",
            updateClassDetails: "api/class/update"
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
            $rootScope.$broadcast("SERVER_ERROR", {
                // some server logging here if I had time
            });
        })
    };

    homeService.updateClassDetails = function(classDetails) {
        return $http({
            method: "POST",
            url: query.updateClassDetails,
            data: {
                classId: classDetails.classId,
                className: classDetails.className,
                location: classDetails.location,
                teacherName: classDetails.teacherName
            }
        }).success(function(data) {
            return data;
        }).error(function() {
            console.log("error in " + query.updateClassDetails);
            $rootScope.$broadcast("SERVER_ERROR", {
                // some server logging here if I had time
            });
        })
    };

    homeService.createNewStudent = function(newStudent) {
        return $http({
            method: "POST",
            url: query.createNewStudent,
            data: {
                studentFirstName: newStudent.studentFirstName,
                studentLastName: newStudent.studentLastName,
                studentAge: newStudent.studentAge,
                studentGPA: newStudent.studentGPA
            }
        }).success(function(data) {
            return data;
        }).error(function() {
            console.log("error in " + query.createNewStudent);
            $rootScope.$broadcast("SERVER_ERROR", {
                // some server logging here if I had time
            });
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

    $scope.createNewClass = function() {
        var modalInstance = $uibModal.open({
            templateUrl: '/Content/template/create-class.html',
            controller: 'CreateEntityCtrl',
            resolve: {
                creatingClass: true,
                creatingStudent: false
            }
        });

        modalInstance.result.then(function(result) {
            if (result) {
                $scope.classes.push(result);
            }
        });
    };

    $scope.createNewStudent = function() {
        var modalInstance = $uibModal.open({
            templateUrl: '/Content/template/create-student.html',
            controller: 'CreateEntityCtrl',
            resolve: {
                creatingClass: false,
                creatingStudent: true
            }
        });

        modalInstance.result.then(function(result) {
            // if (result) {
            //     $scope.classes.push(result);
            // }
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
            $scope.getClassData();
        });
    };

    $scope.editClassDetails = function(classDetails) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Content/template/create-class.html',
            controller: 'EditClassCtrl',
            resolve: {
                classDetails: function() {
                    return classDetails;
                }
            }
        });

        modalInstance.result.then(function(result) {
            $scope.getClassData();
        });
    }

    $scope.getClassData();
}])
.controller('CreateEntityCtrl', ["$rootScope", "$scope", "$timeout", "$uibModalInstance", "HomeService", function($rootScope, $scope, $timeout, $uibModalInstance, homeService) {
    $scope.classDetails = {
        className: null,
        location: null,
        teacherName: null
    };

    $scope.newStudent = {
        studentFirstName: null,
        studentLastName: null,
        studentAge: null,
        studentGPA: null
    };

    $rootScope.$on("SERVER_ERROR", function() {
        $scope.serverError = true;

        $timeout(function() {
            $scope.serverError = false;
        }, 5000)
    })

    $scope.close = function() {
        $uibModalInstance.close();
    };

    $scope.createNewClass = function() {
        // quick and dirty client-side validation
        $scope.classNameMissing = false;
        if ($scope.classDetails.className === null) {
            $scope.classNameMissing = true
        }
        else {
            homeService.createNewClass($scope.classDetails).then(function(data) {
                if (data.data) {
                    $scope.classDetails.classId = data.data;
                    $uibModalInstance.close($scope.classDetails);
                }
            });
        }
    };

    $scope.createNewStudent = function() {
        // quick and even dirtier client-side validation
        $scope.firstNameMissing = false;
        $scope.lastNameMissing = false;
        if ($scope.newStudent.studentFirstName === null) {
            $scope.firstNameMissing = true;
        }
        if ($scope.newStudent.studentLastName === null) {
            $scope.lastNameMissing = true;
        }
        if (!$scope.firstNameMissing && !$scope.lastNameMissing) {
            homeService.createNewStudent($scope.newStudent).then(function(data) {
                if (data.data) {
                    $scope.newStudent.studentFirstName = null;
                    $scope.newStudent.studentLastName = null;
                    $scope.newStudent.studentGPA = null;
                    $scope.newStudent.studentAge = null;
                    $scope.success = true;
                }
            });
        }
    };
}])
.controller('AddEnrolmentsCtrl', ["$scope", "$uibModalInstance", "HomeService", "classId", function($scope, $uibModalInstance, homeService, classId) {
    
    $scope.students = [];

    $scope.close = function() {
        $uibModalInstance.close();
    };

    $scope.enrolStudent = function(studentId) {
        homeService.enrolStudent(studentId, classId).then(function(data) {
            if (data.data) {
                var student = data.data,
                    found = false;
                angular.forEach($scope.enrollableStudents, function(es, index) {
                    if (!found) {
                        if (student.studentId === es.studentId) {
                            $scope.enrollableStudents.splice(index, 1);
                            found = true;
                        }
                    }
                });
                $scope.existingStudents.push(student);
                $scope.getEnrolmentData(classId);
            }
        });
    };

    $scope.getEnrolmentData = function(classId) {
        homeService.getStudentsForEnrolment(classId).then(function(data) {
            if (data.data) {
                $scope.enrollableStudents = data.data.enrollableStudents;
                $scope.existingStudents = data.data.existingStudents;
            }
        });
    };

    $scope.getEnrolmentData(classId);
}])
.controller('EditClassCtrl', ["$scope", "$uibModalInstance", "HomeService", "classDetails", function($scope, $uibModalInstance, homeService, classDetails) {
    
    $scope.classDetails = {
        classId: classDetails.classId,
        className: classDetails.className,
        location: classDetails.location,
        teacherName: classDetails.teacherName
    };

    $scope.close = function() {
        $uibModalInstance.close();
    };

    $scope.updateClassDetails = function(classDetails) {
        homeService.updateClassDetails(classDetails).then(function(data) {
            if (data.data) {
                $scope.success = true;
            }
        });
    };
}]);