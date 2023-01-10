using ELMS.Class.Tables;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ELMS.Class.Enum;

namespace ELMS.Class.DataAccess
{
    class CustomerDAL
    {
        public static DataSet SelectCustomerByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                 sql = $@"SELECT CU.ID,
                               CU.FULL_NAME FULL_NAME,                               
                               B.NAME BRANCH_NAME,
                               C.NAME COUNTRY_NAME,
                               CU.BIRTH_PLACE,
                               CU.REGISTERED_ADDRESS,                            
                               CU.BIRTHDAY,
                               SE.NAME SEX_NAME,                               
                               CU.ADDRESS,                               
                               CU.CLOSED_DATE,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CI.IMAGE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.CUSTOMER CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.CUSTOMER_IMAGE CI,
                               ELMS_USER.BRANCH B
                          WHERE CU.COUNTRY_ID = C.ID
                               AND CU.SEX_ID = SE.ID
                               AND CU.ID = CI.CUSTOMER_ID
                               AND CU.BRANCH_ID = B.ID 
                               ORDER BY CU.ID";
                 else
                sql = $@"SELECT CU.ID,
                               CU.FULL_NAME,                               
                               B.NAME BRANCH_NAME,
                               C.NAME COUNTRY_NAME,
                               CU.BIRTH_PLACE,
                               CU.REGISTERED_ADDRESS,                            
                               CU.BIRTHDAY,
                               SE.NAME SEX_NAME,                               
                               CU.ADDRESS,                               
                               CU.CLOSED_DATE,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CI.IMAGE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.CUSTOMER CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.CUSTOMER_IMAGE CI,
                               ELMS_USER.BRANCH B
                          WHERE     CU.COUNTRY_ID = C.ID
                               AND CU.SEX_ID = SE.ID
                               AND CU.ID = CI.CUSTOMER_ID
                               AND CU.BRANCH_ID = B.ID 
                               WHERE CU.ID = {ID}";

            try
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, GlobalFunctions.GetConnectionString()))
                {
                    DataSet dsAdapter = new DataSet();
                    adapter.Fill(dsAdapter);
                    return dsAdapter;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Müştərinin məlumatları açılmadı.", sql, GlobalVariables.V_UserName, "CustomerDAL", "SelectCustomerByID", exx);
                return null;
            }
        }

        public static DataTable SelectViewData(int? ID)
        {
            string s = $@"SELECT CU.ID,
                               CU.FULL_NAME,                               
                               B.NAME BRANCH_NAME,
                               C.NAME COUNTRY_NAME,
                               SE.NAME SEX_NAME,
                               CU.BIRTH_PLACE,
                               CU.REGISTERED_ADDRESS,                            
                               CU.BIRTHDAY,                               
                               CU.ADDRESS,                               
                               CU.CLOSED_DATE,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.CUSTOMER CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.BRANCH B
                          WHERE     CU.COUNTRY_ID = C.ID
                               AND CU.SEX_ID = SE.ID
                               AND CU.BRANCH_ID = B.ID {(ID.HasValue ? $@" AND CU.ID = {ID}" : null)}
                        ORDER BY CU.ID";

            try
            {
                using (OracleDataAdapter da = new OracleDataAdapter(s, GlobalFunctions.GetConnectionString()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CustomerDAL", "SelectViewData", exx);
                return null;
            }
        }


        public static DataTable SelectCustomerData(string code)
        {
            string s = $@"SELECT CU.ID,
                               P.PHONE,
                               CC.PINCODE,                               
                               B.NAME BRANCH_NAME,
                               C.NAME COUNTRY_NAME,
                               SE.NAME SEX_NAME,
                               CU.FULL_NAME,                            
                               CU.BIRTHDAY,
                               CU.BIRTH_PLACE,                          
                               CU.ADDRESS,
                               CU.REGISTERED_ADDRESS,                                
                               CU.CLOSED_DATE,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.CUSTOMER CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.BRANCH B,
                               ELMS_USER.CUSTOMER_CARDS CC,
                               (SELECT * FROM ELMS_USER.V_PHONE WHERE OWNER_TYPE = {(int)PhoneOwnerEnum.Customer}) P
                          WHERE  CU.ID = CC.CUSTOMER_ID
                                 AND CU.COUNTRY_ID = C.ID
                                 AND CU.SEX_ID = SE.ID
                                 AND CU.BRANCH_ID = B.ID
                                 AND CU.ID = P.OWNER_ID(+)
                                 AND CC.PINCODE = {code}
                        ORDER BY CU.ID";

            try
            {
                using (OracleDataAdapter da = new OracleDataAdapter(s, GlobalFunctions.GetConnectionString()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CustomerDAL", "SelectViewData", exx);
                return null;
            }
        }

        public static DataTable SelectCustomerDataView(int? ID)
        {
            string s = $@"SELECT CU.ID,
                               P.PHONE,
                               CC.PINCODE,                               
                               B.NAME BRANCH_NAME,
                               C.NAME COUNTRY_NAME,
                               SE.NAME SEX_NAME,
                               CU.FULL_NAME,                            
                               CU.BIRTHDAY,
                               CU.BIRTH_PLACE,                          
                               CU.ADDRESS,
                               CU.REGISTERED_ADDRESS,                                
                               CU.CLOSED_DATE,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.CUSTOMER CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.BRANCH B,
                               ELMS_USER.CUSTOMER_CARDS CC,
                               (SELECT * FROM ELMS_USER.V_PHONE WHERE OWNER_TYPE = {(int)PhoneOwnerEnum.Customer}) P
                          WHERE  CU.ID = CC.CUSTOMER_ID
                                 AND CU.COUNTRY_ID = C.ID
                                 AND CU.SEX_ID = SE.ID
                                 AND CU.BRANCH_ID = B.ID
                                 AND CU.ID = P.OWNER_ID(+)
                                 AND CU.ID = {ID}
                        ORDER BY CU.ID";

            try
            {
                using (OracleDataAdapter da = new OracleDataAdapter(s, GlobalFunctions.GetConnectionString()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CustomerDAL", "SelectCustomerDataView", exx);
                return null;
            }
        }

        public static DataTable SelectViewCustomer(int? ID)
        {
            string s = $@"SELECT CU.ID,
                               CU.FULL_NAME,                               
                               B.NAME BRANCH_NAME,
                               C.NAME COUNTRY_NAME,
                               CU.BIRTH_PLACE,
                               CU.REGISTERED_ADDRESS,                            
                               CU.BIRTHDAY,
                               SE.NAME SEX_NAME,                               
                               CU.ADDRESS,                               
                               CU.CLOSED_DATE,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.CUSTOMER CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.BRANCH B
                          WHERE     CU.COUNTRY_ID = C.ID
                               AND CU.SEX_ID = SE.ID
                               AND CU.BRANCH_ID = B.ID {(ID.HasValue ? $@" AND CU.ID = {ID}" : null)}
                        ORDER BY CU.ID";

            try
            {
                using (OracleDataAdapter da = new OracleDataAdapter(s, GlobalFunctions.GetConnectionString()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "CustomerDAL", "SelectViewData", exx);
                return null;
            }
        }

        public static Int32 InsertCustomer(OracleTransaction tran, Customer customer)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.CUSTOMER(FULL_NAME,
                                                                    BRANCH_ID,
                                                                    COUNTRY_ID,
                                                                    SEX_ID,
                                                                    BIRTHDAY,                                                                     
                                                                    REGISTERED_ADDRESS,
                                                                    BIRTH_PLACE,
                                                                    ADDRESS,
                                                                    NOTE)
                                                    VALUES(:inFULL_NAME,
                                                           :inBRANCH_ID,
                                                           :inCOUNTRY_ID,
                                                           :inSEXID,
                                                           :inBIRTHDAY,
                                                           :inREGISTERED_ADDRESS,                                                           
                                                           :inBIRTH_PLACE,                                                           
                                                           :inADDRESS,                                                        
                                                           :inNOTE) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inFULL_NAME", customer.FULL_NAME));
            command.Parameters.Add(new OracleParameter("inBRANCH_ID", customer.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inCOUNTRY_ID", customer.COUNTRY_ID));
            command.Parameters.Add(new OracleParameter("inSEXID", customer.SEX_ID));
            command.Parameters.Add(new OracleParameter("inBIRTHDAY", customer.BIRTHDAY));
            command.Parameters.Add(new OracleParameter("inREGISTERED_ADDRESS", customer.REGISTERED_ADDRESS));
            command.Parameters.Add(new OracleParameter("inBIRTH_PLACE", customer.BIRTH_PLACE));
            command.Parameters.Add(new OracleParameter("inADDRESS", customer.ADDRESS));
            command.Parameters.Add(new OracleParameter("inNOTE", customer.NOTE));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }

        public static void UpdateCustomer(OracleTransaction tran, Customer customer)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER.CUSTOMER SET FULL_NAME = :inFULL_NAME,
                                                                        BRANCH_ID = :inBRANCH_ID,
                                                                        COUNTRY_ID = :inCOUNTRY_ID,
                                                                        SEX_ID = :inSEXID,
                                                                        BIRTHDAY = :inBIRTHDAY,
                                                                        REGISTERED_ADDRESS = :inREGISTERED_ADDRESS,                                                                   
                                                                        BIRTH_PLACE = :inBIRTH_PLACE,
                                                                        ADDRESS = :inADDRESS,
                                                                        NOTE = :inNOTE,
                                                                        USED_USER_ID = :inUSEDUSERID,
                                                                        UPDATE_USER = :inUPDATEUSER,
                                                                        UPDATE_DATE = SYSDATE
                                                            WHERE ID = :inID";
            command.Parameters.Add(new OracleParameter("inFULL_NAME", customer.FULL_NAME));
            command.Parameters.Add(new OracleParameter("inBRANCH_ID", customer.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inCOUNTRY_ID", customer.COUNTRY_ID));
            command.Parameters.Add(new OracleParameter("inSEXID", customer.SEX_ID));
            command.Parameters.Add(new OracleParameter("inBIRTHDAY", customer.BIRTHDAY));
            command.Parameters.Add(new OracleParameter("inREGISTERED_ADDRESS", customer.REGISTERED_ADDRESS));
            command.Parameters.Add(new OracleParameter("inBIRTH_PLACE", customer.BIRTH_PLACE));
            command.Parameters.Add(new OracleParameter("inADDRESS", customer.ADDRESS));
            command.Parameters.Add(new OracleParameter("inNOTE", customer.NOTE));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", customer.USED_USER_ID));
            command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inID", customer.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }



        public static void DeleteCustomerByID(int customerID)
        {
            string commandSql = null;
            using (OracleConnection connection = new OracleConnection())
            {
                OracleTransaction transaction = null;
                try
                {
                    if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                    {
                        connection.ConnectionString = GlobalFunctions.GetConnectionString();
                        connection.Open();
                    }

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        transaction = connection.BeginTransaction();
                        command.Transaction = transaction;
                        command.CommandText = $@"DELETE FROM ELMS_USER.CUSTOMER WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", customerID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Müştəri bazadan silinmədi.", commandSql, GlobalVariables.V_UserName, "CustomerDAL", "DeleteCustomerByID", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteCustomerData(int customerID)
        {
            GlobalProcedures.ExecuteProcedureWithUser("ELMS_USER_TEMP.PROC_DELETE_CUSTOMER_DATA", "P_CUSTOMER_ID", customerID, "Müştəri bazadan silinmədi.");
        }

        public static void DeleteCustomer(int doctorID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_CUSTOMER_CARDS", "P_CUSTOMER_ID", doctorID, "Müştəri bazadan silinmədi.");
        }

        public static void DeleteWorkPlaceTemp(int workID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_WORKPLACE_TEMP", "P_CUSTOMER_ID", workID, "Müştəri bazadan silinmədi.");
        }

        public static void DeleteRelativeTemp(int relativeID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_CUSTOMER_RELATIVE", "P_CUSTOMER_ID", relativeID, "Müştəri bazadan silinmədi.");
        }




    }
}
