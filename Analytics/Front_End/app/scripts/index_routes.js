function configState($stateProvider, $urlRouterProvider, $compileProvider, $urlMatcherFactoryProvider, $httpProvider) {

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
    $urlRouterProvider.otherwise("/login");
    $stateProvider
        .state('app', {
          abstract: true,
          // add new module targets below. (each sticky child needs a dedicated target)
          template: '<div ui-view="header" class="header-container"></div><div ui-view="body"></div>',
          controller: "AppController"
        })
        .state('app.main', {
          //Astract state is activated implicitly when one of its descendants are activated.
          abstract: true,
          //sticky state continue running even after it is "exited". controller of a Sticky State are retained.
          sticky: true,
          deepStateRedirect: true,
          views: {
            "header@app": {
              templateUrl: "views/index/index_header.html",
              controller: "HeaderController"
            }
          }
        })
        // Dashboard - Main page
//        .state('app.main.index', {
//            url: "/index",
//
//            data: {
//                pageTitle: 'Home',
//                specialClass: 'landing-page',
//                activeMenu:'home'
//            },
//            views: {
//              "body@app": {
//                templateUrl: "views/index/index.html",
//                controller: "HomeController"
//              }
//            }
//        })
//        .state('app.main.features', {
//            url: "/features",
//            data: {
//                pageTitle: 'Features',
//                specialClass: 'landing-page',
//                activeMenu:'features'
//            },
//            views: {
//              "body@app": {
//                templateUrl: "views/index/features.html",
//                controller: "FeatureController"
//              }
//            }
//        })
//        .state('app.main.clients', {
//          url: "/clients",
//          data: {
//            pageTitle: 'Clients',
//            specialClass: 'landing-page',
//            activeMenu:'clients'
//          },
//          views: {
//            "body@app": {
//              templateUrl: "views/index/clients.html",
//              controller: "ClientController"
//            }
//          }
//        })
//        .state('app.main.contact', {
//          url: "/contact",
//          data: {
//            pageTitle: 'Contact',
//            specialClass: 'landing-page',
//            activeMenu:'contact'
//          },
//          views: {
//            "body@app": {
//              templateUrl: "views/index/contact.html",
//              controller: "ContactController"
//            }
//          }
//        })
        .state('app.main.login', {
          url: "/login?redirect_url",
          data: {
            pageTitle: 'Login',
            specialClass: 'landing-page',
            activeMenu:'login'
          },
          views: {
            "body@app": {
              templateUrl: "views/common/login.html",
              controller: "LoginController"
            }
          }
        })
        .state('app.main.analytics', {
          url: "/analytics?rid",
          data: {
            pageTitle: 'Analytics',
            specialClass: 'landing-page',
            activeMenu:'analytics'

          },

          views: {
            "body@app": {
              templateUrl: "views/index/analytics.html",
              controller: "AnalyticsController"
            }
          }
        })
}

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
  'app.rid',
  'app.auth',
  "app.template"
])
    .config(configState)
    .run(function($rootScope, $state, appConfig, $location) {
        $rootScope.$state = $state;
        $rootScope.userInfo = appConfig.userInfo;
        $rootScope.$on('$stateChangeSuccess', function (event, toState, toStateParams, fromState, fromStateParams) {

          var isAuthenticationRequired = toState.data
            && toState.data.requiresLogin
            && !($rootScope.userInfo && $rootScope.userInfo.user_id);

          if ( isAuthenticationRequired ) {
            event.preventDefault();
            $state.go('app.main.login', {redirect_url: $location.$$absUrl});
          }
        });



    });