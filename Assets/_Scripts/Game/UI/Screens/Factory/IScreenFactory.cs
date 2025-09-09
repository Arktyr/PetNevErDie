namespace _Scripts.Game.UI.Screens.Factory
{
    public interface IScreenFactory
    {
        bool TryCreate<T>(out T screen) where T : BaseScreen;
    }
}