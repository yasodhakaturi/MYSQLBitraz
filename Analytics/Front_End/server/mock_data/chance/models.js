var chance = require("chance").Chance();
// var moment = require("moment");


exports.adminUserModel = function(){
    return {
      "id": chance.integer({min: 100, max: 99999}),
      "UserName": chance.sentence({words: 2}),
      "Email": chance.email(),
      "IsActive": chance.bool()
    }
};

exports.campaignModel = function(){
  return {
    "Id": chance.integer({min: 100, max: 99999}),
    "ReferenceNumber": chance.string({length: 10,pool: 'abcdefghijklmnopqrstuv'}) ,
    "CampaignName": chance.sentence({words: 2}),
    "CreatedOn": '2017-01-27T11:40:43',
    "CreatedUserId": chance.integer({min: 100, max: 99999}),
    "CreatedUserName": chance.sentence({words: 2}),
    "CreatedUserEmail": chance.email(),
    "CreatedUserActiveState": chance.bool(),
    "IsActive": chance.bool(),
    "HasPassword": chance.bool()
  }
};

//
// exports.accusedProductModel = function(){
//     return {
//       "id": chance.integer({min: 100, max: 99999}),
//       "entity_id": chance.integer({min: 100, max: 99999}),
//       "entity_name": chance.sentence({words: 5}),
//       "accused_product": chance.sentence({words: 15}),
//       "case_key": chance.string({length: 5,pool: 'abcdefghijklmnopqrstuv'}) + '-' + chance.integer({min: 99999, max:999999}),
//       "campaign_name": chance.string({length: 8,pool: 'abcdefghijklmnopqrstuv'}),
//     }
// };
//
// exports.patentCampaignModel = function(){
//     return {
//       "campaign_id": chance.integer({min: 100, max: 99999}),
//       "campaign_name": chance.sentence({words: 5}),
//       "defendant_parent_count": chance.integer({min: 0, max: 20}),
//       "campaign_start_date": chance.date({string: true, american: true}),
//       "campaign_end_date": chance.date({string: true, american: true}),
//       "campaign_jurisdictions": [chance.pickone(['district court', 'PTAB'])],
//       "campaign_status":chance.pickone(['open', 'close']),
//       "most_recent_case_id":chance.integer({min: 100, max: 99999}),
//       "most_recent_case_type":chance.pickone(['district court', 'PTAB']),
//       "most_recent_case_key": chance.string({length: 5,pool: 'abcdefghijklmnopqrstuv'}) + '-' + chance.integer({min: 99999, max:999999})
//     }
// };
//
// exports.patentDefendantModel = function(){
//   return {
//     "id": chance.integer({min: 100, max: 99999}),
//     "defendant_parent": chance.sentence({words: 5}),
//     "defendants": chance.sentence({words: 5}),
//     "recent_case": chance.string({length: 8,pool: 'abcdefghijklmnopqrstuv'}),
//     "case_key": chance.string({length: 5,pool: 'abcdefghijklmnopqrstuv'}) + '-' + chance.integer({min: 99999, max:999999}),
//     "start_date": chance.date({string: true, american: true}),
//     "end_date": chance.date({string: true, american: true}),
//     "jurisdiction": chance.pickone(['district court', 'PTAB'])
//   }
// };