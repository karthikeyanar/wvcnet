"use strict";
// trimming with array ops
String.prototype.trim=function() { return this.split(/\s/).join(' '); }

// string replicator 
String.prototype.times=function(n) { var s=''; var i; for(i=0;i<n;i++) s+=this; return s; }

// zero padding and trailing
String.prototype.zp=function(n) { return '0'.times(n-this.length)+this; }
String.prototype.zt=function(n) { return this+'0'.times(n-this.length); }

// string reverse
String.prototype.reverse=function() { return this.split('').reverse().join(''); }

// clear format from a string representation of a number
String.prototype.clean=function() { return parseFloat(this.replace(/[^0-9|.|-]/g,'')); }

String.prototype.replaceAll=function(str1,str2,ignore) {
    return this.replace(new RegExp(str1.replace(/([\/\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|\<\>\-\&])/g,"\\$&"),(ignore?"gi":"g")),(typeof (str2)=="string")?str2.replace(/\$/g,"$$$$"):str2);
};

Number.prototype.zp=function(n) { return this.toString().zp(n); }

$.extend(window,{
    removeSymbols: function(d) {
        try {
            if(d==null) { d=""; }
            if(d==undefined) { d=""; }
            var value=d.toString();
            value=value.replace(/\$/g,'');
            value=value.replace(/\%/g,'');
            value=value.replace(/\,/g,'');
            value=value.replace(/\(/g,'-');
            value=value.replace(/\)/g,'');
            return value;
        } catch(ex) {
            return "";
        }
    }
    ,cFloat: function(value) {
        if(typeof value==="number") return value;
        value=removeSymbols(value);
        var decimal=".";
        var regex=new RegExp("[^0-9-"+decimal+"]",["g"]),
			unformatted=parseFloat(
				(""+value)
				.replace(/\((.*)\)/,"-$1")
				.replace(regex,'')
				.replace(decimal,'.')
			);
        return !isNaN(unformatted)?unformatted:0;
    }
	,cInt: function(value) {
	    if(typeof value==="number") return parseInt(value);
	    value=removeSymbols(value);
	    var decimal=".";
	    var regex=new RegExp("[^0-9-"+decimal+"]",["g"]),
			unformatted=parseInt(
				(""+value)
				.replace(/\((.*)\)/,"-$1")
				.replace(regex,'')
				.replace(decimal,'.')
			);
	    return !isNaN(unformatted)?unformatted:0;
	}
    ,formatCurrency: function(d,decimalPlace) {
        var precision=cFloat(decimalPlace);
        if(precision<=0) {
            precision=2;
        }
        d=cFloat(d);
        if(d==0) {
            return "";
        } else {
            return accounting.formatMoney(d,{ precision: precision });
        }
    }
	,formatPercentage: function(d) {
	    d=cFloat(d);
	    if(d==0) {
	        return "";
	    } else {
	        return accounting.formatNumber(d,{ precision: 2 })+"%";
	    }
	}
    ,formatDate: function(d,f) {
        if(d==undefined||d==null||d=="") {
            return "";
        }
        if(f==""||f==undefined) {
            f="MM/DD/YYYY";
        }
        return moment(d).format(f);
    }
    ,formatInteger: function(d) {
        var precision=cFloat(0);
        d=cFloat(d);
        if(d==0) {
            return "";
        } else {
            return accounting.formatNumber(d,{ precision: precision });
        }
    }
	,formatNumber: function(d,decimalPlace) {
	    var precision=cFloat(decimalPlace);
	    if(precision<=0) {
	        precision=2;
	    }
	    d=cFloat(d);
	    if(d==0) {
	        return "";
	    } else {
	        return accounting.formatNumber(d,{ precision: precision });
	    }
	}
    ,appendNRF: function(t) {
        var $thead=$("thead",t);
        var $tbody=$("tbody",t);
        $("tr.row-nrf",$tbody).remove();
        var rowLength=$("tr",$tbody).length;
        var columnLength=$("tr > th").length;
        if(rowLength<=0) {
            $tbody.append($("<tr class='row-nrf'><td class='text-center' colspan='"+columnLength+"'>No records found</td></tr>"));
        }
    }
});
