using _Scripts.Game.UI.Screens.Animations.Base;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Game.UI.Screens.Animations.Custom
{
    
    [CreateAssetMenu(menuName = "Game/Configs/Animations/Screens/Fade", fileName = "FadeScreenAnimation")]
    public class FadeScreenAnimation : BaseScreenAnimation
    {
        [SerializeField] private int _hideValue = 0;
        [SerializeField] private int _showValue = 1;
        
        [SerializeField] private int _hideSeconds = 5;
        [SerializeField] private int _showSeconds = 5;
        
        public override async UniTask PlayShow(Transform parent)
        {
            if (parent.TryGetComponent<CanvasGroup>(out var group) == false)
            {
                Debug.LogError($"Object {parent.name}: Animation Not Found Required Component {typeof(CanvasGroup)}");
                return;
            }

            await group.DOFade(_showValue, _showSeconds)
                .From(_hideValue)
                .Play()
                .ToUniTask();
        }

        public override async UniTask PlayHide(Transform parent)
        {
            if (parent.TryGetComponent<CanvasGroup>(out var group) == false)
            {
                Debug.LogError($"Object {parent.name}: Animation Not Found Required Component {typeof(CanvasGroup)}");
                return;
            }

            await group.DOFade(_hideValue, _hideSeconds)
                .From(_showValue)
                .Play()
                .ToUniTask();
        }
    }
}