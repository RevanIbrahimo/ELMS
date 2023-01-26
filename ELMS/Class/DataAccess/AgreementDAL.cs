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
    class AgreementDAL
    {

        public static DataTable SelectAgreements(int? ID)
        {
            string s = $@"SELECT AG.ID,
                               B.NAME BRANCH_NAME,
                               AG.AGREEMENT_AMOUNT,
                               AG.AGREEMENT_NUMBER,
                               AG.NOTE,
                               AG.AGREEMENT_DATE,
                               AG.USED_USER_ID
                          FROM ELMS_USER.AGREEMENT AG,
                               ELMS_USER.BRANCH B
                          WHERE     AG.BRANCH_ID = B.ID {(ID.HasValue ? $@" AND AG.ID = {ID}" : null)}
                        ORDER BY B.NAME";

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

        public static void UpdateContracts(int contractID, int? agreementID)
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
                        command.CommandText = $@"UPDATE ELMS_USER.ORDER_TAB SET AGREEMENT_ID = {agreementID}
                                                        WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inID", contractID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Müqavilənin statusu cədvəldə dəyişmədi.", commandSql, GlobalVariables.V_UserName, "PhoneDAL", "DeletePhone", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void UpdateOrderOperation(int operationID, OperationTypeEnum operationTypeEnum)
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
                        command.CommandText = $@"UPDATE ELMS_USER.ORDER_OPERATION SET  OPERATION_TYPE_ID = :inOPERATION_TYPE_ID
                                                            WHERE ID = :inID";
                        command.Parameters.Add(new OracleParameter("inOPERATION_TYPE_ID", (int)operationTypeEnum));
                        command.Parameters.Add(new OracleParameter("inID", operationID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Müqavilənin statusu cədvəldə dəyişmədi.", commandSql, GlobalVariables.V_UserName, "PhoneDAL", "DeletePhone", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static DataTable SelectDataByAgreementID(int? ID)
        {
            string s = $@"SELECT CU.ID,
                               CU.AGREEMENT_ID,
                               OO.ID OPERATION_ID,
                               OO.OPERATION_TYPE_ID TYPE_ID,
                               OO.NOTE OPERATION_NOTE,
                               OT.NAME OPERATION_NAME,
                               CC.PINCODE,
                               CU.REGISTER_NUMBER,                               
                               B.NAME BRANCH_NAME,
                               S.NAME ORDER_SOURCE,
                               T.PERIOD TIME,                           
                               T.PERCENT,                           
                               CU.ORDER_DATE,                              
                               CU.ADDRESS,
                               CU.FIRST_PAYMENT,
                               CU.ORDER_AMOUNT,
                               CU.CREDIT_AMOUNT,
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
                               AND CU.BRANCH_ID = B.ID {(ID.HasValue ? $@" AND CU.AGREEMENT_ID = {ID}" : null)}
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


        public static DataTable SelectDataByOperationTypeID(int? ID)
        {
            string s = $@"SELECT CU.ID,
                               OO.ID OPERATION_ID,
                               OO.OPERATION_TYPE_ID TYPE_ID,
                               OO.NOTE OPERATION_NOTE,
                               OT.NAME OPERATION_NAME,
                               CC.PINCODE,
                               CU.REGISTER_NUMBER,                               
                               B.NAME BRANCH_NAME,
                               S.NAME ORDER_SOURCE,
                               T.PERIOD TIME,                           
                               T.PERCENT,                           
                               CU.ORDER_DATE,                              
                               CU.ADDRESS,
                               CU.FIRST_PAYMENT,
                               CU.ORDER_AMOUNT,
                               CU.CREDIT_AMOUNT,
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
                               AND CU.BRANCH_ID = B.ID {(ID.HasValue ? $@" AND OO.OPERATION_TYPE_ID = {ID}" : null)}
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

        public static DataTable SelectDocumentByOrderID(int? ID)
        {
            string s = $@"SELECT OD.ORDER_ID
                          FROM ELMS_USER.ORDER_DOCUMENTS OD ,
                               ELMS_USER.ORDER_TAB CU
                          WHERE     OD.ORDER_ID = CU.ID  {(ID.HasValue ? $@" AND CU.ID = {ID}" : null)}
                        ORDER BY OD.ID";

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
                GlobalProcedures.LogWrite("Senedin məlumatları açılmadı.", s, GlobalVariables.V_UserName, "OrderDAL", "SelectDocumentByOrderID", exx);
                return null;
            }
        }
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
                               CU.CREDIT_AMOUNT,
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
                               OO.OPERATION_TYPE_ID TYPE_ID,
                               OO.NOTE OPERATION_NOTE,
                               OT.NAME OPERATION_NAME,
                               CC.PINCODE,
                               CU.REGISTER_NUMBER,                               
                               B.NAME BRANCH_NAME,
                               S.NAME ORDER_SOURCE,
                               T.PERIOD TIME,                           
                               T.PERCENT,                           
                               CU.ORDER_DATE,                              
                               CU.ADDRESS,
                               CU.FIRST_PAYMENT,
                               CU.ORDER_AMOUNT,
                               CU.CREDIT_AMOUNT,
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

        public static DataTable SelectContractData(int? ID_One, int? ID_Two, int? ID_Three)
        {
            string s = $@"SELECT CU.ID,
                               OO.ID OPERATION_ID,
                               OO.OPERATION_TYPE_ID TYPE_ID,
                               OO.NOTE OPERATION_NOTE,
                               OT.NAME OPERATION_NAME,
                               CC.PINCODE,
                               CU.REGISTER_NUMBER,                               
                               B.NAME BRANCH_NAME,
                               S.NAME ORDER_SOURCE,
                               T.PERIOD TIME,                           
                               T.PERCENT,                           
                               CU.ORDER_DATE,                              
                               CU.ADDRESS,
                               CU.FIRST_PAYMENT,
                               CU.ORDER_AMOUNT,
                               CU.CREDIT_AMOUNT,
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
                               AND CU.BRANCH_ID = B.ID AND ( OT.ID = {ID_One} OR OT.ID = {ID_Two} OR OT.ID = {ID_Three} )
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


        public static Int32 InsertAgreement(OracleTransaction tran, Agreement order)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.AGREEMENT(AGREEMENT_NUMBER,
                                                            AGREEMENT_AMOUNT,                           
                                                            AGREEMENT_DATE,
                                                            BRANCH_ID)
                                                    VALUES(:inAGREEMENT_NUMBER,
                                                           :inAGREEMENT_AMOUNT,
                                                           :inAGREEMENT_DATE,
                                                           :inBRANCH_ID) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inAGREEMENT_NUMBER", order.AGREEMENT_NUMBER));
            command.Parameters.Add(new OracleParameter("inAGREEMENT_AMOUNT", order.AGREEMENT_AMOUNT));
            command.Parameters.Add(new OracleParameter("inAGREEMENT_DATE", order.AGREEMENT_DATE));
            command.Parameters.Add(new OracleParameter("inBRANCH_ID", order.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }

        public static void UpdateAgreement(OracleTransaction tran, Agreement order)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER.AGREEMENT SET AGREEMENT_NUMBER = :inAGREEMENT_NUMBER,
                                                                        AGREEMENT_AMOUNT = :inAGREEMENT_AMOUNT,
                                                                        AGREEMENT_DATE = :inAGREEMENT_DATE,
                                                                        BRANCH_ID = :inBRANCH_ID,
                                                                        USED_USER_ID = :inUSEDUSERID,
                                                                        UPDATE_USER = :inUPDATEUSER,
                                                                        UPDATE_DATE = SYSDATE
                                                            WHERE ID = :inID";
            command.Parameters.Add(new OracleParameter("inAGREEMENT_NUMBER", order.AGREEMENT_NUMBER));
            command.Parameters.Add(new OracleParameter("inAGREEMENT_AMOUNT", order.AGREEMENT_AMOUNT));
            command.Parameters.Add(new OracleParameter("inAGREEMENT_DATE", order.AGREEMENT_DATE));
            command.Parameters.Add(new OracleParameter("inBRANCH_ID", order.BRANCH_ID));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", order.USED_USER_ID));
            command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inID", order.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void DeleteAgreement(int doctorID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_PRODUCT_CARDS", "P_CUSTOMER_ID", doctorID, "Müraciət bazadan silinmədi.");
        }

        public static void DeleteWorkPlaceTemp(int workID)
        {
            GlobalProcedures.ExecuteProcedureWithParametr("ELMS_USER_TEMP.PROC_DELETE_WORKPLACE_TEMP", "P_ORDER_TAB_ID", workID, "Müştəri bazadan silinmədi.");
        }
    }
}
