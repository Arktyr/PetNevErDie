using UnityEngine;
using Zenject;

namespace _Scripts.Common.MonobehaviourInjector
{
    public class MonoBehaviourAutoInjector : MonoBehaviour
    {
        private void Awake()
        {
            var sceneContext = FindObjectOfType<SceneContext>();
            
            var container = sceneContext != null ? sceneContext.Container : ProjectContext.Instance.Container;

            container.InjectGameObject(gameObject);
        }
    }
}