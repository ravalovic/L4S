using System;


namespace SQLBulkCopy
{
    class Field
    {

        public string Name;
        public string DataType;
        public int FileFieldPosition;
        public Int64 length;
        public int precision;
        public bool nullable;
        public int scale;

        public bool IsNullable()
        {
            return nullable;
        }
        public object GetNull()
        {
            if (IsNullable())
            { return DBNull.Value; }
            else
            {
                if (DataType.ToLower().Contains("int"))
                {
                    return -1;
                }
                else
                {
                    return "";
                }
            }



        }

    }
}