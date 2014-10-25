"use strict";
define(["knockout","komapping","helper","service"],function(ko,komapping,helper,service) {
	return function WoodVolumeListModel() {
		var self=this;

		this.id=ko.observable(0);

		this.user_id=ko.observable("");
		this.name=ko.observable("");
		this.description=ko.observable("");
		this.division_id=ko.observable("");
		this.division_name=ko.observable("");
		this.district_id=ko.observable("");
		this.district_name=ko.observable("");
		this.range_id=ko.observable("");
		this.range_name=ko.observable("");
		this.taluk_id=ko.observable("");
		this.taluk_name=ko.observable("");
		this.village_id=ko.observable("");
		this.village_name=ko.observable("");

		this.is_edit_mode=ko.observable(false);
		this.is_focus=ko.observable(false);
		this.success_result=ko.observable("");
		this.is_save_and_exit=ko.observable(true);

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
		this.deleteWoodVolume=function() {
			var url=helper.apiUrl("/WoodVolume/Delete/"+self.id());
			$.ajax({
				"url": url,
				"cache": false,
				"type": "DELETE"
			}).done(function(json) {
				if(self.onDelete)
					self.onDelete();
			}).fail(function(jqxhr) {
				alert(helper.deleteErrorMessage);
			});
		}

		this.errors=ko.observable(null);
		this.searchWoodVolume=function(request,response) {
			service.searchWoodVolume(request,response);
		}
		this.onBeforeSelectWoodVolume=null;
		this.onAfterSelectWoodVolume=null;
		this.selectWoodVolume=function(event,ui) {
			self.id(ui.item.id);
			self.load();
		}
		this.load=function() {
			if(self.onBeforeSelectWoodVolume) {
				self.onBeforeSelectWoodVolume();
			}
			var url=helper.apiUrl("/WoodVolume/Find/"+self.id());
			$.ajax({
				"url": url,
				"cache": false,
				"type": "GET"
			}).done(function(json) {
				komapping.fromJS(json,{},self);
				if(self.onAfterSelectWoodVolume) {
					self.onAfterSelectWoodVolume();
				}
			});
		}
		this.onSave=null;
		this.onBeforeSave=null;
		this.onAfterSave=null;
		this.save=function(formElement) {
			self.success_result("");
			var $frm=$(formElement);
			if($frm.valid()) {
				var data=$frm.serializeArray();
				var url=helper.apiUrl("/WoodVolume/Create");
				var type="POST";
				if(self.id()>0) { type="PUT"; url=helper.apiUrl("/WoodVolume/Update/")+self.id(); }
				if(self.onBeforeSave) {
					self.onBeforeSave(formElement);
				}
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