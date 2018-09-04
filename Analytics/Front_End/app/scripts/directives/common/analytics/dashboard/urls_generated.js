angular.module("app.dashboard")
  .component("urlsGenerated", {
    templateUrl: "views/common/dashboard/urls_generated_tmpl.html",
    bindings: {
      data: "<",
      isCampaign:"<"
    },
    controller: ["$scope", "$rootScope", "$uibModal", "$timeout", function ($scope, $rootScope, $uibModal, $timeout) {
      var $ctrl = this;

    }]
  });