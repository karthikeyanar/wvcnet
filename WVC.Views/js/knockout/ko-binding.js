define("ko-binding",['knockout','helper'],function(ko,helper) {
	ko.bindingHandlers.formValidate={
		init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			//var dataFor=ko.contextFor(element);
			//window.log("formValidate=",element);
			$(element).validate({
				ignore: "input[type='text']:hidden"
			});
			//dataFor.$root.sortOfflineUsers();
		},
		update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
		}
	}
	ko.bindingHandlers.initMenuBar={
		init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			setTimeout(function() {
				helper.resizeContentHeight();
			},500);
		},
		update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
		}
	}
	ko.bindingHandlers.initPage={
		init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			window.console.log("initPage=",element);
			helper.init();
		},
		update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
		}
	}
	ko.bindingHandlers.ajaxComboBox={
		init: function(element,valueAccessor,allBindingsAccessor,viewModel) {
			var obj=valueAccessor(),allBindings=allBindingsAccessor();
			obj.position={
				my: "left top",
				at: "left bottom",
				collision: "flip"
			}
			//window.console.log(obj)
			$(element).ajaxcombobox(obj);
		},
		update: function(element,valueAccessor,allBindingsAccessor,viewModel) {
			var obj=valueAccessor(),allBindings=allBindingsAccessor();
			var $uiajaxCombo=$(element).parents(".ui-ajax-combo:first");
			if(obj.inputValue!=""&&obj.inputValue!=null) {
				$uiajaxCombo.addClass("ui-autocomplete-selected");
			} else {
				$uiajaxCombo.removeClass("ui-autocomplete-selected");
			}
			//window.console.log("update ajaxComboBox inputvalue=",obj.inputValue)
		}
	}
	ko.bindingHandlers.autoComplete={
		init: function(element,valueAccessor,allBindingsAccessor,viewModel) {
			var obj=valueAccessor(),allBindings=allBindingsAccessor();
			obj.position={
				my: "left top",
				at: "left bottom",
				collision: "flip"
			}
			$(element).autoCompleteEx(obj);
		},
		update: function(element,valueAccessor,allBindingsAccessor,viewModel) {
		}
	}
	ko.bindingHandlers.bsPagination={
		init: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
			var obj=valueAccessor(),allBindings=allBindingsAccessor();
			//console.log("bsPagination init=",obj);
		},
		update: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
			var obj=valueAccessor(),allBindings=allBindingsAccessor();
			//console.log("bsPagination update obj render=",obj.onRender);
			//console.log("bsPagination update=",obj);
			$(element).twbsPagination({
				total: obj.total_rows,
				rowsPerPage: obj.rows_per_page,
				startPage: obj.page_index,
				onRender: function(status) {
					if(obj.onRender)
						obj.onRender(status);
				},
				onPageClick: function(page) {
					if(obj.onPageClick)
						obj.onPageClick(page);
				}
			});
		}
	}

	ko.bindingHandlers.sortingTable={
		init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			var $element=$(element);
			var $thead=$("thead",element);
			var options=ko.unwrap(valueAccessor());
			$thead.find("th")
			.removeClass("sort").removeClass("asc").removeClass("desc")
			.each(function() {
				var $this=$(this);
				if($this.attr("sortname")==""||$this.attr("sortname")==undefined) return;
				var $div=$("<div></div>");
				$div.html($this.html());
				$this.empty().append($div);
				if($element.attr("sortname")==$this.attr("sortname")) {
					$this.addClass("sort").addClass($element.attr("sortorder"));
				}
				$this.click(function() {
					var sortorder=$element.attr("sortorder");
					if(sortorder=="asc"||sortorder=="") { sortorder="desc"; } else { sortorder="asc"; }
					$element.attr("sortorder",sortorder);
					$this.removeClass("sort").removeClass("asc").removeClass("desc").siblings().removeClass("sort").removeClass("asc").removeClass("desc");
					$this.addClass("sort").addClass(sortorder);
					if(options.onSorting) {
						options.onSorting($this.attr("sortname"),$element.attr("sortorder"));
					}
				});
			});
		},
		update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
		}
	}
	ko.bindingHandlers.popoverConfirm={
		init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			var options=ko.unwrap(valueAccessor());
			$(element).popoverconfirm(options).tooltip();
		},
		update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
		}
	}
	ko.bindingHandlers.dropZone={
		init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			//window.console.log('dropZone init',element)
			var options=ko.unwrap(valueAccessor());
			$(element).dropzone(options);
		},
		update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			//window.console.log('dropZone update',element)
		}
	}
	ko.bindingHandlers.datePicker={
		init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
			var options=ko.unwrap(valueAccessor());
			var $element=$(element);
			if(options) {
				$element.datepicker(options);
				if(options.changeDate) {
					$element.on('changeDate',function(e) {
						options.changeDate(e);
					});
				}
			} else {
				$element.datepicker();
			}
		},
		update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
		}
	}
	ko.bindingHandlers.select2Ajax={
		init: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
			var obj=valueAccessor(),allBindings=allBindingsAccessor();
			window.console.log("select2 init=",obj.placeholder)
			$(element).select2({
				placeholder: obj.placeholder,
				minimumInputLength: (obj.minimumInputLength?obj.minimumInputLength:0),
				multiple: (obj.multiple?obj.multiple:false),
				triggerChange: true,
				allowClear: true,
				ajax: {
					url: obj.url,
					cache: false,
					data: function(term,page) {
						return {
							term: term,
							pageSize: 1000
						};
					},
					results: function(data,page) {
						var returnData=[];
						if(obj.ajaxResult)
							returnData=obj.ajaxResult(data,page);
						//window.console.log("select2 results=",returnData);
						return returnData;
					}
				},
				initSelection: function(element,callback) {
					window.console.log("select2 initSelection=",obj.selectValue);
					var arr=[];
					arr.push({ "name": "term","value": "" });
					$.ajax({
						"url": obj.url,
						"cache": false,
						"type": "GET",
						"data": arr
					}).done(function(data) {
						if(obj.initSelection) {
							obj.initSelection(element,callback,data);
						}
						//window.console.log("select2 initSelection=",returnData)
						//callback({ id: 1,text: 'initSelection test' });
					}).always(function() { });
				}
				//,
				//formatResult: movieFormatResult, // omitted for brevity, see the source of this page
				//formatSelection: movieFormatSelection, // omitted for brevity, see the source of this page
				//dropdownCssClass: "bigdrop", // apply css that makes the dropdown taller
				//escapeMarkup: function(m) { return m; } // we do not want to escape markup since we are displaying html in results
			});
		},
		update: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
			//var obj=valueAccessor(),allBindings=allBindingsAccessor();
			//window.console.log("select2 update=",obj.selectValue);
			//$(element).select2('val',obj.selectValue);
		}
	}
});