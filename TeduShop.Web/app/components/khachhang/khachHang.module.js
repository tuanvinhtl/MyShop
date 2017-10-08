/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="/admin/libs/angular-ui-router/release/angular-ui-router.js" />

(function () {
    angular.module("tedushop.khachhang", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('khachhang', {
                url: '/khachhang',
                parent: 'base',
                templateUrl: '/app/components/khachhang/khachHangListView.html',
                controller: 'khachHangListController'
            })
            .state('khachhang_add', {
                url: '/khachhang_add',
                parent: 'base',
                templateUrl: '/app/components/khachhang/khachHangAddView.html',
                controller: 'khachHangAddController'
            })
            .state('khachhang_edit', {
                url: '/khachhang_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/khachhang/khachHangEditView.html',
                controller: 'khachHangEditController'
            });
    }
})();