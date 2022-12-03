using ELMS.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ELMS.Class
{
    public static class Extensions
    {
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            if (dt.IsEmpty()) return new List<T>();
            return (List<T>)DataTableToObjects<T>(dt);
        }

        public static List<T> ToList<T>(this DataSet ds) where T : class, new()
        {
            return ds.Tables[0].ToList<T>();
        }

        public static bool IsNotEmpty(this DataTable dt)
        {
            return !IsEmpty(dt);
        }

        public static bool IsEmpty(this DataSet ds)
        {
            return (ds == null)
                || (ds.Tables.Count == 0)
                || (ds.Tables[0].Rows.Count == 0);
        }

        public static bool IsEmpty(this DataTable dt)
        {
            return (dt == null)
                || (dt.Rows.Count == 0);
        }

        public static decimal? GetDecimalNull(this DataRow dataRow, string columnName)
        {
            if (dataRow[columnName] == DBNull.Value) return null;
            return (decimal?)dataRow[columnName];
        }

        public static int? GetIntNull(this DataRow dataRow, string columnName)
        {
            if (dataRow[columnName] == DBNull.Value) return null;
            return (int?)dataRow[columnName];
        }        

        public static bool IsNullOrDBNull(object obj)
        {
            if (object.ReferenceEquals(obj, null))
                return true;
            if (obj == DBNull.Value)
                return true;
            return false;
        }

        public static IEnumerable<T> DataTableToObjects<T>(DataTable dt) where T : class, new()
        {
            IList<T> items = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T item = DataRowToObjectConvert(new T(), dt, i) as T;
                if (item != null) items.Add(item);
            }
            return items;
        }

        public static object DataRowToObjectConvert(Object obj, DataTable dt, int irow)
        {
            Type t = obj.GetType();

            PropertyInfo[] tmpP = t.GetProperties();
            string ColumnValue = string.Empty;
            object ColumnObjValue = null;
            if (dt.Rows.Count > 0)

                #region Convert data
                foreach (PropertyInfo property in tmpP)
                {
                    string dbColumnName = property.Name;
                    foreach (object attr in property.GetCustomAttributes(typeof(DBColumnAttribute), false))
                    {
                        dbColumnName = (attr as DBColumnAttribute).DBColumnName;
                    }
                    DataColumn column = dt.Columns[dbColumnName];
                    if (column == null)
                        continue;

                    ColumnValue = dt.Rows[irow][column].ToString();
                    ColumnObjValue = dt.Rows[irow][column];

                    CheckAndSetProperties(obj, property, ColumnObjValue, ColumnValue);
                }
            #endregion

            return obj;

        }

        private static void CheckAndSetProperties(Object obj, PropertyInfo property, object ColumnObjValue, string ColumnValue)
        {
            Type propertyType = property.PropertyType;
            if (propertyType == typeof(String))
            {
                if (!IsNullOrDBNull(ColumnObjValue))
                    property.SetValue(obj, ColumnObjValue, null);
            }
            else if ((propertyType == typeof(Int16)) || (propertyType == typeof(Int32)) ||
                    propertyType.IsNullableOf<Int16>() || propertyType.IsNullableOf<Int32>())
            {
                if (ColumnValue.Length != 0) property.SetValue(obj, Convert.ToInt32(ColumnValue), null);
            }
            else if (propertyType == typeof(Int64) || propertyType.IsNullableOf<Int64>())
            {
                if (ColumnValue.Length != 0)
                {
                    Int64 intColumnValue = Convert.ToInt64(ColumnValue);
                    if (intColumnValue >= UInt32.MinValue && intColumnValue <= UInt32.MaxValue)
                        property.SetValue(obj, (uint)intColumnValue, null);
                    else
                        property.SetValue(obj, intColumnValue, null);
                }
            }
            else if (propertyType == typeof(Boolean) || propertyType.IsNullableOf<Boolean>())
            {
                if (ColumnValue.Length != 0)
                {
                    if (ColumnValue == "0")
                    {
                        property.SetValue(obj, false, null);
                    }
                    else if (ColumnValue == "1")
                    {
                        property.SetValue(obj, true, null);
                    }
                    else
                    {
                        property.SetValue(obj, Convert.ToBoolean(ColumnValue), null);
                    }
                }
            }

            else if (propertyType == typeof(DateTime) || propertyType.IsNullableOf<DateTime>())
            {
                if (ColumnValue.Length != 0)
                    property.SetValue(obj, Convert.ToDateTime(ColumnObjValue), null);
            }
            else if (propertyType == typeof(Decimal) || propertyType.IsNullableOf<Decimal>())
            {
                if (ColumnValue.Length != 0)
                    property.SetValue(obj, Convert.ToDecimal(ColumnValue), null);
            }
            else if (propertyType == typeof(float) || propertyType.IsNullableOf<float>())
            {
                if (ColumnValue.Length != 0)
                    property.SetValue(obj, Convert.ToSingle(ColumnValue), null);
            }
            else if (propertyType == typeof(Double) || propertyType.IsNullableOf<Double>())
            {
                if (ColumnValue.Length != 0)
                    property.SetValue(obj, (float)Convert.ToDouble(ColumnValue), null);
            }
            else if (propertyType == typeof(Guid) || propertyType.IsNullableOf<Guid>())
            {
                if (ColumnValue.Length != 0) property.SetValue(obj, new Guid(ColumnValue), null);
            }
            else if (propertyType == typeof(byte[]))
            {
                if (ColumnValue.Length != 0) property.SetValue(obj, (byte[])ColumnObjValue, null);
            }            
            else
            {
                if (ColumnValue.Length != 0) property.SetValue(obj, ColumnValue, null);
            }
        }

        public static bool IsNullableOf<T>(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type typeUnderlying = Nullable.GetUnderlyingType(type);
                return typeUnderlying == typeof(T);
            }
            return false;
        }
    }
}
