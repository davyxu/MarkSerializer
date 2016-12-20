using System;
using System.Reflection;

namespace MarkSerializer
{
    
    class ClassSerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft.IsClass || ft.IsValueType;
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            Type ft = ins.GetType();

            ser.Serialize<string>(ft.FullName);
           
            var cls = ins as IMarkSerializable;
            if (cls == null)
            {
                throw new Exception("class not inherited from 'IMarkSerializable' " + ft.FullName);
            }

            cls.Serialize(ser);
        }



        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            var className = ser.Deserialize<string>();


            var ins = ft.Assembly.CreateInstance(className);
            if ( ins == null )
            {
                throw new Exception("class create failed: " + className);
            }

            var cls = ins as IMarkSerializable;

            if (cls == null)
            {
                throw new Exception("class not inherited from 'IMarkSerializable'");
            }

            cls.Deserialize(ser);

            return ins;
        }
    }

}
