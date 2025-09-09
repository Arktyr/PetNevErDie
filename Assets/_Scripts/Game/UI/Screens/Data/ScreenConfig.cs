using UnityEngine;

namespace _Scripts.Game.UI.Screens.Provider
{
    [CreateAssetMenu(menuName = "Game/Configs/UI/Screens/Screen", fileName = "ScreenConfig")]
    public class ScreenConfig : ScriptableObject
    {
        [field: SerializeField] public BaseScreen Prefab {get; private set;}
        [field: SerializeField] public ScreenLayoutType LayoutType { get; private set; }
    }
}