define(['app'],function(app) {
	app.controller('DivisionController',['$scope','$http',function($scope,$http) {
		$scope.pageIndex=1;
		$scope.pagingStatus='';
		$scope.total=0;
		$scope.rowsPerPage=5;
		$scope.rows=[];
		$scope.$watch('pageIndex',function(newValue,oldValue) {
			console.log('new value is '+newValue+' old value is'+oldValue);
			if(newValue!=oldValue)
				$scope.getRows();
		});
		$scope.getRows=function() {
			$http.get('/Home/GetDivisions').success(function(data) {
				$scope.total=data.total;
				$scope.rows=data.rows;
			});
		}
		$scope.getRows();
	} ]);
});