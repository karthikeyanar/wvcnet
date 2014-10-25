"use strict";
define(["knockout","komapping","helper","service"],function(ko,komapping,helper,service) {
	return function GroupMemberListModel() {
		var self=this;

		this.id=ko.observable(0);
		this.email=ko.observable("");
		this.password=ko.observable("");
		this.password_change=ko.observable(false);
		this.role=ko.observable("");
		this.aspnetuser_id=ko.observable("");
		this.group_id=ko.observable("");
		this.group_name=ko.observable("");
		this.first_name=ko.observable("");
		this.last_name=ko.observable("");
		this.user_airlines=null;
		this.user_airline_ids=ko.observable("");
		this.user_airports=null;
		this.user_airport_ids=ko.observable("");
		this.name=ko.computed(function() {
			return self.first_name()+" "+self.last_name();
		})
		this.is_active=ko.observable(false);
		this.is_active_display=ko.computed(function() {
			return (self.is_active()==true?"<i class='fa fa-check'/>":"");
		});

		this.changePassword=function() {
			self.password_change(true);
		}
		this.onPasswordChange=null;
		this.password_change.subscribe(function(newValue) {
			if(self.id()>0&&self.password_change()==true) {
				if(self.onPasswordChange) {
					self.onPasswordChange();
				}
			}
		});
		this.is_cost_profit=ko.observable(false);
		this.is_profit_loss=ko.observable(false);
		this.is_document=ko.observable(false);


		this.is_edit_mode=ko.observable(false);
		this.is_focus=ko.observable(false);
		this.success_result=ko.observable("");
		this.is_save_and_exit=ko.observable(true);

		this.getEditUrl=function(role) {
			var url="";
			switch(role) {
				case "GM":
					url="#/ga-groupmember/"+self.id();
					break;
				case "CA":
					url="#/ga-companyadmin/"+self.id();
					break;
				case "CM":
					url="#/ga-companymember/"+self.id();
					break;
			}
			return url;
		}

		this.onEdit=null;
		this.edit=function() {
			if(self.onEdit)
				self.onEdit();
		}
		this.onAdd=null;
		this.add=function() {
			if(self.onAdd)
				self.onAdd();
		}
		this.onDelete=null;
		this.deleteURL="";
		this.deleteGroupMember=function() {
			//var url=helper.apiUrl("/GroupMember/Delete/"+self.id());
			var url=self.deleteURL+"/"+self.id();
			$.ajax({
				"url": url,
				"cache": false,
				"type": "DELETE",
			}).done(function(json) {
				if(self.onDelete)
					self.onDelete();
			}).fail(function(jqxhr) {
				alert(helper.deleteErrorMessage);
			});
		}

		this.errors=ko.observable(null);
		this.searchGroupMember=function(request,response) {
			service.searchGroupMember(request,response);
		}
		this.onBeforeSelectGroupMember=null;
		this.onAfterSelectGroupMember=null;
		this.selectGroupMember=function(event,ui) {
			self.id(ui.item.id);
			self.load();
		}
		this.findURL="";
		this.load=function() {
			if(self.onBeforeSelectGroupMember) {
				self.onBeforeSelectGroupMember();
			}
			//var url=helper.apiUrl("/GroupMember/Find/"+self.id());
			var url=self.findURL+"/"+self.id();
			$.ajax({
				"url": url,
				"cache": false,
				"type": "GET"
			}).done(function(json) {
				komapping.fromJS(json,{},self);
				self.user_airlines=json.user_airlines;
				self.user_airports=json.user_airports;
				if(self.onAfterSelectGroupMember) {
					self.onAfterSelectGroupMember();
				}
			});
		}
		this.onSave=null;
		this.onBeforeSave=null;
		this.onAfterSave=null;
		this.saveURL="";
		this.save=function(formElement) {
			self.success_result("");
			var $frm=$(formElement);
			if($frm.valid()) {
				//var url=helper.apiUrl("/GroupMember/Create");
				var url=self.saveURL;
				var type="POST";
				//if(self.id()>0) { type="PUT"; url=helper.apiUrl("/GroupMember/Update/")+self.id(); }
				if(self.onBeforeSave) {
					if(self.onBeforeSave(formElement)==false) {
						if(self.onAfterSave)
							self.onAfterSave(formElement);
						return;
					}
				}
				var data=$frm.serializeArray();
				$.ajax({
					"url": url,
					"cache": false,
					"type": type,
					"data": data
				}).done(function(json) {
					self.errors(null);
					if(json.Errors==null) {
						komapping.fromJS(json,{},self);
						if(self.onSave) {
							self.onSave(json);
						}
					}
				}).fail(function(response) {
					self.errors(helper.generateErrors(response.responseJSON));
				}).always(function(jqxhr) {
					if(self.onAfterSave)
						self.onAfterSave(formElement);
				});
			}
		}
	}
});