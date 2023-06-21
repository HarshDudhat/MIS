angular.module("MVCApp").service('CommonService', ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    // Get list of languages
    list.GetLanguages = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Account/GetLanguages'
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

    // Get Site/Project By Vertical for dropdown
    list.GetSitesByVertical = function (verticalId, isGetAll) {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + '/SiteMaster/GetSitesByVertical?VerticalId=' + verticalId + '&isGetAll=' + (angular.isDefined(isGetAll) ? isGetAll : false)
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