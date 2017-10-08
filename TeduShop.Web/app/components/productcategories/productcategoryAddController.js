(function (app) {
    app.controller('productcategoryAddController', productcategoryAddController);
    productcategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function productcategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategories = {
            CreatedDate: new Date(),
            Status:true
        }
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.productCategories.Alias = commonService.getSeoTitle($scope.productCategories.Name)
        }
        $scope.addProductCategories = addProductCategories;
        function addProductCategories() {
            apiService.post('api/productcategory/create', $scope.productCategories, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('productcategories_list');
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }
        function loadParentCategory() {
            apiService.get('api/productcategory/getallParent', null, function (result) {
                $scope.parentCategories = result.data;
            }, function (result) {
                if (result.data==null) {
                    notificationService.displayWarning('Không thể load danh mục cha');
                }
            })
        }
        loadParentCategory();
    }
})(angular.module('tedushop.productcategories'))