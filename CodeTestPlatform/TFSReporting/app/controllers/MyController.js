app.controller('GetData', function ($scope, GetBuildNames)
{
    init();

    function init() {
        $scope.builds = GetBuildNames.getData();
    }    
}
);

//---------------------------------------------------------------------------------------------
