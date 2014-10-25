"use strict";
define("helper",function() {


	var helper=new function() {

		var self=this;

		this.themesPath="/css/themes/";
		this.autoCompletePageSize=1000;
		this.deleteErrorMessage="Cann't Delete! Child record found!";
		// IE mode
		this._IsIE8=false;
		this._IsIE9=false;
		this._IsIE10=false;
		this.isHeaderFixed=false;
		this.responsiveHandlers=[];
		this.addResponsiveHandler=function(func) {
			this.responsiveHandlers.push(func);
		}

		this.isIE8=function() {
			return self._IsIE8;
		}

		this.isIE9=function() {
			return self._IsIE9;
		}

		this.isIE10=function() {
			return self._IsIE10;
		}

		this.getViewPort=function() {
			var e=window,a='inner';
			if(!('innerWidth' in window)) {
				a='client';
				e=document.documentElement||document.body;
			}
			return {
				width: e[a+'Width'],
				height: e[a+'Height']
			}
		}

		this.isTouchDevice=function() {
			return ('ontouchstart' in document.documentElement);
		}

		this.scrollTo=function(el,offeset) {
			pos=el?el.offset().top:0;
			$('html,body').animate({
				scrollTop: pos+(offeset?offeset:0)
			},'slow');
		}

		this.scrollTop=function() {
			self.scrollTo();
		}

		this.Left=function(str,n) {
			if(n<=0)
				return "";
			else if(n>String(str).length)
				return str;
			else
				return String(str).substring(0,n);
		}

		this.Right=function(str,n) {
			if(n<=0)
				return "";
			else if(n>String(str).length)
				return str;
			else {
				var iLen=String(str).length;
				return String(str).substring(iLen,iLen-n);
			}
		}

		this.handleMetroCheck=function(target) {
			if(!target) target=$("body");

			$('.metro-checkbox,.metro-radio',$(target)).find(":input").each(function() {
				var $this=$(this);
				if($this.closest("label").size()==1) {
					if($this.closest("label").find(".check").size()==0)
						$this.after("<span class='check'></span>");
				}
			});
		}

		this.handleFormValidation=function() {
			if($.fn.validate) {
				$(".form-validate").validate({
					ignore: "input[type='text']:hidden"
				});
			}
		}

		this.handleToolTip=function(target) {
			if(!target) target=$("body");

			$("[data-toggle='tooltip']",target).tooltip();
			$("[rel='tooltip']",target).tooltip();
		}

		this.handlePopover=function(target) {
			if(!target) target=$("body");
			$("[data-toggle='popover']",target).popover();
		}

		this.unblockUI=function(target) {
			if(target) {
				$(target).unblock({
					onUnblock: function() {
						$(target).css('position','').css('zoom','');
					}
				});
			} else {
				$.unblockUI();
			}
		}

		this.handleBlockUI=function(options) {
			var options=$.extend(true,{},options);
			var message=(options.message?options.message:"<i class='fa fa-circle-o-notch fa-spin'></i>&nbsp;&nbsp;Loading...");
			var color=(options.bgColor?options.bgColor:"bg-primary");
			var centery=(options.verticalTop==true?false:true);
			var basez=(options.zindex?options.zindex:"999999");
			var overlayOpacity=(options.overlayOpacity?options.overlayOpacity:"0.2");
			message='<div class="blockUI-message '+color+'"><span>'+message+'</span></div>';
			if(options.target) {
				$(options.target).block({
					message: message,
					baseZ: basez,
					centerY: centery,
					css: {
						top: '10%',
						border: '0',
						padding: '0',
						backgroundColor: 'none'
					},
					overlayCSS: {
						backgroundColor: 'transparent',
						opacity: overlayOpacity,
						cursor: 'wait'
					}
				});
			} else {
				$.blockUI({
					message: message,
					baseZ: basez,
					centerY: centery,
					css: {
						border: '0',
						padding: '0',
						backgroundColor: 'none'
					},
					overlayCSS: {
						backgroundColor: '#000',
						opacity: overlayOpacity,
						cursor: 'wait'
					}
				});
			}
		}

		this.handleDropdownHoldClick=function() {
			$('body').on('click','.dropdown-menu.hold-on-click',function(e) {
				e.stopPropagation();
			})
		}

		this.generateErrors=function(json) {
			var errros=[];
			if(json.ModelState) {
				var jsonData=json.ModelState;
				for(var obj in jsonData) {
					if(jsonData.hasOwnProperty(obj)) {
						for(var prop in jsonData[obj]) {
							if(jsonData[obj].hasOwnProperty(prop)) {
								//alert(prop+':'+jsonData[obj][prop]);
								errros.push({ "ErrorMessage": jsonData[obj][prop] });
							}
						}
					}
				}
			}
			return errros;
		}

		this.selectMenu=function(menuName) {
			var $menuBar=$(".menu-bar");
			var $menuLis=$(".menu-list",$menuBar);
			$("li[menu-name='"+menuName+"']").addClass("active").siblings().removeClass("active");
		}

		this.handleBackToTop=function() {
			var offset=220;
			var duration=500;
			var $button=$('<a href="#" class="back-to-top"><i class="fa fa-angle-up"></i></a>');
			$(".back-to-top").remove();
			$("body").append($button);

			$(window).scroll(function() {
				if($(this).scrollTop()>offset) {
					$('.back-to-top').fadeIn(duration);
				} else {
					$('.back-to-top').fadeOut(duration);
				}
			});

			$('.back-to-top').click(function(event) {
				event.preventDefault();
				self.scrollTop();
				return false;
			});

		}

		this.resizeContentHeight=function() {
			var $body=$('body');
			var $header=$('.header');
			var $footer=$('.footer');
			var $menubar=$('.menu-bar > .navbar');
			var $pageContainer=$('.page-container');
			var $pageContent=$('.page-content',$pageContainer);

			var windowHeight=this.getViewPort().height;
			var headerHeight=$header.outerHeight(true);
			var footerHeight=$footer.outerHeight(true);
			var menuBarheight=$menubar.outerHeight(true);
			var height=0;
			if(menuBarheight<=0) {
				menuBarheight=40;
			}
			//console.log("resizeContentHeight=",$menubar,menuBarheight);
			height=windowHeight-headerHeight-footerHeight-menuBarheight;

			$pageContent.css({ 'min-height': height+'px' });
		}

		this.responsive=function() {
			// reinitialize other subscribed elements
			for(var i in self.responsiveHandlers) {
				var each=self.responsiveHandlers[i];
				each.call();
			}
		}

		this.jqValidationSetDefaults=function() {
			if($.validator) {
				$.validator.setDefaults({
					highlight: function(element) {
						$(element).closest('.form-group').addClass('has-error');
					},
					unhighlight: function(element) {
						$(element).closest('.form-group').removeClass('has-error');
					},
					errorElement: 'span',
					errorClass: 'help-block',
					errorPlacement: function(error,element) {
						if(element.closest('.input-group').size()===1) {
							error.insertAfter(element.closest('.input-group'));
						} else if(element.closest('.input-icon').size()===1) {
							error.insertAfter(element.closest('.input-icon'));
						} else if(element.closest('.select-group').size()===1) {
							error.insertAfter(element.closest('.select-group'));
						} else {
							error.insertAfter(element);
						}
					}
				});
			}
		}

		this.dynaminCSS=[];
		this.loadCSS=function(files,callback) {
			$.each(files,function(i,fileName) {
				var isExist=false;
				$.each(helper.dynaminCSS,function(i,f) {
					if(f==fileName) {
						isExist=true;
					}
				});
				if(isExist==false) {
					var fileRef=document.createElement("link");
					fileRef.setAttribute("rel","stylesheet");
					fileRef.setAttribute("type","text/css");
					fileRef.setAttribute("href",fileName+"?v="+_Version);
					document.getElementsByTagName("head")[0].appendChild(fileRef);
					helper.dynaminCSS.push(fileName);
				}
			});
			if(callback) {
				callback();
			}
		}
		this.appModel=null;
		this.apiUrl=function(url) {
			return "/api"+url;
		}
		this.getLS=function(key) {
			return $.localStorage.get(key);
		}
		this.setLS=function(key,value) {
			$.localStorage.set(key,value);
		}
		this.getSS=function(key) {
			return $.sessionStorage.get(key);
		}
		this.setSS=function(key,value) {
			$.sessionStorage.set(key,value);
		}
		this.setAuth=function(json,isRememberMe) {
			var key="auth";
			self.setLS(key,null);
			self.setSS(key,null);
			if(json!=null) {
				if(isRememberMe==true) {
					self.setLS(key,json);
				} else {
					self.setSS(key,json);
				}
			}
		}
		this.getAuth=function() {
			var key="auth";
			var isRememberMe=self.getLS("rememberme");
			if(isRememberMe==true) {
				return self.getLS(key);
			} else {
				return self.getSS(key);
			}
		}
		this.addAuthHeader=function(xhr) {
			var authToken=helper.getAuth();
			if(authToken!=null) {
				xhr.setRequestHeader("Authorization","Bearer "+authToken.access_token);
			}
		}
		this.onAjaxError=null;
		this.ajaxSetup=function() {
			$.ajaxSetup({
				beforeSend: function(xhr) {
					helper.addAuthHeader(xhr);
				},
				error: function(jqXHR,exception) {
					//console.log("error=",jqXHR,exception);
					if(helper.onAjaxError) {
						helper.onAjaxError(jqXHR,exception);
					}
					/*
					if(jqXHR.status===0) {
					alert('Not connect. Verify Network.');
					} else if(jqXHR.status==404) {
					alert('Requested page not found');
					} else if(jqXHR.status==500) {
					alert('Internal Server Error');
					} else if(jqXHR.status==401) {
					alert('Unauthorized');
					} else if(exception==='parsererror') {
					alert('Requested JSON parse failed.');
					} else if(exception==='timeout') {
					alert('Time out error.');
					} else if(exception==='abort') {
					alert('Ajax request aborted.');
					} else {
					alert('Uncaught Error.\n'+jqXHR.responseText);
					}
					*/
				}
			});
		}

		this.tempData=[];
		this.clearTempData=function() {
			self.tempData=[];
		}
		this.getTempData=function(key) {
			var data=null;
			$.each(self.tempData,function(i,d) {
				if(d.key==key) {
					data=d.data;
				}
			})
			return data;
		}
		this.setTempData=function(key,data) {
			var existData=self.getTempData(key);
			if(existData==null)
				self.tempData.push({ "key": key,"data": data });
			else
				existData.data=data;
		}

		this.init=function() {
			self._IsIE8=!!navigator.userAgent.match(/MSIE 8.0/);
			self._IsIE9=!!navigator.userAgent.match(/MSIE 9.0/);
			self._IsIE10=!!navigator.userAgent.match(/MSIE 10.0/);
			// Plugins
			self.jqValidationSetDefaults();
			self.handleFormValidation();
			self.handleToolTip();
			self.handlePopover();
			self.handleDropdownHoldClick();
			self.handleMetroCheck();
			self.handleBackToTop();
			self.resizeContentHeight();
			self.ajaxSetup();
		}
	}
	helper.init();
	$(window).resize(function() {
		helper.resizeContentHeight();
		helper.responsive();
	});
	return helper;
});

