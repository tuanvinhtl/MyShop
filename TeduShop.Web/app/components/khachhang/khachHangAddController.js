(function (app) {
    app.controller('khachHangAddController', khachHangAddController);
    khachHangAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function khachHangAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.khachhang = {
            
        }
        $scope.addKhachHang = addKhachHang;
        function addKhachHang() {
            apiService.post('api/khachhang/create', $scope.khachhang, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('khachhang');
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }

    }
})(angular.module('tedushop.khachhang'))