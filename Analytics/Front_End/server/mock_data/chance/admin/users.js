var chance = require("chance").Chance();
var model = require("../models.js");

exports.get = function(query){
  var count = chance.integer({min: 30, max: 100});

  var def = function(){
    return chance.n( function(){
        return model.adminUserModel()
      }, count)
  };

  return def();
};

