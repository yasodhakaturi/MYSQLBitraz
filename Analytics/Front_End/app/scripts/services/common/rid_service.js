angular.module('app.rid', ["ngResource"])
    .service('RidService', ["$resource", 'appConfig', function ($resource, appConfig) {
      return $resource(appConfig.apiEndPoint + '/api/rid/', {}, {
        getInfo: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/rid/info/:id'
        },
        validate: {
          method: 'POST',
          url: appConfig.apiEndPoint + '/rid/validate/:id'
        },
        getSummary: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETSummary'
        },
        getCounts: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETAllCounts'
        },
        getGeoLocations: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETGeoLocations',
          isArray:true
        }
      }, {
        stripTrailingSlashes: false
      });
    }]);


