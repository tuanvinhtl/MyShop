(function (app) {
    app.controller('productListController', productListController);
    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.product = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getProduct = getProduct;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.selected;
        $scope.exportPdf = exportPdf;
        $scope.exportExcel = exportExcel;

        function exportPdf(productId) {
            var config = {
                params: {
                    id: productId
                }
            }
            apiService.get('api/product/ExportPdf', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }


        function exportExcel() {
            var config = {
                params: {
                    filter: $scope.keyWord
                }
            }
            apiService.get('/api/product/ExportXls', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });

        }


        function search() {
            getProduct();
        }

        $scope.deleteMutile = deleteMutile;

        $scope.selectAll = selectAll;

        function deleteMutile() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    listproductId: JSON.stringify(listId)
                }
            }
            apiService.del('/api/product/deletemutile', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                search()
            }, function (error) {
                notificationService.displayWarning('Xóa không thành công')
            });
        };

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.product, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.product, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("product", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);


        $scope.deleteProduct = deleteProduct;
        function deleteProduct(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/product/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data.Name);
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getProduct(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.product = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load product failed.');

            });
        }

        $scope.getProduct();

    }
})(angular.module('tedushop.products'));