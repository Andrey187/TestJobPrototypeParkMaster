using System.Collections.Generic;
using Zenject;

public class RestartCoordinator : IInitializable
{
    private readonly List<IUndo> _restartables = new List<IUndo>();
    private readonly DiContainer _container;

    public RestartCoordinator(DiContainer container)
    {
        _container = container;
        Initialize();
    }

    public void Initialize()
    {
        _restartables.AddRange(_container.ResolveAll<IUndo>());
    }

    public void RestartAll()
    {
        foreach (var restartable in _restartables)
        {
            restartable.Undo();
        }
    }
}
