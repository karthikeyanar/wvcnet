"use strict";
define(["knockout","komapping","../models/WoodVolumeItemModel","helper","service"],function(ko,komapping,WoodVolumeItemModel,helper,service) {
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
        this.volumes=ko.observableArray();


        this.volume_count=ko.computed(function() {
            return self.volumes().length;
        });

        this.is_edit_mode=ko.observable(false);
        this.is_focus=ko.observable(false);
        this.success_result=ko.observable("");
        this.is_save_and_exit=ko.observable(true);

        this.searchDivision=function(request,response) {
            service.searchDivision(request,response);
        }
        this.selectDivision=function(event,ui) {
            self.division_id(ui.item.id);
        }
        this.changeDivision=function(event,ui) {
            self.division_id((ui.item?ui.item.id:0));
        }

        this.searchDistrict=function(request,response) {
            service.searchDistrict(request,response);
        }
        this.selectDistrict=function(event,ui) {
            self.district_id(ui.item.id);
        }
        this.changeDistrict=function(event,ui) {
            self.district_id((ui.item?ui.item.id:0));
        }

        this.searchRange=function(request,response) {
            service.searchRange(request,response);
        }
        this.selectRange=function(event,ui) {
            self.range_id(ui.item.id);
        }
        this.changeRange=function(event,ui) {
            self.range_id((ui.item?ui.item.id:0));
        }

        this.searchTaluk=function(request,response) {
            service.searchTaluk(request,response);
        }
        this.selectTaluk=function(event,ui) {
            self.taluk_id(ui.item.id);
        }
        this.changeTaluk=function(event,ui) {
            self.taluk_id((ui.item?ui.item.id:0));
        }

        this.searchVillage=function(request,response) {
            service.searchVillage(request,response);
        }
        this.selectVillage=function(event,ui) {
            self.village_id(ui.item.id);
        }
        this.changeVillage=function(event,ui) {
            self.village_id((ui.item?ui.item.id:0));
        }


        this.getEditUrl=function(role) {
            var url="";
            url="#/edit-volume/"+self.id();
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
        this.deleteWoodVolume=function() {
            var url=helper.apiUrl("/WoodVolume/Delete/"+self.id());
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
        this.searchWoodVolume=function(request,response) {
            service.searchWoodVolume(request,response);
        }
        this.onBeforeSelectWoodVolume=null;
        this.onAfterSelectWoodVolume=null;
        this.selectWoodVolume=function(event,ui) {
            self.id(ui.item.id);
            self.load();
        }
        this.findURL="";
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
                $.each(json.items,function(i,item) {
                    var itemModel=new WoodVolumeItemModel();
                    itemModel.onDelete = function(){
                        self.volumes.remove(itemModel);
                    }
                    komapping.fromJS(item,{},itemModel);
                    self.volumes.push(itemModel);
                });
                if(self.onAfterSelectWoodVolume) {
                    self.onAfterSelectWoodVolume();
                }
            });
        }
        this.onSave=null;
        this.onBeforeSave=null;
        this.onAfterSave=null;
        this.saveURL="";

         
        this.saveItem=function(a,b) {
            window.console.log(a,b);
        }
        this.save=function(formElement) {
            self.success_result("");
            var $frm=$(formElement);
            if($frm.valid()) {
                var url=helper.apiUrl("/WoodVolume/Create");
                var type="POST";
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