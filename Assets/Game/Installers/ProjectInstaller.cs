using Zenject;

public class ProjectInstaller : MonoInstaller // https://www.youtube.com/watch?v=XapdjT6wEkw&ab_channel=SergeyKazantsev
{
    public override void InstallBindings()
    {
        Container.Bind<GameData>().AsSingle();

        Container.BindInterfacesAndSelfTo<Input>().AsSingle();
        Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
        Container.Bind<SaveLoad>().AsSingle();
        Container.Bind<TimeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MyRandom>().AsSingle();
    }
}
