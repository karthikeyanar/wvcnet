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
            $(element).ajaxcombobox(obj);
            var checkSelect=function(element) {
                var $uiAjaxCombo=$(element).parents(".ui-ajax-combo:first");
                if($.trim(element.value)=="")
                    $uiAjaxCombo.removeClass("ui-autocomplete-selected")
                else
                    $uiAjaxCombo.addClass("ui-autocomplete-selected")
            }
            $(element).on("autocompletechange",function(event,ui) {
                checkSelect(element);
            });
            $(element).on("autocompleteselect",function(event,ui) {
                checkSelect(element);
            });

        },
        update: function(element,valueAccessor,allBindingsAccessor,viewModel) {
            var obj=valueAccessor(),allBindings=allBindingsAccessor();
            var $uiAjaxCombo=$(element).parents(".ui-ajax-combo:first");
            if(obj.inputValue==undefined)
                obj.inputValue="";
            if($.trim(obj.inputValue)=="")
                $uiAjaxCombo.removeClass("ui-autocomplete-selected")
            else
                $uiAjaxCombo.addClass("ui-autocomplete-selected")
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

    ko.bindingHandlers.timePicker={
        init: function(element,valueAccessor,allBindingsAccessor,viewModel) {
            var obj=valueAccessor(),allBindings=allBindingsAccessor();
            $(element).timepicker(obj);
            $(element).timepicker().on('changeTime.timepicker',function(e) {
                if(obj.onChange)
                    obj.onChange(e);
            });
        },
        update: function(element,valueAccessor,allBindingsAccessor,viewModel) {
        }
    }
    ko.bindingHandlers.bsPagination={
        init: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
            var obj=valueAccessor(),allBindings=allBindingsAccessor();
        },
        update: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
            var obj=valueAccessor(),allBindings=allBindingsAccessor();
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
            var options=ko.unwrap(valueAccessor());
            helper.sortingTable(element,options);
        },
        update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
        }
    }
    ko.bindingHandlers.popoverConfirm={
        init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
            var options=ko.unwrap(valueAccessor());
            $(element).confirmation(options).tooltip();
        },
        update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
        }
    }
    ko.bindingHandlers.dropZone={
        init: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
            var options=ko.unwrap(valueAccessor());
            $(element).dropzone(options);
        },
        update: function(element,valueAccessor,allBindings,viewModel,bindingContext) {
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
    ko.bindingHandlers.select2={
        init: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
            var obj=valueAccessor(),allBindings=allBindingsAccessor();
            $(element).select2(obj);
        },
        update: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
        }
    }
    ko.bindingHandlers.select2Ajax={
        init: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
            var obj=valueAccessor(),allBindings=allBindingsAccessor();
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
                        return returnData;
                    }
                },
                initSelection: function(element,callback) {
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
                    }).always(function() { });
                }
            });
        },
        update: function(element,valueAccessor,allBindingsAccessor,viewModel,bindingContext) {
        }
    }
});