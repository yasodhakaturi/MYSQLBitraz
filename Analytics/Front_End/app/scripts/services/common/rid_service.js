angular.module('app.rid', ["ngResource"])
    .service('RidService', ["$resource", 'appConfig', function ($resource, appConfig) {
      return $resource(appConfig.apiEndPoint + '/api/rid/', {}, {
        getInfo: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/auth/ReferenceInfo',
          params:{
            rid:'@id'
          }
        },
        validate: {
          method: 'POST',
          url: appConfig.apiEndPoint + '/auth/ReferenceInfoValidate',
          data:{
            rid:'@id',
            password:'@password'
          }
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


