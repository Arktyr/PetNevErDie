using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Game.UI.Screens.Provider
{
    [CreateAssetMenu(menuName = "Game/Configs/UI/Layouts/AllScreenLayout", fileName = "AllScreenLayoutConfigs")]
    public class AllScreenLayoutConfigs : ScriptableObject
    {
        [field: SerializeField] public ScreenLayout Prefab {get; private set;}
        [field: SerializeField] public List<ScreenLayoutConfig> ScreenLayoutConfigs { get; private set; }
    }
}