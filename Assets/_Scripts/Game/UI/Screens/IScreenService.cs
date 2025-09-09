using Cysharp.Threading.Tasks;

namespace _Scripts.Game.UI.Screens
{
    public interface IScreenService
    {
        bool TryOpenScreen<T>(ScreenOpenHideMode mode, out T screen) where T : BaseScreen;
        bool TryHideScreen<T>(ScreenOpenHideMode mode, out T screen) where T : BaseScreen;
        UniTask<bool> TryHideAndDestroyScreen<T>(ScreenOpenHideMode mode) where T : BaseScreen;
        bool TryGetScreen<T>(out T screen) where T : BaseScreen;
    }
}