"use strict";
define("service",['helper'],function(helper) {
    function Service() {

        var self=this;
        this.selectDivision=function(param,callback) {
            var url=helper.apiUrl("/Division/Select");
            $.ajax({
                "url": url,
                "cache": false,
                "type": "GET",
                "data": param
            }).done(function(json) {
                if(callback)
                    callback(json);
            });
        }
        this.searchDivision=function(request,response,onBefore,onSuccess) {
            var param=new Array();
            param[param.length]={ "name": "pagesize","value": helper.autoCompletePageSize }
            param[param.length]={ "name": "term","value": request.term }
            if(onBefore) {
                onBefore(param);
            }
            self.selectDivision(param,function(json) {
                if(onSuccess) {
                    onSuccess(json)
                }
                response(json);
            });
        }
        this.selectDistrict=function(param,callback) {
            var url=helper.apiUrl("/District/Select");
            $.ajax({
                "url": url,
                "cache": false,
                "type": "GET",
                "data": param
            }).done(function(json) {
                if(callback)
                    callback(json);
            });
        }
        this.searchDistrict=function(request,response,onBefore,onSuccess) {
            var param=new Array();
            param[param.length]={ "name": "pagesize","value": helper.autoCompletePageSize }
            param[param.length]={ "name": "term","value": request.term }
            if(onBefore) {
                onBefore(param);
            }
            self.selectDistrict(param,function(json) {
                if(onSuccess) {
                    onSuccess(json)
                }
                response(json);
            });
        }
        this.selectRange=function(param,callback) {
            var url=helper.apiUrl("/Range/Select");
            $.ajax({
                "url": url,
                "cache": false,
                "type": "GET",
                "data": param
            }).done(function(json) {
                if(callback)
                    callback(json);
            });
        }
        this.searchRange=function(request,response,onBefore,onSuccess) {
            var param=new Array();
            param[param.length]={ "name": "pagesize","value": helper.autoCompletePageSize }
            param[param.length]={ "name": "term","value": request.term }
            if(onBefore) {
                onBefore(param);
            }
            self.selectRange(param,function(json) {
                if(onSuccess) {
                    onSuccess(json)
                }
                response(json);
            });
        }
        this.selectTaluk=function(param,callback) {
            var url=helper.apiUrl("/Taluk/Select");
            $.ajax({
                "url": url,
                "cache": false,
                "type": "GET",
                "data": param
            }).done(function(json) {
                if(callback)
                    callback(json);
            });
        }
        this.searchTaluk=function(request,response,onBefore,onSuccess) {
            var param=new Array();
            param[param.length]={ "name": "pagesize","value": helper.autoCompletePageSize }
            param[param.length]={ "name": "term","value": request.term }
            if(onBefore) {
                onBefore(param);
            }
            self.selectTaluk(param,function(json) {
                if(onSuccess) {
                    onSuccess(json)
                }
                response(json);
            });
        }
        this.selectVillage=function(param,callback) {
            var url=helper.apiUrl("/Village/Select");
            $.ajax({
                "url": url,
                "cache": false,
                "type": "GET",
                "data": param
            }).done(function(json) {
                if(callback)
                    callback(json);
            });
        }
        this.searchVillage=function(request,response,onBefore,onSuccess) {
            var param=new Array();
            param[param.length]={ "name": "pagesize","value": helper.autoCompletePageSize }
            param[param.length]={ "name": "term","value": request.term }
            if(onBefore) {
                onBefore(param);
            }
            self.selectVillage(param,function(json) {
                if(onSuccess) {
                    onSuccess(json)
                }
                response(json);
            });
        }
    }
    var s=new Service();
    return s;
});

