
using System;
namespace MarkSerializer
{
    class Integer32Serializer : TypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(int);
        }

        public override bool Serialize(BinarySerializer ser, Type ft, ref object obj)
        {

            if (obj == null)
            {
                obj = 0;
            }

            if (ser.IsLoading)
            {
                obj = ser.Reader.ReadInt32();
            }
            else
            {
                ser.Writer.Write((int)obj);
            }            

            return true;
        }
    }
}
