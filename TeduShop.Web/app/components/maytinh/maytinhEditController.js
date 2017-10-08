(function (app) {
    app.controller('maytinhEditController', maytinhEditController);
    maytinhEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function maytinhEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.maytinh = {

        }
        $scope.editMayTinh = editMayTinh;

        function editMayTinh() {
            apiService.put('api/maytinh/update', $scope.maytinh, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
                $state.go('maytinh');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadMayTinh() {
            apiService.get('api/maytinh/getbyid/' + $stateParams.id, null, function (result) {
                $scope.maytinh = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        loadMayTinh();
    }
})(angular.module('tedushop.maytinh'))