using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller // https://www.youtube.com/watch?v=XapdjT6wEkw&ab_channel=SergeyKazantsev
{
    public override void InstallBindings()
    {
        Container.Bind<Input>().AsSingle();
        Container.Bind<AudioManager>().AsSingle();

        Container.Bind<GameState>().AsSingle();
    }
}
