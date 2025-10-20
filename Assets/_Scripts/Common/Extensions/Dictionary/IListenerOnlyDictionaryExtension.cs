using System;

namespace _Scripts.Common.Extensions
{
    public interface IListenerOnlyDictionaryExtension<in TKey, out TValue> where TKey : notnull where TValue : notnull
    {
        public void AddListener(ObservablesTypes type, TKey key, Action<TValue> callback);
        public void RemoveFromListener(ObservablesTypes type, TKey key, Action<TValue> callback);
        public void RemoveFromListener(ObservablesTypes type, Action<TValue> callback);
        public void RemoveFromListener(Action<TValue> callback);
    }
}