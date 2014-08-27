define(['routes','services/dependencyResolverFor'],function(config,dependencyResolverFor) {
	var app=angular.module('app',['ngRoute','angular-loading-bar']);
	app.config(
    [
        '$routeProvider',
        '$locationProvider',
        '$controllerProvider',
        '$compileProvider',
        '$filterProvider',
        '$provide',

        function($routeProvider,$locationProvider,$controllerProvider,$compileProvider,$filterProvider,$provide) {
        	app.controller=$controllerProvider.register;
        	app.directive=$compileProvider.directive;
        	app.filter=$filterProvider.register;
        	app.factory=$provide.factory;
        	app.service=$provide.service;

        	//$locationProvider.html5Mode(true);

        	if(config.routes!==undefined) {
        		angular.forEach(config.routes,function(route,path) {
        			$routeProvider.when(path,{ templateUrl: route.templateUrl,resolve: dependencyResolverFor(route.dependencies) });
        		});
        	}
        	console.log(config.defaultRoutePath);
        	if(config.defaultRoutePath!==undefined) {
        		$routeProvider.otherwise({ redirectTo: config.defaultRoutePath });
        	}
        }
    ]);

	app.controller('MenuController',['$scope',function($scope) {
		$scope.page={ heading: 'Division' };
		$scope.menus=[{ "name": "Wood Volume","url": "#/","isactive": false }
		,{ "name": "Division","url": "#/division","isactive": false }
		,{ "name": "Village","url": "#/village","isactive": false}];
	} ]);
	return app;
});