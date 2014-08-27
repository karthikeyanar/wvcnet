define([],function() {
	return {
		defaultRoutePath: '/',
		routes: {
			'/': {
				templateUrl: '/Home/Dashboard',
				dependencies: [
                    'controllers/HomeViewController'
                ]
			},
			'/division': {
				templateUrl: '/Home/Division',
				dependencies: ['controllers/DivisionController']
			},
			'/about': {
				templateUrl: '/Home/AboutUs',
				dependencies: [
                    'controllers/AboutViewController',
                    'directives/app-color'
                ]
			},
			'/contact': {
				templateUrl: '/Home/Contact',
				dependencies: [
                    'controllers/ContactViewController',
                    'directives/app-color'
                ]
			}
		}
	};
});