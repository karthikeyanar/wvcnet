define("LoginController",['ko','helper','finch'],function(ko,helper,finch) {
	return function() {
		var self=this;
		this.template="Login";
		this.error_message=ko.observable("");
		this.user_name=ko.observable("");
		this.user_name_focus=ko.observable(false);
		this.password_focus=ko.observable(false);
		this.signIn=function(formElement) {
			self.error_message("");
			var $frm=$(formElement);
			if($frm.valid()) {
				var $btn=$("#submit",$frm);
				$btn.button('loading');
				var url=helper.apiUrl("/Token");
				var data=[];
				var email=$("#email",$frm).val();
				var rememberMe=$("#rememberme",$frm)[0].checked;
				data.push({ "name": "grant_type","value": "password" });
				data.push({ "name": "username","value": email });
				data.push({ "name": "password","value": $("#password",$frm).val() });
				helper.setLS("rememberme",rememberMe);
				helper.setLS("user_name",email);
				$.ajax({
					"url": url,
					"cache": false,
					"type": "POST",
					"dataType": "JSON",
					"data": data
				}).done(function(json) {
					helper.appModel.setMy(json,rememberMe);
					finch.navigate("/");
				}).fail(function(jqxhr) {
					self.error_message(jqxhr.responseJSON.error_description);
					//console.log("fail=",jqxhr);
					//alert(jqxhr.responseJSON.error_description);
				}).always(function(jqxhr) {
					//console.log("always=",jqxhr);
					$btn.button('reset');
				});
			}
		}
	}
}
);