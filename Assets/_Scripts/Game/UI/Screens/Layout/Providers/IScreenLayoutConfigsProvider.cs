namespace _Scripts.Game.UI.Screens.Provider.Providers
{
    public interface IScreenLayoutConfigsProvider
    {
        bool TryGetLayoutConfigByType(ScreenLayoutType layoutType, out ScreenLayoutConfig config);
        ScreenLayout GetScreenLayoutBasePrefab();
    }
}