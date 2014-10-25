// might as well use requirejs instead of continuing to put script tags everywhere
require.config({
	baseUrl: '/js/controllers',
	paths: {
		'app': '/js/app/app',
		'knockout': '/js/knockout/knockout-3.1.0',
		'komapping': '/js/knockout/knockout.mapping-latest',
		'ko-binding': '/js/knockout/ko-binding',
		'finch': '/js/finch/finch-0.5.13.min',
		'infuser': '/js/finch/infuser-amd.min',
		'trafficCop': '/js/finch/TrafficCop.min',
		'koext': '/js/finch/koExternalTemplateEngine-amd.min',
		'service': '/js/services/service',
		'helper': '/js/helper/helper'
	},
	shim: {
		'helper': {
			deps: []
		}
		,'service': {
			deps: ['helper']
		}
		,'knockout': {
			deps: ['helper']
		}
		,'komapping': {
			deps: ['knockout']
		}
		,'ko-binding': {
			deps: ['knockout']
		}
		,'koext': {
			deps: ['knockout']
		}
		,'app': {
			deps: ['knockout','ko-binding']
		}
		,'finch': {
			exports: 'Finch',
			deps: ['knockout']
		}
		,'infuser': {
			deps: ['finch']
		}
	}
	//,urlArgs: "v="+(new Date()).getTime()
	,urlArgs: "v="+_Version
});
define('jquery',[],function() { return jQuery; });
// hack in a 'ko' library since that's what ko-ext expects
define('ko',['knockout'],function(ko,komapping) { return ko; });


// define route and outer ko viewModel
require(['knockout','app','finch','infuser','koext','helper'],function(ko,AppViewModel,finch,infuser,koext,helper) {
	var app=new AppViewModel();
	// templates
	infuser.defaults.templateUrl="/Home";
	infuser.defaults.templateSuffix="";
	infuser.defaults.loadingTemplate.content="<div class='infuser-loading'>&nbsp;</div>";

	/*
	finch.route("/home",{
		setup: function() {
			console.log("setup home")
		},
		load: function() {
			console.log("Loaded home")
		},
		unload: function() {
			console.log("Unloaded home")
		},
		teardown: function() {
			console.log("Teardown home")
		}
	},function() {
	});
	*/


	// routes
	finch.route("/",function() {
		window.console.log("root app my=",app.my())
		if(app.my()!=null) {
			switch(app.my().role()) {
				case "Admin":
					finch.navigate("/index");
					break;
			}
		}
	});

	finch.route("/blank",{
		setup: function() {
			helper.handleBlockUI();
		},
		load: function() {
			require(['BlankController'],function(ViewModel) {
				var m=new ViewModel();
				app.viewModel(m);
				helper.unblockUI();
			});
		}
	});

	finch.route("/index",{
		setup: function() {
			app.checkRole(['Admin']);
			helper.handleBlockUI();
		},
		load: function() {
			require(['WoodVolumeController'],function(ViewModel) {
				var m=new ViewModel();
				app.viewModel(m);
				helper.selectMenu("Volumes");
				m.loadWoodVolume(function() {
					helper.unblockUI();
				});
			});
		}
	});

	finch.route("/edit-volume/:id",{
		setup: function() {
			app.checkRole(['Admin']);
			helper.handleBlockUI();
		},
		load: function(bindings) {
			require(['WoodVolumeEditController'],function(ViewModel) {
				var m=new ViewModel(bindings.id);
				m.role("GM");
				app.viewModel(m);
				helper.unblockUI();
				m.load();
			});
		}
	});

	finch.route("/login",function() {
		helper.unblockUI();
		helper.loadCSS(['/css/pages/login.css'],function() {
			require(['LoginController'],function(ViewModel) {
				var m=new ViewModel();
				app.loginModel(m);
				var userName=helper.getLS("user_name");
				if(userName!=undefined) {
					m.user_name(userName);
					m.password_focus(true);
				} else {
					m.user_name_focus(true);
				}
			});
		});
	});


	// start running
	ko.applyBindings(app);
	finch.listen();

	helper.appModel=app;
	helper.onAjaxError=function(jqXHR,exception) {
		if(jqXHR.status==401) {
			app.lockOut();
		}
	}

});