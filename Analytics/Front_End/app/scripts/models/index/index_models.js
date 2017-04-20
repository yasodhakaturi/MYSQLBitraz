angular.module('bitraz.models', ['bitraz.models.common'])
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
    }
    return User;
  }]);