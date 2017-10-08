(function (app) {
    app.filter('statusFilter', statusFilter);
    function statusFilter() {
        return function (input) {
            if (input==true) {
                return 'Kích Hoạt';
            }
            else {
                return 'Khóa';
            }
        }
    }
})(angular.module('tedushop.common'));