angular.module("app.dashboard")
  .component("loggedUsers", {
    templateUrl: "views/common/dashboard/logged_users_tmpl.html",
    bindings: {
      data: "<"
    },
    controller: ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {
      var $ctrl = this;

    }]
  });