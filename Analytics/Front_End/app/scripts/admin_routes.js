angular.module('routes', [
  "ui.router",
  'ngAnimate',
  'ngAria',
  'ngCookies',
  'ngMessages',
  'ngSanitize',
  'ngTouch',               // Angular flexible routing
  'ui.bootstrap',             // AngularJS native directives for Bootstrap
  'ui.grid',
  'ui.bootstrap.modal',
  'ui.bootstrap.tpls',
  'daterangepicker',
  'highcharts-ng',
  'bitraz.rid',
  "bitraz.template",
  "bitraz.dashboard",
  "angular-web-notification",
  "cgNotify"
]).config(["$stateProvider", "$httpProvider", "$compileProvider",
    "$urlRouterProvider", "$urlMatcherFactoryProvider",
    function ($stateProvider, $httpProvider, $compileProvider, $urlRouterProvider,
              $urlMatcherFactoryProvider) {

      // Optimize load start with remove binding information inside the DOM element
      // trailing slash in url is optional
      $urlMatcherFactoryProvider.strictMode(false);

      $httpProvider.defaults.xsrfCookieName = 'csrftoken';
      $httpProvider.defaults.xsrfHeaderName = 'X-CSRFToken';
      $httpProvider.defaults.withCredentials = true;
      // $httpProvider.interceptors.push('httpInterceptor');
      $httpProvider.useApplyAsync(true);

      $compileProvider.debugInfoEnabled(true);

      // Set default state
      $urlRouterProvider.otherwise("/index");
      $stateProvider
        .state('bitraz', {
          abstract: true,
          // add new module targets below. (each sticky child needs a dedicated target)
          template: '<div ui-view="header" class="header-container"></div><div ui-view="body"></div>',
          controller: "AppController"
        })
        .state('bitraz.main', {
          //Astract state is activated implicitly when one of its descendants are activated.
          abstract: true,
          //sticky state continue running even after it is "exited". controller of a Sticky State are retained.
          sticky: true,
          deepStateRedirect: true,
          views: {
            "header@bitraz": {
              templateUrl: "views/admin/header.html",
              controller: "HeaderController"
            }
          }
        })
        // Dashboard - Main page
        .state('bitraz.main.index', {
          url: "/index",

          data: {
            pageTitle: 'Home',
            activeMenu:'home',
            requiresLogin: true
          },
          views: {
            "body@bitraz": {
              templateUrl: "views/admin/index.html",
              controller: "HomeController"
            }
          }
        })
        .state('bitraz.main.analytics', {
          url: "/analytics?rid",
          data: {
            pageTitle: 'Analytics',
            activeMenu:'analytics',
            requiresLogin: true
          },
          views: {
            "body@bitraz": {
              templateUrl: "views/common/analytics.html",
              controller: "AnalyticsController"
            }
          }
        })
        .state('bitraz.main.campaigns', {
          url: "/campaigns",
          data: {
            pageTitle: 'Campaigns',
            activeMenu:'campaigns',
            requiresLogin: true
          },
          views: {
            "body@bitraz": {
              templateUrl: "views/admin/campaigns.html",
              controller: "CampaignsController"
            }
          }
        })
        .state('bitraz.main.users', {
          url: "/users",
          data: {
            pageTitle: 'Users',
            activeMenu:'users',
            requiresLogin: true
          },
          views: {
            "body@bitraz": {
              templateUrl: "views/admin/users.html",
              controller: "UsersController"
            }
          }
        })
        .state('bitraz.main.archieves', {
          url: "/archieves",
          data: {
            pageTitle: 'Archieves',
            activeMenu:'archieves',
            requiresLogin: true
          },
          views: {
            "body@bitraz": {
              templateUrl: "views/admin/archieves.html",
              controller: "ArchievesController"
            }
          }
        })
        .state('bitraz.main.settings', {
          url: "/settings",
          data: {
            pageTitle: 'Settings',
            activeMenu:'settings',
            requiresLogin: true
          },
          views: {
            "body@bitraz": {
              templateUrl: "views/admin/settings.html",
              controller: "SettingsController"
            }
          }
        })
        .state('bitraz.main.login', {
          url: "/login?redirect_url",
          data: {
            pageTitle: 'Login',
            specialClass: 'landing-page',
            activeMenu:'login'

          },
          views: {
            "body@bitraz": {
              templateUrl: "views/common/login.html",
              controller: "LoginController"
            }
          }
        })
  }])
  .run(function($rootScope, $state, appConfig, $location) {
    $rootScope.$state = $state;
    $rootScope.userInfo = appConfig.userInfo;
    $rootScope.pageLoading = false;
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toStateParams, fromState, fromStateParams) {
      $rootScope.isAuthenticationRequired = toState.data
        && toState.data.requiresLogin
        && !($rootScope.userInfo && $rootScope.userInfo.user_id);

      if ( $rootScope.isAuthenticationRequired ) {
        event.preventDefault();
        $state.go('bitraz.main.login', {redirect_url: $location.$$absUrl});
      }
    });
  });