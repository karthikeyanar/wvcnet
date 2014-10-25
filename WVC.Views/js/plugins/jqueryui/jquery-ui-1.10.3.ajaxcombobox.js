// Highlight the characters
$.extend($.ui.autocomplete.prototype,{
	_renderItem: function(ul,item) {
		var term=this.element.val(),
            html=item.label.replace(term,"<b>$&</b>");
		return $("<li></li>")
            .data("item.autocomplete",item)
            .append($("<a></a>").html(html))
            .appendTo(ul);
	}
});
$.widget("custom.ajaxcombobox",{

	options: {
		appendTo: "body",
		autoFocus: null,
		delay: 300,
		minLength: 0,
		position: {
			my: "left top",
			at: "left bottom",
			collision: "none"
		},
		source: null,
		// callbacks
		change: null,
		close: null,
		focus: null,
		open: null,
		response: null,
		search: null,
		select: null
	},
	_create: function() {
		this.wrapper=$("<div>")
					.addClass("input-group ui-ajax-combo")
					.insertAfter(this.element);

		this._createAutocomplete();
		this._createShowAllButton();
	},

	_createAutocomplete: function() {

		this.input=$(this.element);

		this.input
		.appendTo(this.wrapper)
		.autocomplete(this.options);

	},

	_createShowAllButton: function() {
		var input=this.input,
					wasOpen=false;

		var wrapper = this.wrapper;

		var $span=$("<span class='input-group-btn'></span>");
		$span.appendTo(this.wrapper)

		var $spanBefore=$("<span class='input-group-btn'><button type='button' class='btn btn-default ui-aj-combo-btn input-sm'><i class='fa fa-search'></i></button></span>");
		$spanBefore.prependTo(this.wrapper)

		var $spanRemove=$("<span class='remove-value'><a href='javascript:;'><i class='fa fa-remove'></i></a></span>");
		$spanRemove.appendTo(this.wrapper);

		$spanRemove.click(function() {
			var autocomplete=$(input).data("ui-autocomplete");
			autocomplete._trigger('select','autocompleteselect',{ item: { id: "",value: "",label: "" } });
			$(input).val("");
			$(wrapper).removeClass("ui-autocomplete-selected");
		});

		var $btn=$("<a class='btn btn-default input-sm'></a>");

		$btn
		.html("<i class='fa fa-angle-down'></i>")
		.attr("tabIndex",-1)
		.attr("title","Show All Items")
		.tooltip()
		.appendTo($span)
		.button({
			icons: {
				primary: "ui-icon-triangle-1-s"
			},
			text: false
		})
		.removeClass("ui-corner-all")
		.addClass("custom-combobox-toggle ui-corner-right")
		.mousedown(function() {
			wasOpen=input.autocomplete("widget").is(":visible");
		})
		.click(function() {
			input.focus();

			// Close if already visible
			if(wasOpen) {
				return;
			}

			// Pass empty string as value to search for, displaying all results
			input.autocomplete("search","");
		});
	}
});