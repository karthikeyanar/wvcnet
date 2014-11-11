"use strict";
define(["knockout","komapping","helper","service"],function(ko,komapping,helper,service) {
    return function WoodVolumeItemModel() {
        var self=this;

        this.id=ko.observable(0);
        this.wood_volume_id=ko.observable("");
        this.description=ko.observable(0);
        this.length=ko.observable(0);
        this.girth=ko.observable(0);
        this.volume=ko.observable(0);
        this.co_efficient=ko.observable(0);
        this.final_volume=ko.observable(0);

        this.is_edit_mode=ko.observable(false);
        this.is_focus=ko.observable(false);
        this.success_result=ko.observable("");
        this.is_save_and_exit=ko.observable(true);

        this.onEdit=null;
        this.edit=function() {
            self.is_edit_mode(true);
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
        this.deleteItem=function() {
            if(self.onDelete) {
                self.onDelete();
            }
            var url=helper.apiUrl("/WoodVolumeItem/Delete/")+self.id();
            $.ajax({
                "url": url,
                "cache": false,
                "type": "DELETE",
            }).done(function(json) {
            }).fail(function(jqxhr) {
                alert(helper.deleteErrorMessage);
            });
        }

        this.errors=ko.observable(null);
        this.searchItem=function(request,response) {
            service.searchItem(request,response);
        }
        this.onBeforeSelectItem=null;
        this.onAfterSelectItem=null;
        this.selectItem=function(event,ui) {
            self.id(ui.item.id);
            self.load();
        }
        this.findURL="";
        this.load=function() {
            if(self.onBeforeSelectItem) {
                self.onBeforeSelectItem();
            }
            //var url=helper.apiUrl("/Item/Find/"+self.id());
            var url=self.findURL+"/"+self.id();
            $.ajax({
                "url": url,
                "cache": false,
                "type": "GET"
            }).done(function(json) {
                komapping.fromJS(json,{},self);
                if(self.onAfterSelectItem) {
                    self.onAfterSelectItem();
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
                //var url=helper.apiUrl("/Item/Create");
                var url=self.saveURL;
                var type="POST";
                //if(self.id()>0) { type="PUT"; url=helper.apiUrl("/Item/Update/")+self.id(); }
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