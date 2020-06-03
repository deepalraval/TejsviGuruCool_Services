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
using TejasviGuruCool;
namespace TejasviServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wsPatient" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select wsPatient.svc or wsPatient.svc.cs at the Solution Explorer and start debugging.
    public class wsModelItem : IwsModelItem
    {

        #region Get List Method
        SqlConnection _mainConnection;

        public List<clstbItemMasterList> GetAllItems(string filter, int pageNumber = 1, int pageSize = 20, string sortColumn = "ItemName", string sortdir = "ASC")
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_ItemMasterAll_Search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchText", filter);
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                return ConvertDataSetToListForList(SqlHelper.ExecuteDataset(cmd), pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<clstbItemMasterList> GetAllModuleItem()// fldTitle
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetAllModuleItem";
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@SearchText", filter);
                //cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                //cmd.Parameters.AddWithValue("@PageSize", pageSize);
                return ConvertDataSetToListForList(SqlHelper.ExecuteDataset(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<clstbItemMaster> ConvertDataSetToList(DataSet ds, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return new List<clstbItemMaster>();

                List<clstbItemMaster> lstRoles = new List<clstbItemMaster>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clstbItemMaster E = new clstbItemMaster();

                    if (dr.Table.Columns.Contains("fldModuleItemID"))
                        E.fldModuleItemID = Convert.ToInt32(dr["fldModuleItemID"]);
                    if (dr.Table.Columns.Contains("fldTitle"))
                        E.fldTitle = Convert.ToString(dr["fldTitle"]);



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
                return new List<clstbItemMaster>();
            }
        }


        private static List<clstbItemMasterList> ConvertDataSetToListForList(DataSet ds, int pageNumber = 1, int pageSize = 10)
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




                    if (pageNumber != 0 && pageSize != 0)
                    {
                        //E.CurrentPage = pageNumber;
                        //E.PageSize = pageSize;
                    }


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

        public List<clstbItemMaster> GetModelItem(string ModuleItemID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetModuleItem";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fldModuleItemID", ModuleItemID);
                List<clstbItemMaster> lstItemEntity = ConvertDataSetToList(SqlHelper.ExecuteDataset(cmd));

                return lstItemEntity;
            }
            catch
            {
                List<clstbItemMaster> lst = new List<clstbItemMaster>();

                return lst;
            }

        }

        public string CreateModelItem(string ModelItemEntity)
        {
            clstbItemMaster PatientMasterJson = JsonConvert.DeserializeObject<clstbItemMaster>(ModelItemEntity);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_tbPatientMasterAdd";
            cmd.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            _mainConnection = SqlHelper.GetConnection();
            _mainConnection.Open();

            cmd.Connection = _mainConnection;

            try
            {
                //cmd.Parameters.Add(new SqlParameter("@fldModuleItemID", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ""));
                //cmd.Parameters.Add(new SqlParameter("@fldTitle", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PatientMasterJson.FName));
                //cmd.Parameters.Add(new SqlParameter("@ConsultDoctorIDF", SqlDbType.Int, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PatientMasterJson.ConsultDoctorIDF));



                cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                cmd.Parameters.Add(new SqlParameter("@PatientReturnID", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Execute query.
                int j = cmd.ExecuteNonQuery();

                int returnID = Common.ConvertDBNullToInt32(cmd.Parameters["@ReturnID"].Value.ToString());
                int PatientReturnID = Common.ConvertDBNullToInt32(cmd.Parameters["@PatientReturnID"].Value.ToString());

                return returnID.ToString() + ":" + j.ToString() + ":" + PatientReturnID.ToString();
            }
            catch (Exception ex)
            {
                return "CreateItem::Insert::Error occured. Error message:" + ex.Message;
            }
            finally
            {
                cmd.Dispose();
            }
        }


        public string UpdateModelItem(string ModelItemEntity)
        {
            clstbItemMaster PatientMasterJson = JsonConvert.DeserializeObject<clstbItemMaster>(ModelItemEntity);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_tbPatientMasterEdit";
            cmd.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            _mainConnection = SqlHelper.GetConnection();
            _mainConnection.Open();

            cmd.Connection = _mainConnection;

            try
            {
                //cmd.Parameters.Add(new SqlParameter("@fldModuleItemID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PatientMasterJson.PatientIDP));
                //cmd.Parameters.Add(new SqlParameter("@fldTitle", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PatientMasterJson.FName));
                //cmd.Parameters.Add(new SqlParameter("@ConsultDoctorIDF", SqlDbType.Int, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PatientMasterJson.ConsultDoctorIDF));


                // Execute query.
                int j = cmd.ExecuteNonQuery();

                int returnID = Common.ConvertDBNullToInt32(cmd.Parameters["@ReturnID"].Value.ToString());

                return returnID.ToString() + ":" + j.ToString();
            }
            catch (Exception ex)
            {
                return "UpdateItem::Update::Error occured. Error message:" + ex.Message;
            }
            finally
            {
                cmd.Dispose();
            }
        }
    }
}
