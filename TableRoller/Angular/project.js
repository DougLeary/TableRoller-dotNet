angular.module('project', ['mongolab']).
	config(function ($routeProvider) {
		$routeProvider.
			when('/', { controller: ListCtrl, templateUrl: 'list.html' }).
			when('/edit/:projectId', { controller: EditCtrl, templateUrl: 'detail.html' }).
			when('/new', { controller: CreateCtrl, templateUrl: 'detail.html' }).
			otherwise({ redirectTo: '/' });
});


function ListCtrl($scope, Project) {
	$scope.projects = Project.query();
}


function CreateCtrl($scope, $location, Project) {
	$scope.save = function () {
		Project.save($scope.project, function (project) {
			$location.path('/edit/' + project._id.$oid);
		});
	}
}