namespace _Scripts.Game.UI.Screens.Provider.Factory
{
    public interface IScreenLayoutFactory
    {
        bool TryCreate(ScreenLayoutType layoutType, out ScreenLayout layout);
    }
}