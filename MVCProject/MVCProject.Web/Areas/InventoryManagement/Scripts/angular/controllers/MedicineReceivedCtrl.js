(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('MedicineOpeningStockCtrl', [
            '$scope', '$q', '$rootScope', '$ngBootbox', '$filter', 'CommonFunctions', 'CommonEnums', 'MedicineReceivedService', 'CommonService', MedicineOpeningStockCtrl
    ]);

    function MedicineOpeningStockCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, MedicineReceivedService, CommonService) {

        //#region Varriables

        $scope.Initstatus = {
            opened: false
        };

        $scope.DefaultExpiryDate = new Date("01-01-1900");

        $scope.options = {
            showAddMedicineDetails: false
        };

        $scope.MaxDate = null;

        $scope.Filter = {
            GroupId: 0,
            ReceiptNo: '',
            ReceivedFrom: '',
            OpeningStockEntryDate: null,
            Description: '',
            SiteLevelId: 0,
            SiteName: "",
        };

        $scope.medicineOpeningStock = [];

        $scope.lastStorageMedicineOpeningStock = $scope.lastStorageMedicineOpeningStock || {};

        //True if records for particuler date is already exist in Database
        $scope.IsEdit = false;

        //Auto Complete URL for Medicine Details based on Medicine Name for filling Grid Details.
        $scope.medicineAutoCompleteUrl = MedicineReceivedService.MedicineAutoCompleteUrl();

        //Auto Complete URL for Medicine Received From Details based on Received From Type.
        $scope.medicineReceivedFromAutoCompleteUrl = MedicineReceivedService.MedicineReceivedFromAutoCompleteUrl();

        //#endregion

        //Get sites
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Filter.SiteLevelId = level.LevelId;
                $scope.Filter.SiteName = level.Name;
                $scope.GetMedicineReceivedDetails();
            }
        };

        //#region On Load Functions

        $scope.Init = function (GroupId) {
            $scope.Filter.GroupId = GroupId;
            if (GroupId > 0) {
                $scope.IsEdit = true;
            }

            $q.all([
               CommonService.GetServerDate(),
               MedicineReceivedService.GetMedicineReceivedDetails($scope.Filter)
            ]).then(function (response) {
                //Server Date
                if (response[0].data.MessageType == 1) {
                    $scope.Filter.OpeningStockEntryDate = moment(new Date(response[0].data.Result)).toDate();
                    $scope.MaxDate = moment(new Date(response[0].data.Result)).toDate();
                } else if (response[0].data.MessageType == 4) {
                    $scope.Filter.OpeningStockEntryDate = '';
                }

                //Medicine Opening Stock Details
                if (response[1].data.MessageType == 1) {
                    $scope.medicineOpeningStock = response[1].data.Result;
                    if ($scope.medicineOpeningStock.length > 0) {
                        $scope.lastStorageMedicineOpeningStock = angular.copy($scope.medicineOpeningStock);
                        $scope.Filter.GroupId = $scope.medicineOpeningStock[0].GroupId;
                        $scope.Filter.ReceiptNo = $scope.medicineOpeningStock[0].ReceiptNo;
                        $scope.Filter.ReceivedFrom = $scope.medicineOpeningStock[0].ReceivedFrom;
                        $scope.Filter.OpeningStockEntryDate = moment(new Date($scope.medicineOpeningStock[0].ReceivedDate)).toDate();
                        $scope.Filter.SiteLevelId = $scope.medicineOpeningStock[0].SiteLevelId
                        $scope.IsEdit = true;
                    }
                    else {
                        $scope.IsEdit = false;
                        //No Record Found Redirect to Dashboard
                    }
                } else if (response[1].data.MessageType == 4) {
                    $scope.medicineOpeningStock = [];
                }
            });
        }

        //#endregion

        //#region Filter Change Functions

        $scope.SetFilter = function () {
            if ($.trim($scope.Filter.ReceiptNo) != '' && $.trim($scope.Filter.ReceivedFrom) != '' && $.trim($scope.Filter.OpeningStockEntryDate) != '') {
                if ($scope.IsEdit) {
                    $scope.medicineOpeningStock = [];
                    $scope.lastStorageMedicineOpeningStock = [];
                    $scope.Filter.GroupId = 0;
                }

                $scope.GetMedicineReceivedDetails();
            }
        }

        $scope.GetMedicineReceivedDetails = function () {
            MedicineReceivedService.GetMedicineReceivedDetails($scope.Filter).then(function (response) {
                $rootScope.isAjaxLoadingChild = true;
                if (response && response.data) {
                    var data = response.data;
                    if (!angular.isUndefined(data)) {
                        if (data.MessageType == messageTypes.Success) {// Success

                            if (data.Result.length > 0) {
                                $scope.medicineOpeningStock = angular.copy(data.Result);
                                $scope.lastStorageMedicineOpeningStock = angular.copy($scope.medicineOpeningStock);
                                $scope.Filter.GroupId = $scope.medicineOpeningStock[0].GroupId;
                                $scope.Filter.ReceiptNo = $scope.medicineOpeningStock[0].ReceiptNo;
                                $scope.Filter.ReceivedFrom = $scope.medicineOpeningStock[0].ReceivedFrom;
                                $scope.Filter.OpeningStockEntryDate = moment(new Date($scope.medicineOpeningStock[0].ReceivedDate)).toDate();
                                $scope.Filter.SiteLevelId = $scope.medicineOpeningStock[0].SiteLevelId;
                                $scope.IsEdit = true;
                            }
                            else {
                                $scope.IsEdit = false;
                            }
                        } else {
                            showtoastr(data.Message, data.MessageType);
                            $scope.Init();
                        }
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        }

        //Medicine Name Changed in Autocomplete For Add New Mode.
        $scope.SetMedicineReceivedFrom = function (selected) {
            if (angular.isDefined(selected)) {
                console.log(selected);
                if (selected.originalObject.ReceivedFrom) {
                    $scope.Filter.ReceivedFrom = selected.originalObject.ReceivedFrom;
                }
                else
                {
                    $scope.Filter.ReceivedFrom = selected.originalObject;
                }
                $scope.SetFilter();
            }
        };

        //#endregion

        //#region Grid Functions

        //Medicine Name Changed in Autocomplete For Add New Mode.
        $scope.ChangedMedicineNameAdd = function (selected, actionToAdd, id) {
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
            if (parseFloat(actionToAdd.Price) > 0 && parseInt(actionToAdd.Quantity) > 0) {

                $scope.IsSave = true;
                $scope.NoFound = false;

                if (!actionToAdd.ExpiryDate || actionToAdd.ExpiryDate == '') {
                    actionToAdd.IsDefaultExpiry = true;
                    actionToAdd.ExpiryMonth = moment($scope.DefaultExpiryDate).format('M');
                    actionToAdd.ExpiryDate = moment($scope.DefaultExpiryDate).endOf('month').toDate();
                }
                else {
                    actionToAdd.IsDefaultExpiry = false;
                    actionToAdd.ExpiryMonth = moment(actionToAdd.ExpiryDate).format('M');
                    actionToAdd.ExpiryDate = moment(actionToAdd.ExpiryDate).endOf('month').toDate();
                    if (Date.parse(actionToAdd.ExpiryDate) < Date.parse(new Date())) {
                        toastr.warning(ExpiryDateLessThenCurrentWarningMessaage, warningTitle);
                    }
                }

                var isDuplicateExist = $filter("filter")(medicineOpeningStock, { MedicineId: actionToAdd.MedicineId, ExpiryMonth: actionToAdd.ExpiryMonth, IsActive: true }, true).length;

                if (isDuplicateExist > 0) {
                    toastr.warning(DuplicateMedicineDetails, warningTitle);
                    return;
                }

                if (frmMedicineDetailsAdd.$valid) {
                    $scope.actionToAdd = {
                        MedicineReceivedId: 0,
                        ReceiptNo: $scope.Filter.ReceiptNo,
                        ReceivedDate: moment($scope.Filter.OpeningStockEntryDate).format($rootScope.apiDateFormat),
                        ReceivedFrom: $scope.Filter.ReceivedFrom,
                        MedReceivedSourceId: 2,
                        MedicineId: 0,
                        MedicineCode: '',
                        MedicineName: '',
                        MedicineType: '',
                        ExpiryDate: null,
                        ExpiryMonth: '',
                        Price: '',
                        Quantity: '',
                        IsActive: true,
                        IsExpiryRequired: true
                    };

                    if (medicineOpeningStock == null)
                        medicineOpeningStock = [];

                    //actionToAdd.ExpiryMonth = moment(actionToAdd.ExpiryDate).format('M');
                    //actionToAdd.ExpiryDate = moment(actionToAdd.ExpiryDate).endOf('month').toDate();
                    actionToAdd.IsActive = true;
                    actionToAdd.MedicineReceivedId = 0;
                    actionToAdd.MedReceivedSourceId = 2;
                    actionToAdd.ReceiptNo = $scope.Filter.ReceiptNo;
                    actionToAdd.ReceivedDate = moment($scope.Filter.OpeningStockEntryDate).format($rootScope.apiDateFormat);
                    actionToAdd.ReceivedFrom = $scope.Filter.ReceivedFrom;
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
            if (parseFloat(actionToEdit.Price) > 0 && parseInt(actionToEdit.Quantity) > 0) {
                var index = medicineOpeningStock.indexOf(item);
                $scope.medicineOpeningStockForDuplicateCheck = angular.copy(medicineOpeningStock);
                $scope.medicineOpeningStockForDuplicateCheck.splice(index, 1)

                if (!actionToEdit.ExpiryDate || actionToEdit.ExpiryDate == '') {
                    actionToEdit.IsDefaultExpiry = true;
                    actionToEdit.ExpiryMonth = moment($scope.DefaultExpiryDate).format('M');
                    actionToEdit.ExpiryDate = moment($scope.DefaultExpiryDate).endOf('month').toDate();
                }
                else {
                    actionToEdit.IsDefaultExpiry = false;
                    actionToEdit.ExpiryMonth = moment(actionToEdit.ExpiryDate).format('M');
                    actionToEdit.ExpiryDate = moment(actionToEdit.ExpiryDate).endOf('month').toDate();
                    if (Date.parse(actionToEdit.ExpiryDate) < Date.parse(new Date())) {
                        toastr.warning(ExpiryDateLessThenCurrentWarningMessaage, warningTitle);
                    }
                }

                var isDuplicateExist = $filter("filter")($scope.medicineOpeningStockForDuplicateCheck, { MedicineId: actionToEdit.MedicineId, ExpiryMonth: actionToEdit.ExpiryMonth, IsActive: true }, true).length;

                if (isDuplicateExist > 0) {
                    toastr.warning(DuplicateMedicineDetails, warningTitle);
                    return;
                }

                if (frmMedicineDetailsEdit.$valid) {
                    if (actionToEdit.MedicineReceivedId > 0) {
                        MedicineReceivedService.GetMinimumQtyForReceivedStock(actionToEdit.GroupId, actionToEdit.MedicineId, moment(actionToEdit.ExpiryDate).endOf('month').format('MMMM-YYYY')).then(function (response) {
                            $rootScope.isAjaxLoadingChild = true;
                            if (response && response.data) {
                                var data = response.data;
                                if (!angular.isUndefined(data)) {
                                    if (data.MessageType == messageTypes.Success) {// Success
                                        if (parseInt(actionToEdit.Quantity) >= parseInt(data.Result) || data.Result == -1) {
                                            //actionToEdit.ExpiryMonth = moment(actionToEdit.ExpiryDate).format('M');
                                            //actionToEdit.ExpiryDate = moment(actionToEdit.ExpiryDate).endOf('month').toDate();
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
                    else
                    {
                        //actionToEdit.ExpiryMonth = moment(actionToEdit.ExpiryDate).format('M');
                        //actionToEdit.ExpiryDate = moment(actionToEdit.ExpiryDate).endOf('month').toDate();
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
            var index = medicineDetails.indexOf(item);
            medicineDetails[index].IsActive = false;
        },
        function () {
        });
        };
        //#endregion

        //#region Form Submission Functions

        //Clear form data.
        $scope.ClearForm = function (frmMedicineReceived) {

            $scope.Filter = {
                GroupId: 0,
                ReceiptNo: '',
                ReceivedFrom: '',
                OpeningStockEntryDate: null,
                Description: ''
            };

            $scope.IsEdit = false;
            $scope.Filter.OpeningStockEntryDate = angular.copy($scope.MaxDate);
            $scope.medicineOpeningStock = [];
            if ($scope.options.showAddMedicineDetails) {
                $scope.options.showAddMedicineDetails = false;
            }
            frmMedicineReceived.$setPristine();
            $scope.GetMedicineReceivedDetails();
            CommonFunctions.ScrollToTop();
        };

        //Reset form data.
        $scope.ResetForm = function (frmMedicineReceived) {
            $scope.medicineOpeningStock = angular.copy($scope.lastStorageMedicineOpeningStock);
            frmMedicineReceived.$setPristine();
            CommonFunctions.ScrollToTop();
        };

        //Save Opening Stock
        $scope.SaveMedicineReceivedDetails = function (medicineDetails, frmMedicineReceived) {
            console.log(medicineDetails);
            MedicineReceivedService.SaveMedicineReceivedDetails(medicineDetails).then(function (response) {
                if (response && response.data) {
                    var data = response.data;
                    showtoastr(data.Message, data.MessageType);
                    if (data.MessageType == messageTypes.Success) {// Success
                        $scope.GetMedicineReceivedDetails();
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        };

        //#endregion
    }
})();