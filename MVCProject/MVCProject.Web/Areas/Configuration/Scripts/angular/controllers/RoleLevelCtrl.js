(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('RoleLevelCtrl', [
            '$scope', '$rootScope', 'RoleService', RoleLevelCtrl
        ]);

    // BEGIN RoleLevelCtrl
    function RoleLevelCtrl($scope, $rootScope, RoleService) {
        /* Initial Declaration */
        $scope.roleLeveles = [];

        //Get Role Level
        $scope.GetRoleLevel = function () {
            RoleService.GetRoleLevel().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {// Success                
                    $scope.roleLeveles = data.Result;
                } else {// Error
                    toastr.error(data.Message, errorTitle);
                }
            });
        }

        // Init
        $scope.Init = function () {
            $scope.GetRoleLevel();
        } ();

    }
    // END RoleLevelCtrl
})();