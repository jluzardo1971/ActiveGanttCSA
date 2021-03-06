using System;
using System.Collections;

namespace MSP2010
{
    internal class clsDictionary : DictionaryBase
    {
        int mp_lKey = 1;

        public clsDictionary()
        {
        }

        public void Add(int Value, String Key)
        {
            this.Dictionary.Add(Key, Value);
        }

        public void Add(String Value)
        {
            this.Dictionary.Add(mp_lKey, Value);
            mp_lKey = mp_lKey + 1;
        }

        public bool Contains(string Key)
        {
            return this.Dictionary.Contains(Key);
        }

        public String StrItem(int Index)
        {
            return (String)this.Dictionary[Index];
        }

        public int this[string Key]
        {
            get { return (int)this.Dictionary[Key]; }

            set { this.Dictionary[Key] = value; }
        }

        public System.Collections.ICollection Keys
        {
            get { return this.Dictionary.Keys; }
        }
    }
}
