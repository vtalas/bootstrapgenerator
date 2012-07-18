(function() {
  var module, newtestCtrl;
  module = angular.module("testmodule", []);
  module.run(function() {
    return console.log("run");
  });
  module.directive("test", function() {
    var directiveDefinitionObject, testCtrl;
    testCtrl = function($scope) {
      console.log("testCtrl");
      return $scope.aaa = "xxx";
    };
    directiveDefinitionObject = {
      controller: testCtrl,
      link: function(scope, el, tAttrs, controller) {
        console.log("test...", controller, scope);
        scope.$watch("aaa", function(newvalue, oldvalue) {
          return console.log("aaa", newvalue, oldvalue);
        });
        return scope.$apply();
      }
    };
    return directiveDefinitionObject;
  });
  module.directive("markdown", function() {
    var testCtrl;
    testCtrl = function($scope) {
      return $scope.aaa = "xxx";
    };
    return {
      restrict: "E",
      controller: testCtrl,
      compile: function(tElement, tAttrs, transclude) {
        console.log("t", tElement);
        return function(scope, element, attrs, controller) {
          return console.log("el", element);
        };
      }
    };
  });
  newtestCtrl = function($scope) {
    return $scope.aaa = "newtestCtrl";
  };
  window.newtestCtrl = newtestCtrl;
}).call(this);
