using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.BindInterfacesAndSelfTo<Factory>().AsSingle();
        Container.Bind<TimeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MyRandom>().AsSingle();
    }
}