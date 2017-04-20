var chance = require("chance").Chance();

exports.getSummary = function(query){

  return {
    "totalUrls": {"count": 8199},
    "users": {
      "total": 8199,
      "uniqueUsers": 2,
      "uniqueUsersToday": 3,
      "usersToday": 3,
      "uniqueUsersYesterday": 2,
      "usersYesterday": 1,
      "uniqueUsersLast7days": 1,
      "usersLast7days": 7831
    },
    "visits": {
      "total": 8235,
      "uniqueVisits": 6416,
      "visitsToday": 2,
      "uniqueVisitsToday": 2,
      "visitsYesterday": 3,
      "uniqueVisitsYesterday": 1,
      "uniqueVisitsLast7days": 2,
      "visitsLast7days": 2986
    },
    "campaigns": {"total": 7, "campaignsLast7days": 2, "campaignsMonth": 2},
    "recentCampaigns": [{
      "id": 1,
      "rid": "1234",
      "createdOn": "2016-11-28T23:47:04.763",
      "endate": null,
      "visits": 6415,
      "users": 8198,
      "status": true
    }, {
      "id": 2,
      "rid": "abcd",
      "createdOn": "2016-12-07T20:18:27.607",
      "endate": null,
      "visits": 1,
      "users": 1,
      "status": true
    }],
    "activities": {
      "today": {
        "urlTotal": 8199,
        "urlPercent": 0.0,
        "visitsTotal": 8235,
        "visitsPercent": 0.0,
        "revisitsTotal": 1819,
        "revisitsPercent": 0.0,
        "noVisitsTotal": 1783,
        "noVisitsPercent": 0.0
      },
      "last7days": {
        "urlTotal": 8199,
        "urlPercent": 25.02,
        "visitsTotal": 8235,
        "visitsPercent": -0.21,
        "revisitsTotal": 1819,
        "revisitsPercent": 0.1,
        "noVisitsTotal": 1783,
        "noVisitsPercent": 5.92
      },
      "month": {
        "urlTotal": 8199,
        "urlPercent": 46.8,
        "visitsTotal": 8235,
        "visitsPercent": -13.47,
        "revisitsTotal": 1819,
        "revisitsPercent": -10.46,
        "noVisitsTotal": 1783,
        "noVisitsPercent": 10.61
      }
    }
  }
};

exports.getCounts = function(query){
  var data = {
    "activity": [{"RequestedDate":"2013-06-02T00:00:00","RequestCount":0.7695},{"RequestedDate":"2013-06-03T00:00:00","RequestCount":0.7648},{"RequestedDate":"2013-06-04T00:00:00","RequestCount":0.7645},{"RequestedDate":"2013-06-05T00:00:00","RequestCount":0.7638},{"RequestedDate":"2013-06-06T00:00:00","RequestCount":0.7549},{"RequestedDate":"2013-06-07T00:00:00","RequestCount":0.7562},{"RequestedDate":"2013-06-09T00:00:00","RequestCount":0.7574},{"RequestedDate":"2013-06-10T00:00:00","RequestCount":0.7543},{"RequestedDate":"2013-06-11T00:00:00","RequestCount":0.751},{"RequestedDate":"2013-06-12T00:00:00","RequestCount":0.7498},{"RequestedDate":"2013-06-13T00:00:00","RequestCount":0.7477},{"RequestedDate":"2013-06-14T00:00:00","RequestCount":0.7492},{"RequestedDate":"2013-06-16T00:00:00","RequestCount":0.7487},{"RequestedDate":"2013-06-17T00:00:00","RequestCount":0.748},{"RequestedDate":"2013-06-18T00:00:00","RequestCount":0.7466},{"RequestedDate":"2013-06-19T00:00:00","RequestCount":0.7521},{"RequestedDate":"2013-06-20T00:00:00","RequestCount":0.7564},{"RequestedDate":"2013-06-21T00:00:00","RequestCount":0.7621},{"RequestedDate":"2013-06-23T00:00:00","RequestCount":0.763},{"RequestedDate":"2013-06-24T00:00:00","RequestCount":0.7623},{"RequestedDate":"2013-06-25T00:00:00","RequestCount":0.7644},{"RequestedDate":"2013-06-26T00:00:00","RequestCount":0.7685},{"RequestedDate":"2013-06-27T00:00:00","RequestCount":0.7671},{"RequestedDate":"2013-06-28T00:00:00","RequestCount":0.7687},{"RequestedDate":"2013-06-30T00:00:00","RequestCount":0.7687},{"RequestedDate":"2013-07-01T00:00:00","RequestCount":0.7654},{"RequestedDate":"2013-07-02T00:00:00","RequestCount":0.7705},{"RequestedDate":"2013-07-03T00:00:00","RequestCount":0.7687},{"RequestedDate":"2013-07-04T00:00:00","RequestCount":0.7744},{"RequestedDate":"2013-07-05T00:00:00","RequestCount":0.7793},{"RequestedDate":"2013-07-07T00:00:00","RequestCount":0.7804},{"RequestedDate":"2013-07-08T00:00:00","RequestCount":0.777},{"RequestedDate":"2013-07-09T00:00:00","RequestCount":0.7824},{"RequestedDate":"2013-07-10T00:00:00","RequestCount":0.7705},{"RequestedDate":"2013-07-11T00:00:00","RequestCount":0.7635},{"RequestedDate":"2013-07-12T00:00:00","RequestCount":0.7652},{"RequestedDate":"2013-07-14T00:00:00","RequestCount":0.7656},{"RequestedDate":"2013-07-15T00:00:00","RequestCount":0.7655},{"RequestedDate":"2013-07-16T00:00:00","RequestCount":0.7598},{"RequestedDate":"2013-07-17T00:00:00","RequestCount":0.7619},{"RequestedDate":"2013-07-18T00:00:00","RequestCount":0.7628},{"RequestedDate":"2013-07-19T00:00:00","RequestCount":0.7609},{"RequestedDate":"2013-07-21T00:00:00","RequestCount":0.7599},{"RequestedDate":"2013-07-22T00:00:00","RequestCount":0.7584},{"RequestedDate":"2013-07-23T00:00:00","RequestCount":0.7562},{"RequestedDate":"2013-07-24T00:00:00","RequestCount":0.7575},{"RequestedDate":"2013-07-25T00:00:00","RequestCount":0.7531},{"RequestedDate":"2013-07-26T00:00:00","RequestCount":0.753},{"RequestedDate":"2013-07-28T00:00:00","RequestCount":0.7526},{"RequestedDate":"2013-07-29T00:00:00","RequestCount":0.754},{"RequestedDate":"2013-07-30T00:00:00","RequestCount":0.754},{"RequestedDate":"2013-07-31T00:00:00","RequestCount":0.7518},{"RequestedDate":"2013-08-01T00:00:00","RequestCount":0.7571},{"RequestedDate":"2013-08-02T00:00:00","RequestCount":0.7529},{"RequestedDate":"2013-08-04T00:00:00","RequestCount":0.7532},{"RequestedDate":"2013-08-05T00:00:00","RequestCount":0.7542},{"RequestedDate":"2013-08-06T00:00:00","RequestCount":0.7515},{"RequestedDate":"2013-08-07T00:00:00","RequestCount":0.7498},{"RequestedDate":"2013-08-08T00:00:00","RequestCount":0.7473},{"RequestedDate":"2013-08-09T00:00:00","RequestCount":0.7494},{"RequestedDate":"2013-08-11T00:00:00","RequestCount":0.7497},{"RequestedDate":"2013-08-12T00:00:00","RequestCount":0.7519},{"RequestedDate":"2013-08-13T00:00:00","RequestCount":0.754},{"RequestedDate":"2013-08-14T00:00:00","RequestCount":0.7543},{"RequestedDate":"2013-08-15T00:00:00","RequestCount":0.7492},{"RequestedDate":"2013-08-16T00:00:00","RequestCount":0.7502},{"RequestedDate":"2013-08-18T00:00:00","RequestCount":0.7503},{"RequestedDate":"2013-08-19T00:00:00","RequestCount":0.7499},{"RequestedDate":"2013-08-20T00:00:00","RequestCount":0.7453},{"RequestedDate":"2013-08-21T00:00:00","RequestCount":0.7487},{"RequestedDate":"2013-08-22T00:00:00","RequestCount":0.7487},{"RequestedDate":"2013-08-23T00:00:00","RequestCount":0.7472},{"RequestedDate":"2013-08-25T00:00:00","RequestCount":0.7471},{"RequestedDate":"2013-08-26T00:00:00","RequestCount":0.748},{"RequestedDate":"2013-08-27T00:00:00","RequestCount":0.7467},{"RequestedDate":"2013-08-28T00:00:00","RequestCount":0.7497},{"RequestedDate":"2013-08-29T00:00:00","RequestCount":0.7552},{"RequestedDate":"2013-08-30T00:00:00","RequestCount":0.7562},{"RequestedDate":"2013-09-01T00:00:00","RequestCount":0.7572},{"RequestedDate":"2013-09-02T00:00:00","RequestCount":0.7581},{"RequestedDate":"2013-09-03T00:00:00","RequestCount":0.7593},{"RequestedDate":"2013-09-04T00:00:00","RequestCount":0.7571},{"RequestedDate":"2013-09-05T00:00:00","RequestCount":0.7622},{"RequestedDate":"2013-09-06T00:00:00","RequestCount":0.7588},{"RequestedDate":"2013-09-08T00:00:00","RequestCount":0.7591},{"RequestedDate":"2013-09-09T00:00:00","RequestCount":0.7544},{"RequestedDate":"2013-09-10T00:00:00","RequestCount":0.7537},{"RequestedDate":"2013-09-11T00:00:00","RequestCount":0.7512},{"RequestedDate":"2013-09-12T00:00:00","RequestCount":0.7519},{"RequestedDate":"2013-09-13T00:00:00","RequestCount":0.7522},{"RequestedDate":"2013-09-15T00:00:00","RequestCount":0.7486},{"RequestedDate":"2013-09-16T00:00:00","RequestCount":0.75},{"RequestedDate":"2013-09-17T00:00:00","RequestCount":0.7486},{"RequestedDate":"2013-09-18T00:00:00","RequestCount":0.7396},{"RequestedDate":"2013-09-19T00:00:00","RequestCount":0.7391},{"RequestedDate":"2013-09-20T00:00:00","RequestCount":0.7394},{"RequestedDate":"2013-09-22T00:00:00","RequestCount":0.7389},{"RequestedDate":"2013-09-23T00:00:00","RequestCount":0.7411},{"RequestedDate":"2013-09-24T00:00:00","RequestCount":0.7422},{"RequestedDate":"2013-09-25T00:00:00","RequestCount":0.7393},{"RequestedDate":"2013-09-26T00:00:00","RequestCount":0.7413},{"RequestedDate":"2013-09-27T00:00:00","RequestCount":0.7396},{"RequestedDate":"2013-09-29T00:00:00","RequestCount":0.741},{"RequestedDate":"2013-09-30T00:00:00","RequestCount":0.7393},{"RequestedDate":"2013-10-01T00:00:00","RequestCount":0.7393},{"RequestedDate":"2013-10-02T00:00:00","RequestCount":0.7365},{"RequestedDate":"2013-10-03T00:00:00","RequestCount":0.7343},{"RequestedDate":"2013-10-04T00:00:00","RequestCount":0.7376},{"RequestedDate":"2013-10-06T00:00:00","RequestCount":0.737},{"RequestedDate":"2013-10-07T00:00:00","RequestCount":0.7362},{"RequestedDate":"2013-10-08T00:00:00","RequestCount":0.7368},{"RequestedDate":"2013-10-09T00:00:00","RequestCount":0.7393},{"RequestedDate":"2013-10-10T00:00:00","RequestCount":0.7397},{"RequestedDate":"2013-10-11T00:00:00","RequestCount":0.7385},{"RequestedDate":"2013-10-13T00:00:00","RequestCount":0.7377},{"RequestedDate":"2013-10-14T00:00:00","RequestCount":0.7374},{"RequestedDate":"2013-10-15T00:00:00","RequestCount":0.7395},{"RequestedDate":"2013-10-16T00:00:00","RequestCount":0.7389},{"RequestedDate":"2013-10-17T00:00:00","RequestCount":0.7312},{"RequestedDate":"2013-10-18T00:00:00","RequestCount":0.7307},{"RequestedDate":"2013-10-20T00:00:00","RequestCount":0.7309},{"RequestedDate":"2013-10-21T00:00:00","RequestCount":0.7308},{"RequestedDate":"2013-10-22T00:00:00","RequestCount":0.7256},{"RequestedDate":"2013-10-23T00:00:00","RequestCount":0.7258},{"RequestedDate":"2013-10-24T00:00:00","RequestCount":0.7247},{"RequestedDate":"2013-10-25T00:00:00","RequestCount":0.7244},{"RequestedDate":"2013-10-27T00:00:00","RequestCount":0.7244},{"RequestedDate":"2013-10-28T00:00:00","RequestCount":0.7255},{"RequestedDate":"2013-10-29T00:00:00","RequestCount":0.7275},{"RequestedDate":"2013-10-30T00:00:00","RequestCount":0.728},{"RequestedDate":"2013-10-31T00:00:00","RequestCount":0.7361},{"RequestedDate":"2013-11-01T00:00:00","RequestCount":0.7415},{"RequestedDate":"2013-11-03T00:00:00","RequestCount":0.7411},{"RequestedDate":"2013-11-04T00:00:00","RequestCount":0.7399},{"RequestedDate":"2013-11-05T00:00:00","RequestCount":0.7421},{"RequestedDate":"2013-11-06T00:00:00","RequestCount":0.74},{"RequestedDate":"2013-11-07T00:00:00","RequestCount":0.7452},{"RequestedDate":"2013-11-08T00:00:00","RequestCount":0.7479},{"RequestedDate":"2013-11-10T00:00:00","RequestCount":0.7492},{"RequestedDate":"2013-11-11T00:00:00","RequestCount":0.746},{"RequestedDate":"2013-11-12T00:00:00","RequestCount":0.7442},{"RequestedDate":"2013-11-13T00:00:00","RequestCount":0.7415},{"RequestedDate":"2013-11-14T00:00:00","RequestCount":0.7429},{"RequestedDate":"2013-11-15T00:00:00","RequestCount":0.741},{"RequestedDate":"2013-11-17T00:00:00","RequestCount":0.7417},{"RequestedDate":"2013-11-18T00:00:00","RequestCount":0.7405},{"RequestedDate":"2013-11-19T00:00:00","RequestCount":0.7386},{"RequestedDate":"2013-11-20T00:00:00","RequestCount":0.7441},{"RequestedDate":"2013-11-21T00:00:00","RequestCount":0.7418},{"RequestedDate":"2013-11-22T00:00:00","RequestCount":0.7376},{"RequestedDate":"2013-11-24T00:00:00","RequestCount":0.7379},{"RequestedDate":"2013-11-25T00:00:00","RequestCount":0.7399},{"RequestedDate":"2013-11-26T00:00:00","RequestCount":0.7369},{"RequestedDate":"2013-11-27T00:00:00","RequestCount":0.7365},{"RequestedDate":"2013-11-28T00:00:00","RequestCount":0.735},{"RequestedDate":"2013-11-29T00:00:00","RequestCount":0.7358},{"RequestedDate":"2013-12-01T00:00:00","RequestCount":0.7362},{"RequestedDate":"2013-12-02T00:00:00","RequestCount":0.7385},{"RequestedDate":"2013-12-03T00:00:00","RequestCount":0.7359},{"RequestedDate":"2013-12-04T00:00:00","RequestCount":0.7357},{"RequestedDate":"2013-12-05T00:00:00","RequestCount":0.7317},{"RequestedDate":"2013-12-06T00:00:00","RequestCount":0.7297},{"RequestedDate":"2013-12-08T00:00:00","RequestCount":0.7296},{"RequestedDate":"2013-12-09T00:00:00","RequestCount":0.7279},{"RequestedDate":"2013-12-10T00:00:00","RequestCount":0.7267},{"RequestedDate":"2013-12-11T00:00:00","RequestCount":0.7254},{"RequestedDate":"2013-12-12T00:00:00","RequestCount":0.727},{"RequestedDate":"2013-12-13T00:00:00","RequestCount":0.7276},{"RequestedDate":"2013-12-15T00:00:00","RequestCount":0.7278},{"RequestedDate":"2013-12-16T00:00:00","RequestCount":0.7267},{"RequestedDate":"2013-12-17T00:00:00","RequestCount":0.7263},{"RequestedDate":"2013-12-18T00:00:00","RequestCount":0.7307},{"RequestedDate":"2013-12-19T00:00:00","RequestCount":0.7319},{"RequestedDate":"2013-12-20T00:00:00","RequestCount":0.7315},{"RequestedDate":"2013-12-22T00:00:00","RequestCount":0.7311},{"RequestedDate":"2013-12-23T00:00:00","RequestCount":0.7301},{"RequestedDate":"2013-12-24T00:00:00","RequestCount":0.7308},{"RequestedDate":"2013-12-25T00:00:00","RequestCount":0.731},{"RequestedDate":"2013-12-26T00:00:00","RequestCount":0.7304},{"RequestedDate":"2013-12-27T00:00:00","RequestCount":0.7277},{"RequestedDate":"2013-12-29T00:00:00","RequestCount":0.7272},{"RequestedDate":"2013-12-30T00:00:00","RequestCount":0.7244},{"RequestedDate":"2013-12-31T00:00:00","RequestCount":0.7275},{"RequestedDate":"2014-01-01T00:00:00","RequestCount":0.7271},{"RequestedDate":"2014-01-02T00:00:00","RequestCount":0.7314},{"RequestedDate":"2014-01-03T00:00:00","RequestCount":0.7359},{"RequestedDate":"2014-01-05T00:00:00","RequestCount":0.7355},{"RequestedDate":"2014-01-06T00:00:00","RequestCount":0.7338},{"RequestedDate":"2014-01-07T00:00:00","RequestCount":0.7345},{"RequestedDate":"2014-01-08T00:00:00","RequestCount":0.7366},{"RequestedDate":"2014-01-09T00:00:00","RequestCount":0.7349},{"RequestedDate":"2014-01-10T00:00:00","RequestCount":0.7316},{"RequestedDate":"2014-01-12T00:00:00","RequestCount":0.7315},{"RequestedDate":"2014-01-13T00:00:00","RequestCount":0.7315},{"RequestedDate":"2014-01-14T00:00:00","RequestCount":0.731},{"RequestedDate":"2014-01-15T00:00:00","RequestCount":0.735},{"RequestedDate":"2014-01-16T00:00:00","RequestCount":0.7341},{"RequestedDate":"2014-01-17T00:00:00","RequestCount":0.7385},{"RequestedDate":"2014-01-19T00:00:00","RequestCount":0.7392},{"RequestedDate":"2014-01-20T00:00:00","RequestCount":0.7379},{"RequestedDate":"2014-01-21T00:00:00","RequestCount":0.7373},{"RequestedDate":"2014-01-22T00:00:00","RequestCount":0.7381},{"RequestedDate":"2014-01-23T00:00:00","RequestCount":0.7301},{"RequestedDate":"2014-01-24T00:00:00","RequestCount":0.7311},{"RequestedDate":"2014-01-26T00:00:00","RequestCount":0.7306},{"RequestedDate":"2014-01-27T00:00:00","RequestCount":0.7314},{"RequestedDate":"2014-01-28T00:00:00","RequestCount":0.7316},{"RequestedDate":"2014-01-29T00:00:00","RequestCount":0.7319},{"RequestedDate":"2014-01-30T00:00:00","RequestCount":0.7377},{"RequestedDate":"2014-01-31T00:00:00","RequestCount":0.7415},{"RequestedDate":"2014-02-02T00:00:00","RequestCount":0.7414},{"RequestedDate":"2014-02-03T00:00:00","RequestCount":0.7393},{"RequestedDate":"2014-02-04T00:00:00","RequestCount":0.7397},{"RequestedDate":"2014-02-05T00:00:00","RequestCount":0.7389},{"RequestedDate":"2014-02-06T00:00:00","RequestCount":0.7358},{"RequestedDate":"2014-02-07T00:00:00","RequestCount":0.7334},{"RequestedDate":"2014-02-09T00:00:00","RequestCount":0.7343},{"RequestedDate":"2014-02-10T00:00:00","RequestCount":0.7328},{"RequestedDate":"2014-02-11T00:00:00","RequestCount":0.7332},{"RequestedDate":"2014-02-12T00:00:00","RequestCount":0.7356},{"RequestedDate":"2014-02-13T00:00:00","RequestCount":0.7309},{"RequestedDate":"2014-02-14T00:00:00","RequestCount":0.7304},{"RequestedDate":"2014-02-16T00:00:00","RequestCount":0.73},{"RequestedDate":"2014-02-17T00:00:00","RequestCount":0.7295},{"RequestedDate":"2014-02-18T00:00:00","RequestCount":0.7268},{"RequestedDate":"2014-02-19T00:00:00","RequestCount":0.7281},{"RequestedDate":"2014-02-20T00:00:00","RequestCount":0.7289},{"RequestedDate":"2014-02-21T00:00:00","RequestCount":0.7278},{"RequestedDate":"2014-02-23T00:00:00","RequestCount":0.728},{"RequestedDate":"2014-02-24T00:00:00","RequestCount":0.728},{"RequestedDate":"2014-02-25T00:00:00","RequestCount":0.7275},{"RequestedDate":"2014-02-26T00:00:00","RequestCount":0.7306},{"RequestedDate":"2014-02-27T00:00:00","RequestCount":0.7295},{"RequestedDate":"2014-02-28T00:00:00","RequestCount":0.7245},{"RequestedDate":"2014-03-02T00:00:00","RequestCount":0.7259},{"RequestedDate":"2014-03-03T00:00:00","RequestCount":0.728},{"RequestedDate":"2014-03-04T00:00:00","RequestCount":0.7276},{"RequestedDate":"2014-03-05T00:00:00","RequestCount":0.7282},{"RequestedDate":"2014-03-06T00:00:00","RequestCount":0.7215},{"RequestedDate":"2014-03-07T00:00:00","RequestCount":0.7206},{"RequestedDate":"2014-03-09T00:00:00","RequestCount":0.7206},{"RequestedDate":"2014-03-10T00:00:00","RequestCount":0.7207},{"RequestedDate":"2014-03-11T00:00:00","RequestCount":0.7216},{"RequestedDate":"2014-03-12T00:00:00","RequestCount":0.7192},{"RequestedDate":"2014-03-13T00:00:00","RequestCount":0.721},{"RequestedDate":"2014-03-14T00:00:00","RequestCount":0.7187},{"RequestedDate":"2014-03-16T00:00:00","RequestCount":0.7188},{"RequestedDate":"2014-03-17T00:00:00","RequestCount":0.7183},{"RequestedDate":"2014-03-18T00:00:00","RequestCount":0.7177},{"RequestedDate":"2014-03-19T00:00:00","RequestCount":0.7229},{"RequestedDate":"2014-03-20T00:00:00","RequestCount":0.7258},{"RequestedDate":"2014-03-21T00:00:00","RequestCount":0.7249},{"RequestedDate":"2014-03-23T00:00:00","RequestCount":0.7247},{"RequestedDate":"2014-03-24T00:00:00","RequestCount":0.7226},{"RequestedDate":"2014-03-25T00:00:00","RequestCount":0.7232},{"RequestedDate":"2014-03-26T00:00:00","RequestCount":0.7255},{"RequestedDate":"2014-03-27T00:00:00","RequestCount":0.7278},{"RequestedDate":"2014-03-28T00:00:00","RequestCount":0.7271},{"RequestedDate":"2014-03-30T00:00:00","RequestCount":0.7272},{"RequestedDate":"2014-03-31T00:00:00","RequestCount":0.7261},{"RequestedDate":"2014-04-01T00:00:00","RequestCount":0.725},{"RequestedDate":"2014-04-02T00:00:00","RequestCount":0.7264},{"RequestedDate":"2014-04-03T00:00:00","RequestCount":0.7289},{"RequestedDate":"2014-04-04T00:00:00","RequestCount":0.7298},{"RequestedDate":"2014-04-06T00:00:00","RequestCount":0.7298},{"RequestedDate":"2014-04-07T00:00:00","RequestCount":0.7278},{"RequestedDate":"2014-04-08T00:00:00","RequestCount":0.7248},{"RequestedDate":"2014-04-09T00:00:00","RequestCount":0.7218},{"RequestedDate":"2014-04-10T00:00:00","RequestCount":0.72},{"RequestedDate":"2014-04-11T00:00:00","RequestCount":0.7202},{"RequestedDate":"2014-04-13T00:00:00","RequestCount":0.7222},{"RequestedDate":"2014-04-14T00:00:00","RequestCount":0.7236},{"RequestedDate":"2014-04-15T00:00:00","RequestCount":0.7239},{"RequestedDate":"2014-04-16T00:00:00","RequestCount":0.7238},{"RequestedDate":"2014-04-17T00:00:00","RequestCount":0.7238},{"RequestedDate":"2014-04-18T00:00:00","RequestCount":0.7238},{"RequestedDate":"2014-04-20T00:00:00","RequestCount":0.7239},{"RequestedDate":"2014-04-21T00:00:00","RequestCount":0.725},{"RequestedDate":"2014-04-22T00:00:00","RequestCount":0.7244},{"RequestedDate":"2014-04-23T00:00:00","RequestCount":0.7238},{"RequestedDate":"2014-04-24T00:00:00","RequestCount":0.7229},{"RequestedDate":"2014-04-25T00:00:00","RequestCount":0.7229},{"RequestedDate":"2014-04-27T00:00:00","RequestCount":0.7226},{"RequestedDate":"2014-04-28T00:00:00","RequestCount":0.722},{"RequestedDate":"2014-04-29T00:00:00","RequestCount":0.724},{"RequestedDate":"2014-04-30T00:00:00","RequestCount":0.7211},{"RequestedDate":"2014-05-01T00:00:00","RequestCount":0.721},{"RequestedDate":"2014-05-02T00:00:00","RequestCount":0.7209},{"RequestedDate":"2014-05-04T00:00:00","RequestCount":0.7209},{"RequestedDate":"2014-05-05T00:00:00","RequestCount":0.7207},{"RequestedDate":"2014-05-06T00:00:00","RequestCount":0.718},{"RequestedDate":"2014-05-07T00:00:00","RequestCount":0.7188},{"RequestedDate":"2014-05-08T00:00:00","RequestCount":0.7225},{"RequestedDate":"2014-05-09T00:00:00","RequestCount":0.7268},{"RequestedDate":"2014-05-11T00:00:00","RequestCount":0.7267},{"RequestedDate":"2014-05-12T00:00:00","RequestCount":0.7269},{"RequestedDate":"2014-05-13T00:00:00","RequestCount":0.7297},{"RequestedDate":"2014-05-14T00:00:00","RequestCount":0.7291},{"RequestedDate":"2014-05-15T00:00:00","RequestCount":0.7294},{"RequestedDate":"2014-05-16T00:00:00","RequestCount":0.7302},{"RequestedDate":"2014-05-18T00:00:00","RequestCount":0.7298},{"RequestedDate":"2014-05-19T00:00:00","RequestCount":0.7295},{"RequestedDate":"2014-05-20T00:00:00","RequestCount":0.7298},{"RequestedDate":"2014-05-21T00:00:00","RequestCount":0.7307},{"RequestedDate":"2014-05-22T00:00:00","RequestCount":0.7323},{"RequestedDate":"2014-05-23T00:00:00","RequestCount":0.7335},{"RequestedDate":"2014-05-25T00:00:00","RequestCount":0.7338},{"RequestedDate":"2014-05-26T00:00:00","RequestCount":0.7329},{"RequestedDate":"2014-05-27T00:00:00","RequestCount":0.7335},{"RequestedDate":"2014-05-28T00:00:00","RequestCount":0.7358},{"RequestedDate":"2014-05-29T00:00:00","RequestCount":0.7351},{"RequestedDate":"2014-05-30T00:00:00","RequestCount":0.7337},{"RequestedDate":"2014-06-01T00:00:00","RequestCount":0.7338},{"RequestedDate":"2014-06-02T00:00:00","RequestCount":0.7355},{"RequestedDate":"2014-06-03T00:00:00","RequestCount":0.7338},{"RequestedDate":"2014-06-04T00:00:00","RequestCount":0.7353},{"RequestedDate":"2014-06-05T00:00:00","RequestCount":0.7321},{"RequestedDate":"2014-06-06T00:00:00","RequestCount":0.733},{"RequestedDate":"2014-06-08T00:00:00","RequestCount":0.7327},{"RequestedDate":"2014-06-09T00:00:00","RequestCount":0.7356},{"RequestedDate":"2014-06-10T00:00:00","RequestCount":0.7381},{"RequestedDate":"2014-06-11T00:00:00","RequestCount":0.7389},{"RequestedDate":"2014-06-12T00:00:00","RequestCount":0.7379},{"RequestedDate":"2014-06-13T00:00:00","RequestCount":0.7384},{"RequestedDate":"2014-06-15T00:00:00","RequestCount":0.7388},{"RequestedDate":"2014-06-16T00:00:00","RequestCount":0.7367},{"RequestedDate":"2014-06-17T00:00:00","RequestCount":0.7382},{"RequestedDate":"2014-06-18T00:00:00","RequestCount":0.7356},{"RequestedDate":"2014-06-19T00:00:00","RequestCount":0.7349},{"RequestedDate":"2014-06-20T00:00:00","RequestCount":0.7353},{"RequestedDate":"2014-06-22T00:00:00","RequestCount":0.7357},{"RequestedDate":"2014-06-23T00:00:00","RequestCount":0.735},{"RequestedDate":"2014-06-24T00:00:00","RequestCount":0.735},{"RequestedDate":"2014-06-25T00:00:00","RequestCount":0.7337},{"RequestedDate":"2014-06-26T00:00:00","RequestCount":0.7347},{"RequestedDate":"2014-06-27T00:00:00","RequestCount":0.7327},{"RequestedDate":"2014-06-29T00:00:00","RequestCount":0.733},{"RequestedDate":"2014-06-30T00:00:00","RequestCount":0.7304},{"RequestedDate":"2014-07-01T00:00:00","RequestCount":0.731},{"RequestedDate":"2014-07-02T00:00:00","RequestCount":0.732},{"RequestedDate":"2014-07-03T00:00:00","RequestCount":0.7347},{"RequestedDate":"2014-07-04T00:00:00","RequestCount":0.7356},{"RequestedDate":"2014-07-06T00:00:00","RequestCount":0.736},{"RequestedDate":"2014-07-07T00:00:00","RequestCount":0.735},{"RequestedDate":"2014-07-08T00:00:00","RequestCount":0.7346},{"RequestedDate":"2014-07-09T00:00:00","RequestCount":0.7329},{"RequestedDate":"2014-07-10T00:00:00","RequestCount":0.7348},{"RequestedDate":"2014-07-11T00:00:00","RequestCount":0.7349},{"RequestedDate":"2014-07-13T00:00:00","RequestCount":0.7352},{"RequestedDate":"2014-07-14T00:00:00","RequestCount":0.7342},{"RequestedDate":"2014-07-15T00:00:00","RequestCount":0.7369},{"RequestedDate":"2014-07-16T00:00:00","RequestCount":0.7393},{"RequestedDate":"2014-07-17T00:00:00","RequestCount":0.7392},{"RequestedDate":"2014-07-18T00:00:00","RequestCount":0.7394},{"RequestedDate":"2014-07-20T00:00:00","RequestCount":0.739},{"RequestedDate":"2014-07-21T00:00:00","RequestCount":0.7395},{"RequestedDate":"2014-07-22T00:00:00","RequestCount":0.7427},{"RequestedDate":"2014-07-23T00:00:00","RequestCount":0.7427},{"RequestedDate":"2014-07-24T00:00:00","RequestCount":0.7428},{"RequestedDate":"2014-07-25T00:00:00","RequestCount":0.7446},{"RequestedDate":"2014-07-27T00:00:00","RequestCount":0.7447},{"RequestedDate":"2014-07-28T00:00:00","RequestCount":0.744},{"RequestedDate":"2014-07-29T00:00:00","RequestCount":0.7458},{"RequestedDate":"2014-07-30T00:00:00","RequestCount":0.7464},{"RequestedDate":"2014-07-31T00:00:00","RequestCount":0.7469},{"RequestedDate":"2014-08-01T00:00:00","RequestCount":0.7446},{"RequestedDate":"2014-08-03T00:00:00","RequestCount":0.7447},{"RequestedDate":"2014-08-04T00:00:00","RequestCount":0.745},{"RequestedDate":"2014-08-05T00:00:00","RequestCount":0.7477},{"RequestedDate":"2014-08-06T00:00:00","RequestCount":0.7472},{"RequestedDate":"2014-08-07T00:00:00","RequestCount":0.7483},{"RequestedDate":"2014-08-08T00:00:00","RequestCount":0.7457},{"RequestedDate":"2014-08-10T00:00:00","RequestCount":0.746},{"RequestedDate":"2014-08-11T00:00:00","RequestCount":0.747},{"RequestedDate":"2014-08-12T00:00:00","RequestCount":0.748},{"RequestedDate":"2014-08-13T00:00:00","RequestCount":0.7482},{"RequestedDate":"2014-08-14T00:00:00","RequestCount":0.7482},{"RequestedDate":"2014-08-15T00:00:00","RequestCount":0.7463},{"RequestedDate":"2014-08-17T00:00:00","RequestCount":0.7469},{"RequestedDate":"2014-08-18T00:00:00","RequestCount":0.7483},{"RequestedDate":"2014-08-19T00:00:00","RequestCount":0.7508},{"RequestedDate":"2014-08-20T00:00:00","RequestCount":0.7541},{"RequestedDate":"2014-08-21T00:00:00","RequestCount":0.7529},{"RequestedDate":"2014-08-22T00:00:00","RequestCount":0.7551},{"RequestedDate":"2014-08-24T00:00:00","RequestCount":0.7577},{"RequestedDate":"2014-08-25T00:00:00","RequestCount":0.758},{"RequestedDate":"2014-08-26T00:00:00","RequestCount":0.7593},{"RequestedDate":"2014-08-27T00:00:00","RequestCount":0.758},{"RequestedDate":"2014-08-28T00:00:00","RequestCount":0.7585},{"RequestedDate":"2014-08-29T00:00:00","RequestCount":0.7614},{"RequestedDate":"2014-08-31T00:00:00","RequestCount":0.7618},{"RequestedDate":"2014-09-01T00:00:00","RequestCount":0.7618},{"RequestedDate":"2014-09-02T00:00:00","RequestCount":0.7614},{"RequestedDate":"2014-09-03T00:00:00","RequestCount":0.7604},{"RequestedDate":"2014-09-04T00:00:00","RequestCount":0.7725},{"RequestedDate":"2014-09-05T00:00:00","RequestCount":0.7722},{"RequestedDate":"2014-09-07T00:00:00","RequestCount":0.7721},{"RequestedDate":"2014-09-08T00:00:00","RequestCount":0.7753},{"RequestedDate":"2014-09-09T00:00:00","RequestCount":0.773},{"RequestedDate":"2014-09-10T00:00:00","RequestCount":0.7742},{"RequestedDate":"2014-09-11T00:00:00","RequestCount":0.7736},{"RequestedDate":"2014-09-12T00:00:00","RequestCount":0.7713},{"RequestedDate":"2014-09-14T00:00:00","RequestCount":0.7717},{"RequestedDate":"2014-09-15T00:00:00","RequestCount":0.7727},{"RequestedDate":"2014-09-16T00:00:00","RequestCount":0.7716},{"RequestedDate":"2014-09-17T00:00:00","RequestCount":0.7772},{"RequestedDate":"2014-09-18T00:00:00","RequestCount":0.7739},{"RequestedDate":"2014-09-19T00:00:00","RequestCount":0.7794},{"RequestedDate":"2014-09-21T00:00:00","RequestCount":0.7788},{"RequestedDate":"2014-09-22T00:00:00","RequestCount":0.7782},{"RequestedDate":"2014-09-23T00:00:00","RequestCount":0.7784},{"RequestedDate":"2014-09-24T00:00:00","RequestCount":0.7824},{"RequestedDate":"2014-09-25T00:00:00","RequestCount":0.7843},{"RequestedDate":"2014-09-26T00:00:00","RequestCount":0.7884},{"RequestedDate":"2014-09-28T00:00:00","RequestCount":0.7891},{"RequestedDate":"2014-09-29T00:00:00","RequestCount":0.7883},{"RequestedDate":"2014-09-30T00:00:00","RequestCount":0.7916},{"RequestedDate":"2014-10-01T00:00:00","RequestCount":0.7922},{"RequestedDate":"2014-10-02T00:00:00","RequestCount":0.7893},{"RequestedDate":"2014-10-03T00:00:00","RequestCount":0.7989},{"RequestedDate":"2014-10-05T00:00:00","RequestCount":0.7992},{"RequestedDate":"2014-10-06T00:00:00","RequestCount":0.7903},{"RequestedDate":"2014-10-07T00:00:00","RequestCount":0.7893},{"RequestedDate":"2014-10-08T00:00:00","RequestCount":0.7853},{"RequestedDate":"2014-10-09T00:00:00","RequestCount":0.788},{"RequestedDate":"2014-10-10T00:00:00","RequestCount":0.7919},{"RequestedDate":"2014-10-12T00:00:00","RequestCount":0.7912},{"RequestedDate":"2014-10-13T00:00:00","RequestCount":0.7842},{"RequestedDate":"2014-10-14T00:00:00","RequestCount":0.79},{"RequestedDate":"2014-10-15T00:00:00","RequestCount":0.779},{"RequestedDate":"2014-10-16T00:00:00","RequestCount":0.7806},{"RequestedDate":"2014-10-17T00:00:00","RequestCount":0.7835},{"RequestedDate":"2014-10-19T00:00:00","RequestCount":0.7844},{"RequestedDate":"2014-10-20T00:00:00","RequestCount":0.7813},{"RequestedDate":"2014-10-21T00:00:00","RequestCount":0.7864},{"RequestedDate":"2014-10-22T00:00:00","RequestCount":0.7905},{"RequestedDate":"2014-10-23T00:00:00","RequestCount":0.7907},{"RequestedDate":"2014-10-24T00:00:00","RequestCount":0.7893},{"RequestedDate":"2014-10-26T00:00:00","RequestCount":0.7889},{"RequestedDate":"2014-10-27T00:00:00","RequestCount":0.7875},{"RequestedDate":"2014-10-28T00:00:00","RequestCount":0.7853},{"RequestedDate":"2014-10-29T00:00:00","RequestCount":0.7916},{"RequestedDate":"2014-10-30T00:00:00","RequestCount":0.7929},{"RequestedDate":"2014-10-31T00:00:00","RequestCount":0.7984},{"RequestedDate":"2014-11-02T00:00:00","RequestCount":0.7999},{"RequestedDate":"2014-11-03T00:00:00","RequestCount":0.8012},{"RequestedDate":"2014-11-04T00:00:00","RequestCount":0.7971},{"RequestedDate":"2014-11-05T00:00:00","RequestCount":0.8009},{"RequestedDate":"2014-11-06T00:00:00","RequestCount":0.8081},{"RequestedDate":"2014-11-07T00:00:00","RequestCount":0.803},{"RequestedDate":"2014-11-09T00:00:00","RequestCount":0.8025},{"RequestedDate":"2014-11-10T00:00:00","RequestCount":0.8051},{"RequestedDate":"2014-11-11T00:00:00","RequestCount":0.8016},{"RequestedDate":"2014-11-12T00:00:00","RequestCount":0.804},{"RequestedDate":"2014-11-13T00:00:00","RequestCount":0.8015},{"RequestedDate":"2014-11-14T00:00:00","RequestCount":0.7985},{"RequestedDate":"2014-11-16T00:00:00","RequestCount":0.7988},{"RequestedDate":"2014-11-17T00:00:00","RequestCount":0.8032},{"RequestedDate":"2014-11-18T00:00:00","RequestCount":0.7976},{"RequestedDate":"2014-11-19T00:00:00","RequestCount":0.7965},{"RequestedDate":"2014-11-20T00:00:00","RequestCount":0.7975},{"RequestedDate":"2014-11-21T00:00:00","RequestCount":0.8071},{"RequestedDate":"2014-11-23T00:00:00","RequestCount":0.8082},{"RequestedDate":"2014-11-24T00:00:00","RequestCount":0.8037},{"RequestedDate":"2014-11-25T00:00:00","RequestCount":0.8016},{"RequestedDate":"2014-11-26T00:00:00","RequestCount":0.7996},{"RequestedDate":"2014-11-27T00:00:00","RequestCount":0.8022},{"RequestedDate":"2014-11-28T00:00:00","RequestCount":0.8031},{"RequestedDate":"2014-11-30T00:00:00","RequestCount":0.804},{"RequestedDate":"2014-12-01T00:00:00","RequestCount":0.802},{"RequestedDate":"2014-12-02T00:00:00","RequestCount":0.8075},{"RequestedDate":"2014-12-03T00:00:00","RequestCount":0.8123},{"RequestedDate":"2014-12-04T00:00:00","RequestCount":0.8078},{"RequestedDate":"2014-12-05T00:00:00","RequestCount":0.8139},{"RequestedDate":"2014-12-07T00:00:00","RequestCount":0.8135},{"RequestedDate":"2014-12-08T00:00:00","RequestCount":0.8119},{"RequestedDate":"2014-12-09T00:00:00","RequestCount":0.8081},{"RequestedDate":"2014-12-10T00:00:00","RequestCount":0.8034},{"RequestedDate":"2014-12-11T00:00:00","RequestCount":0.8057},{"RequestedDate":"2014-12-12T00:00:00","RequestCount":0.8024},{"RequestedDate":"2014-12-14T00:00:00","RequestCount":0.8024},{"RequestedDate":"2014-12-15T00:00:00","RequestCount":0.804},{"RequestedDate":"2014-12-16T00:00:00","RequestCount":0.7993},{"RequestedDate":"2014-12-17T00:00:00","RequestCount":0.8102},{"RequestedDate":"2014-12-18T00:00:00","RequestCount":0.8139},{"RequestedDate":"2014-12-19T00:00:00","RequestCount":0.8177},{"RequestedDate":"2014-12-21T00:00:00","RequestCount":0.818},{"RequestedDate":"2014-12-22T00:00:00","RequestCount":0.8176},{"RequestedDate":"2014-12-23T00:00:00","RequestCount":0.8215},{"RequestedDate":"2014-12-24T00:00:00","RequestCount":0.82},{"RequestedDate":"2014-12-25T00:00:00","RequestCount":0.8182},{"RequestedDate":"2014-12-26T00:00:00","RequestCount":0.8213},{"RequestedDate":"2014-12-28T00:00:00","RequestCount":0.8218},{"RequestedDate":"2014-12-29T00:00:00","RequestCount":0.8229},{"RequestedDate":"2014-12-30T00:00:00","RequestCount":0.8225},{"RequestedDate":"2014-12-31T00:00:00","RequestCount":0.8266},{"RequestedDate":"2015-01-01T00:00:00","RequestCount":0.8262},{"RequestedDate":"2015-01-02T00:00:00","RequestCount":0.8331},{"RequestedDate":"2015-01-04T00:00:00","RequestCount":0.8371},{"RequestedDate":"2015-01-05T00:00:00","RequestCount":0.838},{"RequestedDate":"2015-01-06T00:00:00","RequestCount":0.8411},{"RequestedDate":"2015-01-07T00:00:00","RequestCount":0.8447},{"RequestedDate":"2015-01-08T00:00:00","RequestCount":0.848},{"RequestedDate":"2015-01-09T00:00:00","RequestCount":0.8445},{"RequestedDate":"2015-01-11T00:00:00","RequestCount":0.8425},{"RequestedDate":"2015-01-12T00:00:00","RequestCount":0.8451},{"RequestedDate":"2015-01-13T00:00:00","RequestCount":0.8495},{"RequestedDate":"2015-01-14T00:00:00","RequestCount":0.8482},{"RequestedDate":"2015-01-15T00:00:00","RequestCount":0.8598},{"RequestedDate":"2015-01-16T00:00:00","RequestCount":0.8643},{"RequestedDate":"2015-01-18T00:00:00","RequestCount":0.8648},{"RequestedDate":"2015-01-19T00:00:00","RequestCount":0.8617},{"RequestedDate":"2015-01-20T00:00:00","RequestCount":0.8658},{"RequestedDate":"2015-01-21T00:00:00","RequestCount":0.8613},{"RequestedDate":"2015-01-22T00:00:00","RequestCount":0.8798},{"RequestedDate":"2015-01-23T00:00:00","RequestCount":0.8922},{"RequestedDate":"2015-01-25T00:00:00","RequestCount":0.899},{"RequestedDate":"2015-01-26T00:00:00","RequestCount":0.8898},{"RequestedDate":"2015-01-27T00:00:00","RequestCount":0.8787},{"RequestedDate":"2015-01-28T00:00:00","RequestCount":0.8859},{"RequestedDate":"2015-01-29T00:00:00","RequestCount":0.8834},{"RequestedDate":"2015-01-30T00:00:00","RequestCount":0.8859},{"RequestedDate":"2015-02-01T00:00:00","RequestCount":0.8843},{"RequestedDate":"2015-02-02T00:00:00","RequestCount":0.8817},{"RequestedDate":"2015-02-03T00:00:00","RequestCount":0.871},{"RequestedDate":"2015-02-04T00:00:00","RequestCount":0.8813},{"RequestedDate":"2015-02-05T00:00:00","RequestCount":0.8713},{"RequestedDate":"2015-02-06T00:00:00","RequestCount":0.8837},{"RequestedDate":"2015-02-08T00:00:00","RequestCount":0.8839},{"RequestedDate":"2015-02-09T00:00:00","RequestCount":0.8831},{"RequestedDate":"2015-02-10T00:00:00","RequestCount":0.8833},{"RequestedDate":"2015-02-11T00:00:00","RequestCount":0.8823},{"RequestedDate":"2015-02-12T00:00:00","RequestCount":0.877},{"RequestedDate":"2015-02-13T00:00:00","RequestCount":0.8783},{"RequestedDate":"2015-02-15T00:00:00","RequestCount":0.8774},{"RequestedDate":"2015-02-16T00:00:00","RequestCount":0.8807},{"RequestedDate":"2015-02-17T00:00:00","RequestCount":0.8762},{"RequestedDate":"2015-02-18T00:00:00","RequestCount":0.8774},{"RequestedDate":"2015-02-19T00:00:00","RequestCount":0.8798},{"RequestedDate":"2015-02-20T00:00:00","RequestCount":0.8787},{"RequestedDate":"2015-02-22T00:00:00","RequestCount":0.8787},{"RequestedDate":"2015-02-23T00:00:00","RequestCount":0.8824},{"RequestedDate":"2015-02-24T00:00:00","RequestCount":0.8818},{"RequestedDate":"2015-02-25T00:00:00","RequestCount":0.8801},{"RequestedDate":"2015-02-26T00:00:00","RequestCount":0.8931},{"RequestedDate":"2015-02-27T00:00:00","RequestCount":0.8932},{"RequestedDate":"2015-03-01T00:00:00","RequestCount":0.896},{"RequestedDate":"2015-03-02T00:00:00","RequestCount":0.8941},{"RequestedDate":"2015-03-03T00:00:00","RequestCount":0.8948},{"RequestedDate":"2015-03-04T00:00:00","RequestCount":0.9026},{"RequestedDate":"2015-03-05T00:00:00","RequestCount":0.9066},{"RequestedDate":"2015-03-06T00:00:00","RequestCount":0.9222},{"RequestedDate":"2015-03-08T00:00:00","RequestCount":0.9221},{"RequestedDate":"2015-03-09T00:00:00","RequestCount":0.9214},{"RequestedDate":"2015-03-10T00:00:00","RequestCount":0.9347},{"RequestedDate":"2015-03-11T00:00:00","RequestCount":0.9482},{"RequestedDate":"2015-03-12T00:00:00","RequestCount":0.9403},{"RequestedDate":"2015-03-13T00:00:00","RequestCount":0.9528},{"RequestedDate":"2015-03-15T00:00:00","RequestCount":0.9541},{"RequestedDate":"2015-03-16T00:00:00","RequestCount":0.9462},{"RequestedDate":"2015-03-17T00:00:00","RequestCount":0.9435},{"RequestedDate":"2015-03-18T00:00:00","RequestCount":0.9203},{"RequestedDate":"2015-03-19T00:00:00","RequestCount":0.9381},{"RequestedDate":"2015-03-20T00:00:00","RequestCount":0.9241},{"RequestedDate":"2015-03-22T00:00:00","RequestCount":0.9237},{"RequestedDate":"2015-03-23T00:00:00","RequestCount":0.9135},{"RequestedDate":"2015-03-24T00:00:00","RequestCount":0.9152},{"RequestedDate":"2015-03-25T00:00:00","RequestCount":0.9114},{"RequestedDate":"2015-03-26T00:00:00","RequestCount":0.9188},{"RequestedDate":"2015-03-27T00:00:00","RequestCount":0.9184},{"RequestedDate":"2015-03-29T00:00:00","RequestCount":0.9188},{"RequestedDate":"2015-03-30T00:00:00","RequestCount":0.9231},{"RequestedDate":"2015-03-31T00:00:00","RequestCount":0.9319},{"RequestedDate":"2015-04-01T00:00:00","RequestCount":0.9291},{"RequestedDate":"2015-04-02T00:00:00","RequestCount":0.9188},{"RequestedDate":"2015-04-03T00:00:00","RequestCount":0.9109},{"RequestedDate":"2015-04-05T00:00:00","RequestCount":0.9091},{"RequestedDate":"2015-04-06T00:00:00","RequestCount":0.9154},{"RequestedDate":"2015-04-07T00:00:00","RequestCount":0.9246},{"RequestedDate":"2015-04-08T00:00:00","RequestCount":0.9276},{"RequestedDate":"2015-04-09T00:00:00","RequestCount":0.9382},{"RequestedDate":"2015-04-10T00:00:00","RequestCount":0.9431},{"RequestedDate":"2015-04-12T00:00:00","RequestCount":0.9426},{"RequestedDate":"2015-04-13T00:00:00","RequestCount":0.9463},{"RequestedDate":"2015-04-14T00:00:00","RequestCount":0.9386},{"RequestedDate":"2015-04-15T00:00:00","RequestCount":0.9357},{"RequestedDate":"2015-04-16T00:00:00","RequestCount":0.9293},{"RequestedDate":"2015-04-17T00:00:00","RequestCount":0.9254},{"RequestedDate":"2015-04-19T00:00:00","RequestCount":0.9251},{"RequestedDate":"2015-04-20T00:00:00","RequestCount":0.9312},{"RequestedDate":"2015-04-21T00:00:00","RequestCount":0.9315},{"RequestedDate":"2015-04-22T00:00:00","RequestCount":0.9323},{"RequestedDate":"2015-04-23T00:00:00","RequestCount":0.9236},{"RequestedDate":"2015-04-24T00:00:00","RequestCount":0.9196},{"RequestedDate":"2015-04-26T00:00:00","RequestCount":0.9201},{"RequestedDate":"2015-04-27T00:00:00","RequestCount":0.9184},{"RequestedDate":"2015-04-28T00:00:00","RequestCount":0.9106},{"RequestedDate":"2015-04-29T00:00:00","RequestCount":0.8983},{"RequestedDate":"2015-04-30T00:00:00","RequestCount":0.8909},{"RequestedDate":"2015-05-01T00:00:00","RequestCount":0.8928},{"RequestedDate":"2015-05-03T00:00:00","RequestCount":0.8941},{"RequestedDate":"2015-05-04T00:00:00","RequestCount":0.8972},{"RequestedDate":"2015-05-05T00:00:00","RequestCount":0.894},{"RequestedDate":"2015-05-06T00:00:00","RequestCount":0.8808},{"RequestedDate":"2015-05-07T00:00:00","RequestCount":0.8876},{"RequestedDate":"2015-05-08T00:00:00","RequestCount":0.8925},{"RequestedDate":"2015-05-10T00:00:00","RequestCount":0.8934},{"RequestedDate":"2015-05-11T00:00:00","RequestCount":0.8964},{"RequestedDate":"2015-05-12T00:00:00","RequestCount":0.8917},{"RequestedDate":"2015-05-13T00:00:00","RequestCount":0.8805},{"RequestedDate":"2015-05-14T00:00:00","RequestCount":0.8764},{"RequestedDate":"2015-05-15T00:00:00","RequestCount":0.8732},{"RequestedDate":"2015-05-17T00:00:00","RequestCount":0.8737},{"RequestedDate":"2015-05-18T00:00:00","RequestCount":0.8838},{"RequestedDate":"2015-05-19T00:00:00","RequestCount":0.8969},{"RequestedDate":"2015-05-20T00:00:00","RequestCount":0.9014},{"RequestedDate":"2015-05-21T00:00:00","RequestCount":0.8999},{"RequestedDate":"2015-05-22T00:00:00","RequestCount":0.9076},{"RequestedDate":"2015-05-24T00:00:00","RequestCount":0.9098},{"RequestedDate":"2015-05-25T00:00:00","RequestCount":0.911},{"RequestedDate":"2015-05-26T00:00:00","RequestCount":0.9196},{"RequestedDate":"2015-05-27T00:00:00","RequestCount":0.917},{"RequestedDate":"2015-05-28T00:00:00","RequestCount":0.9133},{"RequestedDate":"2015-05-29T00:00:00","RequestCount":0.9101},{"RequestedDate":"2015-05-31T00:00:00","RequestCount":0.9126},{"RequestedDate":"2015-06-01T00:00:00","RequestCount":0.9151},{"RequestedDate":"2015-06-02T00:00:00","RequestCount":0.8965},{"RequestedDate":"2015-06-03T00:00:00","RequestCount":0.8871},{"RequestedDate":"2015-06-04T00:00:00","RequestCount":0.8898},{"RequestedDate":"2015-06-05T00:00:00","RequestCount":0.8999},{"RequestedDate":"2015-06-07T00:00:00","RequestCount":0.9004},{"RequestedDate":"2015-06-08T00:00:00","RequestCount":0.8857},{"RequestedDate":"2015-06-09T00:00:00","RequestCount":0.8862},{"RequestedDate":"2015-06-10T00:00:00","RequestCount":0.8829},{"RequestedDate":"2015-06-11T00:00:00","RequestCount":0.8882},{"RequestedDate":"2015-06-12T00:00:00","RequestCount":0.8873},{"RequestedDate":"2015-06-14T00:00:00","RequestCount":0.8913},{"RequestedDate":"2015-06-15T00:00:00","RequestCount":0.8862},{"RequestedDate":"2015-06-16T00:00:00","RequestCount":0.8891},{"RequestedDate":"2015-06-17T00:00:00","RequestCount":0.8821},{"RequestedDate":"2015-06-18T00:00:00","RequestCount":0.8802},{"RequestedDate":"2015-06-19T00:00:00","RequestCount":0.8808},{"RequestedDate":"2015-06-21T00:00:00","RequestCount":0.8794},{"RequestedDate":"2015-06-22T00:00:00","RequestCount":0.8818},{"RequestedDate":"2015-06-23T00:00:00","RequestCount":0.8952},{"RequestedDate":"2015-06-24T00:00:00","RequestCount":0.8924},{"RequestedDate":"2015-06-25T00:00:00","RequestCount":0.8925},{"RequestedDate":"2015-06-26T00:00:00","RequestCount":0.8955},{"RequestedDate":"2015-06-28T00:00:00","RequestCount":0.9113},{"RequestedDate":"2015-06-29T00:00:00","RequestCount":0.89},{"RequestedDate":"2015-06-30T00:00:00","RequestCount":0.895}],
    "locations": [

      {
        "code": "AF",
        "value": 53,
        "name": "Afghanistan"
      },
      {
        "code": "AL",
        "value": 117,
        "name": "Albania"
      },
      {
        "code": "DZ",
        "value": 15,
        "name": "Algeria"
      },
      {
        "code": "AS",
        "value": 342,
        "name": "American Samoa"
      },
      {
        "code": "AD",
        "value": 181,
        "name": "Andorra"
      },
      {
        "code": "AO",
        "value": 15,
        "name": "Angola"
      },
      {
        "code": "AI",
        "value": 202,
        "name": "Antigua and Barbuda"
      },
      {
        "code": "AR",
        "value": 15,
        "name": "Argentina"
      },
      {
        "code": "AM",
        "value": 109,
        "name": "Armenia"
      },
      {
        "code": "AW",
        "value": 597,
        "name": "Aruba"
      },
      {
        "code": "AU",
        "value": 3,
        "name": "Australia"
      },
      {
        "code": "AT",
        "value": 102,
        "name": "Austria"
      },
      {
        "code": "AZ",
        "value": 110,
        "name": "Azerbaijan"
      },
      {
        "code": "BS",
        "value": 34,
        "name": "Bahamas, The"
      },
      {
        "code": "BH",
        "value": 1660,
        "name": "Bahrain"
      },
      {
        "code": "BD",
        "value": 1142,
        "name": "Bangladesh"
      },
      {
        "code": "BB",
        "value": 636,
        "name": "Barbados"
      },
      {
        "code": "BY",
        "value": 47,
        "name": "Belarus"
      },
      {
        "code": "BE",
        "value": 359,
        "name": "Belgium"
      },
      {
        "code": "BZ",
        "value": 15,
        "name": "Belize"
      },
      {
        "code": "BJ",
        "value": 80,
        "name": "Benin"
      },
      {
        "code": "BM",
        "value": 1292,
        "name": "Bermuda"
      },
      {
        "code": "BT",
        "value": 19,
        "name": "Bhutan"
      },
      {
        "code": "BO",
        "value": 9,
        "name": "Bolivia"
      },
      {
        "code": "BA",
        "value": 73,
        "name": "Bosnia and Herzegovina"
      },
      {
        "code": "BW",
        "value": 4,
        "name": "Botswana"
      },
      {
        "code": "BR",
        "value": 23,
        "name": "Brazil"
      },
      {
        "code": "BN",
        "value": 76,
        "name": "Brunei Darussalam"
      },
      {
        "code": "BG",
        "value": 69,
        "name": "Bulgaria"
      },
      {
        "code": "BF",
        "value": 60,
        "name": "Burkina Faso"
      },
      {
        "code": "BI",
        "value": 326,
        "name": "Burundi"
      },
      {
        "code": "KH",
        "value": 80,
        "name": "Cambodia"
      },
      {
        "code": "CM",
        "value": 41,
        "name": "Cameroon"
      },
      {
        "code": "CA",
        "value": 4,
        "name": "Canada"
      },
      {
        "code": "CV",
        "value": 123,
        "name": "Cape Verde"
      },
      {
        "code": "KY",
        "value": 234,
        "name": "Cayman Islands"
      },
      {
        "code": "CF",
        "value": 7,
        "name": "Central African Republic"
      },
      {
        "code": "TD",
        "value": 9,
        "name": "Chad"
      },
      {
        "code": "CL",
        "value": 23,
        "name": "Chile"
      },
      {
        "code": "CN",
        "value": 143,
        "name": "China"
      },
      {
        "code": "CO",
        "value": 42,
        "name": "Colombia"
      },
      {
        "code": "KM",
        "value": 395,
        "name": "Comoros"
      },
      {
        "code": "CD",
        "value": 29,
        "name": "Congo, Dem. Rep."
      },
      {
        "code": "CG",
        "value": 12,
        "name": "Congo, Rep."
      },
      {
        "code": "CR",
        "value": 91,
        "name": "Costa Rica"
      },
      {
        "code": "CI",
        "value": 62,
        "name": "Cote d'Ivoire"
      },
      {
        "code": "HR",
        "value": 79,
        "name": "Croatia"
      },
      {
        "code": "CU",
        "value": 106,
        "name": "Cuba"
      },
      {
        "code": "CW",
        "value": 321,
        "name": "Curacao"
      },
      {
        "code": "CY",
        "value": 119,
        "name": "Cyprus"
      },
      {
        "code": "CZ",
        "value": 136,
        "name": "Czech Republic"
      },
      {
        "code": "DK",
        "value": 131,
        "name": "Denmark"
      },
      {
        "code": "DJ",
        "value": 38,
        "name": "Djibouti"
      },
      {
        "code": "DM",
        "value": 90,
        "name": "Dominica"
      },
      {
        "code": "DO",
        "value": 205,
        "name": "Dominican Republic"
      },
      {
        "code": "EC",
        "value": 58,
        "name": "Ecuador"
      },
      {
        "code": "EG",
        "value": 81,
        "name": "Egypt, Arab Rep."
      },
      {
        "code": "SV",
        "value": 299,
        "name": "El Salvador"
      },
      {
        "code": "GQ",
        "value": 25,
        "name": "Equatorial Guinea"
      },
      {
        "code": "ER",
        "value": 52,
        "name": "Eritrea"
      },
      {
        "code": "EE",
        "value": 32,
        "name": "Estonia"
      },
      {
        "code": "ET",
        "value": 83,
        "name": "Ethiopia"
      },
      {
        "code": "FO",
        "value": 35,
        "name": "Faeroe Islands"
      },
      {
        "code": "FJ",
        "value": 47,
        "name": "Fiji"
      },
      {
        "code": "FI",
        "value": 18,
        "name": "Finland"
      },
      {
        "code": "FR",
        "value": 118,
        "name": "France"
      },
      {
        "code": "PF",
        "value": 74,
        "name": "French Polynesia"
      },
      {
        "code": "GA",
        "value": 6,
        "name": "Gabon"
      },
      {
        "code": "GM",
        "value": 173,
        "name": "Gambia, The"
      },
      {
        "code": "GE",
        "value": 78,
        "name": "Georgia"
      },
      {
        "code": "DE",
        "value": 234,
        "name": "Germany"
      },
      {
        "code": "GH",
        "value": 107,
        "name": "Ghana"
      },
      {
        "code": "GR",
        "value": 88,
        "name": "Greece"
      },
      {
        "code": "GL",
        "value": 0,
        "name": "Greenland"
      },
      {
        "code": "GD",
        "value": 307,
        "name": "Grenada"
      },
      {
        "code": "GU",
        "value": 333,
        "name": "Guam"
      },
      {
        "code": "GT",
        "value": 134,
        "name": "Guatemala"
      },
      {
        "code": "GN",
        "value": 41,
        "name": "Guinea"
      },
      {
        "code": "GW",
        "value": 54,
        "name": "Guinea-Bissau"
      },
      {
        "code": "GY",
        "value": 4,
        "name": "Guyana"
      },
      {
        "code": "HT",
        "value": 363,
        "name": "Haiti"
      },
      {
        "code": "HN",
        "value": 68,
        "name": "Honduras"
      },
      {
        "code": "HK",
        "value": 6783,
        "name": "Hong Kong SAR, China"
      },
      {
        "code": "HU",
        "value": 112,
        "name": "Hungary"
      },
      {
        "code": "IS",
        "value": 3,
        "name": "Iceland"
      },
      {
        "code": "IN",
        "value": 394,
        "name": "India"
      },
      {
        "code": "ID",
        "value": 132,
        "name": "Indonesia"
      },
      {
        "code": "IR",
        "value": 45,
        "name": "Iran, Islamic Rep."
      },
      {
        "code": "IQ",
        "value": 73,
        "name": "Iraq"
      },
      {
        "code": "IE",
        "value": 65,
        "name": "Ireland"
      },
      {
        "code": "IM",
        "value": 145,
        "name": "Isle of Man"
      },
      {
        "code": "IL",
        "value": 352,
        "name": "Israel"
      },
      {
        "code": "IT",
        "value": 206,
        "name": "Italy"
      },
      {
        "code": "JM",
        "value": 250,
        "name": "Jamaica"
      },
      {
        "code": "JP",
        "value": 350,
        "name": "Japan"
      },
      {
        "code": "JO",
        "value": 69,
        "name": "Jordan"
      },
      {
        "code": "KZ",
        "value": 6,
        "name": "Kazakhstan"
      },
      {
        "code": "KE",
        "value": 71,
        "name": "Kenya"
      },
      {
        "code": "KI",
        "value": 123,
        "name": "Kiribati"
      },
      {
        "code": "KP",
        "value": 202,
        "name": "Korea, Dem. Rep."
      },
      {
        "code": "KR",
        "value": 504,
        "name": "Korea, Rep."
      },
      {
        "code": "XK",
        "value": 167,
        "name": "Kosovo"
      },
      {
        "code": "KW",
        "value": 154,
        "name": "Kuwait"
      },
      {
        "code": "KG",
        "value": 28,
        "name": "Kyrgyz Republic"
      },
      {
        "code": "LA",
        "value": 27,
        "name": "Lao PDR"
      },
      {
        "code": "LV",
        "value": 36,
        "name": "Latvia"
      },
      {
        "code": "LB",
        "value": 413,
        "name": "Lebanon"
      },
      {
        "code": "LS",
        "value": 72,
        "name": "Lesotho"
      },
      {
        "code": "LR",
        "value": 41,
        "name": "Liberia"
      },
      {
        "code": "LY",
        "value": 4,
        "name": "Libya"
      },
      {
        "code": "LI",
        "value": 225,
        "name": "Liechtenstein"
      },
      {
        "code": "LT",
        "value": 53,
        "name": "Lithuania"
      },
      {
        "code": "LU",
        "value": 195,
        "name": "Luxembourg"
      },
      {
        "code": "MO",
        "value": 19416,
        "name": "Macao SAR, China"
      },
      {
        "code": "MK",
        "value": 82,
        "name": "Macedonia, FYR"
      },
      {
        "code": "MG",
        "value": 36,
        "name": "Madagascar"
      },
      {
        "code": "MW",
        "value": 158,
        "name": "Malawi"
      },
      {
        "code": "MY",
        "value": 86,
        "name": "Malaysia"
      },
      {
        "code": "MV",
        "value": 1053,
        "name": "Maldives"
      },
      {
        "code": "ML",
        "value": 13,
        "name": "Mali"
      },
      {
        "code": "MT",
        "value": 1291,
        "name": "Malta"
      },
      {
        "code": "MH",
        "value": 300,
        "name": "Marshall Islands"
      },
      {
        "code": "MR",
        "value": 3,
        "name": "Mauritania"
      },
      {
        "code": "MU",
        "value": 631,
        "name": "Mauritius"
      },
      {
        "code": "YT",
        "value": 552,
        "name": "Mayotte"
      },
      {
        "code": "MX",
        "value": 58,
        "name": "Mexico"
      },
      {
        "code": "FM",
        "value": 159,
        "name": "Micronesia, Fed. Sts."
      },
      {
        "code": "MD",
        "value": 124,
        "name": "Moldova"
      },
      {
        "code": "MC",
        "value": 17704,
        "name": "Monaco"
      },
      {
        "code": "MN",
        "value": 2,
        "name": "Mongolia"
      },
      {
        "code": "ME",
        "value": 47,
        "name": "Montenegro"
      },
      {
        "code": "MA",
        "value": 72,
        "name": "Morocco"
      },
      {
        "code": "MZ",
        "value": 30,
        "name": "Mozambique"
      },
      {
        "code": "MM",
        "value": 73,
        "name": "Myanmar"
      },
      {
        "code": "NA",
        "value": 3,
        "name": "Namibia"
      },
      {
        "code": "NP",
        "value": 209,
        "name": "Nepal"
      },
      {
        "code": "NL",
        "value": 492,
        "name": "Netherlands"
      },
      {
        "code": "NC",
        "value": 14,
        "name": "New Caledonia"
      },
      {
        "code": "NZ",
        "value": 17,
        "name": "New Zealand"
      },
      {
        "code": "NI",
        "value": 48,
        "name": "Nicaragua"
      },
      {
        "code": "NE",
        "value": 12,
        "name": "Niger"
      },
      {
        "code": "NG",
        "value": 174,
        "name": "Nigeria"
      },
      {
        "code": "MP",
        "value": 132,
        "name": "Northern Mariana Islands"
      },
      {
        "code": "NO",
        "value": 16,
        "name": "Norway"
      },
      {
        "code": "OM",
        "value": 9,
        "name": "Oman"
      },
      {
        "code": "PK",
        "value": 225,
        "name": "Pakistan"
      },
      {
        "code": "PW",
        "value": 45,
        "name": "Palau"
      },
      {
        "code": "PA",
        "value": 47,
        "name": "Panama"
      },
      {
        "code": "PG",
        "value": 15,
        "name": "Papua New Guinea"
      },
      {
        "code": "PY",
        "value": 16,
        "name": "Paraguay"
      },
      {
        "code": "PE",
        "value": 23,
        "name": "Peru"
      },
      {
        "code": "PH",
        "value": 313,
        "name": "Philippines"
      },
      {
        "code": "PL",
        "value": 126,
        "name": "Poland"
      },
      {
        "code": "PT",
        "value": 116,
        "name": "Portugal"
      },
      {
        "code": "PR",
        "value": 449,
        "name": "Puerto Rico"
      },
      {
        "code": "WA",
        "value": 152,
        "name": "Qatar"
      },
      {
        "code": "RO",
        "value": 93,
        "name": "Romania"
      },
      {
        "code": "RU",
        "value": 9,
        "name": "Russian Federation"
      },
      {
        "code": "RW",
        "value": 431,
        "name": "Rwanda"
      },
      {
        "code": "WS",
        "value": 65,
        "name": "Samoa"
      },
      {
        "code": "SM",
        "value": 526,
        "name": "San Marino"
      },
      {
        "code": "ST",
        "value": 172,
        "name": "Sao Tome and Principe"
      },
      {
        "code": "SA",
        "value": 14,
        "name": "Saudi Arabia"
      },
      {
        "code": "SN",
        "value": 65,
        "name": "Senegal"
      },
      {
        "code": "RS",
        "value": 83,
        "name": "Serbia"
      },
      {
        "code": "SC",
        "value": 188,
        "name": "Seychelles"
      },
      {
        "code": "SL",
        "value": 82,
        "name": "Sierra Leone"
      },
      {
        "code": "SG",
        "value": 7252,
        "name": "Singapore"
      },
      {
        "code": "SK",
        "value": 113,
        "name": "Slovak Republic"
      },
      {
        "code": "SI",
        "value": 102,
        "name": "Slovenia"
      },
      {
        "code": "SB",
        "value": 19,
        "name": "Solomon Islands"
      },
      {
        "code": "SO",
        "value": 15,
        "name": "Somalia"
      },
      {
        "code": "ZA",
        "value": 41,
        "name": "South Africa"
      },
      {
        "code": "SS",
        "value": null,
        "name": "South Sudan"
      },
      {
        "code": "ES",
        "value": 92,
        "name": "Spain"
      },
      {
        "code": "LK",
        "value": 333,
        "name": "Sri Lanka"
      },
      {
        "code": "KN",
        "value": 202,
        "name": "St. Kitts and Nevis"
      },
      {
        "code": "LC",
        "value": 285,
        "name": "St. Lucia"
      },
      {
        "code": "MF",
        "value": 556,
        "name": "St. Martin (French part)"
      },
      {
        "code": "VC",
        "value": 280,
        "name": "St. Vincent and the Grenadines"
      },
      {
        "code": "SD",
        "value": 18,
        "name": "Sudan"
      },
      {
        "code": "SR",
        "value": 3,
        "name": "Suriname"
      },
      {
        "code": "SZ",
        "value": 69,
        "name": "Swaziland"
      },
      {
        "code": "SE",
        "value": 23,
        "name": "Sweden"
      },
      {
        "code": "CH",
        "value": 196,
        "name": "Switzerland"
      },
      {
        "code": "SY",
        "value": 111,
        "name": "Syrian Arab Republic"
      },
      {
        "code": "TJ",
        "value": 49,
        "name": "Tajikistan"
      },
      {
        "code": "TZ",
        "value": 51,
        "name": "Tanzania"
      },
      {
        "code": "TH",
        "value": 135,
        "name": "Thailand"
      },
      {
        "code": "TP",
        "value": 76,
        "name": "Timor-Leste"
      },
      {
        "code": "TG",
        "value": 111,
        "name": "Togo"
      },
      {
        "code": "TO",
        "value": 145,
        "name": "Tonga"
      },
      {
        "code": "TT",
        "value": 261,
        "name": "Trinidad and Tobago"
      },
      {
        "code": "TN",
        "value": 68,
        "name": "Tunisia"
      },
      {
        "code": "TR",
        "value": 95,
        "name": "Turkey"
      },
      {
        "code": "TM",
        "value": 11,
        "name": "Turkmenistan"
      },
      {
        "code": "TC",
        "value": 40,
        "name": "Turks and Caicos Islands"
      },
      {
        "code": "TV",
        "value": 328,
        "name": "Tuvalu"
      },
      {
        "code": "UG",
        "value": 170,
        "name": "Uganda"
      },
      {
        "code": "UA",
        "value": 79,
        "name": "Ukraine"
      },
      {
        "code": "AE",
        "value": 90,
        "name": "United Arab Emirates"
      },
      {
        "code": "US",
        "value": 100,
        "name": "United Kingdom"
      },
      {
        "code": "UK",
        "value": 34,
        "name": "United States"
      },
      {
        "code": "UY",
        "value": 19,
        "name": "Uruguay"
      },
      {
        "code": "UZ",
        "value": 66,
        "name": "Uzbekistan"
      },
      {
        "code": "VU",
        "value": 20,
        "name": "Vanuatu"
      },
      {
        "code": "VE",
        "value": 33,
        "name": "Venezuela, RB"
      },
      {
        "code": "VN",
        "value": 280,
        "name": "Vietnam"
      },
      {
        "code": "VI",
        "value": 314,
        "name": "Virgin Islands (U.S.)"
      },
      {
        "code": "PS",
        "value": 690,
        "name": "West Bank and Gaza"
      },
      {
        "code": "EH",
        "value": 2,
        "name": "Western Sahara"
      },
      {
        "code": "YE",
        "value": 46,
        "name": "Yemen, Rep."
      },
      {
        "code": "ZM",
        "value": 17,
        "name": "Zambia"
      },
      {
        "code": "ZW",
        "value": 32,
        "name": "Zimbabwe"
      }
    ],
    "devices": [
      {
        "name": "WinNT",
        "y": 16.33
      },
      {
        "name": "IPhone",
        "y": 34.03,
        "sliced": true,
        "selected": true
      },
      {
        "name": "Android",
        "y": 20.38
      },
      {
        "name": "Symbion",
        "y": 14.77
      },
      {
        "name": "Blackberry",
        "y": 1.91
      },
      {
        "name": "Others",
        "y": 9.2
      }
    ],
    "platforms": [
      {
        "name": "Microsoft Internet Explorer",
        "y": 56.33
      },
      {
        "name": "Chrome",
        "y": 24.03,
        "sliced": true,
        "selected": true
      },
      {
        "name": "Firefox",
        "y": 10.38
      },
      {
        "name": "Safari",
        "y": 4.77
      },
      {
        "name": "Opera",
        "y": 0.91
      },
      {
        "name": "Proprietary or Undetectable",
        "y": 0.2
      }
    ]
  };
  return data;
};


exports.getGeo = function(){
  // return  chance.n(function () {
  //   return { "Latitude" : chance.latitude({fixed: 7}), "Longitude": chance.longitude({fixed: 7}) }
  // }, 1000);
return [{"Latitude":"37.590699799999996","Longitude":"-122.0212583","MobileNumber":"8331877566","createdOn":"2017-03-31T18:32:49","ShortUrl":"http://localhost:8080/EG2lA"},{"Latitude":"37.5907151","Longitude":"-122.0212677","MobileNumber":"8331877566","createdOn":"2017-03-31T18:33:53","ShortUrl":"http://localhost:8080/EG2lA"},{"Latitude":"37.5907142","Longitude":"-122.02126689999999","MobileNumber":"8331877566","createdOn":"2017-03-31T20:01:37","ShortUrl":"http://localhost:8080/EG2lA"},{"Latitude":"37.5907081","Longitude":"-122.0212722","MobileNumber":"8331877566","createdOn":"2017-03-31T20:06:50","ShortUrl":"http://localhost:8080/EG2lA"},{"Latitude":"37.5907236","Longitude":"-122.02126360000001","MobileNumber":"8331877566","createdOn":"2017-03-31T20:09:05","ShortUrl":"http://localhost:8080/EG2lA"},{"Latitude":"37.5907189","Longitude":"-122.0212819","MobileNumber":"8331877566","createdOn":"2017-04-04T18:55:30","ShortUrl":"http://localhost:8080/EG2lA"}]
};
