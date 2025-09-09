using _Scripts.Game.UI.Screens.Animations.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Game.UI.Screens.Animations.Custom
{
    public class TestScreenAnimation : BaseScreenAnimation
    {
        [SerializeField] private int _hideSeconds = 5;
        [SerializeField] private int _showSeconds = 5;
        
        public override UniTask PlayShow(Transform parent)
        {
            throw new System.NotImplementedException();
        }

        public override UniTask PlayHide(Transform parent)
        {
            throw new System.NotImplementedException();
        }
    }
}