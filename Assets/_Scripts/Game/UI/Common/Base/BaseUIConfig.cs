using UnityEngine;

namespace _Scripts.Game.UI.Common.Base
{
    [CreateAssetMenu(menuName = "Game/UI/BaseUI", fileName = "BaseUIConfig")]
    public class BaseUIConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite BaseSprite { get; set; }
    }
}