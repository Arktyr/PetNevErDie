using System;
using System.Collections.Generic;
using _Scripts.Common.Pull.Data;
using UnityEngine;
using Zenject;

namespace _Scripts.Common.Pull
{
    public class ObjectPoolService : IObjectPoolService, IDisposable
    {
        [Inject] private readonly ObjectPoolConfig _objectPoolConfig; 
        [Inject] private readonly DiContainer _diContainer; 
            
        private readonly Dictionary<Type, ObjectPool> _objectPools = new();

        public bool TryGetObjectFromPool<T>(MonoBehaviour prefab, out T existObject) where T : MonoBehaviour
        {
            if (_objectPools.TryGetValue(typeof(T), out ObjectPool pool))
                return pool.TryGetFromPool(out existObject);
            
            ObjectPool newPool = CreateNewPool<T>(prefab);
            return newPool.TryGetFromPool(out existObject);
        }

        public bool TryRemoveObjectPool<T>() where T : MonoBehaviour
        {
            if (_objectPools.TryGetValue(typeof(T), out ObjectPool pool) == false)
                return false;
            
            pool.DestroyPool();
            _objectPools.Remove(typeof(T));
            return true;
        }

        private ObjectPool CreateNewPool<T>(MonoBehaviour prefab)
        {
            ObjectPool newPool = _diContainer.Instantiate<ObjectPool>();
            newPool.Initialize(prefab, _objectPoolConfig.BasePrewarmCount);
            newPool.Prewarm();
            
            _objectPools.Add(typeof(T), newPool);

            return newPool;
        }

        public void Dispose()
        {
            foreach (var pool in _objectPools.Values) 
                pool.DestroyPool();
            
            _objectPools.Clear();
        }
    }
}