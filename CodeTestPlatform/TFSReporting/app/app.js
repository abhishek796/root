var app = angular.module("TFSReportApp", ['ngRoute']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/builds', {
        templateUrl: 'app/partials/buildData.html',
        controller: 'GetData'
    }).otherwise({
        templateUrl: 'app/partials/buildData.html',
        controller: 'GetData'
    });
}]);

app.service('GetBuildNames', function () {
    this.getData = function () {
        var data = [{ Name: 'VLH_CI' }, { Name: 'CDS_CI' }];
        return data;
    }
});