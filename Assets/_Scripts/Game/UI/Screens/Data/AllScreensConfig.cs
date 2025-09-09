using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Game.UI.Screens.Provider
{
    [CreateAssetMenu(menuName = "Game/Configs/UI/Screens/AllScreens", fileName = "AllScreensConfig")]
    public class AllScreensConfig : ScriptableObject
    {
        [field: SerializeField] public List<ScreenConfig> ScreenConfigs { get; private set; }
    }
}