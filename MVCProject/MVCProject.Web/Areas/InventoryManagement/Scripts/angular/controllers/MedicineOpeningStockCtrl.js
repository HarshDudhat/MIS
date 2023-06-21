(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('MedicineOpeningStockCtrl', [
            '$scope', '$q', '$rootScope', '$ngBootbox', '$filter', 'CommonFunctions', 'CommonEnums', 'MedicineOpeningStockService', 'CommonService', MedicineOpeningStockCtrl
    ]);

    function MedicineOpeningStockCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, MedicineOpeningStockService, CommonService) {

        //#region Varriables

        $scope.Initstatus = {
            opened: false
        };

        $scope.DefaultExpiryDate = new Date("01-01-1900");
        $scope.options = {
            showAddMedicineDetails: false
        };

        $scope.Filter = {
            OpeningStockEntryDate: null,
            searchData: '',
            SiteLevelId: 0,
            SiteName: ""
        };

        $scope.medicineOpeningStock = [];

        $scope.lastStorageMedicineOpeningStock = $scope.lastStorageMedicineOpeningStock || {};

        //True if records for particuler date is already exist in Database
        $scope.IsEdit = false;

        //Auto Complete URL for Medicine Details based on Medicine Name for filling Grid Details.
        $scope.medicineAutoCompleteUrl = MedicineOpeningStockService.MedicineAutoCompleteUrl();

        //#endregion

        //Get sites
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Filter.SiteLevelId = level.LevelId;
                $scope.Filter.SiteName = level.Name;
                $scope.SearchRecords();
            }
        };

        //#region On Load Functions

        $scope.Init = function () {
            $q.all([
               CommonService.GetServerDate(),
               MedicineOpeningStockService.GetMedicineOpeningStock($scope.Filter)
            ]).then(function (response) {
                //Server Date
                if (response[0].data.MessageType == 1) {
                    $scope.Filter.OpeningStockEntryDate = moment(new Date(response[0].data.Result)).toDate();
                } else if (response[0].data.MessageType == 4) {
                    $scope.Filter.OpeningStockEntryDate = '';
                }

                //Medicine Opening Stock Details
                if (response[1].data.MessageType == 1) {
                    $scope.medicineOpeningStock = response[1].data.Result;
                    if ($scope.medicineOpeningStock.length > 0) {
                        $scope.lastStorageMedicineOpeningStock = angular.copy($scope.medicineOpeningStock);
                        $scope.IsEdit = true;
                    }
                    else {
                        $scope.IsEdit = false;
                    }
                } else if (response[1].data.MessageType == 4) {
                    $scope.medicineOpeningStock = [];
                }
            });
        }

        //#endregion

        //#region Filter Change Functions

        $scope.GetMedicineOpeningStock = function () {
            MedicineOpeningStockService.GetMedicineOpeningStock($scope.Filter).then(function (response) {
                $rootScope.isAjaxLoadingChild = true;
                if (response && response.data) {
                    var data = response.data;
                    if (!angular.isUndefined(data)) {
                        if (data.MessageType == messageTypes.Success) {// Success
                            $scope.medicineOpeningStock = angular.copy(data.Result);
                            if ($scope.medicineOpeningStock.length > 0) {
                                $scope.lastStorageMedicineOpeningStock = angular.copy($scope.medicineOpeningStock);
                                $scope.IsEdit = true;
                            }
                            else {
                                $scope.IsEdit = false;
                            }
                        } else {
                            showtoastr(data.Message, data.MessageType);
                        }
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        }

        $scope.SearchRecords = function () {
            $q.all([
               CommonService.GetServerDate(),
               MedicineOpeningStockService.GetMedicineOpeningStock($scope.Filter)
            ]).then(function (response) {
                //Server Date
                if (response[0].data.MessageType == 1) {
                    $scope.Filter.OpeningStockEntryDate = moment(new Date(response[0].data.Result)).toDate();
                } else if (response[0].data.MessageType == 4) {
                    $scope.Filter.OpeningStockEntryDate = '';
                }

                //Medicine Opening Stock Details
                if (response[1].data.MessageType == 1) {
                    $scope.medicineOpeningStock = response[1].data.Result;
                    if ($scope.medicineOpeningStock.length > 0) {
                        $scope.lastStorageMedicineOpeningStock = angular.copy($scope.medicineOpeningStock);
                        $scope.IsEdit = true;
                    }
                    else {
                        $scope.IsEdit = false;
                    }
                } else if (response[1].data.MessageType == 4) {
                    $scope.medicineOpeningStock = [];
                }
            });
        };

        //#endregion

        //#region Grid Functions

        //Medicine Name Changed in Autocomplete For Add New Mode.
        $scope.ChangedMedicineNameAdd = function (selected, actionToAdd, id) {
            $scope.Filter.searchData = "";
            if (angular.isDefined(selected) && selected.originalObject.MedicineId > 0) {
                console.log(selected);
                actionToAdd.MedicineId = selected.originalObject.MedicineId;
                actionToAdd.MedicineType = selected.originalObject.MedType;
                actionToAdd.MedicineCode = selected.originalObject.MedCode;
                actionToAdd.MedicineName = selected.originalObject.MedName;
                actionToAdd.IsExpiryRequired = selected.originalObject.IsExpiryRequired;
            }
            else {
                actionToAdd.MedicineId = 0;
                actionToAdd.MedicineType = '';
                actionToAdd.MedicineCode = '';
                actionToAdd.MedicineName = '';
                actionToAdd.IsExpiryRequired = true;
                $scope.$broadcast('angucomplete-alt:clearInput');
            }
        };

        // Add New Medicine Details
        $scope.AddMedicineDetails = function (frmMedicineDetailsAdd, medicineOpeningStock, actionToAdd, options) {
            $scope.Filter.searchData = "";
            if (parseFloat(actionToAdd.Price) > 0 && parseInt(actionToAdd.Quantity) > 0) {
                $scope.IsSave = true;
                $scope.NoFound = false;

                if (!actionToAdd.ExpiryDate || actionToAdd.ExpiryDate == '') {
                    actionToAdd.IsDefaultExpiry = true;
                    actionToAdd.ExpiryMonth = moment($scope.DefaultExpiryDate).format('M');
                    actionToAdd.ExpiryYear = moment($scope.DefaultExpiryDate).format('YYYY');
                    actionToAdd.ExpiryDate = moment($scope.DefaultExpiryDate).endOf('month').toDate();
                }
                else {
                    actionToAdd.IsDefaultExpiry = false;
                    actionToAdd.ExpiryMonth = moment(actionToAdd.ExpiryDate).format('M');
                    actionToAdd.ExpiryYear = moment(actionToAdd.ExpiryDate).format('YYYY');
                    actionToAdd.ExpiryDate = moment(actionToAdd.ExpiryDate).endOf('month').toDate();
                    if (Date.parse(actionToAdd.ExpiryDate) < Date.parse(new Date())) {
                        toastr.warning(ExpiryDateLessThenCurrentWarningMessaage, warningTitle);
                    }
                }

                var isDuplicateExist = $filter("filter")(medicineOpeningStock, { MedicineId: actionToAdd.MedicineId, ExpiryMonth: actionToAdd.ExpiryMonth, ExpiryYear: actionToAdd.ExpiryYear, IsActive: true }, true).length;

                if (isDuplicateExist > 0) {
                    toastr.warning(DuplicateOpeningStockDetails, warningTitle);
                    return;
                }

                if (frmMedicineDetailsAdd.$valid) {
                    //$scope.actionToAdd = {
                    //    MedicineReceivedId: 0,
                    //    ReceivedDate: $scope.Filter.OpeningStockEntryDate,
                    //    ReceivedFrom: 'Opening Stock',
                    //    MedReceivedSourceId: 1,
                    //    MedicineId: 0,
                    //    MedicineCode: '',
                    //    MedicineName: '',
                    //    MedicineType: '',
                    //    ExpiryDate: null,
                    //    ExpiryMonth: '',
                    //    ExpiryYear: '',
                    //    Price: '',
                    //    Quantity: '',
                    //    IsActive: true,
                    //    IsExpiryRequired: true
                    //};

                    if (medicineOpeningStock == null)
                        medicineOpeningStock = [];

                    //if (!actionToAdd.ExpiryDate || actionToAdd.ExpiryDate == '') {
                    //    actionToAdd.IsDefaultExpiry = true;
                    //    actionToAdd.ExpiryMonth = moment($scope.DefaultExpiryDate).format('M');
                    //    actionToAdd.ExpiryDate = moment($scope.DefaultExpiryDate).endOf('month').toDate();
                    //}
                    //else {
                    //    actionToAdd.IsDefaultExpiry = false;
                    //    actionToAdd.ExpiryMonth = moment(actionToAdd.ExpiryDate).format('M');
                    //    actionToAdd.ExpiryDate = moment(actionToAdd.ExpiryDate).endOf('month').toDate();
                    //}
                    actionToAdd.IsActive = true;
                    actionToAdd.MedicineReceivedId = 0;
                    actionToAdd.MedReceivedSourceId = 1;
                    actionToAdd.ReceivedDate = $scope.Filter.OpeningStockEntryDate;
                    actionToAdd.ReceivedFrom = 'Opening Stock';
                    actionToAdd.SiteLevelId = $scope.Filter.SiteLevelId;

                    medicineOpeningStock.push(actionToAdd);

                    options.showAddMedicineDetails = !options.showAddMedicineDetails;
                }
                else {
                    $scope.IsSave = false;
                    return;
                }
            }
            else {
                toastr.warning(PriceAndQtyGreaterThanZeroRequired, warningTitle);
                $scope.IsSave = false;
                return;
            }
        };

        //Medicine Name Changed in Autocomplete For Edit Mode.
        $scope.ChangedMedicineNameEdit = function (selected, actionToEdit, id) {
            $scope.Filter.searchData = "";
            if (angular.isDefined(selected) && selected.originalObject.MedicineId > 0) {
                console.log(selected);
                actionToEdit.MedicineId = selected.originalObject.MedicineId;
                actionToEdit.MedicineType = selected.originalObject.MedType;
                actionToEdit.MedicineCode = selected.originalObject.MedCode;
                actionToEdit.MedicineName = selected.originalObject.MedName;
                actionToEdit.IsExpiryRequired = selected.originalObject.IsExpiryRequired;
            }
            else {
                actionToEdit.MedicineId = 0;
                actionToEdit.MedicineType = '';
                actionToEdit.MedicineCode = '';
                actionToEdit.MedicineName = '';
                actionToEdit.IsExpiryRequired = true;
                $scope.$broadcast('angucomplete-alt:clearInput');
            }
        };

        // Edit Medicine Details
        $scope.EditMedicineDetails = function (item, actionToEdit, showHideAction) {
            $scope.Filter.searchData = "";
            showHideAction.isEdit = !showHideAction.isEdit;
            if (item.IsDefaultExpiry) {
                item.ExpiryDate = null;
            }
            else {
                item.ExpiryDate = new Date(item.ExpiryDate);
            }
            item.SiteLevelId = $scope.Filter.SiteLevelId;
            angular.copy(item, actionToEdit);
        };

        //Update Medicine Details
        $scope.UpdateMedicineDetails = function (frmMedicineDetailsEdit, medicineOpeningStock, item, actionToEdit, showHideAction) {
            $scope.Filter.searchData = "";
            if (parseFloat(actionToEdit.Price) > 0 && parseInt(actionToEdit.Quantity) > 0) {
                var index = medicineOpeningStock.indexOf(item);
                $scope.medicineOpeningStockForDuplicateCheck = angular.copy(medicineOpeningStock);
                $scope.medicineOpeningStockForDuplicateCheck.splice(index, 1)

                if (!actionToEdit.ExpiryDate || actionToEdit.ExpiryDate == '') {
                    actionToEdit.IsDefaultExpiry = true;
                    actionToEdit.ExpiryMonth = moment($scope.DefaultExpiryDate).format('M');
                    actionToEdit.ExpiryYear = moment($scope.DefaultExpiryDate).format('YYYY');
                    actionToEdit.ExpiryDate = moment($scope.DefaultExpiryDate).endOf('month').toDate();
                }
                else {

                    actionToEdit.IsDefaultExpiry = false;
                    actionToEdit.ExpiryMonth = moment(actionToEdit.ExpiryDate).format('M');
                    actionToEdit.ExpiryYear = moment(actionToEdit.ExpiryDate).format('YYYY');
                    actionToEdit.ExpiryDate = moment(actionToEdit.ExpiryDate).endOf('month').toDate();
                    if (Date.parse(actionToEdit.ExpiryDate) < Date.parse(new Date())) {
                        toastr.warning(ExpiryDateLessThenCurrentWarningMessaage, warningTitle);
                    }
                }

                var isDuplicateExist = $filter("filter")($scope.medicineOpeningStockForDuplicateCheck, { MedicineId: actionToEdit.MedicineId, ExpiryMonth: actionToEdit.ExpiryMonth, ExpiryYear: actionToEdit.ExpiryYear, IsActive: true }, true).length;

                if (isDuplicateExist > 0) {
                    toastr.warning(DuplicateOpeningStockDetails, warningTitle);
                    return;
                }

                if (frmMedicineDetailsEdit.$valid) {

                    //if (!actionToEdit.ExpiryDate || actionToEdit.ExpiryDate == '') {
                    //    actionToEdit.IsDefaultExpiry = true;
                    //    actionToEdit.ExpiryMonth = moment($scope.DefaultExpiryDate).format('M');
                    //    actionToEdit.ExpiryDate = moment($scope.DefaultExpiryDate).endOf('month').toDate();
                    //}
                    //else {
                    //    actionToEdit.IsDefaultExpiry = false;
                    //    actionToEdit.ExpiryMonth = moment(actionToEdit.ExpiryDate).format('M');
                    //    actionToEdit.ExpiryDate = moment(actionToEdit.ExpiryDate).endOf('month').toDate();
                    //}

                    //actionToEdit.ExpiryMonth = moment(actionToEdit.ExpiryDate).format('M');
                    //actionToEdit.ExpiryDate = moment(actionToEdit.ExpiryDate).endOf('month').toDate();

                    if (actionToEdit.MedicineReceivedId > 0) {
                        MedicineOpeningStockService.GetMinimumQtyForOpeningStock(actionToEdit.MedicineId, moment(actionToEdit.ExpiryDate).endOf('month').format('MMMM-YYYY')).then(function (response) {
                            $rootScope.isAjaxLoadingChild = true;
                            if (response && response.data) {
                                var data = response.data;
                                if (!angular.isUndefined(data)) {
                                    if (data.MessageType == messageTypes.Success) {// Success
                                        if (parseInt(actionToEdit.Quantity) >= parseInt(data.Result) || data.Result == -1) {
                                            angular.copy(actionToEdit, item);
                                            showHideAction.isEdit = !showHideAction.isEdit;
                                        }
                                        else {
                                            actionToEdit.Quantity = '';
                                            toastr.warning('Quantity must be greater than or equal to available quantity:' + data.Result, warningTitle);
                                        }

                                    } else {
                                        showtoastr(data.Message, data.MessageType);
                                    }
                                }
                            }
                            $rootScope.isAjaxLoadingChild = false;
                        });
                    }
                    else {
                        angular.copy(actionToEdit, item);
                        showHideAction.isEdit = !showHideAction.isEdit;
                    }
                }
            }
            else {
                toastr.warning(PriceAndQtyGreaterThanZeroRequired, warningTitle);
                return;
            }
        };

        // Delete Medicine Details
        $scope.DeleteMedicineDetails = function (medicineDetails, item) {
            $ngBootbox.confirm(DeleteConfirmMsg)
        .then(function () {
            $scope.Filter.searchData = "";
            var index = medicineDetails.indexOf(item);
            medicineDetails[index].IsActive = false;
        },
        function () {
        });
        };

        //#endregion

        //#region Form Submission Functions

        //Clear form data.
        $scope.ClearForm = function (frmMedicineOpeningStock) {
            $scope.medicineOpeningStock = angular.copy($scope.lastStorageMedicineOpeningStock);
            //$scope.IsEdit = false;
            $scope.options.showAddMedicineDetails = false;
            frmMedicineOpeningStock.$setPristine();
            CommonFunctions.ScrollToTop();
            $scope.Filter.searchData = "";
        };

        //Reset form data.
        $scope.ResetForm = function (frmMedicineOpeningStock) {
            $scope.medicineOpeningStock = angular.copy($scope.lastStorageMedicineOpeningStock);
            $scope.Filter.searchData = "";
            frmMedicineOpeningStock.$setPristine();
            CommonFunctions.ScrollToTop();
        };

        //Save Opening Stock
        $scope.SaveMedicineOpeningStock = function (medicineDetails, frmMedicineOpeningStock) {
            $scope.Filter.searchData = "";
            MedicineOpeningStockService.SaveMedicineOpeningStock(medicineDetails).then(function (response) {
                if (response && response.data) {
                    var data = response.data;
                    showtoastr(data.Message, data.MessageType);
                    if (data.MessageType == messageTypes.Success) {// Success
                        $scope.GetMedicineOpeningStock();
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        };

        //#endregion
    }
})();