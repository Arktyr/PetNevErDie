using UnityEngine;

namespace _Scripts.Common.Providers
{
    public class UISceneComponentsContainer : BaseContainer
    {
        [field: SerializeField] public Canvas MainCanvas { get; private set; }
    }
}