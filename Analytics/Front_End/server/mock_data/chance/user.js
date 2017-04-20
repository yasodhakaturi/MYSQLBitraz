var chance = require("chance").Chance();
var model = require("./models.js");

exports.login = function(request){
  var role;
  switch(request.body.uname){
    case 'demo':
      role = 'Member';
      break;

    case 'admin':
      role = 'Admin';
      break;

    case 'user':
      role = 'MemberAdmin';
      break;

    default:
      role = null;

  }

  if(role && request.body.password == 'password'){
  //user_id: 1, UserName: "admin", isAdmin: true, isClient: false, isReadOnly: true
    if(role == 'Member'){
      return {user_info: {user_id: 1, isAdmin:role == 'Admin', isClient: role == 'MemberAdmin', isReadOnly:  role == 'Member',  UserName:request.body.uname}, redirected_url:'/Home'};
    }else if(role == 'MemberAdmin'){
      return {user_info: {user_id: 1, isAdmin:role == 'Admin', isClient: role == 'MemberAdmin', isReadOnly:  role == 'Member',  UserName:request.body.uname}, redirected_url:'/Analytics'};
    }else if(role == 'Admin'){
      return {user_info: {user_id: 1, isAdmin:role == 'Admin', isClient: role == 'MemberAdmin', isReadOnly:  role == 'Member',  UserName:request.body.uname}, redirected_url:'/Admin'};
    }
  }else if(role){
    return {'error': {"message": 'Invalid Credentials'}}
  }else{
    return {'error': {"message": 'Invalid Credentials'}}
  }

};

exports.logout = function(query){
    return {"success": "logged out successfully"};
};

exports.user = function(query){
  return {user_info: {user_id: 1, user_role:'Member', user_name:'test'}, redirected_url:'Index/'};
};

exports.list = function(query){
  var count = chance.integer({min: 0, max: 10});
  return  chance.n(function () {
      return model.adminUserModel()
    }, count)

};

exports.getKey = function(query){
  return {API_KEY: chance.integer({min: 11111111, max: 99999999})};
};

