using System;
using System.Reflection;

namespace MarkSerializer
{
    public delegate void MarkSerializeFieldCallback( FieldInfo fi );

    public sealed class MarkSerializeAttribute : Attribute
    {
        public MarkSerializeAttribute()
        {

        }

        internal MarkSerializeFieldCallback FieldCallback;

        public MarkSerializeAttribute(MarkSerializeFieldCallback callback )
        {
            FieldCallback = callback;
        }
    }

    public interface IMarkSerializable
    {
        void Serialize(BinarySerializer ser);

        void Deserialize(BinaryDeserializer ser);
    }
}
