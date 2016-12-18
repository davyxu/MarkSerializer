using System;
using System.IO;

namespace MarkSerializer
{
    public class BinaryDeserializer
    {
        BinaryReader _reader;


        internal BinaryReader Reader
        {
            get { return _reader; }
        }

        public BinaryDeserializer(Stream s)
        {
            _reader = new BinaryReader(s);

            BinaryTypeSet.RegisterBuiltinTypes();
        }

        public T Deserialize<T>()
        {
            return (T)Deserialize(typeof(T));
        }

        public object Deserialize(Type ft)
        {
            var ts = BinaryTypeSet.MatchType(ft);
            if (ts == null)
            {
                throw new Exception("Deserialize failed, unknown type " + ft.ToString());
            }

            return ts.Deserialize(this, ft);
        }
    }
}
