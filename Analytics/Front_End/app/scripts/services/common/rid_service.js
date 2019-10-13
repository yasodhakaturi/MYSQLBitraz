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

        getSummaryTotalUrls: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_TotalUrls'
        },

        getSummaryUsersCount: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_UsersCount'
        },

        getSummaryVisitsCount: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_VisitsCount'
        },

        getSummaryCampaignsCount: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_CampaignsCount'
        },

        getSummaryRecentCampaignsCount: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_RecentCampaignsCount'
        },

        getSummaryActivityTodayCount: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_ActivityCount_Today'
        },

        getSummaryActivityWeekCount: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_ActivityCount_Week'
        },

        getSummaryActivityMonthCount: {
          method: 'GET',
          url: appConfig.apiEndPoint + '/Analytics/GETDashBoardSummary_ActivityCount_Month'
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


