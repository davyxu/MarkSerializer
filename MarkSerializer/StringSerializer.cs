using System;

namespace MarkSerializer
{
    class StringSerializer : TypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(string);
        }

        public override bool Serialize(BinarySerializer ser, Type ft, ref object obj)
        {

            if (obj == null)
            {
                obj = string.Empty;
            }

            if (ser.IsLoading)
            {
                obj = ser.Reader.ReadString();
            }
            else
            {
                ser.Writer.Write((string)obj);
            }      

            return true;
        }
    }
}
