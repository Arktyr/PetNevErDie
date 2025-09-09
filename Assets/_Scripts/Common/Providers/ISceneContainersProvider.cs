using Cysharp.Threading.Tasks;

namespace _Scripts.Common.Providers
{
    public interface ISceneContainersProvider
    {
        bool TryGetContainerByType<T>(out T container) where T : BaseContainer;
        UniTask<T> TryGetContainerByTypeAsync<T>() where T : BaseContainer;
        void AddContainer(BaseContainer container);
        void RemoveContainer(BaseContainer container);
    }
}