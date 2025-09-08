using Zenject;

public class SceneInstaller : MonoInstaller // класс для вещей что есть на каждой сцене
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Factory>().AsSingle(); // очищаем фабрику при переходе между сценами через Dispose
    }
}