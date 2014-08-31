var App=new function() {

	var self=this;

	this.themesPath="/css/themes/";
	// IE mode
	this._IsIE8=false;
	this._IsIE9=false;
	this._IsIE10=false;
	this.isHeaderFixed=false;
	this.isSidebarFixed=false;
	this.isFixedLayout=false;
	this.sidebarType="";
	this.sidebarPosition="";
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


	this.handleBreadcrumb=function() {
		// dismiss breadcrumb
		$(document).on('click.breadcrumb.data-api',"[data-dismiss='breadcrumb']",function() {
			var $breadcrumb=$(this).parents(".breadcrumb:first");
			$breadcrumb.fadeOut();
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

	this.handlePanelTools=function() {

		$('body').on('click.panel > .btn-group > a',"[data-action='collapse']",function(e) {
			e.preventDefault();
			var el=$(this).closest(".panel").children(".panel-body");
			if($(this).hasClass("expand")==false) {
				$(this).addClass("expand");
				el.slideUp(200);
			} else {
				$(this).removeClass("expand");
				el.slideDown(200);
			}
		});

		$('body').on('click.panel > .btn-group > a',"[data-action='refresh']",function(e) {
			e.preventDefault();
			var el=$(this).closest(".panel").children(".panel-body");
			// for demo purpose
			self.handleBlockUI({ "target": el,"verticalTop": true });
			setTimeout(function() {
				self.unblockUI(el);
			},1000)
		});

		$('body').on('click.panel > .btn-group > a',"[data-action='remove']",function(e) {
			e.preventDefault();
			$(this).closest(".panel").remove();
		});
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
		var basez=(options.zindex?options.zindex:"1000");
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
					backgroundColor: '#000',
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

	this.handleBootstrapSelect=function(target) {
		if(!target) target=$("body");
		if($.fn.selectpicker) {
			$('.selectpicker',target).selectpicker();
		}
	}

	this.handleSwitch=function(target) {
		if(!target) target=$("body");
		if($.fn.bootstrapSwitch) {
			$('.switch',target).bootstrapSwitch();
		}
	}

	this.handleSlider=function(target) {
		if(!target) target=$("body");
		if($.fn.slider) {
			$('.slider',target).slider();
		}
	}

	this.handleSlimScroll=function() {
		if($.fn.slimScroll) {
			$('.slimscroll').each(function() {
				var $this=$(this);
				if($this.attr("data-initialized")=="1") return;
				$this.slimScroll({
					width: ($this.attr("data-width")?$this.attr("data-width"):'auto'),
					height: ($this.attr("data-height")?$this.attr("data-height"):'250px'),
					size: ($this.attr("data-size")?$this.attr("data-size"):'5px'),
					color: ($this.attr("data-color")?$this.attr("data-color"):'#000'),
					position: ($this.attr("data-position")?$this.attr("data-position"):'right'),
					distance: ($this.attr("data-distance")?$this.attr("data-distance"):'1px'),
					start: ($this.attr("data-start")?$this.attr("data-start"):'top'),
					opacity: ($this.attr("data-opacity")?$this.attr("data-opacity"):'0.4'),
					alwaysVisible: ($this.attr("data-always-visible")=="true"?true:false),
					disableFadeOut: ($this.attr("data-disable-fade-out")=="true"?true:false),
					railVisible: ($this.attr("data-rail-visible")=="true"?true:false),
					railColor: ($this.attr("data-rail-color")?$this.attr("data-rail-color"):'#fff'),
					railOpacity: ($this.attr("data-rail-opacity")?$this.attr("data-rail-opacity"):'.2'),
					railDraggable: ($this.attr("data-rail-draggable")=="false"?false:true),
					railClass: ($this.attr("data-rail-class")?$this.attr("data-rail-class"):'slimScrollRail'),
					barClass: ($this.attr("data-bar-class")?$this.attr("data-bar-class"):'slimScrollBar'),
					wrapperClass: ($this.attr("data-wrapper-class")?$this.attr("data-wrapper-class"):'slimScrollDiv'),
					allowPageScroll: ($this.attr("data-allow-page-scroll")=="true"?true:false),
					wheelStep: ($this.attr("data-wheel-step")?$this.attr("data-wheel-step"):'20'),
					touchScrollStep: ($this.attr("data-touch-scroll-step")?$this.attr("data-touch-scroll-step"):'200'),
					borderRadius: ($this.attr("data-border-radius")?$this.attr("data-border-radius"):'0'),
					railBorderRadius: ($this.attr("data-rail-border-radius")?$this.attr("data-rail-border-radius"):'0'),
					animate: ($this.attr("data-animate")=="false"?false:true)
				});
				$this.attr("data-initialized","1");
			});
		}
	}

	this.handleBackToTop=function() {
		var offset=220;
		var duration=500;
		var $button=$('<a href="#" class="back-to-top"><i class="fa fa-angle-up"></i></a>');
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
		var $pageContainer=$('.page-container');
		var $pageContent=$('.page-content',$pageContainer);
		var $sidebar=$('.sidebar',$pageContainer);

		var windowHeight=this.getViewPort().height;
		var headerHeight=$header.outerHeight(true);
		var footerHeight=$footer.outerHeight(true);
		var sidebarHeight=$sidebar.outerHeight(true);
		var height=0;

		if($body.hasClass("sidebar-fixed")) {
			height=windowHeight-headerHeight-footerHeight;
		} else {
			if(this.getViewPort().width<992) {
				height=windowHeight-headerHeight-footerHeight;
			} else {
				height=sidebarHeight+20;
			}
			if(this.getViewPort().width>1024&&(height+headerHeight+footerHeight)<windowHeight) {
				height=windowHeight-headerHeight-footerHeight;
			}
		}
		$pageContent.css({ 'min-height': height+'px' });
	}

	this.responsive=function() {
		// reinitialize other subscribed elements
		for(var i in self.responsiveHandlers) {
			var each=self.responsiveHandlers[i];
			each.call();
		}
	}

	this.handleSidebar=function() {
		var $body=$("body");
		var $header=$(".header");
		var $sidebar=$(".sidebar");
		var $sidebarContent=$(".sidebar-content",$sidebar);
		var $sidebarMenu=$(".sidebar-menu",$sidebar);
		var $pageContent=$(".page-content");

		var toggle=true;
		var slideSpeed=200;
		var slideOffeset= -200;


		// Sidebar User  
		$(".sidebar-user",$sidebar).off('click').off('mouseenter').off('mouseleave');
		if($body.hasClass("sidebar-fixed")==false) {
			$(".sidebar-user",$sidebar)
			.on('click',function() {
				$(".info",this).toggleClass("show");
			}).on('mouseenter',function() {
				$(".info",this).addClass("show");
			}).on('mouseleave',function() {
				$(".info",this).removeClass("show");
			});
		}

		// Sidebar Menu Accordion
		$sidebarMenu.find('li.open').has('ul').children('ul').addClass('collapse in');
		$sidebarMenu.find('li').not('.open').has('ul').children('ul').addClass('collapse');

		$sidebarMenu.children('li').off('mouseenter').off('mouseleave');
		$sidebarMenu.find('li').has('ul').children('a').off('click').on('click',function(e) {
			e.preventDefault();
			var $this=$(this);
			var $currentUL=$(this).parent('li').toggleClass('open').children('ul');
			var $body=$("body");

			var dynamicScrollFunc=function() {
				if($body.hasClass("sidebar-sm")==false&&$body.hasClass("sidebar-md")==false) {
					if($body.hasClass("sidebar-fixed")==false) {
						self.scrollTo($this,slideOffeset);
					} else {
						var offsetTop=$this.offset().top-$(window).scrollTop();
						$sidebarContent.slimScroll({ 'scrollTo': offsetTop });
					}
				}
				self.resizeContentHeight();
			}

			if($currentUL.hasClass("in")==false) {
				$currentUL.slideDown(slideSpeed,function() {
					$(this).addClass("in").css({ "display": "" });
					dynamicScrollFunc();
				});
			} else {
				$currentUL.slideUp(slideSpeed,function() {
					$(this).removeClass("in").css({ "display": "" });
					dynamicScrollFunc();
				});
			}
			if(toggle) {
				$(this).parent('li').siblings().removeClass('open').children('ul.in').slideUp(slideSpeed,function() {
					$(this).removeClass("in").css({ "display": "" });
				});
			}
		});

		$sidebar.off("mouseenter").off("mouseleave");

		// Sidebar Fixed And Sidebar Medium Fixed
		if($body.hasClass("sidebar-fixed")&&$body.hasClass("sidebar-md-fixed")) {
			$sidebar.on('mouseenter',function() {
				if($body.hasClass("sidebar-fixed")) {
					$body.removeClass("sidebar-md");
				}
			}).on('mouseleave',function() {
				if($body.hasClass("sidebar-fixed")) {
					$body.addClass("sidebar-md");
				}
			});
		}

		// Sidebar Fixed And Sidebar Small Fixed 
		if($body.hasClass("sidebar-fixed")&&$body.hasClass("sidebar-sm-fixed")) {
			$sidebar.on('mouseenter',function() {
				if($body.hasClass("sidebar-fixed")) {
					$body.removeClass("sidebar-sm");
				}
			}).on('mouseleave',function() {
				if($body.hasClass("sidebar-fixed")) {
					$body.addClass("sidebar-sm");
				}
			});
		}

		// Sidebar Medium,Small And Sidebar Fixed = False
		$sidebarMenu.children('li').off('mouseenter').off('mouseleave');
		if($body.hasClass("sidebar-fixed")==false&&($body.hasClass("sidebar-sm")||$body.hasClass("sidebar-md"))&&self.getViewPort().width>=992) {
			$sidebarMenu.children('li').on('mouseleave',function(e) {
				e.preventDefault();
				$(this).removeClass('open').removeClass('hover').children('ul.in').removeClass("in").css({
					"display": ""
				});
			})
			.on('mouseenter',function(e) {
				var $this=$(this).removeClass("pull-up");
				var $currentUL=$this.addClass('open hover').children('ul').addClass("in").css({ "display": "" }).css("top","");
				if(!$currentUL.get(0)) {
					$this.addClass("no-submenu")
				} else {
					$this.removeClass("no-submenu")
				}
				var $menuText=$(".menu-text",$currentUL.parent()).css("top","");

				var offset=$this.offset();
				var windowHeight=document.documentElement.clientHeight;
				var scrollY=document.documentElement.scrollTop||document.body.scrollTop;
				windowHeight+=scrollY;
				var menuTogglePositon=offset.top;
				var menuToggleHeight=$this.innerHeight();
				var menuHeight=$currentUL.innerHeight();
				var headerHeight=$(".header").innerHeight();
				var footerHeight=$(".footer").innerHeight();
				var availbleHeight=windowHeight-headerHeight-menuTogglePositon;
				var menuTop=0;
				var menuTextTop=0;
				if(availbleHeight<(menuHeight)) {
					menuTextTop=(menuHeight)-availbleHeight;
					if(menuTextTop>menuTogglePositon) {
						menuTextTop=(menuTogglePositon-headerHeight-5);
					}
					menuTop=(menuTextTop-menuToggleHeight)* -1;
					if(menuTextTop>menuToggleHeight) {
						$this.addClass("pull-up");
					}
					menuTextTop=menuTextTop* -1;
				} else {
					menuTextTop=0;
					menuTop=menuToggleHeight;
				}
				if($body.hasClass("sidebar-md")) {
					menuTop-=menuToggleHeight;
				}
				$currentUL.css("top",menuTop);
				$menuText.css("top",menuTextTop);
				$(this).siblings().removeClass('open').removeClass('hover').children('ul.in').removeClass("in").css({
					"display": ""
				});
			});
		}

		if($body.hasClass("sidebar-fixed")&&$body.hasClass("header-fixed")==false) {
			$sidebar
            .on("affixed.bs.affix",function() {
            	self.handleSidebarSlimScroll();
            })
            .on("affixed-top.bs.affix",function() {
            	self.handleSidebarSlimScroll();
            })
            .affix({
            	offset: {
            		top: function() {
            			return (this.bottom=$('.header').outerHeight(true))
            		},bottom: 0
            	}
            })
		} else {
			$(window).off('.affix')
			$sidebar.removeData('bs.affix').removeClass('affix affix-top affix-bottom')
		}

		self.handleSidebarSlimScroll();
		self.resizeContentHeight();
	}

	this.handleSidebarSlimScroll=function() {
		var $body=$("body");
		var $header=$(".header");
		var $sidebar=$(".sidebar");
		var $sidebarContent=$(".sidebar-content",$sidebar);
		// Sidebar Fixed Custom Scroll
		if($.fn.slimScroll) {
			var windowHeight=$(window).height();
			if($sidebarContent.parent('.slimScrollDiv').size()===1) { // destroy existing instance  
				$sidebarContent.slimScroll({ destroy: true });
				$sidebarContent.css({ 'height': '','overflow': '','width': '' });
			}
			if($body.hasClass("sidebar-fixed")) {
				var height=windowHeight;
				if($body.hasClass("header-fixed")||$sidebar.hasClass("affix-top"))
					height=height-$header.outerHeight(true);

				$sidebarContent.slimScroll({
					allowPageScroll: false,
					size: '5px',
					color: '#000',
					opacity: 1,
					borderRadius: '0px',
					railBorderRadius: '0px',
					wrapperClass: 'slimScrollDiv',
					position: 'right',
					height: height,
					disableFadeOut: true
				});
			}
		}
	}

	this.handleHorizontalMenu=function() {
		var $header=$(".header");
		var $horMenu=$(".hor-menu",$header);
		if(this.isTouchDevice()==true) {
			$horMenu.find('.dropdown-menu').click(function(e) {
				e.stopPropagation();
			});
			// handle submenus
			$horMenu.find('.dropdown-submenu').each(function() {
				var $this=$(this);
				var subTimeout;
				$this.click(function() {
					$this.toggleClass("open");
				});
			});
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

	this.changeLayout=function() {
		self.applyLayoutClasses();
		self.handleSidebar();
		setTimeout(function() {
			self.responsive();
		},500);
	}

	this.applyLayoutClasses=function() {
		var $body=$("body");
		var $header=$(".header");
		var $headerContent=$(".header-content",$header);
		var $pageContainer=$(".page-container");

		if(this.sidebarPosition=="right")
			$body.addClass("sidebar-right");
		else
			$body.removeClass("sidebar-right");

		$body
		.removeClass("sidebar-sm")
		.removeClass("sidebar-sm-fixed")
		.removeClass("sidebar-md")
		.removeClass("sidebar-md-fixed");

		if(self.sidebarType=="sm") {
			$body.addClass("sidebar-sm");
		} else if(self.sidebarType=="md") {
			$body.addClass("sidebar-md");
		}

		if(this.isSidebarFixed) {
			$body.addClass("sidebar-fixed");
			if($body.hasClass("sidebar-sm"))
				$body.addClass("sidebar-sm-fixed")
			else if($body.hasClass("sidebar-md"))
				$body.addClass("sidebar-md-fixed")

			if(self.getViewPort().width<992&&$body.hasClass("sidebar-open")) {
				$body.addClass("page-container-no-scroll");
			}
		} else {
			$body.removeClass("sidebar-fixed")
			.removeClass("sidebar-sm-fixed")
			.removeClass("sidebar-md-fixed")
			.removeClass("page-container-no-scroll");
		}

		if(this.isHeaderFixed) {
			$body.addClass("header-fixed");
			$header.addClass("navbar-fixed-top");
		} else {
			$body.removeClass("header-fixed");
			$header.removeClass("navbar-fixed-top");
		}

		if(this.isFixedLayout&&this.getViewPort().width>=1024) {
			$body.addClass("page-boxed");
			$headerContent.addClass("container").removeClass("container-fluid");
			$pageContainer.addClass("container").removeClass("container-fluid");
		} else {
			$headerContent.removeClass("container").addClass("container-fluid");
			$pageContainer.removeClass("container").addClass("container-fluid");
		}
	}

	this.setTheme=function(themeName) {
		$('#theme-link').attr("href",this.themesPath+themeName+".css?v=1.0");
	}

	this.themePanelSettings=function() {
		var $body=$("body");
		var $themePanel=$(".theme-panel");
		if(!$themePanel.get(0)) return;

		$(".toggler",$themePanel).click(function() {
			$(".theme-options",$themePanel).toggleClass("open");
		});

		var $chkHeader=$("#chkheader",$themePanel);
		var $chkSidebar=$("#chksidebar",$themePanel);
		var $chkLayout=$("#chklayout",$themePanel);
		var $selectSidebarType=$("#selectSidebarType",$themePanel);
		var $selectSidebarPosition=$("#selectSidebarPosition",$themePanel);
		var sidebarType="";
		if($body.hasClass("sidebar-sm"))
			sidebarType="sm";
		else if($body.hasClass("sidebar-md"))
			sidebarType="md";
		else
			sidebarType="large";

		$selectSidebarType.val(sidebarType);

		if($body.hasClass("sidebar-right")) {
			$selectSidebarPosition.val("right")
		} else {
			$selectSidebarPosition.val("left")
		}

		$chkSidebar.get(0).checked=App.isSidebarFixed;
		$chkLayout.get(0).checked=App.isFixedLayout;
		$chkHeader.get(0).checked=App.isHeaderFixed;

		$chkLayout.click(function() {
			App.isFixedLayout=this.checked;
			App.changeLayout();
		});

		$chkHeader.click(function() {
			App.isHeaderFixed=this.checked;
			App.changeLayout();
		});

		$chkSidebar.click(function() {
			App.isSidebarFixed=this.checked;
			App.changeLayout();
		});

		$selectSidebarType.change(function() {
			self.sidebarType=$(this).val();
			$("[data-sidebar-action='sidebar-toggle']").each(function() {
				var type=self.sidebarType;
				if(type=="large"||type=="") type="sm";
				$(this).attr("data-sidebar-type",type);
			});
			self.changeLayout();
		});

		$selectSidebarPosition.change(function() {
			self.sidebarPosition=this.value;
			self.changeLayout();
		});

		$(".theme-group",$themePanel).find("a").click(function() {
			var $this=$(this);
			$this.parent().siblings().removeClass("select");
			$this.parent().addClass("select");
			self.setTheme($this.attr("data-theme-name"));
		});
	}

	this.init=function() {
		self._IsIE8=!!navigator.userAgent.match(/MSIE 8.0/);
		self._IsIE9=!!navigator.userAgent.match(/MSIE 9.0/);
		self._IsIE10=!!navigator.userAgent.match(/MSIE 10.0/);

		var $body=$("body");

		if($body.hasClass("sidebar-right")) {
			this.sidebarPosition="right";
		} else {
			this.sidebarPosition="";
		}

		var $header=$(".header");
		if($body.hasClass("header-fixed")) {
			this.isHeaderFixed=true;
			$header.addClass("navbar-fixed-top");
		} else {
			$header.removeClass("navbar-fixed-top");
		}

		this.isSidebarFixed=$body.hasClass("sidebar-fixed");
		this.isFixedLayout=$body.hasClass("page-boxed");

		if($body.hasClass("sidebar-md")) {
			this.sidebarType="md";
		} else if($body.hasClass("sidebar-sm")) {
			this.sidebarType="sm";
		}
		// Layout,Sidebar
		self.applyLayoutClasses();
		self.themePanelSettings();
		self.handleSidebar();
		self.handleHorizontalMenu();

		// Apply sidebar toggle event
		$("body").addClass("sidebar-animation");
		$("[data-sidebar-action='sidebar-toggle']").on("click",function() {
			var type=$(this).attr("data-sidebar-type");
			if(type=="") type="sm";
			if(self.sidebarType==type) {
				self.sidebarType="";
			} else {
				self.sidebarType=type;
			}
			self.changeLayout();
		});
		// Small Devices Sidebar Toggle
		$("[data-sidebar-action='sidebar-sm-toggle']")
		.on('click',function(e) {
			e.preventDefault();
			e.stopPropagation();
			var $body=$("body");
			$body.toggleClass("sidebar-open");
			if($body.hasClass("sidebar-fixed")&&$body.hasClass("sidebar-open")) {
				$body.addClass("page-container-no-scroll");
			} else {
				$body.removeClass("page-container-no-scroll");
			}
		});

		$(".search-form-toggle").on('click',function(e) {
			$(".search-form",".header").toggleClass("show");
		});

		// Plugins
		self.jqValidationSetDefaults();
		self.handleFormValidation();
		self.handleToolTip();
		self.handlePopover();
		self.handlePanelTools();
		self.handleDropdownHoldClick();
		self.handleBootstrapSelect();
		self.handleSwitch();
		self.handleSlider();
		self.handleMetroCheck();
		self.handleSlimScroll();
		self.handleBackToTop();
	}
}
$(function() {
	App.init();
});
$(window).resize(function() {
	App.handleSidebar();
	App.responsive();
});