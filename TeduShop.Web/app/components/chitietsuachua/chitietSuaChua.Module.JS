/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="/admin/libs/angular-ui-router/release/angular-ui-router.js" />

(function () {
    angular.module("tedushop.chitietsuachua", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('chitietsuachua', {
                url: '/chitietsuachua',
                parent: 'base',
                templateUrl: '/app/components/chitietsuachua/chitietSuaChuaListView.html',
                controller: 'chitietSuaChuaListController'
            })
            .state('chitietsuachua_add', {
                url: '/chitietsuachua_add',
                parent: 'base',
                templateUrl: '/app/components/chitietsuachua/chitietSuaChuaAddView.html',
                controller: 'chitietSuaChuaAddController'
            })
            .state('chitietsuachua_edit', {
                url: '/chitietsuachua_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/chitietsuachua/chitietSuaChuaEditView.html',
                controller: 'chitietSuaChuaEditController'
            });
    }
})();