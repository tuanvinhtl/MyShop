(function (app) {
    app.controller('chitietSuaChuaListController', chitietSuaChuaListController);
    chitietSuaChuaListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function chitietSuaChuaListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.chitietsuachua = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getChiTietSuaChua = getChiTietSuaChua;

        $scope.deleteChiTietSuaChua = deleteChiTietSuaChua;
        function deleteChiTietSuaChua(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/khachhang/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data.Name);
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getChiTietSuaChua(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/chitietsuachua/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.chitietsuachua = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load product failed.');

            });
        }

        $scope.getChiTietSuaChua();

    }
})(angular.module('tedushop.chitietsuachua'));