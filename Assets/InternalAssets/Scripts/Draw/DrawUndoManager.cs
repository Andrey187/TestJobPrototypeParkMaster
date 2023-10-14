public class DrawUndoManager : UndoManager<DrawController>
{
    public static DrawUndoManager Instance { get; private set; }

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
