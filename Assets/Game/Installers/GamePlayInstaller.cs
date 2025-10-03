using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller
{
    public ElectoNetwork electoNetwork;

    public override void InstallBindings()
    {
        Container.Bind<ElectoNetwork>().FromComponentInNewPrefab(electoNetwork).AsSingle();
    }
}