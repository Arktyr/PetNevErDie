using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Infrastructure.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public SceneName CurrentScene { get; private set; }

        public async UniTask LoadScene(SceneName name)
        {
            if (name == CurrentScene)
            {
                Debug.Log($"{name} Scene already loaded");
                return;
            }
            
            var scene = SceneManager.LoadSceneAsync(name.ToString());

            var uniTask = scene.ToUniTask();
            
            CurrentScene = name;

            await uniTask;
        }
    }
}