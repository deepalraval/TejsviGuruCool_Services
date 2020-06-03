using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TejasviGuruCool;


namespace TejasviServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IwsUser" in both code and config file together.
    [ServiceContract]
    public interface IwsUser
    {
        [OperationContract]
        void DoWork();
        #region User Login
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CheckLogin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]//, BodyStyle = WebMessageBodyStyle.Wrapped
        List<UserMasterEntity> CheckLogin(string UserData);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateUser(string ItemMasterEntity);
        #endregion

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/AddLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //string AddLog(string UserMasterEntity);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetUser/{UserID}", ResponseFormat = WebMessageFormat.Json)]
        //List<UserMasterEntity> GetPassword(string UserID);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/UpdateUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //string UpdateUser(string UserMasterEntity);
    }
}
