angular.module("app.dashboard", ['ui.router'])
  .component("dashboardLayout", {
    templateUrl: "views/common/dashboard/dashboard_tmpl.html",
    bindings: {
      config: "<"
    },
    controller: ["$scope", "$rootScope", "$timeout", "RidService", function ($scope, $rootScope, $timeout, RidService) {
      var $ctrl = this;

      $ctrl.params = {};
      $ctrl.data = {};
      $ctrl.totalUrls = {};
      $ctrl.users = {};
      $ctrl.visits = {};
      // $rootScope.pageLoading = true;
      //
      // $ctrl.getSummary = () => {
      //   $rootScope.pageLoading = true;
      //   let summaryDefer = RidService.getSummary($ctrl.getParams()).$promise;
      //   summaryDefer.then((res)=>{
      //     $ctrl.data = res;
      //     $rootScope.pageLoading = false;
      //   }, (err)=>{
      //     $rootScope.pageLoading = false;
      //     console.log("failed to get summary", err);
      //   });
      // };

      $ctrl.getUrlCounts = () => {
        $rootScope.pageTotalUrlsLoading = true;
        let defer = RidService.getSummaryTotalUrls($ctrl.getParams()).$promise;
        defer.then((res)=>{
          $ctrl.totalUrls = res;
          $rootScope.pageTotalUrlsLoading = false;
        }, (err)=>{
          $ctrl.totalUrls = {};
          $rootScope.pageTotalUrlsLoading = false;
          console.log("failed to get urls", err);
        });
      };

      $ctrl.getUsersCounts = () => {
        $rootScope.pageUsersLoading = true;
        let defer = RidService.getSummaryUsersCount($ctrl.getParams()).$promise;
        defer.then((res)=>{
          $ctrl.users = res;
          $rootScope.pageUsersLoading = false;
        }, (err)=>{
          $ctrl.users = {};
          $rootScope.pageUsersLoading = false;
          console.log("failed to get users", err);
        });
      };


      $ctrl.getVisitsCounts = () => {
        $rootScope.pageVisitsLoading = true;
        let defer = RidService.getSummaryVisitsCount($ctrl.getParams()).$promise;
        defer.then((res)=>{
          $ctrl.visits = res;
          $rootScope.pageVisitsLoading = false;
        }, (err)=>{
          $ctrl.visits = {};
          $rootScope.pageVisitsLoading = false;
          console.log("failed to get visits", err);
        });
      };


      $ctrl.getCampaignsCounts = () => {
        $rootScope.pageCampaignsLoading = true;
        let defer = RidService.getSummaryCampaignsCount($ctrl.getParams()).$promise;
        defer.then((res)=>{
          $ctrl.campaigns = res;
          $rootScope.pageCampaignsLoading = false;
        }, (err)=>{
          $ctrl.campaigns = {};
          $rootScope.pageCampaignsLoading = false;
          console.log("failed to get campaigns", err);
        });
      };


      $ctrl.getRecentCampaigns = () => {
        $rootScope.pageRecentCampaignsLoading = true;
        let defer = RidService.getSummaryRecentCampaignsCount($ctrl.getParams()).$promise;
        defer.then((res)=>{
          $ctrl.recentCampaigns = res;
          $rootScope.pageRecentCampaignsLoading = false;
        }, (err)=>{
          $ctrl.recentCampaigns = {};
          $rootScope.pageRecentCampaignsLoading = false;
          console.log("failed to get recent campaigns", err);
        });
      };

      $ctrl.activityStateChange = (state='today') => {
        $rootScope.pageActivityLoading = true;
        $ctrl.activityState = state;
        let defer;
        if(state == 'last7days'){
          defer = RidService.getSummaryActivityWeekCount($ctrl.getParams()).$promise;
        }else if(state == 'month'){
          defer = RidService.getSummaryActivityMonthCount($ctrl.getParams()).$promise;
        }else{
          defer = RidService.getSummaryActivityTodayCount($ctrl.getParams()).$promise;
        }

        defer.then((res)=>{
          $ctrl.activities = res;
          $rootScope.pageActivityLoading = false;
        }, (err)=>{
          $ctrl.activities = {};
          $rootScope.pageActivityLoading = false;
          console.log("failed to get activity", err);
        });
      };


      $ctrl.getParams = ()=> {
        if($ctrl.config.userId){$ctrl.params.cid = $ctrl.config.userId;}
        if($ctrl.config.campaignId){$ctrl.params.rid = $ctrl.config.campaignId;}else{ delete $ctrl.params.rid;}
        return $ctrl.params;
      };

      $ctrl.$onInit = ()=>{
        if($rootScope.userInfo && $rootScope.userInfo.user_id){
          // $ctrl.getSummary();
          $ctrl.getUrlCounts();
          $ctrl.getUsersCounts();
          $ctrl.getVisitsCounts();
          $ctrl.getCampaignsCounts();
          $ctrl.getRecentCampaigns();
          $ctrl.activityStateChange();
        }

      };

      $ctrl.$onChanges = (changes)=>{
        if(changes.config && !changes.config.isFirstChange()){
          $ctrl.$onInit();
        }
      }

    }]
  });
