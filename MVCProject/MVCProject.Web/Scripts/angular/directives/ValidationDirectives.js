
// compareTo directive
angular.module("DorfKetalMVCApp").directive('compareTo', function () {
    return {
        require: "ngModel",
        scope: {
            otherModelValue: "=compareTo"
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.compareTo = function (modelValue) {
                return modelValue == scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    };
});

angular.module("DorfKetalMVCApp").directive('validateEmail', function () {
    var EMAIL_REGEXP = new RegExp(/^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$/);
    return {
        require: 'ngModel',
        restrict: '',
        link: function (scope, elm, attrs, ctrl) {            
            if (ctrl && ctrl.$validators.email) {
                ctrl.$validators.email = function (modelValue) {
                    return ctrl.$isEmpty(modelValue) || EMAIL_REGEXP.test(modelValue);
                };
            }
        }
    }
});


// validate contact #
var contactNumberPattern = /^(((((\d{3}[-]){2})|((\d{3}[ ]){2})|((\d{3}[.]){2}))\d{4})|(((((\+\d{2}[ ])?\(\d{3}\)[ ]\d{3}-)|(\+\d{2})?(\d{6}))\d{4})))$/;
/*
Valid formats:
1234567890
123 456 7890
123-456-7890
123.456.7890
(123) 456-7890
+911234567890
+91 (123) 456-7890
*/
angular.module("DorfKetalMVCApp").directive('contactNumber', function() {
  return {
    require: 'ngModel',
    link: function(scope, elm, attrs, ctrl) {
      ctrl.$validators.contactNumber = function(modelValue, viewValue) {
        if (ctrl.$isEmpty(modelValue)) {
          // consider empty models to be valid
          return true;
        }

        if (contactNumberPattern.test(viewValue)) {
          // it is valid
          return true;
        }
        
        // it is invalid
        return false;
      };
    }
  };
});


var INTEGER_REGEXP = /^\d+$/;
angular.module("DorfKetalMVCApp").directive('integer', function() {
  return {
    require: 'ngModel',
    link: function(scope, elm, attrs, ctrl) {
      ctrl.$validators.integer = function(modelValue, viewValue) {
        if (ctrl.$isEmpty(modelValue)) {
          // consider empty models to be valid
          return true;
        }

        if (INTEGER_REGEXP.test(viewValue)) {
          // it is valid
          return true;
        }

        // it is invalid
        return false;
      };
    }
  };
});

angular.module("DorfKetalMVCApp").directive('validDecimal',function(){
  return{
    require: "ngModel",
    link: function(scope, elm, attrs, ctrl){
      var regex = /^\d{1,7}(?:(\.\d{1,4}))?$/;
      var validator = function(value){
        if(value) {
            ctrl.$setValidity('validDecimal', regex.test(value));
            return value;
        }
      };

      ctrl.$parsers.unshift(validator);
      ctrl.$formatters.unshift(validator);
    }
  };
});