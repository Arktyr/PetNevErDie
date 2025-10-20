using System.Threading;
using _Scripts.Game.UI.Common.Base;
using Cysharp.Threading.Tasks;
using KingdomCum.FTUE.AddressablesControl;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.UI.Common.Image
{
    public class AddressableImage : UnityEngine.UI.Image
    {
        [SerializeField] private string _spriteName;
        [SerializeField] private LifeTimeMode _lifeName;
        
        [Inject] private BaseUIConfig _config;
        [Inject] private IAddressableLoader _loader;

        private CancellationTokenSource _cts = new();

        private string _currentSprite;
        
        protected override void OnEnable()
        {
            ChangeSprite(_spriteName);
            base.OnEnable();
        }

        public void ChangeSprite(string name)
        {
            sprite = _config.BaseSprite;

            if (!string.IsNullOrEmpty(_currentSprite))
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();
                _loader.UnloadAsset(_spriteName);
            }
            
            _currentSprite = name;
            
            LoadImage(name)
                .AttachExternalCancellation(_cts.Token)
                .Forget();
        }

        private async UniTask LoadImage(string spriteName)
        {
            var load = await _loader.LoadAsset<Sprite>(spriteName, _lifeName)
                .AttachExternalCancellation(_cts.Token);

            sprite = load;
        }

        protected override void OnDestroy()
        {
            _cts?.Dispose();
            _loader.UnloadAsset(name);
            base.OnDestroy();
        }
    }
}