angular.module("app.dashboard")
  .component("activities", {
    templateUrl: "views/common/dashboard/activities_tmpl.html",
    bindings: {
      data: "<",
      header: "<",
      isCampaign:"<"
    },
    controller: ["$scope", "$rootScope", "$timeout", function ($scope, $rootScope, $timeout) {
      var $ctrl = this;
      $ctrl.state = 'today';
      $ctrl.show = (state) => {
        $ctrl.state = state;
        $ctrl.activeData = $ctrl.data[state] ? $ctrl.data[state] || {} : {};
      };

      $ctrl.$onInit =  ()=>{
        if($ctrl.data){
          $ctrl.activeData = $ctrl.data[$ctrl.state] ? $ctrl.data[$ctrl.state] || {} : {};
        }else{
          $ctrl.activeData = {};
        }
      };

      $ctrl.$onChanges = (changes) => {
        if(changes.data && !changes.data.isFirstChange()){
          $ctrl.activeData = changes.data.currentValue[$ctrl.state] ? changes.data.currentValue[$ctrl.state] || {} : {};
        }
      }

    }]
  });