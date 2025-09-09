using System;
using _Scripts.Common.MonobehaviourInjector;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Common.Providers
{
    [RequireComponent(typeof(MonoBehaviourAutoInjector))]
    public class BaseContainer : SerializedMonoBehaviour
    {
        [Inject] private ISceneContainersProvider _sceneContainersProvider;

        private void Awake() => 
            _sceneContainersProvider.AddContainer(this);

        private void OnDestroy() => 
            _sceneContainersProvider.RemoveContainer(this);
    }
}