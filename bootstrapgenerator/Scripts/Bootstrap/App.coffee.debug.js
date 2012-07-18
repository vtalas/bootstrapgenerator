(function() {
  /*
  @reference ../jquery-1.7.2.js
  @reference ../angular.js
  */
  var appCtrl, module;
  module = angular.module("bootstrapApp", []);
  module.config(function($routeProvider, $provide, $filterProvider) {
    $filterProvider.register('nameType', function() {
      return function(data, type, name) {
        var key, prop, x;
        x = [];
        for (key in data) {
          prop = data[key];
          if (prop.type === type) {
            x.push("@" + key);
          }
        }
        return x;
      };
    });
    $filterProvider.register('typevalue', function() {
      return function(data, type, type2) {
        var key, prop, x;
        x = {};
        for (key in data) {
          prop = data[key];
          if (prop.type === type || prop.type === type2 && prop.value[0] !== "@") {
            x[key] = prop.value;
          }
        }
        return x;
      };
    });
    $filterProvider.register('refs', function() {
      return function(data, type) {
        var key, prop, x;
        x = {};
        for (key in data) {
          prop = data[key];
          if (prop.type === type && prop.value[0] === "@") {
            x[key] = prop.value;
          }
        }
        return x;
      };
    });
    $provide.factory("datajsonPromise", function($http) {
      var x;
      x = $http({
        method: "POST",
        url: "/bootstrap/JSONDATA"
      });
      return x;
    });
    return $routeProvider.when("/csstest", {
      controller: appCtrl,
      templateUrl: "/Content/bootstrap_templates/csstest.html"
    }).when("/bootswatch", {
      controller: appCtrl,
      templateUrl: "/Content/bootstrap_templates/bootswatch.html"
    }).when("/components", {
      controller: appCtrl,
      templateUrl: "/Content/bootstrap_templates/components.html"
    }).otherwise({
      redirectTo: '/csstest'
    });
  });
  module.directive("bootstrapelem", function($filter, datajsonPromise) {
    var directiveDefinitionObject;
    directiveDefinitionObject = {
      require: '?ngModel',
      link: function(scope, el, tAttrs, controller) {
        var jsondata;
        jsondata = {};
        datajsonPromise.then(function(rs) {
          return jsondata = rs.data;
        });
        console.log("xx");
        controller.$setViewValue = function(val) {
          var basiccolors;
          basiccolors = $filter("nameType")(jsondata, "basiccolor");
          el.typeahead({
            source: basiccolors,
            updater: function(val) {
              var colorsonly, r;
              colorsonly = $filter("typevalue")(jsondata, "color", "basiccolor");
              r = colorsonly[val.substr(1)];
              el.css("background", r ? r : void 0);
              return controller.$viewValue.value = val;
            },
            items: 11
          });
          return controller.$viewValue.value = val;
        };
        return controller.$render = function() {
          var a, colorsonly, r;
          if (!controller.$viewValue) {
            el.val("loading...");
            return;
          }
          a = controller.$viewValue;
          el.val(a.value);
          switch (a.type) {
            case "color":
            case "basiccolor":
              el.width("80px");
              if (a.value[0] !== "@") {
                el.css("background", a.value);
              }
              if (a.value[0] === "@") {
                console.log("aaa");
                colorsonly = $filter("typevalue")(jsondata, "color", "basiccolor");
                r = colorsonly[a.value.substr(1)];
                if (r) {
                  return el.css("background", r);
                }
              }
          }
        };
      }
    };
    return directiveDefinitionObject;
  });
  appCtrl = function($scope, $http) {};
  1;
}).call(this);
