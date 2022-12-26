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
    public class CustomerImageDAL
    {
        public static DataSet SelectCustomerImage(int customerID)
        {
            string sql = $@"SELECT ID,IMAGE FROM ELMS_USER.CUSTOMER_IMAGE WHERE CUSTOMER_ID = {customerID}";

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
                GlobalProcedures.LogWrite("Müştərinin şəkli açılmadı.", sql, GlobalVariables.V_UserName, "CustomerImageDAL", "SelectCustomerImage", exx);
                return null;
            }
        }

        public static void InsertCustomerImage(OracleTransaction tran, CustomerImage image)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"INSERT INTO ELMS_USER.CUSTOMER_IMAGE(CUSTOMER_ID,
                                                                            IMAGE,
                                                                            INSERT_USER)
                                                    VALUES(:inCUSTOMERID,
                                                           :inIMAGE,
                                                           :inINSERTUSER)";
            command.Parameters.Add(new OracleParameter("inCUSTOMERID", image.CUSTOMER_ID));
            command.Parameters.Add(new OracleParameter("inIMAGE", image.IMAGE));
            command.Parameters.Add(new OracleParameter("inINSERTUSER", GlobalVariables.V_UserID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void UpdateCustomerImage(OracleTransaction tran, CustomerImage image)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"UPDATE ELMS_USER.CUSTOMER_IMAGE SET IMAGE = :inIMAGE,
                                                                         UPDATE_USER = :inUPDATEUSER,
                                                                         UPDATE_DATE = SYSDATE
                                                              WHERE CUSTOMER_ID = :inCUSTOMERID";
            command.Parameters.Add(new OracleParameter("inIMAGE", image.IMAGE));
            command.Parameters.Add(new OracleParameter("inUPDATEUSER", GlobalVariables.V_UserID));
            command.Parameters.Add(new OracleParameter("inCUSTOMERID", image.CUSTOMER_ID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public static void DeleteCustomerImage(OracleTransaction tran, int customerID)
        {
            OracleCommand command = tran.Connection.CreateCommand();
            command.CommandText = $@"DELETE ELMS_USER.CUSTOMER_IMAGE WHERE CUSTOMER_ID = :inCUSTOMERID";
            command.Parameters.Add(new OracleParameter("inCUSTOMERID", customerID));

            if (tran != null)
                command.Transaction = tran;

            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
