using System;
using UnityEngine;
using Zenject;

public class ScenesInstaller : MonoInstaller // этот класс должен находится на всех сценах, таким образом в каждой мы будет регестрировать то что тут
{
    [SerializeField] private SceneLoader sceneLoader;

    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
    }
}