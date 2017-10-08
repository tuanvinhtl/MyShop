(function (app) {
    app.controller('khachHangListController', khachHangListController);
    khachHangListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function khachHangListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.khachhang = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getKhachHang = getKhachHang;
        $scope.keyWord = '';
        $scope.search = search;

        function search() {
            getKhachHang();
        }

        $scope.deleteKhachHang = deleteKhachHang;
        function deleteKhachHang(id) {
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

        function getKhachHang(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/khachhang/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.khachhang = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load product failed.');

            });
        }

        $scope.getKhachHang();

    }
})(angular.module('tedushop.khachhang'));