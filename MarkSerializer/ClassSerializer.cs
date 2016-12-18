using System;
using System.Reflection;

namespace MarkSerializer
{
    
    class ClassSerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft.IsClass;
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            Type ft = ins.GetType();

            ser.Serialize<string>(ft.FullName);

            int serfieldCount = 0;

            // 只遍历私有成员
            foreach (var mi in ft.GetFields(BindingFlags.NonPublic |BindingFlags.Public |BindingFlags.Instance))
            {
                if (mi.IsDefined(typeof(MarkSerializeAttribute), false))
                {
                    ser.Serialize(mi.FieldType, mi.GetValue(ins));                    
                    serfieldCount++;
                }
            }

            if (serfieldCount == 0)
            {
                throw new Exception("zero serialize " + ft.ToString());
            }
        }



        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            var className = ser.Deserialize<string>();


            var ins = ft.Assembly.CreateInstance(className);

            int desercount = 0;
            // 只遍历私有成员
            foreach (var mi in ins.GetType().GetFields(BindingFlags.NonPublic| BindingFlags.Public | BindingFlags.Instance))
            {
                if (mi.IsDefined(typeof(MarkSerializeAttribute), false))
                {
                    var v = ser.Deserialize(mi.FieldType);
                    mi.SetValue(ins, v);
                    desercount++;
                }
            }

            if (desercount == 0)
            {
                throw new Exception("zero deserialize " + ft.ToString());
            }

            return ins;
        }
    }

}
