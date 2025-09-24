using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Inject] private Input _input;

    public override void InstallBindings()
    {

    }
}

// скрипт существует что бы форсировать загрузку проджект инсталлера до начала сцен инсталлеров

// потому что зенджект сука и даже если у меня в принципе нет сцен инсталлера, он всё равно пропускает проджект инсталлер
