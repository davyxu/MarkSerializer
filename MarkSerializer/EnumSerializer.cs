using System;

namespace MarkSerializer
{
    
    class EnumSerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft.IsEnum;
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            ser.Serialize<int>(Convert.ToInt32(ins));
        }

        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            var v = ser.Deserialize<int>( );
            return Enum.ToObject(ft, v);
        }
    }

}
