using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<RestartCoordinator>().AsSingle();

        Container.Bind<IProgress>().To<ProgressSystem>().FromComponentInHierarchy().AsSingle().Lazy();
        Container.Bind<ICoinsBank>().To<CoinsBank>().FromComponentInHierarchy().AsSingle().Lazy();

        Container.Bind<IUndo>().To<ProgressSystem>().FromComponentInHierarchy().AsTransient();
        Container.Bind<IUndo>().To<FinishController>().FromComponentInHierarchy().AsTransient();
        Container.Bind<IUndo>().To<MovementSequenceManager>().FromComponentInHierarchy().AsTransient();

        Container.Bind<IUndo>().To<DrawEnableManager>().FromComponentInHierarchy().AsTransient();
        Container.Bind<IUndo>().To<CoinUndoManager>().FromComponentInHierarchy().AsTransient();
        Container.Bind<IUndo>().To<PlayerUndoManager>().FromComponentInHierarchy().AsTransient();
        Container.Bind<IUndo>().To<DrawUndoManager>().FromComponentInHierarchy().AsTransient();

    }
}
