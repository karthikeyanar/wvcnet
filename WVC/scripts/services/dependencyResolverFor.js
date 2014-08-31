define([],function() {
	return function(dependencies) {
		var definition=
        {
        	resolver: ['$q','$rootScope',function($q,$rootScope) {
        		var deferred=$q.defer();

        		console.log("resolve",dependencies);
        		require(dependencies,function() {
        			console.log("resolve apply");
        			$rootScope.$apply(function() {
        				deferred.resolve();
        			});
        		});

        		return deferred.promise;
        	} ]
        }

		return definition;
	}
});