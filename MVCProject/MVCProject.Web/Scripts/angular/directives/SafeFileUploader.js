(function () {
    'use strict';

    //Controller Start
    angular.module("DorfKetalMVCApp").controller('safeFileUploadCtrl', [
            '$scope', '$rootScope', '$filter', '$document', '$timeout', 'CommonService', 'ngTableParams', 'FileUploader', SafeFileUploadCtrl
        ]);

    function SafeFileUploadCtrl($scope, $rootScope, $filter, $document, $timeout, CommonService, ngTableParams, FileUploader) {

        /*Multi File Up-Loader Configuration */
        //File Up-Loader:- Initialize 
        var uploader = $scope.uploader = new FileUploader({
            url: $rootScope.apiURL + '/Upload/UploadFile?databaseName=' + $rootScope.userContext.CompanyDB
        });
        $scope.scrollHeight = 71;
        $scope.SetScrollHeight = function () {
            if (uploader.queue.length > 0) {
                if (uploader.queue.length > 5) {
                    $scope.scrollHeight = 36 + 40 * 5;
                } else {
                    $scope.scrollHeight = 36 + 40 * uploader.queue.length;
                }
            } else {
                $scope.scrollHeight = 71;
            }
        }

        $scope.placeholder = $scope.placeholder || ".JPEG, .PNG, .BMP, .XLS, .XLSX, .DOC, .DOCX, .RTF and .PDF";
        $scope.inputContainerClass = $scope.inputContainerClass || "col-md-6";
        $scope.gridContainerClass = $scope.gridContainerClass || "col-md-12";
        if (angular.isUndefined($scope.isViewOnly)) {
            $scope.isViewOnly = false;
        }

        if (angular.isUndefined($scope.isForThumbnail)) {
            $scope.isForThumbnail = false;
        }

        $scope.$watch('ngModel', function (newValue, oldValue) {
            $scope.LoadUplodedFile(newValue);
        }, true);

        $scope.$on('resetDocuments', function (event, args) {
            angular.copy([], $scope.TempngModel);
            uploader.progress = 0;
            $scope.LoadUplodedFile($scope.ngModel);
        });

        $scope.$on('clearDocuments', function (event, args) {
            angular.copy([], $scope.TempngModel);
            angular.copy([], uploader.queue);
            uploader.progress = 0;
            $scope.SetScrollHeight();
        });

        $scope.IsAllFileUploaded = true;
        $scope.Uploadlenght = $scope.maxLimit ? $scope.maxLimit : 10;
        $scope.AttachmentTempPath = $rootScope.apiAttachmentsURL + "/Temp/";
        //$scope.BaseUrlServer = $rootScope.elmahURL + "/";

        //load uploaded files
        $scope.LoadUplodedFile = function (TempngModel) {
            uploader.queue = [];
            angular.forEach(TempngModel, function (Doc) {
                var Queue = {
                    isCancel: false,
                    isError: false,
                    isReady: false,
                    isSuccess: true,
                    isUploaded: true,
                    isUploading: false,
                    isDeleted: false,
                    isNotATemp: true,
                    AttachmentId: Doc.AttachmentId,
                    ModuleId: Doc.ModuleId,
                    ReferenceId: Doc.ReferenceId,
                    method: "POST",
                    progress: 100,
                    removeAfterUpload: false,
                    file: {
                        "name": Doc.OriginalFileName,
                        "size": Doc.Size,
                        "AttachmentPath": Doc.AttachmentPath,
                        "SName": Doc.FileName
                    }
                };
                uploader.queue.push(Queue);
            });
            $scope.SetScrollHeight();
        };

        $scope.LoadUplodedFile($scope.TempngModel)


        //File Up-Loader:-Filters
        uploader.filters.push({
            name: 'extensionFilter',
            fn: function (item, options) {
                var filename = item.name;
                var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                if (extension == "pdf" || extension == "doc" || extension == "docx" || extension == "xls" || extension == "xlsx" ||
                            extension == "rtf" || extension == "jpg" || extension == "bmp" || extension == "jpeg" || extension == "png")
                    return true;
                else {
                    toastr.warning(msgInvalidFileFormat, warningTitle);
                    return false;
                }
            }
        });
        uploader.filters.push({
            name: 'sizeFilter',
            fn: function (item, options) {
                var fileSize = item.size;
                fileSize = parseInt(fileSize) / (1024 * 1024);
                if (fileSize <= 5)
                    return true;
                else {
                    toastr.warning(msgFileSizeExceeded, warningTitle);
                    return false;
                }
            }
        });
        uploader.filters.push({
            name: 'itemResetFilter',
            fn: function (item, options) {
                if (this.queue.length < $scope.Uploadlenght)
                    return true;
                else {
                    toastr.warning(msgFileLimitExceeded, warningTitle);
                    return false;
                }
            }
        });


        //File Up-Loader:-Callback functions 
        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            var data = response;
            if (data.MessageType == messageTypes.Success) {// Success
                $scope.TempngModel.push(data.Result);
                $scope.SetScrollHeight();
            } else if (data.MessageType == messageTypes.Error) {// Error
                toastr.error(data.Message, errorTitle);
            }
            $scope.SetScrollHeight();
        };
        uploader.onErrorItem = function (fileItem, response, status, headers) {
            toastr.error(response, errorTitle);
        };
        uploader.onCompleteAll = function () {
            $scope.SetScrollHeight();
        };
        uploader.onAfterAddingFile = function (fileItem) {
            var len = uploader.queue.length;
            if (len > 1) {
                var isFileExist = false;
                var TempList = {};
                TempList = $.extend({}, uploader.queue);
                delete TempList[len - 1];
                angular.forEach(TempList, function (Doc) {
                    if (Doc.file.name == fileItem.file.name)
                        isFileExist = true;
                });
                if (isFileExist) {
                    uploader.queue.splice(-1, 1);
                    toastr.warning(msgFileAlreadyExist, warningTitle);
                }
            }
            $scope.SetScrollHeight();
        };

        //File Up-Loader:-Delete Document from UI Queue 
        $scope.deleteDocumentFromQueue = function (item) {
            if (!item.isDeleted) {
                item.isDeleted = true;
                var data = {
                    OriginalFileName: item.file.name,
                    FileName: item.file.SName,
                    IsDeleted: true,
                    AttachmentId: item.AttachmentId,
                    ModuleId: item.ModuleId,
                    ReferenceId: item.ReferenceId
                }
                //$scope.TempngModel.push(data);

                //remove file from array.
                var deletedFile = $filter("filter")($scope.ngModel, { FileName: item.file.SName }, true);
                if (angular.isDefined(deletedFile) && deletedFile.length == 1) {
                    var index = $scope.ngModel.indexOf(deletedFile[0]);
                    $scope.ngModel.splice(index, 1);
                }

                $scope.isDeleted = false;
            }
            var index = uploader.queue.indexOf(item);
            if (index != -1) {
                uploader.queue.splice(index, 1);
            }
            if (uploader.queue.length < 1) {
                $scope.IsAllFileUploaded = true;
            }
            
            var Doc = $filter("filter")($scope.TempngModel, { OriginalFileName: item.file.name }, true);
            var index = $scope.TempngModel.indexOf(Doc[0]);
            if (index != -1) {
                CommonService.DeleteDocumentFromTempLocation(Doc[0].FileName).then(function (res) { });
                $scope.TempngModel.splice(index, 1);
            }
            
            $scope.SetScrollHeight();
        };

        //File Up-Loader:-Check Pending Documents
        $scope.CheckPendingDocument = function (value) {
            $scope.IsAllFileUploaded = value;
        }

        $scope.UploadDocument = function (item) {
            if (!(item.isReady || item.isUploading || item.isSuccess)) {
                item.upload();
            }
        }

        //File Up-loader:- Get Encrypted Name by Original name
        $scope.getEncryptedFileName = function (Name) {
            var Doc = $filter("filter")($scope.TempngModel, { OriginalFileName: Name }, true);
            var index = $scope.TempngModel.indexOf(Doc[0]);
            if (index != -1) {
                return Doc[0].FileName;
            }
            else
                return "";
        }

        $scope.viewImage = function (item) {
            if (item.AttachmentId > 0) {
                window.open(item.file.AttachmentPath, '_blank');
            }
            else {
                window.open($scope.AttachmentTempPath + $scope.getEncryptedFileName(item.file.name), '_blank');
            }
        }
    };
    //Controller End

    //Directive Start
    angular.module("DorfKetalMVCApp").directive('safeFileUpload', safeFileUploadDir);
    function safeFileUploadDir() {
        return {
            restrict: 'E',
            scope: {
                tabIndex: '=',
                options: '=',
                ngModel: '=modelData',
                TempngModel: '=changedData',
                isViewOnly: '=?isViewOnly',
                IsAllFileUploaded: '=isAllFileUploaded',
                inputContainerClass: '@',
                gridContainerClass: '@',
                placeholder: '@',
                maxLimit: '=maxLimit',
                isForThumbnail: '=?isForThumbnail'
            },
            controller: 'safeFileUploadCtrl',
            templateUrl: '/Template/_FileUploder'
        };
    };
    //Directive End

})();
