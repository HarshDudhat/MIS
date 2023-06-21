(function () {
    angular.module("DorfKetalMVCApp").controller("SelectAreaMapPopupCtrl", ["$scope", "$rootScope", "$filter", "$uibModalInstance", "ngTableParams", "SiteMasterService", "title", "AreaDetailScope", "File", "CommonFunctions",
    function ($scope, $rootScope, $filter, $uibModalInstance, ngTableParams, SiteMasterService, title, AreaDetailScope, File, CommonFunctions) {
        // Declare-Initialize Scope and Root Scope Variable   

        $scope.title = title;
        $scope.AreaValue = AreaDetailScope;
        $scope.File = File;
        $scope.selected = {};

        if ($scope.AreaValue.AreaX1 == null)
            $scope.AreaValue.AreaX1 = 0;
        if ($scope.AreaValue.AreaY1 == null)
            $scope.AreaValue.AreaY1 = 0;
        if ($scope.AreaValue.AreaX2 == null)
            $scope.AreaValue.AreaX2 = 0;
        if ($scope.AreaValue.AreaY2 == null)
            $scope.AreaValue.AreaY2 = 0;

        //Watch to Initialize Canvas elements 
        $scope.$watch('AreaValue', function () {
            ias = $('#photo').imgAreaSelect({
                handles: true, movable: true, instance: true, onSelectEnd: function (img, selection) {
                    $scope.selected.AreaX1 = selection.x1;
                    $scope.selected.AreaY1 = selection.y1;
                    $scope.selected.AreaX2 = selection.x2;
                    $scope.selected.AreaY2 = selection.y2;
                }
            });
            if ($scope.AreaValue.AreaX != 0 && $scope.AreaValue.AreaY != 0 && $scope.AreaValue.AreaX2 != 0 && $scope.AreaValue.AreaY2 != 0) {
                ias.setSelection($scope.AreaValue.AreaX1, $scope.AreaValue.AreaY1, $scope.AreaValue.AreaX2, $scope.AreaValue.AreaY2, true);
                ias.setOptions({ show: true });
                ias.update();
            }
        });


        $scope.cancelActionToPerform = function () {
            $scope.selected.AreaX1 = $scope.AreaValue.AreaX1;
            $scope.selected.AreaY1 = $scope.AreaValue.AreaY1;
            $scope.selected.AreaX2 = $scope.AreaValue.AreaX2;
            $scope.selected.AreaY2 = $scope.AreaValue.AreaY2;
            ias.cancelSelection();
            $uibModalInstance.close($scope.selected);
            //$uibModalInstance.dismiss('cancel');
        };

        $scope.OnOkClick = function () {
            console.log(ias.getOptions());
            $scope.selected.AreaX1 = $scope.selected.AreaX1;
            $scope.selected.AreaY1 = $scope.selected.AreaY1;
            $scope.selected.AreaX2 = $scope.selected.AreaX2;
            $scope.selected.AreaY2 = $scope.selected.AreaY2;
            ias.cancelSelection();
            $uibModalInstance.close($scope.selected);
        };

    } ]);

})();