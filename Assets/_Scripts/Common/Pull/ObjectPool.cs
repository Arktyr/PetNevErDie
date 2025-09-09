using System;
using System.Collections.Generic;
using _Scripts.Common.Providers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Scripts.Common.Pull
{
    public class ObjectPool : IDisposable 
    {
        [Inject] private DiContainer _container;
        
        private readonly Queue<MonoBehaviour> _currentAvailableObjects = new();

        private MonoBehaviour _prefab;
        
        private bool _isInitialized;
        private int _prewarmCount;

        public void Initialize(MonoBehaviour prefab, 
            int prewarmCount)
        {
            _prefab = prefab;
            _prewarmCount = prewarmCount;
            _isInitialized = true;
        }

        public void Prewarm()
        {
            for (var i = 0; i < _prewarmCount; i++)
            {
                MonoBehaviour availableObject = _container.InstantiatePrefabForComponent<MonoBehaviour>(_prefab);
                availableObject.gameObject.SetActive(false);
                _currentAvailableObjects.Enqueue(availableObject);
            }
        }

        public bool TryGetFromPool<T>(out T existedObject) where T : MonoBehaviour
        {
            existedObject = null;
            
            if (_isInitialized == false)
            {
                Debug.LogError($"{typeof(T)} : Object pool not initialized for this object");
                return false;
            }

            if (_currentAvailableObjects.TryDequeue(out var availableObject))
            {
                availableObject.gameObject.SetActive(true);
                existedObject = availableObject as T;
                return true;
            }
            
            availableObject = _container.InstantiatePrefabForComponent<T>(_prefab);
            availableObject.gameObject.SetActive(true);
            existedObject = availableObject as T;
            return true;
        }

        public void ReturnToPool<T>(T returnObject) where T : MonoBehaviour
        {
            if (_isInitialized == false)
            {
                Debug.LogError($"{typeof(T)}: Object pool not initialized for this object");
                return;
            }
            
            returnObject.gameObject.SetActive(false);
            _currentAvailableObjects.Enqueue(returnObject);
        }

        public void DestroyPool()
        {
            _isInitialized = false;
            Dispose();
        }

        public void Dispose()
        {
            foreach (var obj in _currentAvailableObjects) 
                Object.Destroy(obj.gameObject);
            
            _currentAvailableObjects.Clear();
        }
    }
}