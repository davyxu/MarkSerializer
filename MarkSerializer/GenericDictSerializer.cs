using System;
using System.Collections;
using System.Collections.Generic;

namespace MarkSerializer
{
    
    class GenericDictSerializer : BinaryTypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft.IsGenericType && ft.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        public override void Serialize(BinarySerializer ser, object ins)
        {
            ser.Serialize<int>((ins as ICollection).Count);

            foreach (DictionaryEntry dictItem in ins as IDictionary)
            {
                ser.Serialize(dictItem.Key.GetType(), dictItem.Key);
                ser.Serialize(dictItem.Value.GetType(), dictItem.Value);
            }
        }

        public override object Deserialize(BinaryDeserializer ser, Type ft )
        {
            var size = ser.Deserialize<int>();

            var ins = Activator.CreateInstance(ft) as IDictionary;

            var keyType = ft.GetGenericArguments()[0];
            var valueType = ft.GetGenericArguments()[1];

            for (int i = 0; i < size; i++)
            {
                var key = ser.Deserialize(keyType);
                var value = ser.Deserialize(valueType);
                ins.Add(key, value);
            }

            return ins;
        }
    }

}
