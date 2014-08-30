define([],function() {
	return {
		defaultRoutePath: '/home',
		routes: {
			'/home': {
				templateUrl: '/Home/Dashboard',
				dependencies: [
                    'controllers/HomeViewController'
                ]
			},
			'/division': {
				templateUrl: '/Home/Division',
				dependencies: ['controllers/DivisionController','directives/app-color','directives/twbsPagination']
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