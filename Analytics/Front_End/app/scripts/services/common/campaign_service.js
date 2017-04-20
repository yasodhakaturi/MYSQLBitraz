angular.module('bitraz.campaigns', ["ngResource"])
  .service('CampaignService', ["$resource", 'appConfig', function ($resource, appConfig) {
    return $resource(appConfig.apiEndPoint + '/Analytics/GETAllCampaigns/', {}, {
      getCampaigns: {
        method: 'get',
        url: appConfig.apiEndPoint + '/Campaign/Search',
        isArray: true
      },
      getDetails: {
        method: 'get',
        url: appConfig.apiEndPoint + '/Analytics/GETAllCampaigns/:id',
      },
      exportAnalytics: {
        method: 'get',
        url: appConfig.apiEndPoint + '/Campaign/ExportAnalytics',
      }
    }, {
      stripTrailingSlashes: false
    });
  }])


