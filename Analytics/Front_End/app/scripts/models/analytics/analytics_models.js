angular.module('app.models', ['app.models.common'])
  .factory('UserModel', ['$http', '$q', 'appConfig', function($http, $q, appConfig) {
    class User {
      constructor(data) {
        this.id = data.id || null;
        this.UserName = data.UserName;
        this.Email = data.Email;
        this.IsActive = data.IsActive || false;
      }
      resetPassword(){
        let refDefer = $q.defer();
        $http({
          method: 'PUT',
          url: appConfig.apiEndPoint + '/Customer/UpdateClient',
          data: {id: this.id, Password: this.Password}
        }).then((userObj) => {
          refDefer.resolve(new UserModel(userObj.data));
        }, (err) => {
          console.log('user update failed', err);
          refDefer.reject(err);
        });
        return refDefer.promise;
      }
      getKey(){
        let refDefer = $q.defer();
        $http({
          method: 'GET',
          url: appConfig.apiEndPoint + '/Customer/GetAPIKEY?clientid=:clientid',
          data: {clientid: this.id}
        }).then((keyObj) => {
          this.key = keyObj.data.API_KEY;
          refDefer.resolve({"key": this.key});
        }, (err) => {
          console.log('failed to load key', err);
          refDefer.reject(err);
        });
        return refDefer.promise;
      }
    }
    return User;
  }])

  .factory('CampaignsCollectionModel', ['$http', '$q', 'appConfig', 'CampaignModel', 'UserModel', function($http, $q, appConfig, CampaignModel, UserModel) {
        "use strict";
        class Campaigns {
          constructor(data={}) {
            this.collection = [];
            this.lastFetchedOn = null;
          }

          getAll(){
            var getAllDefer = $q.defer();
            $http({
              method: 'GET',
              url: appConfig.apiEndPoint + '/Campaign/Search'
            })
              .then((response) => {
                var campaigns = [];
                angular.forEach(response.data, function(campaign){
                  campaigns.push(new CampaignModel(campaign));
                });
                this.collection = campaigns;
                getAllDefer.resolve(campaigns);
              }, (err) => {
                getAllDefer.reject(err);
              });
            return getAllDefer.promise;
          };
        }

      return new Campaigns();
    }])
  .factory('CampaignModel', ['$http', '$q', 'appConfig', function($http, $q, appConfig) {
        class Campaign {
          constructor(data) {
            this.Id = data.Id || null;
            this.CampaignName = data.CampaignName;
            this.ReferenceNumber = data.ReferenceNumber;
            this.CreatedOn = data.CreatedDate || '';
            this.CreatedUserId = data.CreatedUserId || '';
            this.CreatedUserName = data.CreatedUserName || '';
            this.CreatedUserEmail = data.CreatedUserEmail || '';
            this.CreatedUserActiveState = data.CreatedUserActiveState || '';
            this.HasPassword = !!data.HasPassword || false;
            this.IsActive = data.IsActive || false;
            this.Password = '';
            this.EditPassword = false;
          }

          save(){
            var refDefer = $q.defer();

            if(!this.Id){
              //save
              var data = {CampaignName: this.CampaignName, IsActive: this.IsActive, CreatedUserId: this.CreatedUserId};
              if(this.Password !=''){
                data.Pwd = this.Password
              }else{
                 data.Pwd = false;
               }
              $http({
                method: 'POST',
                url: appConfig.apiEndPoint + '/Campaign/AddCampaign',
                data: data
              }).then((campaignObj) => {
//                console.log('campaign save', campaignObj)
                refDefer.resolve(new CampaignModel(campaignObj.data));
              }, (err) => {
                console.log('campaign save failed', err);
                refDefer.reject(err);
              });

            }else{
              //update
              var data = {ReferenceNumber:this.ReferenceNumber, CampaignName: this.CampaignName, IsActive: this.IsActive};
              if(this.EditPassword && this.RemovePassword){
                data.Pwd = '';
              }else if(this.EditPassword && this.Password !=''){
                data.Pwd = this.Password
              }
              $http({
                method: 'POST',
                url: appConfig.apiEndPoint + '/Campaign/UpdateCampaign',
                data: data
              }).then((campaignObj) => {
//                console.log('campaign update', campaignObj);
                refDefer.resolve(new Campaign(campaignObj.data));
              }, (err) => {
                console.log('campaign update failed', err);
                refDefer.reject(err);
              });
            }
            return refDefer.promise;
          }

          generate(form) {

            var refDefer = $q.defer();
            var data = {CampaignId: this.Id, LongUrlorMessage: form.LongUrlorMessage, MobileNumbers: form.MobileNumbersList, UploadType: form.UploadType};

            $http({
              method: 'POST',
              url: appConfig.apiEndPoint + '/Campaign/UploadData',
              data: data
            }).then((campaignObj) => {
//                console.log('campaign save', campaignObj)
              refDefer.resolve(new CampaignModel(campaignObj.data));
            }, (err) => {
              console.log('campaign generation failed', err);
              refDefer.reject(err);
            });

          }
        }
        return Campaign;
    }])