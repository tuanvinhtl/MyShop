(function (app) {
    app.controller('maytinhAddController', maytinhAddController);
    maytinhAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function maytinhAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.maytinh = {
            CategoryPC:false
        }
        $scope.addMayTinh = addMayTinh;
        function addMayTinh() {
            apiService.post('api/maytinh/create', $scope.maytinh, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('maytinh');
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }

    }
})(angular.module('tedushop.maytinh'))