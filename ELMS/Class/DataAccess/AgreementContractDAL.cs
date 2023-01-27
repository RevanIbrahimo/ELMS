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
    class AgreementContractDAL
    {
        public static Int32 InsertAgreementTemp(OracleTransaction tran, AgreementContract order)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.AGREEMENT_TEMP(AGREEMENT_ID,
                                                            ORDER_TAB_ID,                           
                                                            OPERATION_ID)
                                                    VALUES(:inAGREEMENT_ID,
                                                           :inORDER_TAB_ID,
                                                           :inOPERATION_ID) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inAGREEMENT_ID", order.AGREEMENT_ID));
            command.Parameters.Add(new OracleParameter("inORDER_TAB_ID", order.ORDER_TAB_ID));
            command.Parameters.Add(new OracleParameter("inOPERATION_ID", order.OPERATION_ID));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }


        public static void InsertAgreementTemp(int orderID, int operationID, int? agreementID)
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
                        command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.AGREEMENT_TEMP(AGREEMENT_ID,
                                                            ORDER_TAB_ID,                           
                                                            OPERATION_ID)
                                                    VALUES(:inAGREEMENT_ID,
                                                           :inORDER_TAB_ID,
                                                           :inOPERATION_ID)";
                        command.Parameters.Add(new OracleParameter("inAGREEMENT_ID", orderID));
                        command.Parameters.Add(new OracleParameter("inORDER_TAB_ID", operationID));
                        command.Parameters.Add(new OracleParameter("inOPERATION_ID", agreementID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("İş yeri temp cədvəldən silinmədi.", commandSql, GlobalVariables.V_UserName, "CustomerWorkDAL", "DeleteCustomerWork", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        
        public static DataTable SelectTempByAgreementID(int? ID)
        {
            string s = $@"SELECT  AT.ID AGREEMENT_TEMP_ID,
                               CU.ID,
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
                          FROM ELMS_USER_TEMP.AGREEMENT_TEMP AT,
                               ELMS_USER.ORDER_TAB CU,
                               ELMS_USER.BRANCH B,
                               ELMS_USER.TIMES T,
                               ELMS_USER.SOURCE S,
                               ELMS_USER.CUSTOMER C,
                               ELMS_USER.CUSTOMER_CARDS CC,
                               ELMS_USER.ORDER_OPERATION OO,
                               ELMS_USER.OPERATION_TYPE OT
                          WHERE     CU.ID = AT.ID
                               AND CU.SOURCE_ID = S.ID
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
    }
}
