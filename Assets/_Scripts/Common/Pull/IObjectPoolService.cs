using UnityEngine;

namespace _Scripts.Common.Pull
{
    public interface IObjectPoolService
    {
        bool TryGetObjectFromPool<T>(MonoBehaviour prefab, out T existObject) where T : MonoBehaviour;
        bool TryRemoveObjectPool<T>() where T : MonoBehaviour;
    }
}