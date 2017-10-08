(function (app) {
    app.controller('maytinhListController', maytinhListController);
    maytinhListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function maytinhListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.maytinh = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getMayTinh = getMayTinh;
        $scope.keyWord = '';
        $scope.search = search;

        function search() {
            getMayTinh();
        }

        $scope.deleteMayTinh = deleteMayTinh;
        function deleteMayTinh(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/maytinh/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data.Name);
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getMayTinh(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('api/maytinh/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.maytinh = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load product failed.');

            });
        }

        $scope.getMayTinh();

    }
})(angular.module('tedushop.maytinh'));