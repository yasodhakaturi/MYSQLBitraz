var chance = require("chance").Chance();

exports.get = function(query){
  return {
    "rid": chance.integer({min: 10000, max: 99999}),
    "has_authentication": chance.bool(),
    "is_authorized": chance.bool()
  };
};

exports.post = function(query){
  var auth = chance.bool();
  if(auth){
    return {"success": "validated successfully"};
  }else{
    return {"error": "Invalid Authentication"};
  }
};

