using System;
using System.IO;

namespace MarkSerializer
{
    
    class StringSerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft == typeof(string);
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            if (string.IsNullOrEmpty((string)ins))
            {
                ser.Writer.Write(string.Empty);
            }
            else
            {
                ser.Writer.Write((string)ins);
            }
        }


        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            return ser.Reader.ReadString();
        }
    }

}
