angular.module('bitraz.models', ['bitraz.models.common', "ngFileUpload"])
  .factory('UsersCollectionModel', ['$http', '$q', 'appConfig', 'UserModel', function($http, $q, appConfig, UserModel) {
    "use strict";
    class Users {
      constructor(data={}) {
        //this.UserService = UserService;
        this.collection = [];
        this.lastFetchedOn = null;
      }

      getAll(){
        var getAllDefer = $q.defer();
        $http({
          method: 'GET',
          url: appConfig.apiEndPoint + '/Customer'
        })
          .then((response) => {
            var users = [];
            angular.forEach(response.data, function(user){
              users.push(new UserModel(user));
            });
            this.collection = users;
            getAllDefer.resolve(users);
          }, (err) => {
            getAllDefer.reject(err);
          });
        return getAllDefer.promise;
      };
    }

  return new Users();
}])
  .factory('UserModel', ['$http', '$q', 'appConfig', function($http, $q, appConfig) {
    class User {
      constructor(data) {
        this.id = data.id || null;
        this.UserName = data.UserName;
        this.Email = data.Email;
        this.IsActive = data.IsActive || false;
        this.key = null;
      }
 
      save(){
        var refDefer = $q.defer();

        if(!this.id){
          //save
          $http({
            method: 'POST',
            url: appConfig.apiEndPoint + '/Customer/AddClient',
            data: {UserName: this.UserName, Email: this.Email, Password: this.Password, IsActive: this.IsActive}
          }).then((userObj) => {
//            console.log('user save', userObj)
            refDefer.resolve(new UserModel(userObj.data));
          }, (err) => {
            console.log('user save failed', err);
            refDefer.reject(err);
          });
          
        }else{
          //update
          $http({
            method: 'PUT',
            url: appConfig.apiEndPoint + '/Customer/UpdateClient',
            data: {id: this.id, UserName: this.UserName, Email: this.Email,  IsActive: this.IsActive}
          }).then((userObj) => {
//            console.log('user update', userObj);
            refDefer.resolve(new UserModel(userObj.data));
          }, (err) => {
            console.log('user update failed', err);
            refDefer.reject(err);
          });
        }
        return refDefer.promise;
      }

      resetPassword(){
        let refDefer = $q.defer();
        $http({
          method: 'PUT',
          url: appConfig.apiEndPoint + '/Customer/UpdateClient',
          data: {id: this.id, Password: this.Password}
        }).then((userObj) => {
//          console.log('user update', userObj);
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
          url: appConfig.apiEndPoint + '/Customer/GetAPIKEY?clientid=' + this.id,
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
          //this.UserService = UserService;
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
  .factory('CampaignModel', ['$http', '$q', 'appConfig', 'BatchModel', 'Upload', function($http, $q, appConfig, BatchModel, Upload) {
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
          this.RemovePassword = false;
          this.batchList = [];
        }

        save(){
          var refDefer = $q.defer();

          if(!this.Id){
            //save
            var data = {CampaignName: this.CampaignName, IsActive: this.IsActive, CreatedUserId: this.CreatedUserId};
            if(this.Password !=''){
              data.Pwd = this.Password
            }
            $http({
              method: 'POST',
              url: appConfig.apiEndPoint + '/Campaign/AddCampaign',
              data: data
            }).then((campaignObj) => {
//              console.log('campaign save', campaignObj)
              var newCampaign = new Campaign(campaignObj.data);
              refDefer.resolve(newCampaign);
            }, (err) => {
              console.log('sampaign save failed', err);
              refDefer.reject(err);
            });

          }else{
            //update
            var data = {CampaignName: this.CampaignName, ReferenceNumber: this.ReferenceNumber, CreatedUserId: this.CreatedUserId, IsActive: this.IsActive};
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
//              console.log('campaign update', campaignObj);

              //CampaignsCollectionModel.push(newCampaign);
              refDefer.resolve(new Campaign(campaignObj.data));
            }, (err) => {
              console.log('campaign update failed', err);
              refDefer.reject(err);
            });
          }
          return refDefer.promise;
        }

        generate(form, type) {
          var refDefer = $q.defer();
          var data = {ReferenceNumber:this.ReferenceNumber, CampaignID: this.Id, LongUrlorMessage: form.LongUrlorMessage, MobileNumbers: form.MobileNumbers, type:type, UploadType: form.UploadType};

          $http({
            method: 'POST',
            url: appConfig.apiEndPoint + '/Campaign/UploadData',
            data: data
          }).then((campaignObj) => {
            if(campaignObj.data.ShortenUrl){
              refDefer.resolve(campaignObj.data);
            }else if(campaignObj.data.BatchID){
              var batch = new BatchModel(campaignObj.data);
              this.batchList.push(batch);
              refDefer.resolve(batch);
            }

          }, (err) => {
            console.log('campaign generation failed', err);
            refDefer.reject(err);
          });
          return refDefer.promise;
        }


        generateFromFile(form, type) {

          var refDefer = $q.defer();
          var data = {ReferenceNumber:this.ReferenceNumber, CampaignID: this.Id, LongUrlorMessage: form.LongUrlorMessage, UploadFile: form.File, type:type, UploadType: form.UploadType};
          var _that = this;
          Upload.upload({
            url: appConfig.apiEndPoint + '/Campaign/UploadData',
            data: data
          }).then(function (campaignObj) {
            if(campaignObj.data.ShortenUrl){
              refDefer.resolve(campaignObj.data);
            }else if(campaignObj.data.BatchID){
              var batch = new BatchModel(campaignObj.data);
              if(!_that.batchList){
                _that.batchList = [];
              }
              _that.batchList.push(batch);
              refDefer.resolve(batch);
            }
          }, function (err) {
            console.log('campaign generation failed', err);
            refDefer.reject(err);
          }, function (evt) {
            refDefer.notify(evt);
             var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
             console.log('progress: ',evt);
          });

          // $http({
          //   method: 'POST',
          //   url: appConfig.apiEndPoint + '/Campaign/UploadData',
          //   data: data
          // }).then((campaignObj) => {
          //   if(campaignObj.data.ShortenUrl){
          //     refDefer.resolve(campaignObj.data);
          //   }else if(campaignObj.data.BatchID){
          //     var batch = new BatchModel(campaignObj.data);
          //     this.batchList.push(batch);
          //     refDefer.resolve(batch);
          //   }
          //
          // }, (err) => {
          //   console.log('campaign generation failed', err);
          //   refDefer.reject(err);
          // });
          return refDefer.promise;
        }

        getBatchIds(){
          var refDefer = $q.defer();

          $http({
            method: 'GET',
            url: appConfig.apiEndPoint + '/Campaign/GetBatchIDs',
            params: {ReferenceNumber: this.ReferenceNumber}
          }).then((resp) => {
            this.batchList = [];
            angular.forEach(resp.data, (batch)=>{
              this.batchList.push(new BatchModel(batch));
            });
            refDefer.resolve(this.batchList);

          }, (err) => {
            console.log('campaign batch list failed', err);
            refDefer.reject(err);
          });
          return refDefer.promise;
        }
      }
      return Campaign;
  }])
  .factory('BatchModel', ['$http', '$q', 'appConfig', '$window', function($http, $q, appConfig, $window){
    class Batch {
      constructor(data) {
        this.BatchID = data.BatchID;
        this.BatchName = data.BatchName;
        this.CreatedDate = data.CreatedDate || '';
        this.Status = data.Status || '';
        this.loading = false;
      }

      getStatus(){
        let refDefer = $q.defer();
        this.loading = true;
        $http({
          method: 'GET',
          url: appConfig.apiEndPoint + '/Campaign/GetBatchStatus',
          params: {BatchID: this.BatchID}
        }).then((keyObj) => {
          this.loading = false;
          this.Status = keyObj.data.Status;
          refDefer.resolve({"Status": this.Status});
        }, (err) => {
          this.loading = false;
          console.log('failed to load Status', err);
          refDefer.reject(err);
        });
        return refDefer.promise;
      }

      download(){
        if(this.Status == 'Completed'){
          $window.open(appConfig.apiEndPoint + '/Campaign/GetBatchDownloadedFile?BatchID='+this.BatchID);
        }

      }
    }
    return Batch;
  }])