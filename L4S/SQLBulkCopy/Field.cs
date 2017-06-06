using System;


namespace SQLBulkCopy
{
    class Field
    {

        public string Name;
        public string DataType;
        public int FileFieldPosition;
        public Int64 Length;
        public int Precision;
        public bool Nullable;
        public int Scale;

        public bool IsNullable()
        {
            return Nullable;
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
