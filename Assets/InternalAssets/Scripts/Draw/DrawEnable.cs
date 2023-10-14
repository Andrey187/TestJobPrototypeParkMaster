using UnityEngine;

public class DrawEnable : MonoBehaviour, IUndo
{
    [SerializeField] private DrawController DrawController;
    private IPlayer _player;
    private IDrawController _drawController;
    private bool _isDrawing;
    private bool _canDraw;

    private void Start()
    {
        _player = gameObject.GetComponent<IPlayer>();
        _drawController = DrawController.GetComponent<IDrawController>();
        EventManagers.Instance.IsDrawing += Drawing;
        DrawEnableManager.Instance.RegisterController(this);
        SceneReloadEvent.Instance.UnsubscribeEvents.AddListener(UnsubscribeEvents);
    }

    private void UnsubscribeEvents()
    {
        EventManagers.Instance.IsDrawing -= Drawing;
    }

    private void Update()
    {
        if (_player.Rb.velocity.magnitude > 0)
        {
            _canDraw = false;
            EventManagers.Instance.CanDrawLine(_canDraw, _player.PlayerIndex);
        }

    }

    private void OnMouseEnter()
    {
        if (DrawController.CountOfTries > 0)
        {
            _canDraw = true;
            EventManagers.Instance.CanDrawLine(_canDraw, _player.PlayerIndex);
            EventManagers.Instance.Dictionary(_player, _drawController);
        }
    }
    private void OnMouseExit()
    {
        if (!_isDrawing) // Disable drawing only if we don't continue drawing
        {
            _canDraw = false;
            EventManagers.Instance.CanDrawLine(_canDraw, _player.PlayerIndex);
        }
    }

    private void Drawing(bool isDrawing)
    {
        _isDrawing = isDrawing;
    }

    public void Undo()
    {
        _canDraw = false;
    }
}
