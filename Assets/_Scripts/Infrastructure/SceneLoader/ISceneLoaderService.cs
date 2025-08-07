using Cysharp.Threading.Tasks;

namespace _Scripts.Infrastructure.SceneLoader
{
    public interface ISceneLoaderService
    {
        UniTask LoadScene(SceneName name);
    }
}