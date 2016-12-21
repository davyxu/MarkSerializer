using System;
using System.Collections.Generic;

namespace MarkSerializer
{
    public class TypeSerializer
    {
        public virtual bool Match(Type ft) 
        {
            throw new NotImplementedException();            
        }

        public virtual bool Serialize(BinarySerializer ser, Type ft, ref object ins)
        {
            throw new NotImplementedException();            
        }

        public virtual int Order
        {
            get { return 0; }
        }
    }

    public class TypeSerializerSet
    {
        List<TypeSerializer> _arr = new List<TypeSerializer>();

        public TypeSerializerSet()
        {
            Register(new Integer32Serializer());
            Register(new Float32Serializer());
            Register(new StringSerializer());
            Register(new BoolSerializer());
            Register(new EnumSerializer());
            Register(new ArraySerializer());
            Register(new ClassSerializer());
        }

        static TypeSerializerSet ins;

        public static TypeSerializerSet Instance
        {
            get
            {
                if ( ins == null )
                {
                    ins = new TypeSerializerSet();
                }

                return ins;
            }
        }

        public void Register(TypeSerializer ser)
        {
            _arr.Add(ser);
            _arr.Sort(delegate (TypeSerializer a, TypeSerializer b) 
            {
                return a.Order.CompareTo(b.Order);
            });
        }

        public TypeSerializer Match(Type ft)
        {
            foreach (var s in _arr)
            {
                if (s.Match( ft))
                {
                    return s;
                }
            }

            return null;
        }
    }
}
