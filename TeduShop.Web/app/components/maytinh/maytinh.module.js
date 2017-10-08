/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="/admin/libs/angular-ui-router/release/angular-ui-router.js" />

(function () {
    angular.module("tedushop.maytinh", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('maytinh', {
                url: '/maytinh',
                parent: 'base',
                templateUrl: '/app/components/maytinh/maytinhListView.html',
                controller: 'maytinhListController'
            })
            .state('maytinh_add', {
                url: '/maytinh_add',
                parent: 'base',
                templateUrl: '/app/components/maytinh/maytinhAddView.html',
                controller: 'maytinhAddController'
            })
            .state('maytinh_edit', {
                url: '/maytinh_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/maytinh/maytinhEditView.html',
                controller: 'maytinhEditController'
            });
    }
})();