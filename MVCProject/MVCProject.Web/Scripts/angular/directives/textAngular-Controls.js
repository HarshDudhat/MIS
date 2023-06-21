angular.module("DorfKetalMVCApp").config(["$provide", function ($provide) {
    $provide.decorator('taOptions', ['taRegisterTool', '$delegate', '$uibModal', '$document', function (taRegisterTool, taOptions, $uibModal, $document) {

        // Upload image
        taRegisterTool('uploadLocalImage', {
            iconclass: "fa fa-image",
            action: function (deferred) {
                var textAngular = this;
                var savedSelection = rangy.saveSelection();
                $uibModal.open({
                    animation: true,
                    template:
                    '<div class="modal-header">'
                    + '<button type="button" class="close" ng-click="cancelActionToPerform()">×</button>'
                    + '<h4 class="modal-title">Choose file</h4>'
                    + '</div>'
                    + '<div class="modal-body">'
                    + '<div class="custom_file_upload">'
                    + '     <input type="text" class="file" placeholder="select files" disabled="disabled">'
                    + '     <div class="file_upload btn btn-icon btn-custom btn-trans waves-effect waves-light cursor-pointer relative" style="cursor: pointer;" id="file_upload" name="file_info">'
                    + '         <div class="upload-txt">'
                    + '             <i class="fa fa-paperclip"></i>'
                    + '         </div>'
                    + '         <input id="fileEditorImage" type="file" />'
                    + '     </div>'
                    + '</div>'
                    + '<div class="c"></div>'
                    + '</div>',
                    size: 'sm',
                    controller: 'UploadLocalImageController'
                }).result.then(
                            function (result) {
                                if (result) {
                                    rangy.restoreSelection(savedSelection);
                                    textAngular.$editor().wrapSelection('insertImage', result);
                                }
                                deferred.resolve();
                            },
                            function () {
                                deferred.resolve();
                            }
                        );
                return false;
            }
        });

        //Hide default insert image icon.
        angular.element().ready(function () {
            var insertImage = $document[0].getElementsByName('insertImage');
            var containerID = angular.element(insertImage).hide();
        });

        // Font Name
        taRegisterTool('fontName', {
            display: ""
                        + "<div title='font' uib-dropdown is-open='status.isopen'>"
                        + "<span id='ta-btnFonts' uib-dropdown-toggle>"
                        + "<i class='fa fa-font'></i>"
                        + "</span>"
                        + "<ul class='dropdown-menu' uib-dropdown-menu role='menu' aria-labelledby='ta-btnFonts'>"
                        + "    <li role='menuitem' ng-repeat='o in options'><a href type='button' style='font-family: {{o.css}}; width: 100%' ng-click='action($event, options,o);'><i ng-if='o.active' class='fa fa-check'></i>{{o.name}}</a></li>"
                        + "  </ul>"
                        + "</div>"
                        + "",
            action: function (event, options, font) {
                if (font) {
                    var me = this;
                    if (!this.$editor().wrapSelection) {
                        setTimeout(function () {
                            me.action(event, options, font);
                        }, 100)
                    } else {
                        angular.forEach(options, function (opt) {
                            opt.active = false;
                        });
                        font.active = true;
                        return this.$editor().wrapSelection('fontName', font.css);
                    }
                }
            },
            options: [
                { name: 'Sans-Serif', css: 'Arial, Helvetica, sans-serif' },
                { name: 'Serif', css: "'times new roman', serif" },
                { name: 'Wide', css: "'arial black', sans-serif" },
                { name: 'Narrow', css: "'arial narrow', sans-serif" },
                { name: 'Comic Sans MS', css: "'comic sans ms', sans-serif" },
                { name: 'Courier New', css: "'courier new', monospace" },
                { name: 'Garamond', css: 'garamond, serif' },
                { name: 'Georgia', css: 'georgia, serif' },
                { name: 'Tahoma', css: 'tahoma, sans-serif' },
                { name: 'Trebuchet MS', css: "'trebuchet ms', sans-serif" },
                { name: "Helvetica", css: "'Helvetica Neue', Helvetica, Arial, sans-serif" },
                { name: 'Verdana', css: 'verdana, sans-serif' },
                { name: 'Proxima Nova', css: 'proxima_nova_rgregular' }
            ]
        });

        // Font Size
        taRegisterTool('fontSize', {
            display: ""
                        + "<div title='font size' uib-dropdown is-open='status.isopen'>"
                        + "<span id='ta-btnSize' uib-dropdown-toggle>"
                        + "<i class='fa fa-text-height'></i>"
                        + "</span>"
                        + "<ul class='dropdown-menu' uib-dropdown-menu role='menu' aria-labelledby='ta-btnSize'>"
                        + "    <li role='menuitem' ng-repeat='o in options'><a style='font-size: {{o.css}}; width: 100%' ng-click='action($event, options,o);'><i ng-if='o.active' class='fa fa-check'></i>{{o.name}}</a></li>"
                        + "  </ul>"
                        + "</div>"
                        + "",
            action: function (event, options, font) {
                if (font) {
                    var me = this;
                    if (!this.$editor().wrapSelection) {
                        setTimeout(function () {
                            me.action(event, options, font);
                        }, 100)
                    } else {
                        angular.forEach(options, function (opt) {
                            opt.active = false;
                        });
                        font.active = true;
                        return this.$editor().wrapSelection('fontSize', font.value);
                    }
                }
            },
            options: [
                { name: 'xx-small', css: 'xx-small', value: 1 },
                { name: 'x-small', css: 'x-small', value: 2 },
                { name: 'small', css: 'small', value: 3 },
                { name: 'medium', css: 'medium', value: 4 },
                { name: 'large', css: 'large', value: 5 },
                { name: 'x-large', css: 'x-large', value: 6 },
                { name: 'xx-large', css: 'xx-large', value: 7 }

            ]
        });

        // Background Color
        taRegisterTool('backgroundColor', {
            display: "<div title='fill color' spectrum-colorpicker ng-model='color' on-change='!!color && action(color)' format='\"hex\"' options='options'></div>",
            action: function (color) {
                var me = this;
                if (!this.$editor().wrapSelection) {
                    setTimeout(function () {
                        me.action(color);
                    }, 100)
                } else {
                    return this.$editor().wrapSelection('backColor', color);
                }
            },
            options: {
                replacerClassName: 'zmdi zmdi-format-color-fill', showButtons: false
            },
            color: "#fff"
        });

        // Font Color
        taRegisterTool('fontColor', {
            display: "<spectrum-colorpicker title='font color' trigger-id='{{trigger}}' ng-model='color' on-change='!!color && action(color)' format='\"hex\"' options='options'></spectrum-colorpicker>",
            action: function (color) {
                var me = this;
                if (!this.$editor().wrapSelection) {
                    setTimeout(function () {
                        me.action(color);
                    }, 100)
                } else {
                    return this.$editor().wrapSelection('foreColor', color);
                }
            },
            options: {
                replacerClassName: 'zmdi zmdi-format-color-text', showButtons: false
            },
            color: "#000"
        });

        taOptions.toolbar = [
            ["h1", "h2", "h3", "h4", "h5", "h6", "p"],
            ["bold", "italics", "underline", "strikeThrough", "ul", "ol", "redo", "undo", "clear", "backgroundColor", "fontColor", "fontName", "fontSize"],
            ["justifyLeft", "justifyCenter", "justifyRight", "justifyFull", "indent", "outdent"],
            ["html", "insertImage", "insertLink", "insertVideo", "wordcount", "charcount","uploadLocalImage"]
        ];

        return taOptions;
    } ]);
} ]);

angular.module("DorfKetalMVCApp").controller('UploadLocalImageController', ['$rootScope', '$scope', '$http', '$uibModalInstance', function ($rootScope, $scope, $http, $uibModalInstance) {

    var directoryPathEnumName = "Attachment_Temp";
    if ($rootScope.directoryPathEnumName) {
        directoryPathEnumName = $scope.directoryPathEnumName;
    }

    $scope.cancelActionToPerform = function () {
        $uibModalInstance.close();
    };
    $(document).on("change", "#fileEditorImage", function (e) {
        var fileControl = e.target;
        if (fileControl.files.length > 0) {
            var file = fileControl.files[0];
            var fileName = file.name;
            var size = file.size;
            var fileExtention = fileName.substr(fileName.lastIndexOf('.') + 1);
            var isValid = true;

            var fileAccepted = ["png", "jpeg", "jpg", "bmp"];
            if (!(fileAccepted.indexOf(fileExtention.toLowerCase()) >= 0)) {
                e.preventDefault();
                fileControl.value = "";
                isValid = false;
            }

            if (isValid) {
                var formData = new FormData();
                formData.append("file", file);
                $http({
                    method: 'POST',
                    url: $rootScope.apiURL + '/Upload/UploadFile?databaseName=' + $rootScope.userContext.CompanyDB + "&directoryPathEnumName=" + directoryPathEnumName,
                    data: formData,
                    headers: {
                        'Content-Type': undefined
                    }
                }).then(function (response) {
                    var data = response.data;
                    if (data.MessageType == messageTypes.Success) {
                        $uibModalInstance.close(data.Result.FileRelativePath);
                    }
                });
            }
        }
    });
} ]);