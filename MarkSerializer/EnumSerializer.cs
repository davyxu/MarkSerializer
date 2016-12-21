using System;

namespace MarkSerializer
{
    class EnumSerializer : TypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft.IsEnum;
        }

        public override bool Serialize(BinarySerializer ser, Type ft, ref object obj)
        {

            if (ser.IsLoading)
            {
                Int32 value = 0;
                ser.Serialize(ref value);                

                obj = Enum.ToObject(obj.GetType(), value);
            }
            else
            {
                Int32 value = Convert.ToInt32(obj);
                ser.Serialize(ref value);
            }

            return true;
        }
    }
}
