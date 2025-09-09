using UnityEngine;

namespace _Scripts.Common.Pull.Data
{
    [CreateAssetMenu(menuName = "Game/Common/ObjectPool", fileName = "ObjectPoolConfig")]
    public class ObjectPoolConfig :  ScriptableObject
    {
        [field: SerializeField] public int BasePrewarmCount { get; private set; }
    }
}