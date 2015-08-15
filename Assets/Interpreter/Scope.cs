using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    /*
    public class Scope : IDictionary<string, object>, IDictionary
    {
        protected Dictionary<string, object> dict = new Dictionary<string, object>();

        public virtual object this[object key]
        {
            get
            {
                return ((IDictionary)dict)[key];
            }

            set
            {
                ((IDictionary)dict)[key] = value;
            }
        }

        public virtual object this[string key]
        {
            get
            {
                return dict[key];
            }

            set
            {
                dict[key] = value;
            }
        }

        public virtual int Count
        {
            get
            {
                return dict.Count;
            }
        }

        public virtual bool IsFixedSize
        {
            get
            {
                return ((IDictionary)dict).IsFixedSize;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return ((IDictionary)dict).IsSynchronized;
            }
        }

        public virtual ICollection<string> Keys
        {
            get
            {
                return dict.Keys;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return ((IDictionary)dict).SyncRoot;
            }
        }

        public virtual ICollection<object> Values
        {
            get
            {
                return dict.Values;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return ((IDictionary)dict).Keys;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return ((IDictionary)dict).Values;
            }
        }

        public virtual void Add(KeyValuePair<string, object> item)
        {
            dict.Add(item.Key, item.Value);
        }

        public virtual void Add(object key, object value)
        {
            ((IDictionary)dict).Add(key, value);
        }

        public virtual void Add(string key, object value)
        {
            dict.Add(key, value);
        }

        public virtual void Clear()
        {
            dict.Clear();
        }

        public virtual bool Contains(object key)
        {
            return ((IDictionary)dict).Contains(key);
        }

        public virtual bool Contains(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)dict).Contains(item);
        }

        public virtual bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }

        public virtual void CopyTo(Array array, int index)
        {
            ((IDictionary)dict).CopyTo(array, index);
        }

        public virtual void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((IDictionary<string, object>)dict).CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        public virtual void Remove(object key)
        {
            ((IDictionary)dict).Remove(key);
        }

        public virtual bool Remove(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)dict).Remove(item);
        }

        public virtual bool Remove(string key)
        {
            return dict.Remove(key);
        }

        public virtual bool TryGetValue(string key, out object value)
        {
            return dict.TryGetValue(key, out value);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)dict).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }*/
}
