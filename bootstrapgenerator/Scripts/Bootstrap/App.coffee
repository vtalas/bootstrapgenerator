###
@reference ../jquery-1.7.2.js
@reference ../angular.js
###

module = angular.module("bootstrapApp", [  ])

module.config ($routeProvider,$provide,$filterProvider) ->
  $filterProvider.register('nameType', ()->
    (data, type, name)->
      x = []
      #vyfiltruj to co je 'type' a zaroven nezacina na '@'
      for key,prop of data
        x.push("@"+key) if prop.type is type
      x
  )
  $filterProvider.register('typevalue', ()->
    (data, type, type2)->
      x = {}
      #vyfiltruj to co je 'type' a zaroven nezacina na '@'
      for key,prop of data
        x[key] = prop.value if prop.type is type || prop.type is type2 && prop.value[0] isnt "@"
      x
  )
  $filterProvider.register('refs', ()->
    (data, type)->
      x = {}
      #vyfiltruj to co je 'type' a zaroven ZACINA na '@'
      for key,prop of data
        x[key] = prop.value if prop.type is type && prop.value[0] is "@"
      x
  )

#  $provide.factory("datajson", ($http)->
#    d = $.parseJSON angular.element("html").data("modeldata")
#  )

  $provide.factory("datajsonPromise", ($http)->
    x = $http(
      method: "POST"
      url: "/bootstrap/JSONDATA"
#      data: $scope.data
    )
    x
  )

#  $provide.factory("colorsonly", ($filter,datajsonPromise, $q)->
#    defer = $q.defer()
#    datajsonPromise.then((rs)->
#      defer.resolve($filter("typevalue")(rs.data, "color"))
#    )
#    defer.promise
#  )

  $routeProvider
  .when("/csstest", controller: appCtrl, templateUrl : "/Content/bootstrap_templates/csstest.html"  )
  .when("/bootswatch", controller: appCtrl ,templateUrl : "/Content/bootstrap_templates/bootswatch.html")
  .when("/components", controller: appCtrl , templateUrl : "/Content/bootstrap_templates/components.html"
  ).otherwise redirectTo: '/csstest'



module.directive "colorpicker", ($filter) ->
  1


module.directive "bootstrapelem", ($filter, datajsonPromise) ->
  directiveDefinitionObject =
    require : '?ngModel',
    link: (scope, el, tAttrs, controller) ->
      jsondata = {}
      datajsonPromise.then((rs)->jsondata = rs.data)

      controller.$setViewValue = (val)->
        basiccolors=$filter("nameType")(jsondata, "basiccolor" )
        el.typeahead({
                          source:basiccolors,
                          updater: (val)->
                            colorsonly =  $filter("typevalue")(jsondata, "color","basiccolor" )
                            r = colorsonly[val.substr(1)]
                            el.css("background", r if r)

                            controller.$viewValue.value = val
                          items:11
                          });
        controller.$viewValue.value = val

      controller.$render = ()->
        if !controller.$viewValue
          el.val("loading...")
          return

        a = controller.$viewValue
        el.val(a.value);

        switch a.type
          when "color","basiccolor"
            el.width "80px"
            if a.value[0] != "@"
              el.css "background", a.value

            if a.value[0] == "@"
              colorsonly =  $filter("typevalue")(scope.data, "color","basiccolor" )
              r = colorsonly[a.value.substr(1)]
              el.css "background", r if r

  directiveDefinitionObject

appCtrl = ($scope, $http) ->
  # Type here!

#  days =
#    monday: 1
#    tuesday: 2
#    wednesday: 3
#    thursday: 4
#    friday: 5
#    saturday: 6
#    sunday: 7
#
#  for xx of days
#    console.log xx


#$scope.colorsrefonly = colorsrefonly
1



