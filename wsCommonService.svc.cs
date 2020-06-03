using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TejasviGuruCool;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.IO;

using System.Net;
using TejasviGuruCool;
namespace TejasviServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wsCommonService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select wsCommonService.svc or wsCommonService.svc.cs at the Solution Explorer and start debugging.
    public class wsCommonService : IwsCommonService
    {



        #region Get UserType List
        public List<UserType> GetUserTypeList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetUserTypeList";
                cmd.CommandType = CommandType.StoredProcedure;
               
                return ConvertDataSetToDDLListUserType(SqlHelper.ExecuteDataset(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<UserType> ConvertDataSetToDDLListUserType(DataSet ds)
        {
            try
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return new List<UserType>();

                List<UserType> lstDDLUserTypee = new List<UserType>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserType objUserTypee = new UserType();

                    if (dr.Table.Columns.Contains("IDP"))
                        objUserTypee.IDP = Convert.ToInt32(dr["IDP"]);
                    if (dr.Table.Columns.Contains("Name"))
                        objUserTypee.Name = Convert.ToString(dr["Name"]);
                    lstDDLUserTypee.Add(objUserTypee);
                }
                return lstDDLUserTypee;
            }
            catch
            {
                return new List<UserType>();
            }
        }
        #endregion
  
    

    
        #region Get Item List
        public List<clstbItemMasterList> GetItemList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetAllModuleItem";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RefIDF", 0);
                return ConvertDataSetToList(SqlHelper.ExecuteDataset(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<clstbItemMasterList> ConvertDataSetToList(DataSet ds, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return new List<clstbItemMasterList>();

                List<clstbItemMasterList> lstRoles = new List<clstbItemMasterList>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clstbItemMasterList E = new clstbItemMasterList();

                    if (dr.Table.Columns.Contains("fldModuleItemID"))
                        E.fldModuleItemID = Convert.ToInt32(dr["fldModuleItemID"]);
                    if (dr.Table.Columns.Contains("fldTitle"))
                        E.fldTitle = Convert.ToString(dr["fldTitle"]);

                    if (dr.Table.Columns.Contains("fldType"))
                        E.fldType = Convert.ToString(dr["fldType"]);
                    if (dr.Table.Columns.Contains("fldFilePath"))
                        E.fldFilePath = Convert.ToString(dr["fldFilePath"]);


                    if (dr.Table.Columns.Contains("fldDesc"))
                        E.fldDesc = Convert.ToString(dr["fldDesc"]);
                    if (dr.Table.Columns.Contains("fldFilePath"))
                        E.fldFilePath = Convert.ToString(dr["fldFilePath"]);
                    //if (pageNumber != 0 && pageSize != 0)
                    //{
                    //    E.CurrentPage = pageNumber;
                    //    E.PageSize = pageSize;
                    //}


                    lstRoles.Add(E);
                }
                return lstRoles;
            }
            catch
            {
                return new List<clstbItemMasterList>();
            }
        }
        #endregion
      





        #region Get Class List
        public List<Batch> GetBatchList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetBatchList";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RefIDF", 0);
                return ConvertDataSetToDDLclassList(SqlHelper.ExecuteDataset(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<Batch> ConvertDataSetToDDLclassList(DataSet ds)
        {
            try
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return new List<Batch>();

                List<Batch> list = new List<Batch>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Batch obj = new Batch();

                    if (dr.Table.Columns.Contains("IDP"))
                        obj.IDP = Convert.ToInt32(dr["IDP"]);
                    if (dr.Table.Columns.Contains("Name"))
                        obj.Name = Convert.ToString(dr["Name"]);
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return new List<Batch>();
            }
        }
        #endregion

        #region Get Subject List
        public List<Subject> GetSubjectList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetSubjectList";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RefIDF", 0);
                return ConvertDataSetToDDLSubjectList(SqlHelper.ExecuteDataset(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<Subject> ConvertDataSetToDDLSubjectList(DataSet ds)
        {
            try
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return new List<Subject>();

                List<Subject> list = new List<Subject>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Subject obj = new Subject();

                    if (dr.Table.Columns.Contains("IDP"))
                        obj.IDP = Convert.ToInt32(dr["IDP"]);
                    if (dr.Table.Columns.Contains("Name"))
                        obj.Name = Convert.ToString(dr["Name"]);
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return new List<Subject>();
            }
        }
        #endregion


        #region Get Module List
        public List<Module> GetModuleList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetModuleList";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RefIDF", 0);
                return ConvertDataSetToDDLModuleList(SqlHelper.ExecuteDataset(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<Module> ConvertDataSetToDDLModuleList(DataSet ds)
        {
            try
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return new List<Module>();

                List<Module> list = new List<Module>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Module obj = new Module();

                    if (dr.Table.Columns.Contains("IDP"))
                        obj.IDP = Convert.ToInt32(dr["IDP"]);
                    if (dr.Table.Columns.Contains("Name"))
                        obj.Name = Convert.ToString(dr["Name"]);
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return new List<Module>();
            }
        }
        #endregion

    }
}
