using System.Collections.Generic;
using UnityEngine;

public abstract class UndoManager<T> : MonoBehaviour, IUndo where T: Component, IUndo
{
    private List<T> _controllers = new List<T>();

    private void Start()
    {
        SceneReloadEvent.Instance.UnsubscribeEvents.AddListener(UnsubscribeEvents);
    }

    private void UnsubscribeEvents()
    {
        _controllers.Clear();
    }

    public void RegisterController(T controller)
    {
        _controllers.Add(controller);
    }

    public void Undo()
    {
        foreach (var controller in _controllers)
        {
            controller.Undo();
        }
    }
}
