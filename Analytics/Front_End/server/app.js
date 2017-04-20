var express = require('express');
var fs = require("fs"),
  path = require("path"),
  multer = require('multer'),
  bodyParser = require('body-parser')
chance = require("chance").Chance();
var app = express();
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());


var traverseFileSystem = function (currentPath) {
  //console.log(currentPath);
  var files = fs.readdirSync(currentPath);
  for (var i in files) {
    var currentFile = currentPath + '/' + files[i];
    var stats = fs.statSync(currentFile);
    if (stats.isFile()) {
      console.log(currentFile);
    }
    else if (stats.isDirectory()) {
      traverseFileSystem(currentFile);
    }
  }
};
console.log("Static Json files:");
traverseFileSystem('./mock_data');


var accessObject;
var config;


app.all("*", function (req, res, next) {
  var p = req.path;
  console.log("request: " + req.method + ":" + p);
  var origin = req.headers.origin;
  console.log(origin)
  accessObject = {"Access-Control-Allow-Origin": origin || '*',
    "Access-Control-Allow-Credentials":"true",
    "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept",
    "Access-Control-Allow-Methods": "PUT, PATCH, OPTIONS, GET, POST, DELETE"
  };
  config = {root: "./mock_data", headers: accessObject}

  if(req.method === "OPTIONS") {
    res.append("Access-Control-Allow-Origin", origin || '*')
      .append("Access-Control-Allow-Credentials", "true")
      .append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept")
      .append("Access-Control-Allow-Methods", "PUT, PATCH, OPTIONS, GET, POST, DELETE")
      .status(200).end();
  }

  res.header(accessObject);

 if( /\/api\/Users/.exec(p)){
   var users = require("./mock_data/chance/admin/users");
   res.json( users.get() );
  }
  else if( /\/api\/rid\/info\/\d*$/.exec(p)){
    var rid = require("./mock_data/chance/rid");
    res.json( rid.get() );
  }
 else if( /\/api\/rid\/validate\/\d*$/.exec(p)){
    var rid = require("./mock_data/chance/rid");
    res.json( rid.post() );
  }

 else if( /\/Analytics\/GETSummary/.exec(p)){
   var rid = require("./mock_data/chance/analytics");
   res.json( rid.getSummary() );
 }

 else if( /\/Analytics\/GETGeoLocations/.exec(p)){
   var rid = require("./mock_data/chance/analytics");
   res.json( rid.getGeo() );
 }else if( /\/Analytics\/GETAllCounts/.exec(p)){
   var rid = require("./mock_data/chance/analytics");
   res.json( rid.getCounts() );
 }else if( /\/Analytics\/GETAllCampaigns/.exec(p)){
   var rid = require("./mock_data/chance/campaigns");
   res.json( rid.list() );


 } else if( /\/Campaign\/Search/.exec(p)){
    var camp = require("./mock_data/chance/campaigns");
    res.json( camp.list() );
 }else if( /\/Campaign\/GetBatchStatus/.exec(p)){
    var camp = require("./mock_data/chance/campaigns");
    setTimeout(()=>{
      res.json( camp.batchStatus(req.query) );
    }, 2000);

 }else if( /\/Campaign\/GetBatchIDs/.exec(p)){
    var camp = require("./mock_data/chance/campaigns");
    res.json( camp.batchList(req.query) );
 } else if( /\/Campaign\/UploadData/.exec(p)){

   console.log(req.body.type)
    var camp = require("./mock_data/chance/campaigns");
   if(req.body.type == "simple"){
     res.json( camp.uploadSimple(req.body) );
   }else if(req.body.type == "advanced"){
     res.json( camp.uploadAdvanced(req.body) );
   }

 }
 else if( /\/Customer\/GetAPIKEY/.exec(p)){
   var user = require("./mock_data/chance/user");
   setTimeout(function(){
     res.json( user.getKey() );
   }, 1000)
 }

  else if( /\/Customer/.exec(p)){
    var user = require("./mock_data/chance/user");
    res.json( user.list() );
  }

 else if( /\/Auth\/Login/.exec(p)){
   var user = require("./mock_data/chance/user");
   res.json( user.login(req) );
 } else if( /\/Auth\/Logout/.exec(p)){
   var user = require("./mock_data/chance/user");
   res.json( user.logout(req) );
 }
  else{
    console.log("P:" + p + req.query.portfolio);
    res.sendFile( "index.html", {root: ".", headers: accessObject} );
  }
});

app.listen(process.env.PORT || 3301, '192.168.1.66');
console.log("Server started: http://192.168.1.66:3301/");
