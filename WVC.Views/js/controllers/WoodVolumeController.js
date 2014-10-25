"use strict";
define("WoodVolumeController",["knockout","komapping","../models/GridModel","../models/WoodVolumeModel","helper"],function(ko,komapping,GridModel,WoodVolumeModel,helper) {
	return function() {
		var self=this;
		this.template="WoodVolume";

		/* modal setup */
		this.templates=[];
		this.getTemplate=function(name) {
			var template="";
			$.each(self.templates,function(i,temp) {
				if(temp.name==name) {
					template=temp.template;
				}
			});
			return template;
		}
		this.loadTemplate=function($modal,data,callback) {
			var template=self.getTemplate(data.name);
			if(template=="") {
				$.get(data.url,function(html) {
					template=html;
					self.templates.push({ "name": data.name,"template": template });
					$(".modal-body",$modal).html(template);
					if(callback) { callback(); }
				});
			} else {
				$(".modal-body",$modal).html(template);
				if(callback) { callback(); }
			}
		}
		this.openModal=function(data,callback) {
			var $cnt=$(".modal-container");
			var modalID="modal-"+data.name;
			$("#"+modalID,$cnt).remove();
			$("#modal-template").tmpl(data).appendTo($cnt);
			var $modal=$("#"+modalID,$cnt);
			var $body=$("body");
			helper.handleBlockUI({
				"target": $body
			});
			self.loadTemplate($modal,data,function() {
				helper.unblockUI($body);
				if(callback) { callback($modal); }
			});
		}
		/* end modal setup */

		/* load controllers */
		this.controllers=[];
		this.loadController=function(name,callback) {
			var controller=null;
			$.each(self.controllers,function(i,c) {
				if(c.name==name) {
					controller=c.object;
				}
			});
			if(controller==null) {
				helper.handleBlockUI();
				require([name],function(WoodVolumeController) {
					helper.unblockUI();
					var controller=new WoodVolumeController(self);
					self.controllers.push({ "name": name,"object": controller });
					if(callback)
						callback(controller);
				});
			} else {
				if(callback) {
					callback(controller);
				}
			}
		}
		/* end load controllers */


		/* woodVolume */
		this.grid=ko.observable(null);
		this.addWoodVolume=function(id,onSave) {
			self.openModal({
				"name": "EditWoodVolume"
				,"url": "/EA/AddWoodVolume"
				,"title": (id==0?"Add WoodVolume":"Edit WoodVolume")
				,"is_modal_full": false
				,"position": "top"
			}
			,function($modal) {
				var $target=$(".modal-body",$modal);
				$target.addClass('no-padding');
				var $editCnt=$(".edit-cnt",$modal);
				var m=new WoodVolumeModel();
				m.is_edit_mode(true);
				if(id==0) {
					m.is_edit_mode(false);
				}
				m.onBeforeSave=function(formElement) {
					$("#save",formElement).button('loading');
				}
				m.onAfterSave=function(formElement) {
					$("#save",formElement).button('reset');
					if(m.errors()==null) {
						$modal.modal('hide');
					}
				}
				m.onBeforeSelectWoodVolume=function() {
					helper.handleBlockUI({
						"target": $editCnt,verticalTop: true
					});
				}
				m.onAfterSelectWoodVolume=function() {
					helper.unblockUI($editCnt);
				}
				m.onSave=function(json) {
					if(onSave) {
						onSave(m,json);
					}
				}
				m.onPasswordChange=function() {
					$("#gapassword",$modal).removeAttr("readonly");
				}
				ko.applyBindings(m,$target[0]);
				$modal.modal('show');
				if(id>0) {
					m.id(id);
					m.load();
					m.is_focus(true);
				}
			});
		}
		this.assignEditEvents=function(model,gridModel) {
			if(model.onEdit==null) {
				model.onEdit=function() {
					self.addWoodVolume(model.id(),function(em,json) {
						var findRow=gridModel.findWoodVolume(json.id);
						if(findRow!=null) {
							komapping.fromJS(json,{},findRow);
						}
					});
				}
			}
			if(model.onDelete==null) {
				model.onDelete=function() {
					gridModel.rows.remove(this);
				}
			}
		}
		this.loadWoodVolumeGrid=function(m,$target) {
			//console.log("loadGrid");
			var $table=$("#woodVolumeTable");
			if($table[0]) { $target=$table; }
			helper.handleBlockUI({ "target": $target });
			var arr=new Array();
			arr[arr.length]={ "name": "pageIndex","value": m.page_index() };
			arr[arr.length]={ "name": "pageSize","value": m.rows_per_page() };
			arr[arr.length]={ "name": "sortName","value": m.sort_name() };
			arr[arr.length]={ "name": "sortOrder","value": m.sort_order() };
			var url=helper.apiUrl("/WoodVolume/Search");
			$.ajax({
				"url": url,
				"cache": false,
				"type": "GET",
				"data": arr
			}).done(function(json) {
				m.total_rows(json.total);
				m.rows.removeAll();
				$.each(json.rows,function(i,row) {
					var rm=new WoodVolumeModel();
					self.assignEditEvents(rm,m)
					komapping.fromJS(row,{},rm);
					m.rows.push(rm);
				});
				if(m.refreshCallBack==null) {
					m.refreshCallBack=function(m) {
						self.loadWoodVolumeGrid(m,$target);
					}
				}
			}).always(function() {
				helper.unblockUI($target);
			});
		}
		this.loadWoodVolume=function(callback) {
			GridModel.prototype.findWoodVolume=function(id) {
				var findRow=null;
				$.each(this.rows(),function(i,row) {
					if(row.id()==id) {
						findRow=row;
					}
				});
				return findRow;
			}
			GridModel.prototype.add=function() {
				var that=self.grid();
				self.addWoodVolume(0,function(woodVolume) {
					self.assignEditEvents(woodVolume,that);
					that.rows.splice(0,0,woodVolume);
				})
			}
			self.grid(new GridModel())
			self.grid().sort_name("name");
			var $target=$(".page-content");
			self.loadWoodVolumeGrid(self.grid(),$target);
			if(callback)
				callback();
		}
		/* end woodVolume */

	}
}
);