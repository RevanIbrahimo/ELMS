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
    public class PhoneDescriptionDAL
    {
        public static DataSet SelectPhoneDescriptionByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                sql = "SELECT ID,NAME,NOTE,USED_USER_ID,ORDER_ID FROM ELMS_USER.PHONE_DESCRIPTIONS ORDER BY ORDER_ID";
            else
                sql = $@"SELECT ID,NAME,NOTE,USED_USER_ID,ORDER_ID FROM ELMS_USER.PHONE_DESCRIPTIONS WHERE ID = {ID}";

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
                GlobalProcedures.LogWrite("Telefonun təsvirləri açılmadı.", sql, GlobalVariables.V_UserName, "PhoneDescriptionDAL", "SelectPhoneDescriptionByID", exx);
                return null;
            }
        }

        public static void InsertPhoneDescription(OracleTransaction tran, PhoneDescription description)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.PHONE_DESCRIPTIONS(ID,
                                                                              NAME,
                                                                              NOTE,
                                                                              INSERT_USER)
                                                    VALUES(:inID,
                                                           :inNAME,
                                                           :inNOTE,
                                                           :inINSERTUSER)";
            command.Parameters.Add(new OracleParameter("inID", description.ID));
            command.Parameters.Add(new OracleParameter("inNAME", description.NAME));
            command.Parameters.Add(new OracleParameter("inNOTE", description.NOTE));
            command.Parameters.Add(new OracleParameter("inINSERTUSER", GlobalVariables.V_UserID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void UpdatePhoneDescription(OracleTransaction tran, PhoneDescription description)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER.PHONE_DESCRIPTIONS SET NAME = :inNAME,                                                                                  
                                                                               NOTE = :inNOTE,
                                                                               USED_USER_ID = :inUSEDUSERID,
                                                                               ORDER_ID = :inORDERID,
                                                                               UPDATE_USER = :inUPDATEUSER,
                                                                               UPDATE_DATE = SYSDATE
                                                                    WHERE ID = :inID";
            command.Parameters.Add(new OracleParameter("inNAME", description.NAME));
            command.Parameters.Add(new OracleParameter("inNOTE", description.NOTE));
            command.Parameters.Add(new OracleParameter("inUSEDUSERID", description.USED_USER_ID));
            command.Parameters.Add(new OracleParameter("inORDERID", description.ORDER_ID));
            command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inID", description.ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
