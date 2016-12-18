using System;
using System.Collections;
using System.Collections.Generic;

namespace MarkSerializer
{
    
    class GenericListSerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {            
            return ft.IsGenericType && ft.GetGenericTypeDefinition() == typeof(List<>);
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            ser.Serialize<int>((ins as ICollection).Count);

            foreach (var listItem in ins as IEnumerable)
            {
                ser.Serialize(listItem.GetType(), listItem);
            }
        }

        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            var size = ser.Deserialize<int>();

            var parameterType = ft.GetGenericArguments()[0];

            var ins = Activator.CreateInstance(ft) as IList;

            for (int i = 0; i < size; i++)
            {
                var v = ser.Deserialize(parameterType);
                ins.Add(v);
            }

            return ins;
        }
    }

}
