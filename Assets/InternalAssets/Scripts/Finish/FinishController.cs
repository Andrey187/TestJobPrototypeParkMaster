using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FinishController : MonoBehaviour, IUndo
{
    [SerializeField] private List<FinishTrigger> _finishes;
    [SerializeField] private int _countOfCoinsToWin;
    [SerializeField] private GameObject _finishPanel;

    [Space]
    [Inject] private IProgress _progressSystem;
    [Inject] private ICoinsBank _coinsBank;
    private bool _allPlayersFinished;

    private void Start()
    {
        EventManagers.Instance.Finish += Finish;
        SceneReloadEvent.Instance.UnsubscribeEvents.AddListener(UnsubscribeEvents);
    }

    private void UnsubscribeEvents()
    {
        EventManagers.Instance.Finish -= Finish;
    }

    private void Finish()
    {
        foreach (FinishTrigger finishTrigger in _finishes)
        {
            if (!finishTrigger.IsFinished)
            {
                _allPlayersFinished = false;
                break;
            }
            else
            {
                _allPlayersFinished = true;
            }
        }

        if (_allPlayersFinished && _progressSystem.CoinsCount == _countOfCoinsToWin)
        {
            _coinsBank.AddTokens(_progressSystem.CoinsCount);
            SceneReloadEvent.Instance.OnUnsubscribeEvents();
            Invoke(nameof(FinishWindow), 3f);
        }
    }

    private void FinishWindow()
    {
        _finishPanel.SetActive(true);
    }

    public void Undo()
    {
        foreach (FinishTrigger finishTrigger in _finishes)
        {
            finishTrigger.IsFinished = false;
        }
    }
}
