angular.module("MVCApp").directive('fileReader', ["MultiTaskService", function (MultiTaskService) {
    return {
        restrict: 'A',
        scope: {
            ngModel: '=',
            clearFile: '='
        },
        link: function (scope, element, attr) {

            element.bind('change', function () {
                var formData = new FormData();
                formData.append('file', element[0].files[0]);
                MultiTaskService.ImportTaskSheet(formData).then(function (res) {
                    if (res) {
                        if (res.data.MessageType == messageTypes.Success) {
                            scope.ngModel = res.data.Result;
                        } else if (res.data.MessageType == messageTypes.Error) {// Error      
                            toastr.error(res.data.Message, errorTitle);
                        }
                    }
                });
                if (scope.clearFile) {
                    $(element[0]).val("");
                }
            });

        }
    };
} ]);

