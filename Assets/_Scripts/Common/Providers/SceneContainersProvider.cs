using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Common.Providers
{
    public class SceneContainersProvider : ISceneContainersProvider
    {
        private readonly List<BaseContainer> _containers = new();

        public void AddContainer(BaseContainer container)
        {
            if (_containers.Contains(container))
                return;
            
            _containers.Add(container);
        }

        public void RemoveContainer(BaseContainer container)
        {
            if (_containers.Contains(container) == false)
                return;
            
            _containers.Add(container);
        }
        
        public bool TryGetContainerByType<T>(out T container) where T : BaseContainer
        {
            BaseContainer findContainer = _containers.Find(x => x is T);
            
            container = findContainer as T;
            
            return findContainer != null;
        }
        
        public async UniTask<T> TryGetContainerByTypeAsync<T>() where T : BaseContainer
        {
            await UniTask.WaitUntil(() => _containers.Exists(x => x is T));
            
            BaseContainer findContainer = _containers.Find(x => x is T);

            return findContainer as T;
        }
    }
}