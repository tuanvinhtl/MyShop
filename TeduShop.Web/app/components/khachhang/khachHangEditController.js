(function (app) {
    app.controller('khachHangEditController', khachHangEditController);
    khachHangEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function khachHangEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.khachhang = {
           
        }
        $scope.editKhachHang = editKhachHang;

        function editKhachHang() {
            apiService.put('api/khachhang/update', $scope.khachhang, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
                $state.go('khachhang');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadKhachHang() {
            apiService.get('api/khachhang/getbyid/' + $stateParams.id, null, function (result) {
                $scope.khachhang = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        loadKhachHang();
    }
})(angular.module('tedushop.khachhang'))