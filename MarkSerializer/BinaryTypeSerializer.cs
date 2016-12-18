using System;

namespace MarkSerializer
{
    public class BinaryTypeSerializer
    {
        public virtual bool Match(Type ft)
        {
            return false;
        }
        public virtual void Serialize(BinarySerializer ser, object ins)
        {
            
        }

        public virtual object Deserialize(BinaryDeserializer ser, Type ft)
        {
            return null;
        }
    }



}
