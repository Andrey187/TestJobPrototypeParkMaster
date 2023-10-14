public class CoinUndoManager : UndoManager<CoinController>
{
    public static CoinUndoManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
