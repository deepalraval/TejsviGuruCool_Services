using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using TejasviGuruCool;
namespace TejasviServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IwsPatient" in both code and config file together.
    [ServiceContract]
    public interface IwsModelItem
    {

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllModuleItem")]
        List<clstbItemMasterList> GetAllModuleItem();


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllItems?filter={filter}&pageNumber={pageNumber}&pageSize={pageSize}&sortColumn={sortColumn}&sortdir={sortdir}")]
        List<clstbItemMasterList> GetAllItems(string filter, int pageNumber = 1, int pageSize = 20, string sortColumn = "ItemName", string sortdir = "ASC");

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllPatientsUnalllocated?filter={filter}&pageNumber={pageNumber}&pageSize={pageSize}&sortColumn={sortColumn}&sortdir={sortdir}")]
        //List<clstbItemMasterList> GetAllPatientsUnalllocated(string filter, int pageNumber = 1, int pageSize = 20, string sortColumn = "PatientName", string sortdir = "ASC");
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/AlloctedPatient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //string AlloctedPatient(string ItemMasterEntity);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateModelItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateModelItem(string ModelItemEntity);

        [OperationContract]
        [WebGet(UriTemplate = "GetModelItem/{fldModuleItemID}", ResponseFormat = WebMessageFormat.Json)]
        List<clstbItemMaster> GetModelItem(string ModuleItemID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateModelItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string UpdateModelItem(string ModelItemEntity);

    }
}
