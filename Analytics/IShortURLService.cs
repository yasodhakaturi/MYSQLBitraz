using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Analytics
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShortURLService" in both code and config file together.
    [ServiceContract]
    public interface IShortURLService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",//GET
            ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetApiKey?UserName={UserName}&Email={Email}&Password={Password}")]
        string GetApiKey(string UserName, string Email, string Password);

        [OperationContract]
        [WebInvoke(Method = "POST",//GET
            ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "AuthenticateUser?Email={Email}&Password={Password}&Api_Key={Api_Key}")]
        string AuthenticateUser(string Email, string Password, string Api_Key);


        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "RegisterCampaign?CampaignName={CampaignName}&Password={Password}")]
        string RegisterCampaign(string CampaignName, string Password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllCampaigns")]
        string GetAllCampaigns();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetShortUrl?CampaignId={CampaignId}&Type={Type}&longurlorMessage={longurlorMessage}&mobilenumber={mobilenumber}")]
        string GetShortUrl(string CampaignId, string Type, string longurlorMessage, string mobilenumber);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "GetCampaignReferenceNumber?CampaignName={CampaignName}&Password={Password}")]
        //string GetCampaignReferenceNumber(string CampaignName, string Password);
       
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetCampaignAnalyticsData?CampaignId={CampaignId}")]
        string GetCampaignAnalyticsData(string CampaignId);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "AuthenticateUser?username={username}&encryptedPassword={encryptedPassword}")]
        //string AuthenticateUser(string username, string encryptedPassword);

        //[OperationContract(Name = "oauth/token")]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //   BodyStyle = WebMessageBodyStyle.Wrapped,
        // UriTemplate = "oauth/token")]
        //string Authenticate_Token(Stream api_key);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "IsAuthorized?username={username}&password={password}&apikey={apikey}")]
        //string IsAuthorized(string username,string password,string apikey);

    }
}
