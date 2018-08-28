angular.module("app")
  .component("analyticsLayout", {
    templateUrl: "views/common/directives/analytics_layout.html",
    bindings: {
      campaignId: "<"
    },
    controller: ["$scope", "$rootScope", "$uibModal", "$timeout", "highchartsNG", "RidService", "$interval", "NgMap", function ($scope, $rootScope, $uibModal, $timeout, highchartsNG, RidService, $interval, NgMap) {
      let $ctrl = this;

      $rootScope.pageLoading = true;

      $scope.data = {
        "activity": [],
        "locations": [],
        "devices": [],
        "platforms": []
      };

      $scope.date = {startDate: moment(),
        endDate: moment()};

      $scope.opts = {
        showDropdowns: true,
        ranges: {
          'Today': [moment(), moment()],
          'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
          'Last 7 Days': [moment().subtract(6, 'days'), moment()],
          'Last 30 Days': [moment().subtract(29, 'days'), moment()],
          'This Month': [moment().startOf('month'), moment().endOf('month')],
          'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        opens: 'left',
        buttonClasses: ['btn btn-default'],
        applyClass: 'btn-small btn-primary',
        cancelClass: 'btn-small',
        format: 'MM/DD/YYYY',
        separator: ' to ',
        locale: {
          "format": "MM/DD/YYYY",
          "separator": " to ",
          "applyLabel": "Apply",
          "cancelLabel": "Cancel",
          "fromLabel": "From",
          "toLabel": "To",
          "customRangeLabel": "Custom",
          "weekLabel": "W",
          "daysOfWeek": [
            "Su",
            "Mo",
            "Tu",
            "We",
            "Th",
            "Fr",
            "Sa"
          ],
          "monthNames": [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
          ],
          "firstDay": 1
        },
        startDate: moment().format('MM/DD/YYYY'),
        endDate: moment().format('MM/DD/YYYY'),
        minDate: moment('01/01/2016').format('MM/DD/YYYY'),
        maxDate: moment().format('MM/DD/YYYY'),
        eventHandlers: {'apply.daterangepicker': function(ev, picker) {
          $scope.date = {startDate: ev.model.startDate,
            endDate: ev.model.endDate};
          $ctrl.$onInit();
        }}
      };

      $scope.chartConfig = {
        options:{
          chart: {
            zoomType: 'x'
          },
          title: {
            text: 'user activities over time'
          },
          subtitle: {
            text: document.ontouchstart === undefined ?
              'Click and drag in the plot area to zoom in' : 'Pinch the chart to zoom in'
          },
          xAxis: {
            type: 'datetime'
          },
          yAxis: {
            title: {
              text: 'Counts'
            }
          },
          legend: {
            enabled: false
          },
          plotOptions: {
            area: {
              fillColor: {
                linearGradient: {
                  x1: 0,
                  y1: 0,
                  x2: 0,
                  y2: 1
                },
                stops: [
                  [0, Highcharts.getOptions().colors[0]],
                  [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                ]
              },
              marker: {
                radius: 2
              },
              lineWidth: 1,
              states: {
                hover: {
                  lineWidth: 1
                }
              },
              threshold: null
            }
          },

        },
        series: [{
          type: 'area',
          name: 'Count',
          data: _.map($scope.data.activity, (activity)=>{return [(new Date(activity.RequestedDate)).getTime(), activity.RequestCount];})
        }]

      };

      $scope.deviceConfig = {
        options: {
          chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
          },
          title: {
            text: ''
          },
          tooltip: {
            pointFormat: '{series.name}: <b>({point.y}) - {point.percentage:.1f}%</b>'
          },
          plotOptions: {

            pie: {
              size:'75%',
              allowPointSelect: true,
              cursor: 'pointer',
              dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                style: {
                  color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                }
              },
              showInLegend: false
            }
          }
        },
        series: [{
          name: 'Devices',
          colorByPoint: true,
          data: $scope.data.devices
        }]
      };

      $scope.platformConfig = {
        options: {
          chart: {
            plotBackgroundColor: null,
            plotBorderWidth: 0,
            plotShadow: false
          },
          title: {
            text: '',
            align: 'center',
            verticalAlign: 'middle',
            y: 40
          },
          tooltip: {
            pointFormat: '{series.name}: <b>({point.y}) - {point.percentage:.1f}%</b>'
          },
          plotOptions: {
            pie: {
              dataLabels: {
                enabled: true,
                distance: -30,
                style: {
                  fontWeight: 'bold',
                  color: 'white'
                }
              },
              startAngle: -90,
              endAngle: 90,
              center: ['50%', '75%']
            }
          }

        },
        series: [{
          type: 'pie',
          name: 'Browser',
          innerSize: '50%',
          data: []
        }]
      };

      $scope.locationConfig = {

        options: {

          colors: ['rgba(31,35,124,0.05)', 'rgba(31,35,124,0.2)', 'rgba(31,35,124,0.4)',
            'rgba(31,35,124,0.5)', 'rgba(31,35,124,0.6)', 'rgba(31,35,124,0.8)', 'rgba(31,35,124,1)'],

          title: {
            text: ''
          },

          mapNavigation: {
            enabled: false
          },

          legend: {
            title: {
              text: 'Visitors',
              style: {
                color: (Highcharts.theme && Highcharts.theme.textColor) || 'black'
              }
            },
            align: 'left',
            verticalAlign: 'bottom',
            floating: true,
            layout: 'vertical',
            valueDecimals: 0,
            backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || 'rgba(255, 255, 255, 0.85)',
            symbolRadius: 0,
            symbolHeight: 14
          },

          colorAxis: {
            dataClasses: [{
              from:0,
              to: 3
            }, {
              from: 3,
              to: 10
            }, {
              from: 10,
              to: 30
            }, {
              from: 30,
              to: 100
            }, {
              from: 100,
              to: 300
            }, {
              from: 300,
              to: 1000
            }, {
              from: 1000
            }]
          }
        },
        chartType: 'map',
        series: [{
          allAreas: true,
          data: $scope.data.locations,
          mapData: Highcharts.maps['custom/world'],
          joinBy: ['iso-a2', 'code'],
          animation: true,
          name: 'Visitors',
          states: {
            hover: {
              color: '#a4edba'
            }
          },
          tooltip: {
            valueSuffix: ''
          }
                }
        ]
      };


      var timer;
      $ctrl.$onInit = () => {
        $ctrl.params = {rid: $ctrl.campaignId, DateFrom: $scope.date.startDate.format('YYYY-MM-DD'), DateTo: $scope.date.endDate.format('YYYY-MM-DD')};
        $ctrl.loadData($ctrl.params);
      };

      $ctrl.$onChanges = (changes) => {
        if(changes.campaignId && !changes.campaignId.isFirstChange()){
          $ctrl.params.rid = changes.campaignId.currentValue;
          $ctrl.firstTime = true;
          $ctrl.loadData($ctrl.params);
        }
      };

      $ctrl.refreshCharts = () => {
        $scope.locationConfig.series[0].data =  $scope.data.locations;
        $scope.platformConfig.series[0].data =  $scope.data.platforms;
        $scope.deviceConfig.series[0].data =  $scope.data.devices;
        $scope.chartConfig.series[0].data =  _.map($scope.data.activity, (activity)=>{return [(new Date(activity.RequestedDate)).getTime(), activity.RequestCount];});
      };
      $ctrl.firstTime = true;
      $ctrl.loadData = (params) => {
        $ctrl.dashboardConfig = {
          type:'campaign',
          userId: $rootScope.userInfo.user_id,
          campaignId: params.rid
        };
        if(timer){
          $interval.cancel(timer);
        }
        if($ctrl.firstTime){
          $rootScope.pageLoading = true;
        }

        RidService.getCounts(params).$promise.then((resp)=>{
          $scope.data = resp;

          $rootScope.pageLoading = false;
          $ctrl.fetchGeoPositions(params);
          $ctrl.refreshCharts();
          $ctrl.reflow();
          timer = $interval(function(){
            if(moment(params.DateTo).isSame(moment(), 'day')){
              $ctrl.resetTime();
            }

          },120000);
          $ctrl.firstTime = false;
        }, (err)=>{
          console.log("failed to load summary", err);
          $rootScope.pageLoading = false;
        });
        // $timeout($ctrl.tick, tickInterval);
      };
      $ctrl.dynMarkers = [];

      $ctrl.fetchGeoPositions = (params) => {
        $rootScope.geoLoading = true;
        RidService.getGeoLocations(params).$promise.then((resp)=>{
          $ctrl.dynMarkers = [];
          NgMap.getMap().then(function(map) {
            for (var i=0; i<resp.length; i++) {
              var latLng = new google.maps.LatLng(resp[i].Latitude, resp[i].Longitude);
              $ctrl.dynMarkers.push(new google.maps.Marker({position:latLng}));
            }
            $ctrl.markerClusterer = new MarkerClusterer(map, $ctrl.dynMarkers, {});
          });
        },(err)=>{
          console.log("failed to load positions", err);
          $rootScope.geoLoading = false;
        });
      }

      $ctrl.reflow = () => {
        _.forEach( Highcharts.charts, function(chart){
          if(chart){
            chart.reflow();
          }
        });
      };

      $ctrl.googleMapsUrl="https://maps.googleapis.com/maps/api/js?key=AIzaSyCIYe5VvCNxbrPr-Sg7dJpTgsSHK5SI0a4";



      // var tickInterval = 1000;
      // var timeLimit = 60;
      //
      // $ctrl.tick = () => {
      //   $ctrl.timeLeft--;
      //   if($ctrl.timeLeft == 0){
      //     $ctrl.resetTime();
      //   }else{
      //     $timeout($ctrl.tick, tickInterval);
      //   }
      // };

      // $ctrl.timeLeft = timeLimit;

      $ctrl.resetTime = () => {
        // $ctrl.timeLeft = timeLimit;
        $ctrl.$onInit();
      };

      $ctrl.$onDestroy = () => {
        if(timer){
          $interval.cancel(timer);
        }
      }

    }]
  });