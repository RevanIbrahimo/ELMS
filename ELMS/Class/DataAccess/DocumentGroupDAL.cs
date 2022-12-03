using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class.DataAccess
{
    class DocumentGroupDAL
    {
        public static DataSet SelectDocumentGroupByID(int? ID)
        {
            string sql = null;
            if (ID == null)
                sql = $@"SELECT ID,NAME FROM ELMS_USER.DOCUMENT_GROUP ORDER BY ID";
            else
                sql = $@"SELECT ID,NAME FROM ELMS_USER.DOCUMENT_GROUP WHERE ID = {ID}";

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
                GlobalProcedures.LogWrite("Qrup açılmadı.", sql, GlobalVariables.V_UserName, "DocumentGroupDAL", "SelectDocumentGroupByID", exx);
                return null;
            }
        }
    }
}
