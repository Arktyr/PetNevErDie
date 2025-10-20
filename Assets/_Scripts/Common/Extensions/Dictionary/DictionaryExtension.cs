using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Common.Extensions.Dictionary
{
    public class DictionaryExtension<TKey, TValue> :
        IListenerOnlyDictionaryExtension<TKey, TValue> 
        where TKey : notnull where TValue : notnull
    {
        private readonly Dictionary<TKey, TValue> _commonDictionary = new();
        private readonly List<DictionaryExtensionListener<TKey, TValue>> _listeners = new();
        
        public TValue this[TKey key]
        {
            get
            {
                if (_commonDictionary.TryGetValue(key, out var value))
                    return value;
                
                Debug.LogError($"Key {key} not found in dictionary");
                return value;
            }

            set
            {
                if (!_commonDictionary.ContainsKey(key))
                {
                    Debug.LogError($"{key.GetType()} Key Doesn't exists");
                    return;
                }
                
                _commonDictionary[key] = value;
                NotifyListenersAboutChanges(ObservablesTypes.ChangeValueObserver, key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (_commonDictionary.TryAdd(key, value))
            {
                NotifyListenersAboutChanges(ObservablesTypes.AddKeyObserver, key, value);
                return;
            }
            
            Debug.LogError($"{key.GetType()} Key already exists");
        }

        public List<TKey> GetAllKeysAsList() => 
            _commonDictionary.Keys.ToList();

        public List<TValue> GetAllValuesAsList() =>
            _commonDictionary.Values.ToList();

        public bool TryGetValue(TKey key, out TValue value) => 
            _commonDictionary.TryGetValue(key, out value);

        public bool ContainsKey(TKey key)
            => _commonDictionary.ContainsKey(key);
        
        public bool ContainsValue(TValue value)
            => _commonDictionary.ContainsValue(value);

        public void Remove(TKey key)
        {
            if (!_commonDictionary.Remove(key, out var value))
            {
                Debug.LogError($"{key.GetType()} Key Doesn't exists");
                return;
            }

            NotifyListenersAboutChanges(ObservablesTypes.RemoveKeyObserver, key, value);
        }

        public void AddListener(ObservablesTypes type, TKey key, Action<TValue> callback)
        {
            var newListener = new DictionaryExtensionListener<TKey, TValue>
            {
                Key = key,
                Value = callback,
                ObservablesType = type
            };
            
            _listeners.Add(newListener);
        }

        public void RemoveFromListener(ObservablesTypes type, TKey key, Action<TValue> callback) => 
            _listeners.RemoveAll(x =>
                x.ObservablesType == type && x.Value == callback && Equals(key, x.Key));
        
        public void RemoveFromListener(ObservablesTypes type, Action<TValue> callback) => 
            _listeners.RemoveAll(x =>
                x.ObservablesType == type && x.Value == callback);

        public void RemoveFromListener(Action<TValue> callback) => 
            _listeners.RemoveAll(x => x.Value == callback);

        private void NotifyListenersAboutChanges(ObservablesTypes type, TKey key, TValue value)
        {
            var cachedListeners =
                _listeners.Where(x => x.ObservablesType == type && Equals(x.Key, key)).ToList();

            foreach (var listener in cachedListeners) 
                listener?.Value.Invoke(value);
        }
    }
}