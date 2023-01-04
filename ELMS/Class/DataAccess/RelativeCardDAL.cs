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
    class RelativeCardDAL
    {


        public static DataTable SelectRelativeByOwnerID(int ownerID, PhoneOwnerEnum ownerType, int? id = null)
        {
            string sql = $@"SELECT CRT.ID,
                                   CRT.CUSTOMER_ID,
                                   CRT.PHONE_ID,
                                   KR.NAME KIND_NAME,
                                   PN.NAME PROFESSION_NAME,
                                   PD.NAME DESCRIPTION_NAME,
                                   P.PHONE_NUMBER,
                                   CRT.NAME,
                                   CRT.SALARY,
                                   CRT.NOTE,
                                   CRT.USED_USER_ID
                              FROM ELMS_USER_TEMP.CUSTOMER_RELATIVE_TEMP CRT,
                                   ELMS_USER_TEMP.PHONE_TEMP P,
                                   ELMS_USER.PHONE_DESCRIPTIONS PD,
                                   ELMS_USER.KINDSHIP_RATE KR,
                                   ELMS_USER.PROFESSION PN
                             WHERE CRT.PHONE_ID = P.ID 
                               AND P.PHONE_DESCRIPTION_ID = PD.ID
                               AND CRT.KINDSHIP_RATE_ID = KR.ID
                               AND CRT.PROFESSION_ID = PN.ID
                               AND P.OWNER_TYPE = {(int)ownerType} 
                               AND CRT.IS_CHANGE != {(int)ChangeTypeEnum.Delete}    
                               AND CRT.CUSTOMER_ID = {ownerID}{(id.HasValue ? $@" AND CRT.ID = {id}" : null)}";
            try
            {
                using (OracleDataAdapter da = new OracleDataAdapter(sql, GlobalFunctions.GetConnectionString()))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception exx)
            {
                GlobalProcedures.LogWrite("Telefon açılmadı.", sql, GlobalVariables.V_UserName, "RelativeDAL", "SelectRelativeByOwnerID", exx);
                return null;
            }
        }


        public static Int32 InsertCustomerRelative(OracleTransaction tran, CustomerRelative relative)
        {
            Int32 id = 0;
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER_TEMP.CUSTOMER_RELATIVE_TEMP(NAME,
                                                                    KINDSHIP_RATE_ID,
                                                                    PROFESSION_ID,
                                                                    SALARY,
                                                                    NOTE,
                                                                    PHONE_ID,
                                                                    CUSTOMER_ID)
                                                    VALUES(:inNAME,
                                                           :inKIND_ID,
                                                           :inPROFESSION_ID,
                                                           :inSALARY,
                                                           :inNOTE,
                                                           :inPHONE_ID,                                                           
                                                           :inCUSTOMER_ID) RETURNING ID INTO :outID";
            command.Parameters.Add(new OracleParameter("inNAME", relative.NAME));
            command.Parameters.Add(new OracleParameter("inKIND_ID", relative.KIND_ID));
            command.Parameters.Add(new OracleParameter("inPROFESSION_ID", relative.PROFESSION_ID));
            command.Parameters.Add(new OracleParameter("inSALARY", relative.SALARY));
            command.Parameters.Add(new OracleParameter("inNOTE", relative.NOTE));
            command.Parameters.Add(new OracleParameter("inPHONE_ID", relative.PHONE_ID));
            command.Parameters.Add(new OracleParameter("inCUSTOMER_ID", relative.CUSTOMER_ID));
            command.Parameters.Add(new OracleParameter("outID", OracleDbType.Int32, ParameterDirection.Output));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            id = Convert.ToInt32(command.Parameters["outID"].Value.ToString());

            command.Dispose();

            return id;
        }

        public static void UpdateCustomerRelative(OracleTransaction tran, CustomerRelative relative)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER_TEMP.CUSTOMER_RELATIVE_TEMP SET NAME = :inNAME,
                                                                        KINDSHIP_RATE_ID = :inKIND_ID,
                                                                        PROFESSION_ID = :inPROFESSION_ID,
                                                                        SALARY = :inSALARY,
                                                                        NOTE = :inNOTE,
                                                                        PHONE_ID = :inPHONE_ID,                                                                   
                                                                        CUSTOMER_ID = :inCUSTOMER_ID,
                                                                        USED_USER_ID = :inUSEDUSERID,
                                                                        IS_CHANGE = :inIS_CHANGE
                                                            WHERE ID = :inID";
            command.Parameters.Add(new OracleParameter("inNAME", relative.NAME));
            command.Parameters.Add(new OracleParameter("inKIND_ID", relative.KIND_ID));
            command.Parameters.Add(new OracleParameter("inPROFESSION_ID", relative.PROFESSION_ID));
            command.Parameters.Add(new OracleParameter("inSALARY", relative.SALARY));
            command.Parameters.Add(new OracleParameter("inNOTE", relative.NOTE));
            command.Parameters.Add(new OracleParameter("inPHONE_ID", relative.PHONE_ID));
            command.Parameters.Add(new OracleParameter("inCUSTOMER_ID", relative.CUSTOMER_ID));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inIS_CHANGE", relative.IS_CHANGE));
            command.Parameters.Add(new OracleParameter("inID", relative.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }


        public static void DeleteCustomerRelative(int phoneID, int ownerID)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.CUSTOMER_RELATIVE_TEMP SET IS_CHANGE = {(int)ChangeTypeEnum.Delete}
                                                        WHERE  CUSTOMER_ID = :inCUSTOMERID
                                                          AND ID = :inID";
                        command.Parameters.Add(new OracleParameter("inCUSTOMERID", ownerID));
                        command.Parameters.Add(new OracleParameter("inID", phoneID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Qohumlar temp cədvəldən silinmədi.", commandSql, GlobalVariables.V_UserName, "CustomerWorkDAL", "DeleteCustomerWork", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

        public static void DeleteRelativePhone(int phoneID, int? ownerID, PhoneOwnerEnum phoneOwner)
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
                        command.CommandText = $@"UPDATE ELMS_USER_TEMP.PHONE_TEMP SET IS_CHANGE = {(int)ChangeTypeEnum.Delete}
                                                        WHERE OWNER_TYPE = :inOWNERTYPE
                                                          AND OWNER_ID = :inOWNERID
                                                          AND ID = :inID";
                        command.Parameters.Add(new OracleParameter("inOWNERTYPE", (int)phoneOwner));
                        command.Parameters.Add(new OracleParameter("inOWNERID", ownerID));
                        command.Parameters.Add(new OracleParameter("inID", phoneID));
                        commandSql = command.CommandText;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Connection.Close();
                    }
                }
                catch (Exception exx)
                {
                    transaction.Rollback();
                    GlobalProcedures.LogWrite("Telefon temp cədvəldən silinmədi.", commandSql, GlobalVariables.V_UserName, "PhoneDAL", "DeletePhone", exx);
                }
                finally
                {
                    transaction.Dispose();
                    connection.Dispose();
                }
            }
        }

    }
}
