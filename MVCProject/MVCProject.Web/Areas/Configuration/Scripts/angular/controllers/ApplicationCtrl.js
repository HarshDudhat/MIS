(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('ApplicationCtrl', [
            '$scope', '$rootScope', '$q', '$filter', 'ngTableParams', 'CommonFunctions', 'CommonEnums', 'FileService', 'ApplicationService', 'SiteMasterService', 'CommonService', ApplicationCtrl
        ]);

    // BEGIN ApplicationCtrl
    function ApplicationCtrl($scope, $rootScope, $q, $filter, ngTableParams, CommonFunctions, CommonEnums, FileService, ApplicationService, SiteMasterService, CommonService) {
        var applicationConfiguration = {
            No_of_Days_before_alert_for_Company_subscription: { ConfigId: 1, ConfigValue: '', ConfigCode: 'No_of_Days_before_alert_for_Company_subscription' },
            No_of_Days_before_alert_for_User_subscription: { ConfigId: 2, ConfigValue: '', ConfigCode: 'No_of_Days_before_alert_for_User_subscription' },
            Contractor_or_Department: { ConfigId: 3, ConfigValue: '', ConfigCode: 'Contractor_or_Department' },
            Incident_local_standards: { ConfigId: 4, ConfigValue: '', ConfigCode: 'Incident_local_standards', SiteApplicationConfiguration: [] },
            Incident_british_standards: { ConfigId: 5, ConfigValue: '', ConfigCode: 'Incident_british_standards', SiteApplicationConfiguration: [] },
            Incident_OSHA_standards: { ConfigId: 6, ConfigValue: '', ConfigCode: 'Incident_OSHA_standards', SiteApplicationConfiguration: [] },
            Employee_No_of_Manday_Lost: { ConfigId: 7, ConfigValue: '', ConfigCode: 'Employee_No_of_Manday_Lost', SiteApplicationConfiguration: [] },
            Employee_No_of_Manhour_Lost: { ConfigId: 8, ConfigValue: '', ConfigCode: 'Employee_No_of_Manhour_Lost', SiteApplicationConfiguration: [] },
            Application_Logo: { ConfigId: 9, ConfigValue: '', ConfigCode: 'Application_Logo', FileRelativePath: '' },
            Report_Logo: { ConfigId: 10, ConfigValue: '', ConfigCode: 'Report_Logo', FileRelativePath: '' },
            SMTP_Server_Host: { ConfigId: 11, ConfigValue: '', ConfigCode: 'SMTP_Server_Host' },
            SMTP_Server_Port: { ConfigId: 12, ConfigValue: '', ConfigCode: 'SMTP_Server_Port' },
            SMTP_Enable_SSL: { ConfigId: 13, ConfigValue: '', ConfigCode: 'SMTP_Enable_SSL' },
            SMTP_Server_Network_Credential_User_Name: { ConfigId: 14, ConfigValue: '', ConfigCode: 'SMTP_Server_Network_Credential_User_Name' },
            SMTP_Server_Network_Credential_Password: { ConfigId: 15, ConfigValue: '', ConfigCode: 'SMTP_Server_Network_Credential_Password' },
            From_Email_Name: { ConfigId: 16, ConfigValue: '', ConfigCode: 'From_Email_Name' }
        };


        /* Initial Declaration */
        $scope.PermissionLevel = CommonEnums.PermissionLevel;
        $scope.applicationConfiguration = angular.copy(applicationConfiguration);
        $scope.site = [];
        $scope.siteId = angular.copy($rootScope.userContext.SiteId);
        $scope.terminology = [];
        $scope.lastStorage = {};

        // Init
        $scope.Init = function () {
            $scope.applicationConfiguration = angular.copy(applicationConfiguration);
            $scope.site = [];
            $scope.terminology = [];

            // Get Application Logo Path
            $scope.GetApplicationLogoPath = function (logo) {
                var url = "/Content/images/company-logo.png";
                if (logo) {
                    url = "{0}/{1}/ApplicationLogo/{2}".format($rootScope.apiAttachmentsURL, $rootScope.userContext.CompanyDB, logo);
                }
                return url;
            }

            // Get Report Logo Path
            $scope.GetReportLogoPath = function (logo) {
                var url = "/Content/images/company-logo.png";
                if (logo) {
                    url = "{0}/{1}/ReportLogo/{2}".format($rootScope.apiAttachmentsURL, $rootScope.userContext.CompanyDB, logo);
                }
                return url;
            }

            $q.all([
                ApplicationService.GetApplicationConfiguration(),
                SiteMasterService.GetSiteList()
            ]).then(function (response) {
                // Get Application Configuration
                if (response[0].data.MessageType == messageTypes.Success) {// Success  
                    var data = response[0].data;
                    angular.forEach(data.Result, function (value, key) {
                        $scope.applicationConfiguration[value.ConfigCode].ConfigId = value.ConfigId;
                        $scope.applicationConfiguration[value.ConfigCode].ConfigValue = value.ConfigValue;

                        if (value.ConfigCode == 'SMTP_Enable_SSL') {
                            $scope.applicationConfiguration[value.ConfigCode].ConfigValue = value.ConfigValue.toLowerCase() == "true";
                        }

                        if (value.ConfigCode == 'Contractor_or_Department' && value.ConfigValueUnit) {
                            $scope.terminology = value.ConfigValueUnit.split('#');
                        }

                        if (value.ConfigCode == 'Application_Logo') {
                            $scope.applicationConfiguration[value.ConfigCode].FileRelativePath = $scope.GetApplicationLogoPath(value.ConfigValue);
                        }

                        if (value.ConfigCode == 'Report_Logo') {
                            $scope.applicationConfiguration[value.ConfigCode].FileRelativePath = $scope.GetReportLogoPath(value.ConfigValue);
                        }

                        if (value.ConfigShotCode == 'Incident Configuration') {
                            $scope.applicationConfiguration[value.ConfigCode].SiteApplicationConfiguration = value.SiteApplicationConfiguration;
                        }
                    });
                    $scope.lastStorage = angular.copy($scope.applicationConfiguration);
                }

                // Get Sites
                if (response[1].data.MessageType == messageTypes.Success) {// Success  
                    var data = response[1].data;
                    if (data.MessageType == messageTypes.Success) {
                        $scope.site = data.Result;
                        if ($scope.site.length >= 0) {                            
                            $scope.GetSiteAllicationConfiguration($scope.siteId);
                        }
                    }
                }
            });
        };
        $scope.Init();

        // Get Site Application Configuration
        $scope.GetSiteAllicationConfiguration = function (siteId) {
            if (siteId) {
                var incident_local_standards = $filter("filter")($scope.applicationConfiguration.Incident_local_standards.SiteApplicationConfiguration, { SiteId: $scope.siteId });
                if (incident_local_standards.length >= 1) {
                    $scope.applicationConfiguration.Incident_local_standards.ConfigValue = angular.copy(incident_local_standards[0].ConfigValue);
                } else {
                    $scope.applicationConfiguration.Incident_local_standards.ConfigValue = '';
                }
                var incident_british_standards = $filter("filter")($scope.applicationConfiguration.Incident_british_standards.SiteApplicationConfiguration, { SiteId: $scope.siteId });
                if (incident_local_standards.length >= 1) {
                    $scope.applicationConfiguration.Incident_british_standards.ConfigValue = angular.copy(incident_british_standards[0].ConfigValue);
                } else {
                    $scope.applicationConfiguration.Incident_british_standards.ConfigValue = '';
                }
                var incident_OSHA_standards = $filter("filter")($scope.applicationConfiguration.Incident_OSHA_standards.SiteApplicationConfiguration, { SiteId: $scope.siteId });
                if (incident_local_standards.length >= 1) {
                    $scope.applicationConfiguration.Incident_OSHA_standards.ConfigValue = angular.copy(incident_OSHA_standards[0].ConfigValue);
                } else {
                    $scope.applicationConfiguration.Incident_OSHA_standards.ConfigValue = '';
                }
                var employee_No_of_Manday_Lost = $filter("filter")($scope.applicationConfiguration.Employee_No_of_Manday_Lost.SiteApplicationConfiguration, { SiteId: $scope.siteId });
                if (incident_local_standards.length >= 1) {
                    $scope.applicationConfiguration.Employee_No_of_Manday_Lost.ConfigValue = angular.copy(employee_No_of_Manday_Lost[0].ConfigValue);
                } else {
                    $scope.applicationConfiguration.Employee_No_of_Manday_Lost.ConfigValue = '';
                }
                var employee_No_of_Manhour_Lost = $filter("filter")($scope.applicationConfiguration.Employee_No_of_Manhour_Lost.SiteApplicationConfiguration, { SiteId: $scope.siteId });
                if (incident_local_standards.length >= 1) {
                    $scope.applicationConfiguration.Employee_No_of_Manhour_Lost.ConfigValue = angular.copy(employee_No_of_Manhour_Lost[0].ConfigValue);
                } else {
                    $scope.applicationConfiguration.Employee_No_of_Manhour_Lost.ConfigValue = '';
                }
            } else {
                $scope.applicationConfiguration.Incident_local_standards.ConfigValue = angular.copy($scope.lastStorage.Incident_local_standards.ConfigValue);
                $scope.applicationConfiguration.Incident_british_standards.ConfigValue = angular.copy($scope.lastStorage.Incident_british_standards.ConfigValue);
                $scope.applicationConfiguration.Incident_OSHA_standards.ConfigValue = angular.copy($scope.lastStorage.Incident_OSHA_standards.ConfigValue);
                $scope.applicationConfiguration.Employee_No_of_Manday_Lost.ConfigValue = angular.copy($scope.lastStorage.Employee_No_of_Manday_Lost.ConfigValue);
                $scope.applicationConfiguration.Employee_No_of_Manhour_Lost.ConfigValue = angular.copy($scope.lastStorage.Employee_No_of_Manhour_Lost.ConfigValue);
            }
        };

        // Save Application Configuration
        $scope.SaveApplicationConfiguration = function (appConfig, frmApplicationConfiguration) {
            if (!$rootScope.permission.CanWrite) { return; }
            if (frmApplicationConfiguration.$invalid) {
                return;
            }
            var applicationConfigurationList = [];
            angular.forEach(appConfig, function (value, key) {
                applicationConfigurationList.push(value);
            });
            ApplicationService.SaveApplicationConfiguration(applicationConfigurationList, $scope.siteId).then(function (res) {
                if (res) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success) {// Success  
                        $scope.ResetForm(frmApplicationConfiguration);
                        var filename = appConfig.Application_Logo.FileRelativePath.split('/').reverse()[0];
                        if (filename) {
                            filename = $scope.GetApplicationLogoPath(filename);
                            $rootScope.userContext.ApplicationLogo = filename;
                            ApplicationService.UpdateApplicationLogo(filename).then(function (response) { });
                        }
                        toastr.success(data.Message, successTitle);
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        };

        // Reset Application Configuration Form
        $scope.ResetForm = function (frmApplicationConfiguration) {
            $scope.Init();
            frmApplicationConfiguration.$setPristine();
        };

        var fileAccepted = ["png", "jpeg", "jpg", "bmp"];
        var maxFileSize = 1 * 1024 * 1024; // 1 MB

        // on selection of Application Logo upload to temp location
        $(document).on("change", "#fileApplicationLogo", function (e) {
            if (!$rootScope.permission.CanWrite) { return; }
            var fileControl = e.target;
            if (fileControl.files.length > 0) {
                var file = fileControl.files[0];
                var fileName = file.name;
                var size = file.size;
                var fileExtention = fileName.substr(fileName.lastIndexOf('.') + 1);
                var isValid = true;

                if (!(fileAccepted.indexOf(fileExtention.toLowerCase()) >= 0)) {
                    e.preventDefault();
                    fileControl.value = "";
                    toastr.error(InvalidFileExtensionMsg + fileAccepted.join(', '), errorTitle);
                    isValid = false;
                }

                if (size > maxFileSize) {
                    e.preventDefault();
                    fileControl.value = "";
                    toastr.error(FileSizeValidationMsg, errorTitle);
                    isValid = false;
                }

                if (isValid) {
                    var formData = new FormData();
                    formData.append("file", file);
                    ApplicationService.UploadLogo(formData).then(function (response) {
                        var data = response.data;
                        if (data.MessageType == messageTypes.Success) {
                            $scope.applicationConfiguration.Application_Logo.ConfigValue = data.Result.FileName;
                            $scope.applicationConfiguration.Application_Logo.FileRelativePath = data.Result.FileRelativePath;
                            document.getElementById('fileApplicationLogo').value = '';
                        }
                    });
                }
            }
        });

        // on selection of Report Logo upload to temp location
        $(document).on("change", "#fileReportLogo", function (e) {
            if (!$rootScope.permission.CanWrite) { return; }
            var fileControl = e.target;
            if (fileControl.files.length > 0) {
                var file = fileControl.files[0];
                var fileName = file.name;
                var size = file.size;
                var fileExtention = fileName.substr(fileName.lastIndexOf('.') + 1);
                var isValid = true;

                if (!(fileAccepted.indexOf(fileExtention.toLowerCase()) >= 0)) {
                    e.preventDefault();
                    fileControl.value = "";
                    toastr.error(InvalidFileExtensionMsg + fileAccepted.join(', '), errorTitle);
                    isValid = false;
                }

                if (size > maxFileSize) {
                    e.preventDefault();
                    fileControl.value = "";
                    toastr.error(FileSizeValidationMsg, errorTitle);
                    isValid = false;
                }

                if (isValid) {
                    var formData = new FormData();
                    formData.append("file", file);
                    ApplicationService.UploadLogo(formData).then(function (response) {
                        var data = response.data;
                        if (data.MessageType == messageTypes.Success) {
                            $scope.applicationConfiguration.Report_Logo.ConfigValue = data.Result.FileName;
                            $scope.applicationConfiguration.Report_Logo.FileRelativePath = data.Result.FileRelativePath;
                            document.getElementById('fileReportLogo').value = '';
                        }
                    });
                }
            }
        });
    }

})();