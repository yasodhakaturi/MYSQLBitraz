angular.module("bitraz.dashboard")
  .component("recentCampaigns", {
    templateUrl: "views/common/dashboard/recent_campaigns_tmpl.html",
    bindings: {
      data: "<"
    },
    controller: ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {
      var $ctrl = this;

    }]
  });