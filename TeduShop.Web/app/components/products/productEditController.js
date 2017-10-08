(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.products = {
            UpdateDate: new Date(),
        }
        $scope.UpdateProduct = UpdateProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.MoreImages = [];

        function getSeoTitle() {
            $scope.products.Alias = commonService.getSeoTitle($scope.products.Name)
        }

        function UpdateProduct() {
            $scope.products.MoreImages = JSON.stringify($scope.MoreImages);
            apiService.put('api/product/update', $scope.products, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
                $state.go('products');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadProduct() {
            apiService.get('api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.products = result.data;
                $scope.MoreImages =JSON.parse($scope.products.MoreImages);
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        function loadParentCategory() {
            apiService.get('api/productcategory/getallParent', null, function (result) {
                $scope.parentCategories = result.data;
            }, function (result) {
                if (result.data == null) {
                    notificationService.displayWarning('Không thể load danh mục cha');
                }
            })
        }

        $scope.ChooseImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.products.Images = fileUrl;
                })
               
            }
            finder.popup();
        }

       
        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.MoreImages.push(fileUrl);
                })

            }
            finder.popup();
        }

        loadParentCategory();
        loadProduct();
    }
})(angular.module('tedushop.productcategories'))