using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TimeManager>().AsSingle();
        Container.Bind<MyRandom>().AsSingle();

        Container.Bind<GameMapState>().AsSingle();
    }
}