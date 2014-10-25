"use strict";
define("WoodVolumeEditController",["knockout","komapping","../models/GridModel","../models/WoodVolumeModel","finch","helper","service"],function(ko,komapping,GridModel,WoodVolumeModel,finch,helper,service) {
	return function(id) {
		var self=this;
		this.template="AddWoodVolume";
		this.role=ko.observable("");

		this.model=ko.observable(null);
		this.applyPlugins=function() {
			var $editCnt=$(".page-content");
		}
		this.load=function() {
			var $editCnt=$(".page-content");
			var m=new WoodVolumeModel();
			m.id(id);
			m.is_edit_mode(true);
			m.deleteURL=helper.apiUrl("/WoodVolume/Delete");
			m.findURL=helper.apiUrl("/WoodVolume/Find");
			m.saveURL=helper.apiUrl("/WoodVolume/Create");
			if(id==0) {
				m.is_edit_mode(false);
			}
			m.onBeforeSave=function(formElement) {
				$("#save",formElement).button('loading');
				return true;
			}
			m.onAfterSave=function(formElement) {
				$("#save",formElement).button('reset');
				finch.navigate("/index");
			}
			m.onBeforeSelectWoodVolume=function() {
				helper.handleBlockUI({
					"target": $editCnt,verticalTop: true
				});
			}
			m.onAfterSelectWoodVolume=function() {
				helper.unblockUI($editCnt);
				self.applyPlugins();
			}
			m.onSave=function(json) {
				//if(onSave) {
				//	onSave(m,json);
				//}
			}
			m.onPasswordChange=function() {
				$("#password",$editCnt).removeAttr("readonly");
			}
			self.model(m);
			if(id>0) {
				m.load();
				m.is_focus(true);
			} else {
				self.applyPlugins();
			}
		}
	}
}
);