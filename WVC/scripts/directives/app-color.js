define(['app'],function(app) {
	app.directive('appColor',
    [
        function() {
        	return function(scope,$element,attrs) {
        		console.log('appColor',$element);
        		$element.css({ 'color': attrs.appColor });
        	}
        }
    ])
});