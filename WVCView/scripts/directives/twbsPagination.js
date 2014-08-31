define(['app'],function(app) {
	app.directive('twbsPagination',
    [
        function() {
        	return function($scope,$element,attrs) {
        		//console.log("twbsPagination",$scope.total,$scope.rowsPerPage,$scope.pageIndex,$element);

        		$scope.$watch('total',function(newValue,oldValue) {
        			//console.log('new value is '+newValue);
        			update();
        		});

        		update();

        		function update() {
        			//console.log("twbsPagination update",$scope.total,$scope.rowsPerPage,$scope.pageIndex,$element);
        			$($element).twbsPagination({
        				total: $scope.total,
        				rowsPerPage: $scope.rowsPerPage,
        				startPage: $scope.pageIndex,
        				onPageClick: function(page) {
        					$scope.$apply(function() {
        						$scope.pageIndex=page;
        					});
        				}
        			});
        		}
        	}
        }
    ])
});