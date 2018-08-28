angular.module("app.dashboard")
  .component("totalVisits", {
    templateUrl: "views/common/dashboard/total_visits_tmpl.html",
    bindings: {
      data: "<",
      isCampaign:"<"
    },
    controller: ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {
      var $ctrl = this;

    }]
  });