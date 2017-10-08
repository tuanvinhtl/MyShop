(function (app) {
    app.controller('applicationUserEditController', applicationUserEditController);
    applicationUserEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function applicationUserEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.productCategories = {
            UpdateDate: new Date(),
        }
        $scope.UpdateProductCategories = UpdateProductCategories;
        $scope.getSeoTitle = getSeoTitle;
        function getSeoTitle() {
            $scope.productCategories.Alias = commonService.getSeoTitle($scope.productCategories.Name)
        }

        function UpdateProductCategories() {
            apiService.put('api/productcategory/update', $scope.productCategories, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
                $state.go('productcategories_list');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadProductCategory() {
            apiService.get('api/productcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategories=result.data;
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
        loadParentCategory();
        loadProductCategory();
    }
})(angular.module('tedushop.productcategories'))