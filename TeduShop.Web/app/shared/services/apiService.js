﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'authenticationService'];

    function apiService($http, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del:del
        }
        function del(url, params, success, failure) {
            authenticationService.setHeader()
            $http.delete(url, params).then(function (result) {
                success(result);
            }, function (result) {
                failure(failure);
            })
        }

        function put(url, data, success, failure) {
            authenticationService.setHeader()
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (result) {
                failure(failure);
            })
        }

        function post(url, data, success, failure) {
            authenticationService.setHeader()
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (result) {
                failure(failure);
            })
        }
        function get(url, params, success, failure) {
            authenticationService.setHeader()
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }
    }
})(angular.module('tedushop.common'));