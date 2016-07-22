app.directive('phoneBook', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/partials/phonebook.html'
    };
})
    .directive('search', function () {
        return {
            restrict: 'E',
            templateUrl: 'app/partials/Filters.html'
        };
    });