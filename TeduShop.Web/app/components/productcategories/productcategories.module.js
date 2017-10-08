(function () {
    angular.module("tedushop.productcategories", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('productcategories_list', {
                url: '/productcategories_list',
                parent: 'base',
                templateUrl: '/app/components/productcategories/productcategoryListView.html',
                controller: 'productcategoryListController'
            })
            .state('productcategory_add', {
                url: '/productcategory_add',
                parent: 'base',
                templateUrl: '/app/components/productcategories/productcategoryAddView.html',
                controller: 'productcategoryAddController'
            })
            .state('productcategory_edit', {
                url: '/productcategory_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/productcategories/productcategoryEditView.html',
                controller: 'productcategoryEditController'
            });
    }
})();