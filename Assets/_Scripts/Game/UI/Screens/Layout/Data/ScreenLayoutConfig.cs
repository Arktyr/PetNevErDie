using UnityEngine;

namespace _Scripts.Game.UI.Screens.Provider
{
    [CreateAssetMenu(menuName = "Game/Configs/UI/Layouts/ScreenLayer", fileName = "ScreenLayoutConfig")]
    public class ScreenLayoutConfig : ScriptableObject
    {
        [field: SerializeField] public ScreenLayoutType LayoutType { get; private set; }
        [field: SerializeField] public int CanvasSortingOrder { get; private set; }
    }
}