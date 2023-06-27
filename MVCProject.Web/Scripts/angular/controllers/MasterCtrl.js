(function () {
	'use strict';

	angular.module("MVCApp").controller('MasterCtrl', [
			'$scope', '$rootScope', '$uibModal', '$document', '$window', '$timeout', '$sce', 'AccountService', 'CommonService', 'CommonFunctions', 'CommonEnums', MasterCtrl
	]);

	//BEGIN MasterCtrl
	function MasterCtrl($scope, $rootScope, $uibModal, $document, $window, $timeout, $sce, AccountService, CommonService, CommonFunctions, CommonEnums) {
		$scope.askPasswordResult = {};
		$rootScope.MasterAttachmentsCount = 0;
		$scope.isLogoLoaded = false;
		$rootScope.contactNumberHtmlPopover = $sce.trustAsHtml('<b>Valid formats:</b><br>1234567890<br>123 456 7890<br>123-456-7890<br>123.456.7890<br>(123) 456-7890<br>+911234567890<br>+91 (123) 456-7890');


		//Open  Attachments Popup
		$scope.OpenAttachmentsPopup = function (moduleId, referenceId, isReadOnly, reference) {
			var modalInstance = $uibModal.open({
				animation: $scope.animationsEnabled,
				templateUrl: '/Template/_AttachmentsPopup',
				controller: "AttachmentsPopupCtrl",
				size: 'lg',
				keyboard: true,
				resolve: {
					moduleId: function () {
						return moduleId
					},
					referenceId: function () {
						return referenceId;
					},
					isReadOnly: function () {
						return isReadOnly;
					}
				}
			});

			modalInstance.result.then(function (Total) {
				if ((Total != 'undefined')) {
					if (Total.length > 0) {
						$rootScope.MasterAttachmentsCount = Total;
					} else if (Total > 0) {
						$rootScope.MasterAttachmentsCount = Total;
					}
					else {
						$rootScope.MasterAttachmentsCount = 0;
					}
				} else if (angular.isDefined(Total) && angular.isDefined(reference) && angular.isDefined(reference.AttchmentCount)) {
					reference.AttchmentCount = Total;
				}

				//toastr.success(Result.Data, successTitle);
			}, function () {
				//toastr.warning('Modal dismissed at: ' + new Date(), warningTitle);
			});
		};

		//Open  Comments Popup
		$scope.OpenCommentsPopup = function (moduleId, referenceId, isReadOnly) {
			var modalInstance = $uibModal.open({
				animation: $scope.animationsEnabled,
				templateUrl: '/Template/_CommentsPopup',
				controller: "CommentsPopupCtrl",
				size: 'lg',
				keyboard: true,
				resolve: {
					moduleId: function () {
						return moduleId;
					},
					referenceId: function () {
						return referenceId;
					},
					isReadOnly: function () {
						return isReadOnly;
					}
				}
			});

			modalInstance.result.then(function (Result) {
				//toastr.success(Result.Data, successTitle);
			}, function () {
				//toastr.warning('Modal dismissed at: ' + new Date(), warningTitle);
			});
		};

		//Open  Notes Popup
		$scope.OpenNotesPopup = function (moduleId, referenceId, isReadOnly) {
			var modalInstance = $uibModal.open({
				animation: $scope.animationsEnabled,
				templateUrl: '/Template/_NotesPopup',
				controller: "NotesPopupCtrl",
				size: 'lg',
				keyboard: true,
				resolve: {
					moduleId: function () {
						return moduleId;
					},
					referenceId: function () {
						return referenceId;
					},
					isReadOnly: function () {
						return isReadOnly;
					}
				}
			});

			modalInstance.result.then(function (Result) {
				//toastr.success(Result.Data, successTitle);
			}, function () {
				//toastr.warning('Modal dismissed at: ' + new Date(), warningTitle);
			});
		};

		//Open Message Popup
		$scope.OpenMassagePopup = function (text, header) {
			var modalInstance = $uibModal.open({
				animation: $scope.animationsEnabled,
				templateUrl: '/Template/_MessagePopup',
				controller: "MessagePopupCtrl",
				size: 'lg',
				keyboard: true,
				resolve: {
					Text: function () {
						return text;
					},
					Header: function () {
						return header;
					}
				}
			});

			modalInstance.result.then(function (Result) {
				//toastr.success(Result.Data, successTitle);
			}, function () {
				//toastr.warning('Modal dismissed at: ' + new Date(), warningTitle);
			});
		};


		$scope.GetProfilePicturePath = function (companyDB, profilePicture) {
			var url = "/Content/images/avatar-1.jpg";
			if (profilePicture) {
				url = "{0}/{1}/Employee/{2}".format($rootScope.apiAttachmentsURL, companyDB, profilePicture);
			}
			return url;
		}


		// Check user session
		$scope.CheckUserSession = function () {
			$rootScope.userContext = userContext;
			$rootScope.userContext.ProfilePicturePath = $scope.GetProfilePicturePath(userContext.CompanyDB, userContext.ProfilePicturePath);
			$rootScope.userContext.ApplicationLogo = userContext.ApplicationLogo || "/Content/images/company-logo.png";
			if (sessionStorage.getItem("IsSmallMenu") != null) {
				$scope.isSmallMenu = true;
			}
			$rootScope.userCurrentRoles = roles;
			$scope.isLogoLoaded = true;
		};

		// Change Role
		$scope.ChangeRole = function (roleId) {
			AccountService.ChangeRole(roleId).then(function (res) {
				if (res) {
					var data = res.data;
					if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
						CommonService.CreateSession(data.Result).then(function (response) {
							$rootScope.isAjaxLoadingMaster = true;
							CommonFunctions.RedirectToDefaultUrl();
						});
					}
					else {
						toastr.error(data.Message);
					}
				}
			});
		}

		$scope.getCurrentRole = function () {
			console.log(userContext)
			AccountService.GetCurrentRole(userContext.RoleId).then(function (res) {
				if (res) {
					if (res.data.MessageType == messageTypes.Success) {
						$scope.role = res.data.Result;
					}
					else if (res.data.MessageType == messageTypes.Error) {
						toastr.error(res.data.Message, errorTitle);
					}
				}
			})
		}

		// Set Toggle Menu
		$scope.SetToggleMenu = function () {
			// To resize dashboard chart
			$scope.$broadcast('resize');
			CommonFunctions.SetFixHeader();
			if (sessionStorage.getItem("IsSmallMenu") == null) {
				sessionStorage.setItem("IsSmallMenu", "Yes");
				$scope.isSmallMenu = true;
			} else {
				sessionStorage.removeItem("IsSmallMenu");
				$scope.isSmallMenu = false;
			}
		};

		// BEGIN Synchronize Reporting Data
		$scope.SynchronizeReportingData = function () {
			CommonService.SynchronizeReportingData().then(function (res) {
				if (res) {
					var data = res.data;
					if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
						toastr.success(data.Message);
						setInterval(function () {
							if (window.location.pathname.toLowerCase() == ("/Dashboard/Dashboard").toLowerCase()) {
								window.location.reload();
							}
						}, 2000);
					}
					else {
						toastr.error(data.Message);
					}
				}
			});
		};


		// BEGIN Synchronize Reporting Data
		$scope.UserNotificationSetting = function () {
			window.location.href = "/Configuration/UserwiseNotificationSettings";
		};


		// BEGIN Log out user
		$scope.Logout = function () {
			AccountService.DoLogOut().then(function (res) {
				if (res) {
					var data = res.data;
					CommonFunctions.RedirectToLoginPage(false);
				}
			});
		};
		//END Log out user
		// Logout if Idle Start     
		$scope.isActiveWindow = true;
		$scope.TimeOut_Thread = null;
		$scope.TimeOut_Resetter = function () {
			// Stop the pending timeout
			$timeout.cancel($scope.TimeOut_Thread);
			// Reset the timeout
			// CommonService.ResetSession().then(function (response) { });
			var timeoutMinutes = 60;
			$scope.TimeOut_Thread = $timeout(function () {
				if ($scope.isActiveWindow) {
					sessionStorage.setItem("LoggedOut", "Yes");
					$scope.Logout();
				}
			}, timeoutMinutes * 60 * 1000);
		};
		var bodyElement = angular.element($document);
		angular.forEach(['keydown', 'keyup', 'click', 'mousemove', 'DOMMouseScroll', 'mousewheel', 'mousedown', 'touchstart', 'touchmove', 'scroll', 'focus'],
		function (EventName) {
			bodyElement.bind(EventName, function (e) { $scope.TimeOut_Resetter(); });
		});
		$window.onfocus = function () {
			if (sessionStorage.getItem("LoggedOut") != null) {
				//sessionStorage.removeItem("LoggedOut");
				//window.location = '/Account/Login';
			} else {
				$scope.isActiveWindow = true;
				$scope.TimeOut_Resetter();
			}
		}
		$window.onblur = function () {
			$scope.isActiveWindow = false;
		}
		// Logout if Idle End 

		//Start Open Ask Password Modal
		$scope.openAskPasswordPopup = function (callback, data, frm) {
			var size = '';
			var keyboard = false;

			if (callback != null) {
				size = 'lg';
				keyboard = true;
			}
			var askPasswordModalInstance = $uibModal.open({
				templateUrl: '/Template/_AskPassword',
				scope: $scope,
				controller: AskPasswordCtrl,
				size: size,
				backdrop: 'static',
				keyboard: keyboard,
				resolve: {
				}
			});

			askPasswordModalInstance.rendered.then(function () {
				$rootScope.isAjaxLoadingMaster = false;
			});

			askPasswordModalInstance.result.then(function (result) {
				$scope.askPasswordResult = result;
				if (callback && result.isSuccess) {
					callback(data, frm);
				}
			});
		}
		//End Open Ask Password Modal

		//Start Ask Password Modal Controller
		var AskPasswordCtrl = ["$scope", "$rootScope", "$uibModalInstance", function ($scope, $rootScope, $uibModalInstance) {

			//$rootScope.autofocus();
			$scope.remarks = "";
			$scope.userDetail = {};
			$scope.result = {
				isSuccess: false,
				remarks: ''
			};


			$scope.SaveActionToPerform = function (userDetail, frmModel) {
				$scope.result.Remarks = userDetail.remarks;
				$scope.result.isSuccess = true;
				$uibModalInstance.close($scope.result);
			};
			$scope.cancelActionToPerform = function () {
				$uibModalInstance.close($scope.result);
			};
		} ];
		//End Ask Password Modal Controller

		// Begin open popup to  reminder and escalation settings
		$scope.OpenPopupToSetReminder = function (title, EmailReminder, callBack) {
			var modalInstance = $uibModal.open({
				animation: $scope.animationsEnabled,
				templateUrl: '/Template/_SetReminderPopup',
				controller: "SetReminderPopupCtrl",
				size: 'lg',
				keyboard: true,
				backdrop: 'static',
				resolve: {
					EmailReminder: function () {
						return angular.copy(EmailReminder);
					},
					title: function () {
						return title;
					}
				}
			});

			modalInstance.rendered.then(function () {
				$rootScope.isAjaxLoadingMaster = false;
			});

			modalInstance.result.then(function (result) {
				//toastr.success(Result.Data, successTitle);
				if (callBack)
					callBack(result);
			}, function () {
				//toastr.warning('Modal dismissed at: ' + new Date(), warningTitle);
			});
		}
		// Begin open popup to  reminder and escalation settings


		// begin open popup to select area map
		$scope.OpenPopupToSelectAreaMap = function (title, areaDetailScope, File) {
			var modalInstance = $uibModal.open({
				animation: $scope.animationsEnabled,
				templateUrl: '/Template/_SelectAreaMapPopup',
				controller: "SelectAreaMapPopupCtrl",
				size: 'lg',
				keyboard: true,
				backdrop: 'static',
				resolve: {
					AreaDetailScope: function () {
						return areaDetailScope
					},
					File: function () {
						return File
					},
					title: function () {
						return title;
					}
				}
			});

			modalInstance.rendered.then(function () {
				$rootScope.isAjaxLoadingMaster = false;
			});

			modalInstance.result.then(function (selectedItem) {
				areaDetailScope.AreaX1 = selectedItem.AreaX1;
				areaDetailScope.AreaY1 = selectedItem.AreaY1;
				areaDetailScope.AreaX2 = selectedItem.AreaX2;
				areaDetailScope.AreaY2 = selectedItem.AreaY2;
			}, function () {
				//toastr.warning('Modal dismissed at: ' + new Date(), warningTitle);
			});
		}
		// end open popup to select area map




	}
	//END MasterCtrl

})();