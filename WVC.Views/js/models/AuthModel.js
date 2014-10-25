// and a module that has a controller that depends on the ListOfItemsService
define(["knockout"],function(ko) {
	return function AuthModel() {
		var self=this;
		this.first_name=ko.observable("");
		this.last_name=ko.observable("");
		this.user_name=ko.observable("");
		this.role=ko.observable("");
		this.id=ko.observable("");
		this.group_id=ko.observable("");
		this.menus=ko.observableArray([]);
		this.getRoleName=ko.computed(function() {
			var roleName="";
			switch(self.role()) {
				case "EA": roleName="WVC Admin"; break;
				case "EM": roleName="WVC Member"; break;
				case "CA": roleName="Company Admin"; break;
				case "CM": roleName="Company Member"; break;
				case "GA": roleName="Group Admin"; break;
				case "GM": roleName="Group Member"; break;
				case "AA": roleName="Agent Admin"; break;
				case "AM": roleName="Agent Member"; break;
				case "Admin": roleName="Admin"; break;
			}
			return roleName;
		});
		this.role.subscribe(function(newValue) {
			switch(self.role()) {
				case "Admin":
					self.menus.push(
					 { "name": "Volumes","url": "#/index","icon": "fa fa-database","is_active": false,submenus: [] }
					);
					break;
				case "EA":
				case "EM":
					self.menus.push(
					 { "name": "Masters","url": "#/ea-index","icon": "fa fa-database","is_active": false,submenus: [] }
					,{ "name": "Groups","url": "#/ea-groups","icon": "fa fa-folder-open-o","is_active": false,submenus: [] }
					,{ "name": "GSA","url": "#/ea-gsa","icon": "fa fa-file-o","is_active": false,submenus: [] }
					,{ "name": "Forex","url": "#/ea-forex","icon": "fa fa-dollar","is_active": false,submenus: [] }
					);
					break;
				case "CA":
					break;
				case "CM":
					self.menus.push(
					 { "name": "Import","url": "#/ca-importsales","icon": "fa fa-upload","is_active": false,submenus: [] }
					);
					break;
				case "GA":
					self.menus.push(
					 { "name": "Reports","url": "#/ga-reports","icon": "fa fa-database","is_active": false,submenus: [] }
					,{ "name": "Documents","url": "#/ga-documents","icon": "fa fa-folder-open-o","is_active": false,submenus: [] }
					,{ "name": "Companies","url": "#/ga-companies","icon": "fa fa-university","is_active": false,submenus: [] }
					,{ "name": "Users","url": "#/ga-users","icon": "fa fa-users","is_active": false,submenus: [] }
					,{
						"name": "Manage","url": "javascript:;","icon": "fa fa-cog","is_active": false,submenus:
						  [{ "name": "Zones","url": "#/ga-zones","icon": "","is_active": false }]
					}
					);
				case "GM":
					self.menus.push(
					 { "name": "Reports","url": "#/ga-reports","icon": "fa fa-database","is_active": false,submenus: [] }
					,{ "name": "Documents","url": "#/ga-documents","icon": "fa fa-folder-open-o","is_active": false,submenus: [] }
					,{ "name": "Companies","url": "#/ga-companies","icon": "fa fa-university","is_active": false,submenus: [] }
					,{
						"name": "Manage","url": "javascript:;","icon": "fa fa-cog","is_active": false,submenus:
						  [{ "name": "Zones","url": "#/ga-zones","icon": "","is_active": false }]
					}
					);
					break;
				case "AA": roleName="Airport Admin"; break;
				case "AM": roleName="Airport Manager"; break;
			}
		});
		this.getMenus=ko.computed(function() {

		});
		this.getName=ko.computed(function() {
			return self.first_name()+" "+self.last_name();
		});
		this.selectMenu=function(name) {
			$.each(self.menus,function(i,menu) {
				if(menu.name==name) {
					menu.is_active=true;
				} else {
					menu.is_active=false;
				}
			});
		}
	}
});
