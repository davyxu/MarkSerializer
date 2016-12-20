using System;
using System.IO;

namespace MarkSerializer
{
    
    class BoolSerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(bool);
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            ser.Writer.Write((bool)ins);
        }

        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            return ser.Reader.ReadBoolean();
        }
    }

}
