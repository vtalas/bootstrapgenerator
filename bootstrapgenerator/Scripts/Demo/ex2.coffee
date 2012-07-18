module = angular.module("testmodule", [  ])


module.run () ->
  console.log("run")


module.directive "test", () ->
  testCtrl  = ($scope)->
    console.log("testCtrl")
    $scope.aaa = "xxx"

  directiveDefinitionObject=
    controller : testCtrl
    link: (scope, el, tAttrs, controller) ->
      console.log("test...", controller, scope)

#      scope.$watch(
#        (ss)->
#          console.log("digest called", ss)
#
#        (newvalue, oldvalue)->
#          console.log("ccdkhbfksdjbfsdjkbf",newvalue, oldvalue)
#      )

      scope.$watch("aaa",(newvalue, oldvalue)->
          console.log("aaa",newvalue, oldvalue)
      )
      scope.$apply()


  directiveDefinitionObject


module.directive "markdown", () ->
  testCtrl  = ($scope)->
    $scope.aaa = "xxx"

  restrict: "E"
  controller : testCtrl
  compile: (tElement, tAttrs, transclude)->
    console.log("t",tElement)
    (scope, element, attrs, controller) ->
      console.log("el",element)


newtestCtrl = ($scope)->
  $scope.aaa = "newtestCtrl"
window.newtestCtrl = newtestCtrl



