using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TimeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MyRandom>().AsSingle();
    }
}