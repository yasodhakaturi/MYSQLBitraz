var chance = require("chance").Chance();
var model = require("./models.js");

exports.list = function(query){
  var count = chance.integer({min: 1, max: 10});
  return  chance.n(function () {
      return model.campaignModel()
    }, count)
  
};

exports.uploadSimple = function(query){
  return {Status: "Not Started", ShortenUrl:'http://g0.pe/'+chance.integer({min: 111, max: 9999}), MobileNumber:query.MobileNumbers}
};

exports.uploadAdvanced = function(query){
  return {Status: "Not Started", BatchID:chance.integer({min: 111, max: 9999})}
};

exports.batchStatus = function(query){
  return {Status: chance.pickone(["Not Started", "In Progress", "Insertion Completed", "Completed","Completed","Completed"]), BatchID:query.BatchID}
};

exports.batchList = function(query){
  var count = chance.integer({min: 1, max: 10});
  return  chance.n(function () {
    return {
      CreatedDate: chance.date({string: true, american: true}),
      BatchID: chance.integer({min: 111, max: 9999}),
      Status: chance.pickone(["Not Started", "In Progress", "Insertion Completed", "Completed","Failed"])
    }
  }, count)
};