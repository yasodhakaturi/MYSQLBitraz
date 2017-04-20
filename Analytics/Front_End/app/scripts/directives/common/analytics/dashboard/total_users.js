angular.module("bitraz.dashboard")
  .component("totalUsers", {
    templateUrl: "views/common/dashboard/total_users_tmpl.html",
    bindings: {
      data: "<",
      isCampaign:"<"
    },
    controller: ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {
      var $ctrl = this;

    }]
  });