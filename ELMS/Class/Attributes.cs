using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELMS.Class
{
    public class IsForeignKeyAttribute : Attribute 
    {
        Enum _parentColumn;
        public IsForeignKeyAttribute() { }
        public IsForeignKeyAttribute(Enum parentColumn)
        {
            _parentColumn = parentColumn;
        }

        public Enum ParentColumn
        { get { return _parentColumn; } }
    }

    public class StringOptionsAttribute : Attribute
    {
        private int _maxValue;

        public StringOptionsAttribute(int maxValue)
        { 
            _maxValue = maxValue;
        }

        public int MaxValue
        { get { return _maxValue; } }
    }

    public class NumericOptionsAttribute : Attribute
    {
        int _precision, _scale;
        public NumericOptionsAttribute(int precision, int scale)
        {
            _precision = precision;
            _scale = scale;
        }
    }

    public class DBColumnAttribute : Attribute
    {
        private string _dbType;
        private IsNullable _nullable;
        private string _dbColumnName;
        public DBColumnAttribute(IsNullable nullable, string dbColumnName, string dbType)
        {
            _nullable = nullable;
            _dbType = dbType;
            _dbColumnName = dbColumnName;
        }

        public string DBColumnName
        { get { return _dbColumnName; } }

        public string DBType
        { get { return _dbType; } }

        public IsNullable IsNullable { get { return _nullable; } }
    }

    public class IsPrimaryKeyAttribute : Attribute { }

    public class IsIdentityAttribute : Attribute { }

    public class DBTableNameAttribute : Attribute
    {
        string _dbTableName;
        public DBTableNameAttribute(string dbTableName)
        {
            _dbTableName = dbTableName;
        }
    }

    public enum IsNullable
    {
        Yes,No
    }
}
