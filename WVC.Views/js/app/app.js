// and a module that has a controller that depends on the ListOfItemsService
define("app",["knockout","komapping","../models/AuthModel","finch","helper"],function(ko,komapping,AuthModel,finch,helper) {
	return function() {
		var self=this;
		this.my=ko.observable(null);
		this.viewModel=ko.observable();
		this.loginModel=ko.observable();

		this.setMy=function(json,isRememberMe) {
			if(json==null||json==undefined) return;
			helper.setAuth(json,isRememberMe);
			var m=new AuthModel();
			komapping.fromJS(json,{},m);
			self.my(m);
			self.loginModel(null);
			$("body").removeClass("no-authorize");
		}

		this.initPage=function() {
			helper.init();
		}

		this.checkAuth=function() {
			// if token is expire redirect to login
			$.getJSON(helper.apiUrl("/Account/CheckToken"),function() { });
		}

		this.lockOut=function() {
            $("body").addClass("no-authorize");
			var auth=helper.getAuth();
			if(auth!=null) {
				$.ajax({
					"url": helper.apiUrl("/Account/Logout"),
					"cache": false,
					"type": "POST"
				}).done(function(json) {
					console.log("logout");
				});
			}
			helper.setAuth(null,helper.getLS("rememberme"));
			self.my(null);
			$(".modal-scrollable").remove();
			$(".modal-backdrop").remove();
			finch.navigate("/login");
		}

		this.checkRole=function(roles) {
			var isPass=false;
			if(self.my()!=null) {
				$.each(roles,function(i,roleName) {
					if(self.my().role()==roleName) {
						isPass=true;
					}
				});
			}
			if(isPass==false)
				self.lockOut();
		}

		self.setMy(helper.getAuth(),helper.getLS("rememberme"));
		//this.checkAuth();
	}
}
);