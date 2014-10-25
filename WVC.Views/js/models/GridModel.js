"use strict";
define(["knockout","komapping","helper"],function(ko,komapping,helper) {
	return function GridModel() {
		var self=this;
		this.rows=ko.observableArray([]);
		this.total_rows=ko.observable(0);
		this.page_index=ko.observable(1);
		this.rows_per_page=ko.observable(15);
		this.sort_name=ko.observable("");
		this.sort_order=ko.observable("");
		this.paging_status=ko.observable("");
	 
		this.refreshCallBack=null;
		this.onRenderPagination=function(status) {
			//console.log("onRenderPagination=",status);
			self.paging_status(status);
		}
		this.onPageClick=function(pageIndex) {
			self.page_index(pageIndex);
		}
		this.page_index.subscribe(function(newValue) {
			if(self.refreshCallBack)
				self.refreshCallBack(self);
		});
		this.rows_per_page.subscribe(function(newValue) {
			if(self.page_index()!=1) {
				self.page_index(1);
			} else {
				if(self.refreshCallBack)
					self.refreshCallBack(self);
			}
		});
		this.changeSortOrder=function(sortname,sortorder) {
			self.sort_order(sortorder);
			self.sort_name(sortname);
			if(self.refreshCallBack)
				self.refreshCallBack(self);
		};
	}
});