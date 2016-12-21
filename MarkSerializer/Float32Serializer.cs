using System;

namespace MarkSerializer
{
    class Float32Serializer : TypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(float);
        }

        public override bool Serialize(BinarySerializer ser, Type ft, ref object obj)
        {

            if (obj == null)
            {
                obj = 0;
            }

            if (ser.IsLoading)
            {
                obj = ser.Reader.ReadSingle();
            }
            else
            {
                ser.Writer.Write((float)obj);
            }        

            return true;
        }
    }
}
