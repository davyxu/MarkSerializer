using System;

namespace MarkSerializer
{
    class BoolSerializer : TypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(bool);
        }

        public override bool Serialize(BinarySerializer ser, Type ft, ref object obj)
        {

            if (obj == null)
            {
                obj = false;
            }

            if (ser.IsLoading)
            {
                obj = ser.Reader.ReadBoolean();
            }
            else
            {
                ser.Writer.Write((bool)obj);
            }         

            return true;
        }
    }
}
