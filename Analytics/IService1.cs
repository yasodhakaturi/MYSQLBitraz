using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Analytics;
using System.IO;

namespace Analytics
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetApiKey?UserName={UserName}&Email={Email}&Password={Password}")]
        string GetApiKey(string UserName, string Email, string Password);

        
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
            UriTemplate = "GetShortUrl?referencenumber={referencenumber}&longurl={longurl}&mobilenumber={mobilenumber}")]
        string GetShortUrl(string referencenumber, string longurl, string mobilenumber);


        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "GETAllCounts?Fk_Uniqueid={Fk_Uniqueid}&DateFrom={DateFrom}&DateTO={DateTO}")]
        //Stream GETAllCounts(string Fk_Uniqueid, string DateFrom, string DateTO);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "GETSummary?Fk_Uniqueid={Fk_Uniqueid}")]
        //Stream GETSummary(string Fk_Uniqueid);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "Monitize?uid={uid}")]
        //    //UriTemplate = "/{uid}")]
        //void Monitize(string uid);


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



        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "AdminOperations?username={username}&Email={Email}&Password={Password}")]
        //string AdminOperations(string username, string Email,string Password);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "AdminOperations")]
        //string AdminOperations(string username, string Email, string Password);






    }

    
}
