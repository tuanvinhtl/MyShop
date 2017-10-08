(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.products = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.addProduct = addProduct;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle() {
            $scope.products.Alias = commonService.getSeoTitle($scope.products.Name)
        }
        
        function addProduct() {
            $scope.products.MoreImages = JSON.stringify($scope.MoreImages);
            apiService.post('api/product/create', $scope.products, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('products');
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
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

        $scope.MoreImages=[]
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
    }
})(angular.module('tedushop.productcategories'))