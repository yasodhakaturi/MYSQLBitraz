angular
  .module('bitraz.common.controllers', ['bitraz.auth', 'ngMap'])
  .controller('LoginController', LoginController)
  .controller('AnalyticsController', AnalyticsController);


function LoginController($rootScope, $scope, AuthService, appConfig, $state, $location, $window) {
  $scope.loginError = '';
  $scope.uname = '';
  $scope.pswd = '';
  $scope.loading = false;

  $scope.login = (loginForm) => {
    $scope.loading = true;
    $scope.loginError = '';
    if(loginForm.$valid){
      AuthService.login({uname: loginForm.username.$viewValue, password: loginForm.password.$viewValue}).$promise.then((res)=>{
        if(res.error){
          $scope.loginError = res.error && res.error.message;
        }else{
          $rootScope.userInfo = res.user_info;
          $rootScope.userInfo.user_id = $rootScope.userInfo.user_id || null;
          if($state.params.redirect_url){
            $window.location.href = $state.params.redirect_url ? $state.params.redirect_url : '/';
          }else{
            if(res.redirect_url){
              $window.location.href = res.redirect_url || '/';
            }else{
              $state.go('bitraz.main.analytics', {}, {location: 'replace'})
            }
          }
        }
        $scope.loading = false;
      }, (err)=>{
        $scope.loginError =  err.error && err.error.message;
        $scope.loading = false;
      });

    }else{
      if (loginForm.$invalid) {
        angular.forEach(loginForm.$error, function (field) {
          angular.forEach(field, function(errorField){
            errorField.$setTouched();
          });
        });
      }
      $scope.loading = false;
    }
  }

  $scope.redirectUser = () => {
    if($rootScope.userInfo.isAdmin){
      $window.location.href = '/Admin';
    }else if($rootScope.userInfo.isClient){
      $window.location.href = '/Analytics';
    }else{
      $state.go('bitraz.main.analytics', {}, {location: 'replace'})
    }
  }

  $rootScope.pageLoading = false;

}

function AnalyticsController($rootScope, $state, $scope, CampaignService, $location, $window, $interval) {
  $scope.isLoaded = false;
  $scope.selectedCampaign = $state.params.rid || 0;

  $scope.init = () => {
    CampaignService.getCampaigns().$promise.then((res)=>{
      if(res.error){
        if(res.error.redirect_url){
          $window.location.href = res.error.redirect_url || '/';
        }else{
          $location.path('/')
        }
      }else{
        if(res){
          $scope.campaigns = res.length > 0? res : [];

          if(_.indexOf(_.map($scope.campaigns, 'ReferenceNumber'), $scope.selectedCampaign) < 0){
            $scope.selectedCampaign = $scope.campaigns[0].ReferenceNumber;
            $state.go('.', {rid: $scope.selectedCampaign}, {notify: false});
          }
          $scope.isLoaded = true;
        }
      }
    },(err)=>{
      if(err.redirect_url){
        $window.location.href = err.redirect_url || '/';
      }else{
        $location.path('/')
      }
    });

    $scope.campaignChange = (val) => {
      $state.go('.', {rid: val}, {notify: false})
    }

  };
  if($rootScope.userInfo && $rootScope.userInfo.user_id){
    $scope.init();
  }
}

