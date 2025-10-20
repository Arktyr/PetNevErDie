using UnityEngine;

namespace _Scripts.Common.Extensions.Monobehaviour
{
    public static class MonoBehaviourExtensions
    {
        public static void Enable(this MonoBehaviour monoBehaviour)
        {
            if (monoBehaviour.gameObject.activeSelf)
                return;
            
            monoBehaviour.gameObject.SetActive(true);
        }
        
        public static void Disable(this MonoBehaviour monoBehaviour)
        {
            if (!monoBehaviour.gameObject.activeSelf)
                return;
            
            monoBehaviour.gameObject.SetActive(false);
        }
    }
}