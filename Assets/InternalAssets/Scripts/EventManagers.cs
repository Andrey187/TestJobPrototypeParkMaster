using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManagers : MonoBehaviour
{
    private Action<List<Vector3>, int> _movePlayer;
    private Action<bool, int> _canDraw;
    private Action<bool> _isDrawing;

    private Action _takeCoin;

    private Action _finish;

    private Action<IPlayer, IDrawController> _addToDictionary;


    private static EventManagers _instance;
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        if (_instance == null)
        {
            GameObject obj = new GameObject("PlayerEventManager");
            _instance = obj.AddComponent<EventManagers>();
            DontDestroyOnLoad(obj);
        }
    }

    public static EventManagers Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EventManagers>();
            }

            return _instance;
        }
    }

    public event Action<List<Vector3>, int> MovePlayer
    {
        add { _movePlayer += value; }
        remove { _movePlayer -= value; }
    }

    public event Action<bool, int> CanDraw
    {
        add { _canDraw += value; }
        remove { _canDraw -= value; }
    }

    public event Action<bool> IsDrawing
    {
        add { _isDrawing += value; }
        remove { _isDrawing -= value; }
    }


    public event Action TakeCoin
    {
        add { _takeCoin += value; }
        remove { _takeCoin -= value; }
    }


    public event Action Finish
    {
        add { _finish += value; }
        remove { _finish -= value; }
    }

    public event Action<IPlayer, IDrawController> AddToDictionary
    {
        add { _addToDictionary += value; }
        remove { _addToDictionary -= value; }
    }


    public void MoveOnLine(List<Vector3> linePoints, int index)
    {
        _movePlayer?.Invoke(linePoints, index);
    }

    public void CanDrawLine(bool isDraw, int index)
    {
        _canDraw?.Invoke(isDraw, index);
    }

    public void Drawing(bool isDrawing)
    {
        _isDrawing?.Invoke(isDrawing);
    }


    public void TakeCoins()
    {
        _takeCoin?.Invoke();
    }


    public void CanFinish()
    {
        _finish?.Invoke();
    }

    public void Dictionary(IPlayer player, IDrawController draw)
    {
        _addToDictionary?.Invoke(player, draw);
    }

}
