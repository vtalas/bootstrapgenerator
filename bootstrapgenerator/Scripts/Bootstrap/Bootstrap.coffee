###
@reference ../jquery-1.7.2.js
@reference ../angular.js
###

bootstrap = ($scope, $http, $element,$filter,datajsonPromise) ->

#  $scope.data = $.parseJSON $element.data("model")

  datajsonPromise.then((rs)->
    $scope.data = rs.data
  )

#  $scope.colorsrefonly = colorsonly
#  colorsonly.then((data)->
#    console.log(data)
#  )

  $scope.refreshtoken = ''

  $scope.showColorPicker = ($event,item)->

    all = $scope.data
    basiccolors=$filter("nameType")(all, "basiccolor", item.value )

    el = $event.target

    $(el).miniColors({
                    letterCase: 'uppercase',
                    change:(hex, rgb)->
                      item.value = hex
                      $(el).css("background-color", hex);
    })
    $(el).miniColors("show")

  $scope.refresh = ->
    $http(
      method: "POST"
      url: "/bootstrap/Refresh"
      data: $scope.data
    ).success((data, status, headers, config) ->
      $scope.refreshNow()
    )

  $scope.refreshNow = ->
    $scope.refreshtoken = new Date().getMilliseconds()

  $scope.toggleValue = (item) ->
    return (if $scope.hider[item] then true else false)


  $scope.toggle = (item)->
    $scope.hider[item] = !$scope.toggleValue(item);

  1
window.bootstrap = bootstrap
