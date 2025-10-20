using System;

namespace _Scripts.Common.Extensions
{
    [Serializable]
    public class DictionaryExtensionListener<Tkey, TValue> 
    {
        public Tkey Key;
        public Action<TValue> Value;
        public ObservablesTypes ObservablesType;
    }
}