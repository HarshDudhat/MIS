angular.module("DorfKetalMVCApp").service('CommonService', ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    // Get list of languages
    list.GetLanguages = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Account/GetLanguages'
        });
    };

    // Get list of themes
    list.GetThemes = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetThemes'
        });
    };

    // Get list of themes
    list.GetTrainings = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetTrainings'
        });
    };

    // Get list of equipments
    list.GetEquipments = function (SiteLevelId, PlannerDetailId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetEquipments?SiteLevelId=' + SiteLevelId + '&PlannerDetailId=' + PlannerDetailId
        });
    };


    // Get list of themes
    list.GetDeficienciesBasedOnTheme = function (themeId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetDeficienciesBasedOnTheme?ThemeId=' + themeId
        });
    };

    // Get list of HOD Employees
    list.GetSitewiseHODUser = function (siteLevelId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetSitewiseHODUser?SiteLevelId=' + siteLevelId
        });
    };

    // Create Session @web end
    list.CreateSession = function (SessionParams) {
        return $http({
            method: 'POST',
            url: '/Account/CreateSession',
            data: JSON.stringify(SessionParams)
        });
    };

    // Reset Session @web end
    list.ResetSession = function () {
        return $http({
            method: 'PUT',
            url: '/Account/ResetSession'
        });
    };

    // Delete Document from Temporary Location
    list.DeleteDocumentFromTempLocation = function (fileName) {
        return $http({
            method: 'DELETE',
            url: $rootScope.apiURL + '/Upload/DeleteDocumentFromTempLocation?databaseName=' + $rootScope.userContext.CompanyDB + '&fileToRemove=' + fileName
        });
    };

    list.GetAttachments = function (moduleId, referenceId, attachmentType) {
        if (attachmentType == null || attachmentType == undefined) {
            attachmentType = 0;
        }
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetAttachments?moduleId=' + moduleId + '&referenceId=' + referenceId + '&attachmentType=' + attachmentType
        });
    };

    list.SaveAttachments = function (model) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Common/SaveAttachments',
            data: JSON.stringify(model)
        });
    };

    list.GetComments = function (moduleId, referenceId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetComments?moduleId=' + moduleId + '&referenceId=' + referenceId
        });
    };

    list.SaveComments = function (model) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Common/SaveComments',
            data: JSON.stringify(model)
        });
    };

    list.GetNotes = function (moduleId, referenceId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetNotes?moduleId=' + moduleId + '&referenceId=' + referenceId
        });
    };

    list.SaveNotes = function (model) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Common/SaveNotes',
            data: JSON.stringify(model)
        });
    };

    // Get Server Date and time.
    list.GetServerDate = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetServerDateTime'
        });
    };

    list.GetTrainingList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/TrainingMaster/GetTrainingList'
        });
    };

    list.GetAuditTitleList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/AuditTitle/GetForDropdown' + (angular.isDefined(isGetAll) ? '?isGetAll=' + isGetAll : '')
        });
    };

    // Get Company Logo before login
    list.GetCompanyLogo = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Landing/GetCompanyLogo'
        });
    };
    // Synchronize Reporting Data
    list.SynchronizeReportingData = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/SynchronizeReportingData'
        });
    };

    // get Encrypted UserName
    list.GetEncryptedUserName = function (userName) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetEncryptedUserName?userName=' + userName
        });
    };

    // Save Change password detail
    list.SaveChangePassword = function (changePasswordDetail) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Account/SaveChangePassword',
            data: JSON.stringify(changePasswordDetail)
        });
    };

    // get Encrypted UserName
    list.GetCompanyName = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetCompanyName'
        });
    };
    //Get GetDepartmentList
    list.GetDepartmentList = function (param) {
        var defaultParam = {
            siteIds: []
        };
        angular.extend(defaultParam, param);
        var str = [];
        for (var p in defaultParam) {
            if (!(angular.isArray(defaultParam[p]) && defaultParam[p].length == 0)) {
                if (defaultParam.hasOwnProperty(p)) {
                    if (angular.isArray(defaultParam[p]) && defaultParam[p].length > 0) {
                        for (var i = 0; i < defaultParam[p].length; i++) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p][i]));
                        }
                    }
                    else {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p]));
                    }
                }
            }
        }
        var url = $rootScope.apiURL + '/Common/GetDepartmentList?' + str.join("&");
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }
    };

    //Get Departments
    list.GetDepartments = function (param) {
        var defaultParam = {
            siteIds: []
        };
        angular.extend(defaultParam, param);
        var str = [];
        for (var p in defaultParam) {
            if (!(angular.isArray(defaultParam[p]) && defaultParam[p].length == 0)) {
                if (defaultParam.hasOwnProperty(p)) {
                    if (angular.isArray(defaultParam[p]) && defaultParam[p].length > 0) {
                        for (var i = 0; i < defaultParam[p].length; i++) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p][i]));
                        }
                    }
                    else {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p]));
                    }
                }
            }
        }
        var url = $rootScope.apiURL + '/Common/GetDepartments?' + str.join("&");
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }
    };

    //Get Functions
    list.GetAreas = function (param) {
        var defaultParam = {
            siteIds: []
        };
        angular.extend(defaultParam, param);
        var str = [];
        for (var p in defaultParam) {
            if (!(angular.isArray(defaultParam[p]) && defaultParam[p].length == 0)) {
                if (defaultParam.hasOwnProperty(p)) {
                    if (angular.isArray(defaultParam[p]) && defaultParam[p].length > 0) {
                        for (var i = 0; i < defaultParam[p].length; i++) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p][i]));
                        }
                    }
                    else {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p]));
                    }
                }
            }
        }
        var url = $rootScope.apiURL + '/Common/GetAreas?' + str.join("&");
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }
    };

    //Get Project
    list.GetProjects = function (param) {
        var defaultParam = {
            verticalIds: []
        };
        angular.extend(defaultParam, param);
        var str = [];
        for (var p in defaultParam) {
            if (!(angular.isArray(defaultParam[p]) && defaultParam[p].length == 0)) {
                if (defaultParam.hasOwnProperty(p)) {
                    if (angular.isArray(defaultParam[p]) && defaultParam[p].length > 0) {
                        for (var i = 0; i < defaultParam[p].length; i++) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p][i]));
                        }
                    }
                    else {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p]));
                    }
                }
            }
        }
        var url = $rootScope.apiURL + '/Common/GetProjects?' + str.join("&");
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }
    };


    //Risk Assessment

    // Get Hazard List
    list.GetHazardList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetHazardList'
        });
    };

    // Get SoE List
    list.GetSOEList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetSOEList'
        });
    };

    // Get Hazard List
    list.GetRiskMatrixDetails = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetRiskMatrixDetails'
        });
    };

    // Get Injury Classification List
    list.GetInjuryClassificationList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetInjuryClassificationList'
        });
    };

    // Get Hierarchy of Control List
    list.GetHierarchyofControlList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetHierarchyofControlList'
        });
    };

    list.GetRiskAssessmetSearch = function (model) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Common/GetRiskAssessmetSearch',
            data: JSON.stringify(model)
        });
    };

    //Get UnitOfMeasurement
    list.GetUnitOfMeasurementList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/UnitOfMeasurement/GetForDropdown?isGetAll=' + (angular.isDefined(isGetAll) ? isGetAll : false)
        });
    };


    //Get EmployeeContractor
    list.GetEmployeeContractorList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/EmployeeContractor/GetForDropdown'
        });
    };

    // Get Area List
    list.GetAreasBySite = function (siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/AreaMaster/GetArea?siteId=' + siteId
        });
    };

    // Get Shift List
    list.GetShiftsBySite = function (siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/ShiftMaster/GetShift?siteLevelId=' + siteId
        });
    };

    // Get Department List
    list.GetDepartmentsBySite = function (siteId, isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/DepartmentMaster/GetDepartmentsBySite',
            params: (angular.isDefined(isGetAll) ? { siteIds: siteId, isGetAll: isGetAll } : { siteIds: siteId })
        });
    };

    //Get Vertical
    list.GetVerticalList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/VerticalMaster/GetForDropdown?isGetAll=' + (angular.isDefined(isGetAll) ? isGetAll : false)
        });
    };

    //Get Inspection Category
    list.GetInspectionCategoryList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetInspectionCategoryListDropdown'
        });
    };

    // Get Site/Project By Vertical for dropdown
    list.GetSitesByVertical = function (verticalId, isGetAll) {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + '/SiteMaster/GetSitesByVertical?VerticalId=' + verticalId + '&isGetAll=' + (angular.isDefined(isGetAll) ? isGetAll : false)
        });
    };

    //Get GetSitesByVerticalIds
    list.GetSitesByVerticalIds = function (param) {
        var defaultParam = {
            verticalIds: []
        };
        angular.extend(defaultParam, param);
        var str = [];
        for (var p in defaultParam) {
            if (!(angular.isArray(defaultParam[p]) && defaultParam[p].length == 0)) {
                if (defaultParam.hasOwnProperty(p)) {
                    if (angular.isArray(defaultParam[p]) && defaultParam[p].length > 0) {
                        for (var i = 0; i < defaultParam[p].length; i++) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p][i]));
                        }
                    }
                    else {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p]));
                    }
                }
            }
        }
        var url = $rootScope.apiURL + '/SiteMaster/GetSitesByVerticalIds?' + str.join("&");
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }
    };

    // Get Hierarchy of Control List
    list.GetESLocationList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetESLocationList'
        });
    };

    list.GetApprovalTeamMemberForEHS = function (siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetApprovalTeamMemberForEHS?siteId=' + siteId
        });
    };

    //Get Vertical
    list.GetAreaListBasedonSites = function (Sites) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/AreaMaster/GetAreasBasedOnSites?Sites=' + Sites
        });
    };


    list.GetSeverityList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetSeverityList'
        });
    };


    // For Auto complete level list
    list.GetLevelAutoCompleteUrl = function (levelIds, hierarchyIds, moduleId, parentLevelId) {
        return $rootScope.apiURL + '/ConfigLevel/FilterList?levelIds=' + levelIds + '&hierarchyIds=' + hierarchyIds + '&moduleId=' + moduleId + '&parentLevelId=' + parentLevelId + '&searchText=';
    };


    // For Dropdown level list
    list.GetLevelFromHierarchyId = function (hierarchyId, parentLevelId) {
        if (!parentLevelId)
            parentLevelId = 0;
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/ConfigLevel/FilterList?levelIds=&hierarchyIds=' + hierarchyId + '&moduleId=0&parentLevelId=' + parentLevelId + '&searchText='
        });
    };

    // Get Audit Planner IsComplete Common Services
    list.GetAuditIsComplete = function (filterPlan) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/AuditPlanner/GetAuditIsComplete',
            data: JSON.stringify(filterPlan)
        });
    };

    // Get Audit Planner IsComplete Common Services
    list.GetEHSTrainingPlanIsComplete = function (filterPlan) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Planner/GetEHSTrainingPlanIsComplete',
            data: JSON.stringify(filterPlan)
        });
    };

    //Get module wise state list for search
    list.GetModuleWiseStates = function (moduleIds) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetModuleWiseStates?moduleIds=' + moduleIds
        });
    };

    //Get Site/Project
    list.GetSitesForDropdownn = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Landing/GetSitesForDropdownn'
        });
    };


    //For Employee Selection Popup
    list.GetEquipmentToSelectForPopup = function (equipmentFilter) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/EquipmentRegister/AutoCompleteEquipmentListPopup?hideLoader=1&SiteId=' + (angular.isDefined(equipmentFilter.siteId) ? equipmentFilter.siteId : '') + '&firstCharacter=' + equipmentFilter.charcharter + '&LocationLevelId=' + (angular.isDefined(equipmentFilter.LocationLevelId) ? equipmentFilter.LocationLevelId : '') + '&EquipmentTypeId=' + (angular.isDefined(equipmentFilter.EquipmentTypeId) ? equipmentFilter.EquipmentTypeId : '') + '&searchText=' + equipmentFilter.searchText
        });
    };

    //Get GetEquipmentTypeList
    list.GetEquipmentTypeList = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/EquipmentType/GetAllEquipmentTypeForDropDown'
        });
    };

    // Get All list of Checkup Criteria Details
    list.GetCheckupCriteria = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetCheckupCriteria'
        });
    };

    // Get All list of Medical Static Type
    list.GetMedicalStaticType = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetMedicalStaticType'
        });
    };

    //GetUserGridSetting
    list.gridSetting = function (param) {
        var defaultParam = {
            pageId: 0,
            EmployeeId: 0,
            RoleId: 0
        };

        angular.extend(defaultParam, param);
        var url = $rootScope.apiURL + '/Common/GetGridSettings?' + $httpParamSerializer(defaultParam);
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }
    };

    // Get IHI Status
    list.GetIHIStatus = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/GetIHIStatus'
        });
    };

    list.downloadReportFile = function (name, filename) {
        $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Common/DonwloadReport',
            params: { fileName: name },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers) {
            var headers = headers();

            //var filename = headers['filename'];
            var contentType = headers['content-type'];

            var linkElement = document.createElement('a');
            try {
                var blob = new Blob([data], { type: contentType });
                var url = window.URL.createObjectURL(blob);

                linkElement.setAttribute('href', url);
                linkElement.setAttribute("download", filename);

                if (navigator.appName == 'Microsoft Internet Explorer' || !!(navigator.userAgent.match(/Trident/) || navigator.userAgent.match(/rv:11/)) || (typeof $.browser !== "undefined" && $.browser.msie == 1)) {
                    //var clickEvent = document.createEvent("MouseEvent");
                    //clickEvent.initMouseEvent("click", true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
                    //linkElement.dispatchEvent(clickEvent);

                    if (window.navigator && window.navigator.msSaveOrOpenBlob) {
                        window.navigator.msSaveOrOpenBlob(blob, filename);
                        return;
                    }

                }
                else {
                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                }
            } catch (ex) {
                console.log(ex);
            }
        }).error(function (data) {
            console.log(data);
        });
    };

    return list;
}]);