(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('DailyMedicineConsumptionCtrl', [
            '$scope', '$q', '$ngBootbox', '$rootScope', '$filter', 'CommonFunctions', 'CommonEnums', 'DailyMedicineConsumptionService', 'CommonService', DailyMedicineConsumptionCtrl
    ]);

    function DailyMedicineConsumptionCtrl($scope, $q, $ngBootbox, $rootScope, $filter, CommonFunctions, CommonEnums, DailyMedicineConsumptionService, CommonService) {

        //#region Varriables

        $scope.Initstatus = {
            opened: false
        };

        $scope.maxChar = 8000;

        $scope.options = {
            showAddMedicineDetails: false
        };

        $scope.Filter = {
            OpeningStockEntryDate: null,
            Description: '',
            SiteLevelId: 0,
            SiteName: ""
        };

        $scope.medicineOpeningStock = [];

        $scope.lastStorageMedicineOpeningStock = $scope.lastStorageMedicineOpeningStock || {};

        //True if records for particuler date is already exist in Database
        $scope.IsEdit = false;

        //#endregion

        //Get sites
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Filter.SiteLevelId = level.LevelId;
                $scope.Filter.SiteName = level.Name;
                $scope.GetDailyConsumptionStock();
            }
        };

        //#region On Load Functions

        $scope.Init = function () {
            $q.all([
               CommonService.GetServerDate(),
               DailyMedicineConsumptionService.GetDailyConsumptionStock($scope.Filter)
            ]).then(function (response) {
                //Server Date
                if (response[0].data.MessageType == 1) {
                    $scope.Filter.OpeningStockEntryDate = moment(new Date(response[0].data.Result)).toDate();
                    $scope.MaxDate = moment(new Date(response[0].data.Result)).toDate();

                    //Auto Complete URL for Medicine Details based on Medicine Name for filling Grid Details.
                    $scope.AutoCompleteParams = { ConsumptionDate: $scope.Filter.OpeningStockEntryDate };
                    $scope.medicineAutoCompleteUrl = DailyMedicineConsumptionService.MedicineAutoCompleteUrl($scope.Filter.OpeningStockEntryDate).url;

                } else if (response[0].data.MessageType == 4) {
                    $scope.Filter.OpeningStockEntryDate = '';
                }

                //Medicine Opening Stock Details
                if (response[1].data.MessageType == 1) {
                    $scope.medicineOpeningStock = response[1].data.Result;
                    if ($scope.medicineOpeningStock.length > 0) {
                        $scope.lastStorageMedicineOpeningStock = angular.copy($scope.medicineOpeningStock);
                        $scope.Filter.Description = $scope.medicineOpeningStock[0].Description;
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

        $scope.GetDailyConsumptionStock = function () {
            $scope.Filter.Description = '';

            $scope.AutoCompleteParams = { ConsumptionDate: $scope.Filter.OpeningStockEntryDate };
            $scope.medicineAutoCompleteUrl = DailyMedicineConsumptionService.MedicineAutoCompleteUrl($scope.Filter.OpeningStockEntryDate).url;

            DailyMedicineConsumptionService.GetDailyConsumptionStock($scope.Filter).then(function (response) {
                $rootScope.isAjaxLoadingChild = true;
                if (response && response.data) {
                    var data = response.data;
                    if (!angular.isUndefined(data)) {
                        if (data.MessageType == messageTypes.Success) {// Success
                            $scope.medicineOpeningStock = angular.copy(data.Result);
                            if ($scope.medicineOpeningStock.length > 0) {
                                $scope.lastStorageMedicineOpeningStock = angular.copy($scope.medicineOpeningStock);
                                $scope.Filter.Description = $scope.medicineOpeningStock[0].Description;
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

        //#endregion

        //#region Grid Functions

        //Medicine Name Changed in Autocomplete For Add New Mode.
        $scope.ChangedMedicineNameAdd = function (selected, actionToAdd, id) {
            if (angular.isDefined(selected) && selected.originalObject.MedicineId > 0) {
                actionToAdd.MedicineId = selected.originalObject.MedicineId;
                actionToAdd.MedicineType = selected.originalObject.MedType;
                actionToAdd.MedicineCode = selected.originalObject.MedCode;
                actionToAdd.MedicineName = selected.originalObject.MedName;
                actionToAdd.ExpiryDate = moment(selected.originalObject.ExpiryDate).endOf('month').toDate();
                //actionToAdd.Quantity = selected.originalObject.Quantity;
                actionToAdd.MaxQuantity = selected.originalObject.Quantity;
                actionToAdd.IsDefaultExpiry = selected.originalObject.IsDefaultExpiry;
            }
            else {
                actionToAdd.MedicineId = 0;
                actionToAdd.MedicineType = '';
                actionToAdd.MedicineCode = '';
                actionToAdd.MedicineName = '';
                actionToAdd.ExpiryDate = '';
                actionToAdd.Quantity = '';
                actionToAdd.MaxQuantity = '';
                actionToAdd.IsDefaultExpiry = false;
                $scope.$broadcast('angucomplete-alt:clearInput');
            }
        };

        // Add New Medicine Details
        $scope.AddMedicineDetails = function (frmMedicineDetailsAdd, medicineOpeningStock, actionToAdd, options) {
            if (parseInt(actionToAdd.Quantity) > 0) {
                $scope.IsSave = true;
                $scope.NoFound = false;

                if (parseInt(actionToAdd.Quantity) <= parseInt(actionToAdd.MaxQuantity)) {

                    var isDuplicateExist = $filter("filter")(medicineOpeningStock, { MedicineId: actionToAdd.MedicineId, ExpiryMonth: moment(actionToAdd.ExpiryDate).format('M'), IsActive: true }, true).length;

                    if (isDuplicateExist > 0) {
                        toastr.warning(DuplicateMedicineConsumptionDetails, warningTitle);
                        return;
                    }

                    if (frmMedicineDetailsAdd.$valid) {
                        $scope.actionToAdd = {
                            DeadStockId: 0,
                            ConsumptionEntryDate: $scope.Filter.OpeningStockEntryDate,
                            ReceivedFrom: '',
                            MedReceivedSourceId: 1,
                            MedicineId: 0,
                            MedicineCode: '',
                            MedicineName: '',
                            MedicineType: '',
                            ExpiryDate: null,
                            ExpiryMonth: '',
                            //Price: '',
                            Quantity: '',
                            MaxQuantity: '',
                            IsActive: true,
                            Description: $scope.Filter.Description
                        };

                        if (medicineOpeningStock == null)
                            medicineOpeningStock = [];

                        actionToAdd.ExpiryMonth = moment(actionToAdd.ExpiryDate).format('M');
                        actionToAdd.ExpiryDate = moment(actionToAdd.ExpiryDate).endOf('month').toDate();
                        actionToAdd.IsActive = true;
                        actionToAdd.DeadStockId = 0;
                        actionToAdd.MedReceivedSourceId = 1;
                        actionToAdd.ConsumptionEntryDate = $scope.Filter.OpeningStockEntryDate;
                        actionToAdd.Description = $scope.Filter.Description;
                        actionToAdd.ReceivedFrom = '';
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
                    toastr.warning(MaxQuantityReached +': '+ actionToAdd.MaxQuantity, warningTitle);
                }
            }
            else {
                toastr.warning(QtyGreaterThanZeroRequired, warningTitle);
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
                actionToEdit.ExpiryDate = moment(selected.originalObject.ExpiryDate).endOf('month').toDate();
                //actionToEdit.Quantity = selected.originalObject.Quantity
                actionToEdit.MaxQuantity = selected.originalObject.Quantity;
                actionToEdit.IsDefaultExpiry = selected.originalObject.IsDefaultExpiry;
            }
            else {
                actionToEdit.MedicineId = 0;
                actionToEdit.MedicineType = '';
                actionToEdit.MedicineCode = '';
                actionToEdit.MedicineName = '';
                actionToEdit.ExpiryDate = '';
                actionToEdit.Quantity = '';
                actionToEdit.MaxQuantity = '';
                actionToEdit.IsDefaultExpiry = false;
                $scope.$broadcast('angucomplete-alt:clearInput');
            }
        };

        // Edit Medicine Details
        $scope.EditMedicineDetails = function (item, actionToEdit, showHideAction) {
            showHideAction.isEdit = !showHideAction.isEdit;
            item.ExpiryDate = new Date(item.ExpiryDate);
            item.SiteLevelId = $scope.Filter.SiteLevelId;
            angular.copy(item, actionToEdit);
        };

        //Update Medicine Details
        $scope.UpdateMedicineDetails = function (frmMedicineDetailsEdit, medicineOpeningStock, item, actionToEdit, showHideAction) {

            if (parseInt(actionToEdit.Quantity) > 0) {
                if (parseInt(actionToEdit.Quantity) <= parseInt(actionToEdit.MaxQuantity)) {
                    var index = medicineOpeningStock.indexOf(item);
                    $scope.medicineOpeningStockForDuplicateCheck = angular.copy(medicineOpeningStock);
                    $scope.medicineOpeningStockForDuplicateCheck.splice(index, 1)

                    var isDuplicateExist = $filter("filter")($scope.medicineOpeningStockForDuplicateCheck, { MedicineId: actionToEdit.MedicineId, ExpiryMonth: moment(actionToEdit.ExpiryDate).format('M'), IsActive: true }, true).length;

                    if (isDuplicateExist > 0) {
                        toastr.warning(DuplicateMedicineConsumptionDetails, warningTitle);
                        return;
                    }

                    if (frmMedicineDetailsEdit.$valid) {
                        actionToEdit.ExpiryMonth = moment(actionToEdit.ExpiryDate).format('M');
                        actionToEdit.ExpiryDate = moment(actionToEdit.ExpiryDate).endOf('month').toDate();
                        angular.copy(actionToEdit, item);
                        showHideAction.isEdit = !showHideAction.isEdit;
                    }
                }
                else {
                    toastr.warning(MaxQuantityReached + ': ' + actionToEdit.MaxQuantity, warningTitle);
                }
            }
            else {
                toastr.warning(QtyGreaterThanZeroRequired, warningTitle);
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
        $scope.ClearForm = function (frmMedicineOpeningStock) {

            $scope.Filter = {
                OpeningStockEntryDate: null,
                Description: ''
            };
            $scope.medicineOpeningStock = [];
            if ($scope.options.showAddMedicineDetails) {
                $scope.options.showAddMedicineDetails = false;
            }
            $scope.Filter.OpeningStockEntryDate = angular.copy($scope.MaxDate);
            frmMedicineOpeningStock.$setPristine();
            $scope.GetDailyConsumptionStock();
            CommonFunctions.ScrollToTop();
        };

        //Reset form data.
        $scope.ResetForm = function (frmMedicineOpeningStock) {
            $scope.medicineOpeningStock = angular.copy($scope.lastStorageMedicineOpeningStock);
            frmMedicineOpeningStock.$setPristine();
            CommonFunctions.ScrollToTop();
        };

        //Save Opening Stock
        $scope.SaveDailyConsumptionStock = function (medicineDetails, frmMedicineOpeningStock) {
            console.log(medicineDetails);
            angular.forEach(medicineDetails, function (item) { item.Description = $scope.Filter.Description; });
            DailyMedicineConsumptionService.SaveDailyConsumptionStock(medicineDetails).then(function (response) {
                if (response && response.data) {
                    var data = response.data;
                    showtoastr(data.Message, data.MessageType);
                    if (data.MessageType == messageTypes.Success) {// Success
                        $scope.GetDailyConsumptionStock();
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        };

        //#endregion
    }
})();