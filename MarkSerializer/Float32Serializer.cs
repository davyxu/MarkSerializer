using System;

namespace MarkSerializer
{
    
    class Float32Serializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(float);
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            ser.Writer.Write((float)ins);
        }

        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            return ser.Reader.ReadSingle();
        }
    }

}
