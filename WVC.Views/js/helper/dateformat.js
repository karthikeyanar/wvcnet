// trimming with array ops
String.prototype.trim=function() { return this.split(/\s/).join(' '); }

// string replicator 
String.prototype.times=function(n) { s=''; for(i=0;i<n;i++) s+=this; return s; }

// zero padding and trailing
String.prototype.zp=function(n) { return '0'.times(n-this.length)+this; }
String.prototype.zt=function(n) { return this+'0'.times(n-this.length); }

// string reverse
String.prototype.reverse=function() { return this.split('').reverse().join(''); }

// clear format from a string representation of a number
String.prototype.clean=function() { return parseFloat(this.replace(/[^0-9|.|-]/g,'')); }

Number.prototype.zp=function(n) { return this.toString().zp(n); }
// a global month names array
var gsMonthNames=new Array(
'January',
'February',
'March',
'April',
'May',
'June',
'July',
'August',
'September',
'October',
'November',
'December'
);
// a global day names array
var gsDayNames=new Array(
'Sunday',
'Monday',
'Tuesday',
'Wednesday',
'Thursday',
'Friday',
'Saturday'
);
// the date format prototype
Date.prototype.format=function(f) {
    if(!this.valueOf())
        return '&nbsp;';

    var d=this;

    return f.replace(/(yyyy|mmmm|mmm|mm|dddd|ddd|dd|hh|nn|ss|a\/p)/gi,
        function($1) {
            switch($1.toLowerCase()) {
                case 'yyyy': return d.getFullYear();
                case 'mmmm': return gsMonthNames[d.getMonth()];
                case 'mmm': return gsMonthNames[d.getMonth()].substr(0,3);
                case 'mm': return (d.getMonth()+1).zp(2);
                case 'dddd': return gsDayNames[d.getDay()];
                case 'ddd': return gsDayNames[d.getDay()].substr(0,3);
                case 'dd': return d.getDate().zp(2);
                case 'hh': return ((h=d.getHours()%12)?h:12).zp(2);
                case 'nn': return d.getMinutes().zp(2);
                case 'ss': return d.getSeconds().zp(2);
                case 'a/p': return d.getHours()<12?'AM':'PM';
            }
        }
    );
}

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
});
