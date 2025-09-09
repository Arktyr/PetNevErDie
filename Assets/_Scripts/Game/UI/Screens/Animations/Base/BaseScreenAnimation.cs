using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Game.UI.Screens.Animations.Base
{
    public abstract class BaseScreenAnimation : ScriptableObject
    {
        public abstract UniTask PlayShow(Transform parent);
        public abstract UniTask PlayHide(Transform parent);
    }
}