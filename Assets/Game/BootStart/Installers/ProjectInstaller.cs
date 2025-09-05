using Zenject;

public class ProjectInstaller : MonoInstaller // https://www.youtube.com/watch?v=XapdjT6wEkw&ab_channel=SergeyKazantsev
{
    public override void InstallBindings()
    {
        Container.Bind<GameData>().AsSingle();

        Container.BindInterfacesAndSelfTo<Input>().AsSingle();
        Container.BindInterfacesAndSelfTo<Factory>().AsSingle(); // пусть пока тут полежит, аудито скрипт в меню жалуется
    }
}
