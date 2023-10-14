using UnityEngine;

public class ProgressSystem : MonoBehaviour, IProgress, IUndo
{
    [SerializeField] private int _countCoins;

    public int CoinsCount => _countCoins;

    private void Start()
    {
        EventManagers.Instance.TakeCoin += AddCoins;
        SceneReloadEvent.Instance.UnsubscribeEvents.AddListener(UnsubscribeEvents);
    }

    private void UnsubscribeEvents()
    {
        EventManagers.Instance.TakeCoin -= AddCoins;
    }

    private void AddCoins()
    {
        _countCoins++;
    }

    public void Undo()
    {
        _countCoins = 0;
    }
}
