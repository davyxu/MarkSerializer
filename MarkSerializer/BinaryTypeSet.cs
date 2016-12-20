using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarkSerializer
{
    public class BinaryTypeSet
    {
        static List<BinaryTypeSerializer> _ser = new List<BinaryTypeSerializer>();

        internal static T GetCustomAttribute<T>(Type type) where T : class
        {
            object[] objs = type.GetCustomAttributes(typeof(T), false);
            if (objs.Length > 0)
                return (T)objs[0];

            return null;
        }



        public static void RegisterBuiltinTypes()
        {
            // 顺序不要随便调整
            Register(new Integer32Serializer());
            Register(new StringSerializer());
            Register(new Float32Serializer());
            Register(new BoolSerializer());
            Register(new GenericListSerializer());
            Register(new GenericDictSerializer());
            Register(new ArraySerializer());
            Register(new EnumSerializer());
            Register(new ClassSerializer());
        }

        public static void Register(BinaryTypeSerializer ins)
        {
            _ser.Add(ins);
        }

        internal static BinaryTypeSerializer MatchType(Type ft)
        {
            foreach (var ts in _ser)
            {
                if (ts.Match(ft))
                {
                    return ts;
                }
            }

            return null;
        }
    }




}
