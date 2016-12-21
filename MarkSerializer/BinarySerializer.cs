using System;
using System.Collections.Generic;
using System.IO;

namespace MarkSerializer
{
    public interface IMarkSerializable
    {
        void Serialize(BinarySerializer ser);
    }

    public class BinarySerializer
    {
        internal BinaryWriter Writer;
        internal BinaryReader Reader;

        public BinarySerializer(Stream s, bool loading)
        {
            if (loading)
            {
                Reader = new BinaryReader(s);
            }
            else
            {
                Writer = new BinaryWriter(s);
            }

        }

        public bool IsLoading
        {
            get { return Writer == null; }
        }


        public BinarySerializer Serialize<T>(ref T obj)
        {                       
            // 处理容器从基类, 实际里面是子类问题
            Type final;
            if ( obj == null )
            {
                final = typeof(T);
            }
            else
            {
                final = obj.GetType();
            }

            object ins = (object)obj;
            Serialize(final, ref ins);

            if ( IsLoading )
            {
                obj = (T)ins;
            }

            return this;
        }

        public BinarySerializer Serialize(Type ft, ref object obj)
        {       
            var ser = TypeSerializerSet.Instance.Match(ft);

            if (ser != null)
            {
                ser.Serialize(this, ft, ref obj);

                return this;
            }

            throw new Exception("unknown type to serialize: " + ft.FullName);
        }

        // 列表
        public BinarySerializer Serialize<T>(ref List<T> list)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            int size = 0;

            if (IsLoading)
            {
                Serialize(ref size);

                for (int i = 0; i < size; i++)
                {
                    T v = default(T);
                    Serialize<T>(ref v);                    
                    list.Add(v);
                }
            }
            else
            {
                size = list.Count;
                Serialize(ref size);

                for (int i = 0; i < list.Count;i++ )
                {
                    var data = list[i];
                    Serialize(ref data);
                }
            }

            return this;
        }


        public BinarySerializer Serialize<TKey, TValue>(ref Dictionary<TKey, TValue> dict)
        {
            if (dict == null)
            {
                dict = new Dictionary<TKey, TValue>();
            }

            int size = 0;

            if (IsLoading)
            {
                Serialize(ref size);

                for (int i = 0; i < size; i++)
                {
                    TKey key = default(TKey);
                    Serialize(ref key);

                    TValue value = default(TValue);
                    Serialize(ref value);

                    dict.Add(key, value);
                }
            }
            else
            {
                size = dict.Count;
                Serialize(ref size);

                foreach( var kv in dict )
                {
                    TKey key = kv.Key;
                    Serialize(ref key);

                    TValue value = kv.Value;
                    Serialize(ref value);
                }
            }

            return this;
        }

    }

}
