(function (app) {
    app.controller('chitietSuaChuaAddController', chitietSuaChuaAddController);
    chitietSuaChuaAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function chitietSuaChuaAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.chitietsuachua = {
            NgaySuaChua:new Date
        }
        $scope.addChiTietSuaChua = addChiTietSuaChua;
        function addChiTietSuaChua() {
            apiService.post('api/chitietsuachua/createMutile', $scope.chitietsuachua, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('chitietsuachua');
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }

    }
})(angular.module('tedushop.chitietsuachua'))