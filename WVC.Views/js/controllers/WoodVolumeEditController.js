"use strict";
define("WoodVolumeEditController",["knockout","komapping","../models/GridModel","../models/WoodVolumeModel","../models/WoodVolumeItemModel","finch","helper","service"],function(ko,komapping,GridModel,WoodVolumeModel,WoodVolumeItemModel,finch,helper,service) {
    return function(id) {
        var self=this;
        this.template="AddWoodVolume";
        this.role=ko.observable("");

        this.model=ko.observable(null);
        this.applyPlugins=function() {
            var $editCnt=$(".page-content");
        }

       


        ko.bindingHandlers.saveTo={
            init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
                var $element=$(element);
                $element.keyup(function(event) {
                    var $tr=$element.parents("tr:first");
                    if(event.keyCode==13) {
                        $(".save-btn",$tr).click();
                        return false;
                    }
                });
            },
            update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
            }
        }
        ko.bindingHandlers.calcWVCValue={
            init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
                var $element=$(element);
                $element.keyup(function(event) {
                    var $tr=$element.parents("tr:first");
                    if(event.keyCode==13) {
                        $(".save-btn",$tr).click();
                        return false;
                    }
                    var $length=$(":input[name='length']",$tr);
                    var $girth=$(":input[name='girth']",$tr);
                    var $volume=$("#volume",$tr);
                    var $finalVolume=$("#finalvolume",$tr);
                    var coeff=cFloat($("#coefficient",$tr).html());

                    var volume=cFloat((cFloat($girth.val())/4)*(cFloat($girth.val())/4)*cFloat($length.val()));
                    var finalVolume=volume*coeff;

                    //window.console.log((cFloat($girth.val())/4)*(cFloat($girth.val())/4));

                    $volume.html(volume.toFixed(4));
                    $finalVolume.html(finalVolume.toFixed(4));
                });
            },
            update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
            }
        }

        ko.bindingHandlers.saveItem={
            init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
                var $btn=$(element);
                $btn
                .unbind("click")
                .click(function() {
                    var $tr=$btn.parents("tr:first");
                    $btn.button("loading")
                    var params=[];
                    $(":input",$tr).each(function() {
                        params[params.length]={ "name": $(this).attr("name"),"value": $(this).val() };
                    });
                    var url=helper.apiUrl("/WoodVolumeItem/Create");
                    $.ajax({
                        "url": url,
                        "cache": false,
                        "type": "POST",
                        "data": params
                    }).done(function(json) {
                        var dataFor=ko.dataFor($btn[0]);
                        if(dataFor) {
                            if(dataFor.is_edit_mode) {
                                dataFor.volume(json.volume.toFixed(4));
                                dataFor.final_volume(json.final_volume.toFixed(4));
                                dataFor.is_edit_mode(false);
                            } else {
                                var itemModel=new WoodVolumeItemModel();
                                itemModel.onDelete=function() {
                                    self.volumes.remove(itemModel);
                                }
                                komapping.fromJS(json,{},itemModel);
                                self.model().volumes.push(itemModel);
                                $(":input[type='text']",$tr).val("");
                            }
                        }
                    }).fail(function(response) {
                        var msg=helper.generateAlertMessage(response.responseJSON);
                        helper.alert(msg);
                    }).always(function(jqxhr) {
                        $btn.button("reset");
                    });
                });
            },
            update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
            }
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