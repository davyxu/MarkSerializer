using System;
using System.IO;

namespace MarkSerializer
{
    public class BinarySerializer
    {
        BinaryWriter _writer;

        internal BinaryWriter Writer
        {
            get { return _writer; }
        }

        public BinarySerializer(Stream s)
        {
            _writer = new BinaryWriter(s);

            BinaryTypeSet.RegisterBuiltinTypes();
        }

        public void Serialize<T>(object ins)
        {
            Serialize(typeof(T), ins);
        }

        public void Serialize(Type ft, object ins)
        {
            var ts = BinaryTypeSet.MatchType(ft);
            if (ts == null)
            {
                throw new Exception("Serialize failed, unknown type " + ft.ToString());
            }

            ts.Serialize(this, ins);
        }
    }
}
