using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using TejasviGuruCool;
using System.ComponentModel.DataAnnotations;

namespace TejasviServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IwsCommonService" in both code and config file together.
    [ServiceContract]
    public interface IwsCommonService
    {
        //[OperationContract]
        //[WebGet(UriTemplate = "GetItemTypeList", ResponseFormat = WebMessageFormat.Json)]
        //List<DDLItemType> GetItemTypeList();

        [OperationContract]
        [WebGet(UriTemplate = "GetUserTypeList", ResponseFormat = WebMessageFormat.Json)]
        List<UserType> GetUserTypeList();

        [OperationContract]
        [WebGet(UriTemplate = "GetItemList", ResponseFormat = WebMessageFormat.Json)]
        List<clstbItemMasterList> GetItemList();

        [OperationContract]
        [WebGet(UriTemplate = "GetSubjectList", ResponseFormat = WebMessageFormat.Json)]
        List<Subject> GetSubjectList();
        [OperationContract]
        [WebGet(UriTemplate = "GetModuleList", ResponseFormat = WebMessageFormat.Json)]
        List<Module> GetModuleList();
        [OperationContract]
        [WebGet(UriTemplate = "GetBatchList", ResponseFormat = WebMessageFormat.Json)]
        List<Batch> GetBatchList();
        //[OperationContract]
        //[WebGet(UriTemplate = "GetDoctorList", ResponseFormat = WebMessageFormat.Json)]
        //List<Doctor> GetDoctorList();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetPatientList", ResponseFormat = WebMessageFormat.Json)]
        //List<Patient> GetPatientList();


        //[OperationContract]
        //[WebGet(UriTemplate = "GetTreatmentList", ResponseFormat = WebMessageFormat.Json)]
        //List<CommonType> GetTreatmentList();

        //[OperationContract]
        //[WebGet(UriTemplate = "GetTreatMentPhaseList", ResponseFormat = WebMessageFormat.Json)]
        //List<CommonType> GetTreatMentPhaseList();
        //[OperationContract]
        //[WebGet(UriTemplate = "GetMeritalStatusList", ResponseFormat = WebMessageFormat.Json)]
        //List<CommonType> GetMeritalStatusList();
        //[OperationContract]
        //[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAppStatusList?Reftype={Reftype}")]
        //List<CommonType> GetAppStatusList(int Reftype);
        //[OperationContract]
        //[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSalesItemList?Reftype={Reftype}")]
        //List<DDLItem> GetSalesItemList(int Reftype);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetGSTPlanList", ResponseFormat = WebMessageFormat.Json)]
        //List<DDLGSTPlan> GetGSTPlanList();

       

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/sendSMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //string sendSMS(string obj);
        //[OperationContract]        
        //string sendSMSStr(string MobNo, string msg);
    }
}
