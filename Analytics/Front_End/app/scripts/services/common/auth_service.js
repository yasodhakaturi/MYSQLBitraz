angular.module('bitraz.auth', ["ngResource"])
  .service('AuthService', ["$resource", 'appConfig', function ($resource, appConfig) {
    return $resource(appConfig.apiEndPoint + '/Auth/Login/', {}, {
      login: {
        method: 'post',
        stripTrailingSlashes: false,
        data: {
          uname: '@uname',
          password: '@password'
        }
      },
      details: {
        method: 'get',
        url: appConfig.apiEndPoint + '/Auth/UserInfo'
      },
      
      logout: {
        method: 'post',
        url: appConfig.apiEndPoint + '/Auth/Logout/'
      }
    }, {
      stripTrailingSlashes: false
    });
  }])


