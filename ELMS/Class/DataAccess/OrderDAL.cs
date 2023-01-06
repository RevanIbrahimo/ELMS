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
    class OrderDAL
    {
        public static DataSet SelectOrderByID(int? ID)
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
                          FROM ELMS_USER.ORDER_TAB CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.ORDER_TAB_IMAGE CI,
                               ELMS_USER.BRANCH B
                          WHERE CU.COUNTRY_ID = C.ID
                               AND CU.SEX_ID = SE.ID
                               AND CU.ID = CI.ORDER_TAB_ID
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
                          FROM ELMS_USER.ORDER_TAB CU,
                               ELMS_USER.SEX SE,
                               ELMS_USER.COUNTRY C,
                               ELMS_USER.ORDER_TAB_IMAGE CI,
                               ELMS_USER.BRANCH B
                          WHERE     CU.COUNTRY_ID = C.ID
                               AND CU.SEX_ID = SE.ID
                               AND CU.ID = CI.ORDER_TAB_ID
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
                GlobalProcedures.LogWrite("Müştərinin məlumatları açılmadı.", sql, GlobalVariables.V_UserName, "OrderDAL", "SelectOrderByID", exx);
                return null;
            }
        }

        public static DataTable SelectViewData(int? ID)
        {
            string s = $@"SELECT CU.ID,
                               CC.PINCODE,
                               CU.REGISTER_NUMBER,                               
                               B.NAME BRANCH_NAME,
                               S.NAME ORDER_SOURCE,
                               T.PERIOD TIME,                           
                               CU.ORDER_DATE,                              
                               CU.ADDRESS,
                               CU.FIRST_PAYMENT,
                               CU.ORDER_AMOUNT,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.ORDER_TAB CU,
                               ELMS_USER.BRANCH B,
                               ELMS_USER.TIMES T,
                               ELMS_USER.SOURCE S,
                               ELMS_USER.CUSTOMER C,
                               ELMS_USER.CUSTOMER_CARDS CC
                          WHERE     CU.SOURCE_ID = S.ID
                               AND CU.TIME_ID = T.ID
                               AND CU.CUSTOMER_ID = C.ID
                               AND C.ID = CC.CUSTOMER_ID
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
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "OrderDAL", "SelectViewData", exx);
                return null;
            }
        }


        public static DataTable SelectConfirmData(int? ID)
        {
            string s = $@"SELECT CU.ID,
                               OO.ID OPERATION_ID,
                               OO.NOTE OPERATION_NOTE,
                               OT.NAME OPERATION_NAME,
                               CC.PINCODE,
                               CU.REGISTER_NUMBER,                               
                               B.NAME BRANCH_NAME,
                               S.NAME ORDER_SOURCE,
                               T.PERIOD TIME,                           
                               CU.ORDER_DATE,                              
                               CU.ADDRESS,
                               CU.FIRST_PAYMENT,
                               CU.ORDER_AMOUNT,
                               CU.NOTE,
                               CU.INSERT_DATE,
                               CU.USED_USER_ID
                          FROM ELMS_USER.ORDER_TAB CU,
                               ELMS_USER.BRANCH B,
                               ELMS_USER.TIMES T,
                               ELMS_USER.SOURCE S,
                               ELMS_USER.CUSTOMER C,
                               ELMS_USER.CUSTOMER_CARDS CC,
                               ELMS_USER.ORDER_OPERATION OO,
                               ELMS_USER.OPERATION_TYPE OT
                          WHERE     CU.SOURCE_ID = S.ID
                               AND CU.TIME_ID = T.ID
                               AND CU.CUSTOMER_ID = C.ID
                               AND C.ID = CC.CUSTOMER_ID
                               AND CU.ID = OO.ORDER_ID 
                               AND OO.OPERATION_TYPE_ID = OT.ID
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
                GlobalProcedures.LogWrite("Musterinin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "OrderDAL", "SelectConfirmData", exx);
                return null;
            }
        }


        public static Int32 InsertOrder(OracleTransaction tran, Order order)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.ORDER_TAB(CUSTOMER_ID,                               
                                                            BRANCH_ID,
                                                            SOURCE_ID,
                                                            TIME_ID,                           
                                                            ORDER_DATE,
                                                            FIRST_PAYMENT,
                                                            ORDER_AMOUNT,
                                                            NOTE)
                                                    VALUES(:inCUSTOMER_ID,
                                                           :inBRANCH_ID,
                                                           :inSOURCE_ID,
                                                           :inTIME_ID,
                                                           :inORDER_DATE,
                                                           :inFIRST_PAYMENT,                                                           
                                                           :inORDER_AMOUNT,                                                        
                                                           :inNOTE) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inCUSTOMER_ID", order.CUSTOMER_ID));
            command.Parameters.Add(new OracleParameter("inBRANCH_ID", order.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inSOURCE_ID", order.SOURCE_ID));
            command.Parameters.Add(new OracleParameter("inTIME_ID", order.TIME_ID));
            command.Parameters.Add(new OracleParameter("inORDER_DATE", order.ORDER_DATE));
            command.Parameters.Add(new OracleParameter("inFIRST_PAYMENT", order.FIRST_PAYMENT));
            command.Parameters.Add(new OracleParameter("inORDER_AMOUNT", order.ORDER_AMOUNT));
            command.Parameters.Add(new OracleParameter("inNOTE", order.NOTE));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }

        public static void UpdateOrder(OracleTransaction tran, Order order)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER.ORDER_TAB SET CUSTOMER_ID = :inCUSTOMER_ID,
                                                                        BRANCH_ID = :inBRANCH_ID,
                                                                        SOURCE_ID = :inSOURCE_ID,
                                                                        TIME_ID = :inTIME_ID,
                                                                        ORDER_DATE = :inORDER_DATE,
                                                                        FIRST_PAYMENT = :inFIRST_PAYMENT,                                                                   
                                                                        ORDER_AMOUNT = :inORDER_AMOUNT,
                                                                        NOTE = :inNOTE,
                                                                        USED_USER_ID = :inUSEDUSERID,
                                                                        UPDATE_USER = :inUPDATEUSER,
                                                                        UPDATE_DATE = SYSDATE
                                                            WHERE ID = :inID";
            command.Parameters.Add(new OracleParameter("inCUSTOMER_ID", order.CUSTOMER_ID));
            command.Parameters.Add(new OracleParameter("inBRANCH_ID", order.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inSOURCE_ID", order.SOURCE_ID));
            command.Parameters.Add(new OracleParameter("inTIME_ID", order.TIME_ID));
            command.Parameters.Add(new OracleParameter("inORDER_DATE", order.ORDER_DATE));
            command.Parameters.Add(new OracleParameter("inFIRST_PAYMENT", order.FIRST_PAYMENT));
            command.Parameters.Add(new OracleParameter("inORDER_AMOUNT", order.ORDER_AMOUNT));
            command.Parameters.Add(new OracleParameter("inNOTE", order.NOTE));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", order.USED_USER_ID));
            command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inID", order.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void DeleteOrder(int doctorID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_PRODUCT_CARDS", "P_CUSTOMER_ID", doctorID, "Müraciət bazadan silinmədi.");
        }

        public static void DeleteWorkPlaceTemp(int workID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_WORKPLACE_TEMP", "P_ORDER_TAB_ID", workID, "Müştəri bazadan silinmədi.");
        }


       
    }
}
