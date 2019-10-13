angular.module("app.dashboard")
  .component("activities", {
    templateUrl: "views/common/dashboard/activities_tmpl.html",
    bindings: {
      data: "<",
      state: "<",
      header: "<",
      isCampaign:"<",
      onTypeChange:"=",
    },
    controller: ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {
      var $ctrl = this;

      $ctrl.show = (state) => {
        $ctrl.state = state;
        $ctrl.onTypeChange(state);
      };

      $ctrl.$onInit =  ()=>{

      };

    }]
  });
