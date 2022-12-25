using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class SexDAL
    {
        public static DataSet SelectSexByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                sql = $@"SELECT ID,NAME FROM ELMS_USER.SEX ORDER BY ID";
            else
                sql = $@"SELECT ID,NAME FROM ELMS_USER.SEX WHERE ID = {ID}";

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
                GlobalProcedures.LogWrite("Cins açılmadı.", sql, GlobalVariables.V_UserName, "SexDAL", "SelectSexByID", exx);
                return null;
            }
        }
    }
}
