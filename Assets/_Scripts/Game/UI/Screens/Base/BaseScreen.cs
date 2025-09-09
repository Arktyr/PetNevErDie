using System;
using _Scripts.Game.UI.Screens.Animations.Base;
using _Scripts.Game.UI.Screens.Provider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Game.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private BaseScreenAnimation _screenAnimation;

        public ScreenLayoutType ScreenLayoutType { get; private set; }
        
        public event Action<BaseScreen> OnShow;
        public event Action<BaseScreen> OnHide;
        public event Action<BaseScreen> OnDestroy;
        
        public void Setup(ScreenLayoutType layoutType)
        {
            ScreenLayoutType = layoutType;
        }

        public virtual async UniTask Show(ScreenOpenHideMode mode)
        {
            gameObject.SetActive(true);
            
            switch (mode)
            {
                case ScreenOpenHideMode.Animated:
                    
                    if (_screenAnimation == null)
                        break;
                    
                    await _screenAnimation.PlayShow(this.transform);
                    
                    break;
                case ScreenOpenHideMode.Immediately:
                    break;
                default:
                    Debug.LogError($"{mode} is not implemented");
                    break;
            }
            
            OnShow?.Invoke(this);
        }

        public virtual async UniTask Hide(ScreenOpenHideMode mode)
        {
            switch (mode)
            {
                case ScreenOpenHideMode.Animated:
                    await _screenAnimation.PlayHide(this.transform);
                    break;
                case ScreenOpenHideMode.Immediately:
                    break;
                default:
                    Debug.LogError($"{mode} is not implemented");
                    break;
            }
            
            gameObject.SetActive(false);
            
            OnHide?.Invoke(this);
        }

        public virtual UniTask Destroy()
        {
            Destroy(gameObject);
            OnDestroy?.Invoke(this);
            return UniTask.CompletedTask;
        }
    }
}