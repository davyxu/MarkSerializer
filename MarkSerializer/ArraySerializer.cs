using System;

namespace MarkSerializer
{
    class ArraySerializer : TypeSerializer
    {
        public override bool Match(Type ft)
        {
            return ft.IsArray;
        }

        public override bool Serialize(BinarySerializer ser, Type ft, ref object obj)
        {

            if ( obj == null )
            {
                obj = Activator.CreateInstance(ft);
            }

            var ins = obj as System.Array;

            var elementType = ins.GetType().GetElementType();

            if ( ser.IsLoading )
            {
                int size = 0;
                ser.Serialize(ref size);


                for (int i = 0; i < size; i++)
                {
                    object value = null;
                    ser.Serialize(elementType, ref value);

                    ins.SetValue( value, i);
                }

            }
            else
            {
                int size = ins.Length;
                ser.Serialize(ref size);

                

                for (int i = 0; i < size; i++)
                {
                    var value = ins.GetValue(i);                    
                    ser.Serialize(elementType, ref value);
                }
            }
            

            return true;
        }
    }
}
