using System;
using System.IO;

namespace MarkSerializer
{
    
    class Integer32Serializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(int);
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            ser.Writer.Write((int)ins);
        }

        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            return ser.Reader.ReadInt32();
        }
    }

}
