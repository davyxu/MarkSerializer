using System;

namespace MarkSerializer
{
    
    class ArraySerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft.IsArray;
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            var arr = ins as System.Array;

            ser.Serialize<int>(arr.Length);

            var elementType = ins.GetType().GetElementType();

            for (int i = 0; i < arr.Length; i++)
            {
                var obj = arr.GetValue(i);
                ser.Serialize(elementType, obj);                
            }
        }

        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            var size = ser.Deserialize<int>();

            var ins = Activator.CreateInstance(ft, size) as System.Array;

            for (int i = 0; i < size; i++)
            {
                var v = ser.Deserialize(ft.GetElementType());
                ins.SetValue(v, i);
            }

            return ins;
        }
    }

}
