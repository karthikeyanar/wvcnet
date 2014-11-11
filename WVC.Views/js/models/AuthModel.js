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
                case "member": roleName="Member"; break;
                case "admin": roleName="Admin"; break;
            }
            return roleName;
        });
        this.role.subscribe(function(newValue) {
            switch(self.role()) {
                case "member":
                    self.menus.push(
					 { "name": "Volumes","url": "#/index","icon": "fa fa-database","is_active": false,submenus: [] }
					);
                    break;
            }
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
