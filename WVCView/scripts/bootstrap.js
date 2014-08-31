require.config({
	baseUrl: '/scripts',
	paths: {
		'angular': '/js/angular/angular.min',
		'angular-route': '/js/angular-route/angular-route.min',
		'angular-loading-bar': '/js/angular-loading-bar/loading-bar.min'
	},
	shim: {
		'app': {
			deps: ['angular','angular-route','angular-loading-bar']
		},
		'angular-route': {
			deps: ['angular']
		},
		'angular-loading-bar': {
			deps: ['angular','angular-route']
		}
	}
});

require
(
    [
        'app'
    ],
    function(app) {
    	angular.bootstrap(document,['app']);
    }
);