using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller // https://www.youtube.com/watch?v=XapdjT6wEkw&ab_channel=SergeyKazantsev
{
    public override void InstallBindings()
    {
        Container.Bind<GeneralData>().AsSingle();
        Container.Bind<TileMap>().AsSingle();
        Container.Bind<InventoryData>().AsSingle();
        Container.Bind<World>().AsSingle();

        Container.BindInterfacesAndSelfTo<Input>().AsSingle();
        Container.Bind<Factory>().AsSingle();
        Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
        Container.Bind<SaveLoad>().AsSingle();
        Container.Bind<TimeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MyRandom>().AsSingle();
    }
}
