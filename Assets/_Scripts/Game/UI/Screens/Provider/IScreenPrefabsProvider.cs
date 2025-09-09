namespace _Scripts.Game.UI.Screens.Provider
{
    public interface IScreenPrefabsProvider
    {
        bool TryGetConfig<T>(out ScreenConfig config) where T : BaseScreen;
    }
}