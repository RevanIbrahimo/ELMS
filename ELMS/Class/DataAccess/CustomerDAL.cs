using ELMS.Class.Tables;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                                                    ADDRESS,)
                                                    VALUES(:inNAME,
                                                           :inBRANCHID,
                                                           :inSEXID,
                                                           :inBIRTHDAY,
                                                           :inAGE,                                                           
                                                           :inADDRESS,
                                                           :inEMAIL,
                                                           :inWORKPLACE,                                                           
                                                           :inNOTE,
                                                           :inISPHONEOPTIONAL,
                                                           :inINSERTUSER) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inNAME", customer.NAME));
            command.Parameters.Add(new OracleParameter("inBRANCHID", customer.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inSEXID", customer.SEX_ID));
            command.Parameters.Add(new OracleParameter("inBIRTHDAY", customer.BIRTHDAY));
            command.Parameters.Add(new OracleParameter("inAGE", customer.AGE));
            command.Parameters.Add(new OracleParameter("inADDRESS", customer.ADDRESS));
            command.Parameters.Add(new OracleParameter("inEMAIL", customer.EMAIL));
            command.Parameters.Add(new OracleParameter("inWORKPLACE", customer.WORKPLACE));
            command.Parameters.Add(new OracleParameter("inNOTE", customer.NOTE));
            command.Parameters.Add(new OracleParameter("inISPHONEOPTIONAL", customer.IS_PHONE_OPTIONAL));
            command.Parameters.Add(new OracleParameter("inINSERTUSER", GlobalVariables.V_UserID));
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
            command.CommandText = $@"UPDATE DENTAL_USER.CUSTOMER SET NAME = :inNAME,
                                                                        BRANCH_ID = :inBRANCHID,
                                                                        SEX_ID = :inSEXID,
                                                                        BIRTHDAY = :inBIRTHDAY,
                                                                        AGE = :inAGE,                                                                   
                                                                        ADDRESS = :inADDRESS,
                                                                        EMAIL = :inEMAIL,
                                                                        WORKPLACE = :inWORKPLACE,                                                                        
                                                                        NOTE = :inNOTE,
                                                                        IS_PHONE_OPTIONAL = :inISPHONEOPTIONAL,
                                                                        USED_USER_ID = :inUSEDUSERID,
                                                                        ORDER_ID = :inORDERID,
                                                                        UPDATE_USER = :inUPDATEUSER,
                                                                        UPDATE_DATE = SYSDATE
                                                            WHERE ID = :inID";
            command.Parameters.Add(new OracleParameter("inNAME", customer.NAME));
            command.Parameters.Add(new OracleParameter("inBRANCHID", customer.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inSEXID", customer.SEX_ID));
            command.Parameters.Add(new OracleParameter("inBIRTHDAY", customer.BIRTHDAY));
            command.Parameters.Add(new OracleParameter("inAGE", customer.AGE));
            command.Parameters.Add(new OracleParameter("inADDRESS", customer.ADDRESS));
            command.Parameters.Add(new OracleParameter("inEMAIL", customer.EMAIL));
            command.Parameters.Add(new OracleParameter("inWORKPLACE", customer.WORKPLACE));
            command.Parameters.Add(new OracleParameter("inNOTE", customer.NOTE));
            command.Parameters.Add(new OracleParameter("inISPHONEOPTIONAL", customer.IS_PHONE_OPTIONAL));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", customer.USED_USER_ID));
            command.Parameters.Add(new OracleParameter("inORDERID", customer.ORDER_ID));
            command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inID", customer.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void DeleteCustomer(int doctorID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("DENTAL_USER.PROC_DELETE_CUSTOMER", "P_CUSTOMER_ID", doctorID, "Xəstə bazadan silinmədi.");
        }
    }
}
