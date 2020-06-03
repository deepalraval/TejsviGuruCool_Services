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
namespace TejasviServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wsUser" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select wsUser.svc or esUser.svc.cs at the Solution Explorer and start debugging.
    public class wsUser : IwsUser
    {
        SqlConnection _mainConnection;
        static SqlConnection _mainConnectionS;
        public void DoWork()
        {
        }
        #region User Login
        public List<UserMasterEntity> CheckLogin(string UserData)
        {
            try
            {
                UserMasterEntity U = JsonConvert.DeserializeObject<UserMasterEntity>(UserData);

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_LoginUser";
                cmd.CommandType = CommandType.StoredProcedure;
                //U.Password=ServiceUtility.Encrypt(U.Password); Security
                cmd.Parameters.AddWithValue("@UserName", U.fldUserName);
                U.fldPassword = Encrypt(U.fldPassword);


                cmd.Parameters.AddWithValue("@Password", U.fldPassword);
                //cmd.Parameters.AddWithValue("@UserType", U.UserType);


                List<UserMasterEntity> lstUser = ConvertDataSetToList(SqlHelper.ExecuteDataset(cmd));
                    //
                //if (lstUser.Count() >= 0)
                //{
                //    U.fldUserID = lstUser[0].fldUserID;
                //    string UserDatatt = JsonConvert.SerializeObject(U);
                //    //AddLog(UserDatatt);
                //}
                return lstUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private static List<UserMasterEntity> ConvertDataSetToList(DataSet ds)
        {

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return new List<UserMasterEntity>();

            List<UserMasterEntity> lstUsers = new List<UserMasterEntity>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UserMasterEntity U = new UserMasterEntity();

                try
                {
                    if (dr.Table.Columns.Contains("fldUserID"))
                        U.fldUserID = Convert.ToInt32(dr["fldUserID"]);

                    if (dr.Table.Columns.Contains("fldUserName"))
                        U.fldUserName = Convert.ToString(dr["fldUserName"]);
                    if (dr.Table.Columns.Contains("fldPassword"))
                        U.fldPassword = Convert.ToString(dr["fldPassword"]);

                    

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    lstUsers.Add(U);
                }

            }
            return lstUsers;
        }


        #region Encryption Password Dicryptioon Password
        public static string Encrypt(string password)
        {

            string passEncodedData = string.Empty;
            //----Password Encription Logic Start.....
            var pass = password;
            byte[] encData_byte1 = new byte[pass.Length];
            encData_byte1 = System.Text.Encoding.UTF8.GetBytes(pass);
            passEncodedData = Convert.ToBase64String(encData_byte1);
            //----End Password Encription Logic           
            password = passEncodedData;
            return password;

        }

        public static string Decrypt(string password)
        {

            //----Decription For Password Logic Start
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string pass = new String(decoded_char);
            //----End Decription For Password Logic
            return pass;

        }
        #endregion

        //public static string AddPatientDuringRegistration(string UserEntity)
        //{
        //    UserMasterEntity USerMasterJson = JsonConvert.DeserializeObject<UserMasterEntity>(UserEntity);

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "sp_tbPatientMasterAddDuringRegistration";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    _mainConnectionS = SqlHelper.GetConnection();
        //    _mainConnectionS.Open();

        //    cmd.Connection = _mainConnectionS;

        //    try
        //    {


        //        //@UniqueNumber nvarchar(50),@FullName nvarchar(50),@ConsultDoctorIDF int,@TreatmentPhaseIDF int,@TreatmentIDF int,@BloodGroupIDF int,
        //        //@Gender tinyint,@DateOfBirth smalldatetime,@MaritalStatus tinyint,@Mobile nvarchar(50),@Email nvarchar(50),@Address nvarchar(MAX),
        //        //@Description nvarchar(1000),@IsAllocated bit,@PatientPhotoURL nvarchar(MAX),@Height nvarchar(50),@Weight nvarchar(50),@Glucose nvarchar(50),@systolic nvarchar(50),
        //        //@Diastolic nvarchar(50),@Worktype nvarchar(50),@Diet nvarchar(MAX),@Alcoholconsumption nvarchar(MAX),@Breakfast nvarchar(MAX),@Lunch nvarchar(MAX),@Snacks nvarchar(MAX),
        //        cmd.Parameters.Add(new SqlParameter("@UniqueNumber", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ""));
        //        cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.RefName));


        //        cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.Gender));
        //        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.ContactNumber));
        //        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.Address));
        //        cmd.Parameters.Add(new SqlParameter("@EntryDateTime", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.EntryDatetime));
        //        cmd.Parameters.Add(new SqlParameter("@EntryBy", SqlDbType.Int, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.EntrybyIDF));
        //        cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
        //        // Execute query.
        //        int j = cmd.ExecuteNonQuery();

        //        int returnID = Common.ConvertDBNullToInt32(cmd.Parameters["@ReturnID"].Value.ToString());

        //        return returnID.ToString();// +":" + returnID.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Create User::Insert::Error occured. Error message:" + ex.Message;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}

        public string AddLog(string UserEntity)
        {
            UserMasterEntity USerMasterJson = JsonConvert.DeserializeObject<UserMasterEntity>(UserEntity);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_AddLog_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            _mainConnection = SqlHelper.GetConnection();
            _mainConnection.Open();

            cmd.Connection = _mainConnection;

            try
            {

                //UserIDF,MACAddress,ComputerName,LogInTime,BrowserName,LogOffTime
                //cmd.Parameters.Add(new SqlParameter("@UserIDF", SqlDbType.Int, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.UserIDP));
                //cmd.Parameters.Add(new SqlParameter("@MACAddress", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.MacAddress));
                //cmd.Parameters.Add(new SqlParameter("@ComputerName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.ComputerName));
                //cmd.Parameters.Add(new SqlParameter("@LogInTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.EntryDatetime));
                //cmd.Parameters.Add(new SqlParameter("@LogOffTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.EntryDatetime));
                //cmd.Parameters.Add(new SqlParameter("@BrowserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.BrowserName));

                cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                // Execute query.
                int j = cmd.ExecuteNonQuery();

                int returnID = Common.ConvertDBNullToInt32(cmd.Parameters["@ReturnID"].Value.ToString());

                return j.ToString() + ":" + returnID.ToString();
            }
            catch (Exception ex)
            {
                return "Create User::Insert::Error occured. Error message:" + ex.Message;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        #region Add and Update Methods
        public string CreateUser(string UserEntity)
        {
            UserMasterEntity USerMasterJson = JsonConvert.DeserializeObject<UserMasterEntity>(UserEntity);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_User_Insert";
            cmd.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            _mainConnection = SqlHelper.GetConnection();
            _mainConnection.Open();

            cmd.Connection = _mainConnection;

            try
            {
                //try
                //{
                //    var id = AddPatientDuringRegistration(JsonConvert.SerializeObject(USerMasterJson));
                //    USerMasterJson.ReferanceIDF = Convert.ToInt32(id);
                //}
                //catch
                //{
                //    USerMasterJson.ReferanceIDF = null;
                //}
                USerMasterJson.fldPassword = Encrypt(USerMasterJson.fldPassword);
                //@RefName@Password@Gender@ContactNumber@UserType@AddressEntryDateTime@EntryByIsDelete
                //cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.UserName));
                //cmd.Parameters.Add(new SqlParameter("@RefName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.RefName));
                //cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.Password));
                //cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.Gender));
                //cmd.Parameters.Add(new SqlParameter("@ContactNumber", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.ContactNumber));
                //cmd.Parameters.Add(new SqlParameter("@ReferanceIDF", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.ReferanceIDF));
                //cmd.Parameters.Add(new SqlParameter("@UserType", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.UserType));
                //cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.Address));
                //cmd.Parameters.Add(new SqlParameter("@EntryDateTime", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.EntryDatetime));
                //cmd.Parameters.Add(new SqlParameter("@EntryBy", SqlDbType.Int, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, USerMasterJson.EntrybyIDF));
                cmd.Parameters.Add(new SqlParameter("@IsDelete", SqlDbType.Bit, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, 0));
                cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                // Execute query.
                int j = cmd.ExecuteNonQuery();

                int returnID = Common.ConvertDBNullToInt32(cmd.Parameters["@ReturnID"].Value.ToString());



                //try
                //{

                //    return j.ToString() + ":" + returnID.ToString();
                //}
                //catch
                //{
                return returnID.ToString() + ":" + j.ToString();
                //}
            }
            catch (Exception ex)
            {
                return "Create User::Insert::Error occured. Error message:" + ex.Message;
            }
            finally
            {
                cmd.Dispose();
            }
        }


        public List<UserMasterEntity> GetPassword(string UserID)
        {
            //sdsd Shubham
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_GetUser_SelectOne";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserIDP", UserID);
                List<UserMasterEntity> lstUserEntity = ConvertDataSetToList(SqlHelper.ExecuteDataset(cmd));
                return lstUserEntity;
            }
            catch
            {
                List<UserMasterEntity> lst = new List<UserMasterEntity>();
                return lst;
            }
        }

        public string UpdateUser(string UserEntity)
        {
            //sdsd shubham
            UserMasterEntity UserMasterJson = JsonConvert.DeserializeObject<UserMasterEntity>(UserEntity);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_User_Update";
            cmd.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            _mainConnection = SqlHelper.GetConnection();
            _mainConnection.Open();

            cmd.Connection = _mainConnection;

            try
            {
                //cmd.Parameters.Add(new SqlParameter("@UserIDP", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UserMasterJson.UserIDP));
                //cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UserMasterJson.Password));                
                cmd.Parameters.Add(new SqlParameter("@ReturnID", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Execute query.
                int j = cmd.ExecuteNonQuery();

                int returnID = Common.ConvertDBNullToInt32(cmd.Parameters["@ReturnID"].Value.ToString());

                return returnID.ToString() + ":" + j.ToString();
            }
            catch (Exception ex)
            {
                return "UpdateUser::Update::Error occured. Error message:" + ex.Message;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        #endregion
    }
}
