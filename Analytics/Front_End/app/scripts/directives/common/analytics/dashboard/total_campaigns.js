angular.module("app.dashboard")
  .component("totalCampaigns", {
    templateUrl: "views/common/dashboard/total_campaigns_tmpl.html",
    bindings: {
      data: "<",
      isCampaign:"<"
    },
    controller: ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {
      var $ctrl = this;

    }]
  });