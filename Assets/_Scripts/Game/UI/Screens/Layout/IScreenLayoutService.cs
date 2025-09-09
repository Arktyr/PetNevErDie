namespace _Scripts.Game.UI.Screens.Provider
{
    public interface IScreenLayoutService
    {
        bool TryGetLayout(ScreenLayoutType layerConfig, out ScreenLayout layout);
        void AddScreenToLayoutByType(ScreenLayoutType layoutType, BaseScreen baseScreen);
        void RemoveFromLayoutByType(BaseScreen baseScreen);
    }
}