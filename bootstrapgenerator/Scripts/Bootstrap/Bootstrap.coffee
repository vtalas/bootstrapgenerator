###
@reference ../jquery-1.7.2.js
@reference ../angular.js
###

bootstrap = ($scope, $http, $element,colorsonly, $filter) ->

  $scope.data = $.parseJSON $element.data("model")
  $scope.colorsrefonly = colorsonly
  $scope.refreshtoken = ''


  $scope.showColorPicker = ($event,item)->

    all = $scope.data
    basiccolors=$filter("nameType")(all, "basiccolor", item.value )
    el = $event.currentTarget

    $(el).typeahead({
                  source:basiccolors,
                  updater: (val)->
                    $(el).val(val)
                    $scope.data[name].value = val
                  items:11
                  });
    1
    colorPicker($event)

    colorPicker.exportColor = ->
      console.log colorPicker.CP.hex
      item.value = "#"+colorPicker.CP.hex

  $scope.refresh = ->
    $http(
      method: "POST"
      url: "/bootstrap/Refresh"
      data: $scope.data
    ).success((data, status, headers, config) ->
      console.log data
      #$scope.refreshNow()
    )

  $scope.refreshNow = ->
    $scope.refreshtoken = new Date().getMilliseconds()

  $scope.toggleValue = (item) ->
    return (if $scope.hider[item] then true else false)


  $scope.toggle = (item)->
    $scope.hider[item] = !$scope.toggleValue(item);
    console.log $scope.hider, item

  1
window.bootstrap = bootstrap
