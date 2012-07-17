(function() {
  /*
  @reference ../jquery-1.7.2.js
  @reference ../angular.js
  */
  var bootstrap;
  bootstrap = function($scope, $http, $element, $filter, datajsonPromise) {
    datajsonPromise.then(function(rs) {
      return $scope.data = rs.data;
    });
    $scope.refreshtoken = '';
    $scope.showColorPicker = function($event, item) {
      var all, basiccolors, el;
      all = $scope.data;
      basiccolors = $filter("nameType")(all, "basiccolor", item.value);
      el = $event.target;
      $(el).miniColors({
        letterCase: 'uppercase',
        change: function(hex, rgb) {
          item.value = hex;
          return $(el).css("background-color", hex);
        }
      });
      return $(el).miniColors("show");
    };
    $scope.refresh = function() {
      return $http({
        method: "POST",
        url: "/bootstrap/Refresh",
        data: $scope.data
      }).success(function(data, status, headers, config) {
        return $scope.refreshNow();
      });
    };
    $scope.refreshNow = function() {
      return $scope.refreshtoken = new Date().getMilliseconds();
    };
    $scope.toggleValue = function(item) {
      if ($scope.hider[item]) {
        return true;
      } else {
        return false;
      }
    };
    $scope.toggle = function(item) {
      return $scope.hider[item] = !$scope.toggleValue(item);
    };
    return 1;
  };
  window.bootstrap = bootstrap;
}).call(this);
