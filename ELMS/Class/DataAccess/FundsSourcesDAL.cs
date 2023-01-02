using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class FundsSourcesDAL
    {
        public static DataSet SelectFundsSourcesByID(int? typeID)
        {
            string sql = null;
            if (typeID == null)
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.FUNDS_SOURCES C
                            ORDER BY C.ID";
            else
                sql = $@"SELECT C.ID,
                                 C.NAME,
                                 C.NOTE,
                                 C.USED_USER_ID,
                                 C.ORDER_ID
                            FROM ELMS_USER.FUNDS_SOURCES C 
                           WHERE C.ID = {typeID}";

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
                GlobalProcedures.LogWrite("Sifarişin mənbəsi açılmadı.", sql, GlobalVariables.V_UserName, "FundsSourcesDAL", "SelectFundsSourcesByID", exx);
                return null;
            }
        }
    }
}
