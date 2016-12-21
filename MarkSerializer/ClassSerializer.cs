using System;

namespace MarkSerializer
{
    class ClassSerializer : TypeSerializer
    {
        public override int Order
        {
            get { return 1000; }
        }

        public override bool Match(Type ft)
        {
            return ft.IsClass || ft.IsValueType;
        }

        public override bool Serialize(BinarySerializer ser, Type ft, ref object obj)
        {

            string name;

            if (ser.IsLoading)
            {
                name = string.Empty;
                ser.Serialize(ref name);
            }
            else
            {
                name = ft.FullName;
                ser.Serialize(ref name);
            }

            if (obj == null)
            {
                obj = ft.Assembly.CreateInstance(name);
            }

            var ins = (IMarkSerializable)obj;
            ins.Serialize(ser);     

            return true;
        }
    }
}
