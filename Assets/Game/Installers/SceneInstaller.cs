using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Inject] private Input input; // просто что бы ProjectInstaller сработал ДО SceneInstaller

    public override void InstallBindings()
    {
        //Container.Bind<EventBus>().AsSingle();
        //Container.Bind<ObjectPool>().AsTransient();
    }
}
