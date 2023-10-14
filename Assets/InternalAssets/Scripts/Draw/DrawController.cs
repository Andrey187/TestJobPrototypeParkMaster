using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrawController : MonoBehaviour, IUndo, IDrawController
{
    [Space]
    [SerializeField] protected Color paintColor;
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected GameObject ground;

    [Space]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField, Range(1f, 2f)] protected int _width;
    [SerializeField] protected internal int _playerIndex;
    protected Plane groundPlane; // "Ground" plane to limit drawing
    protected bool _onGround;
    protected bool _isDrawing = false; // Flag indicating whether a line is currently being drawn
    protected bool _isDraw = false; // Flag indicating whether drawing can be done
    protected List<Vector3> linePoints = new List<Vector3>(); // List of points for a line
    protected int _countOfTries = 1;

    public event Action PathDrawingCompleted;
    public int CountOfTries => _countOfTries;
    public bool CharacterMoving { get; set; }
    protected RaycastHit hit;
    protected void Start()
    {
        lineRenderer.positionCount = 0;
        groundPlane = new Plane(ground.transform.up, ground.transform.position);
        
        lineRenderer.startColor = lineRenderer.endColor = paintColor;
        lineRenderer.startWidth = lineRenderer.endWidth = _width;
        EventManagers.Instance.CanDraw += CanDraw;
        DrawUndoManager.Instance.RegisterController(this);
        SceneReloadEvent.Instance.UnsubscribeEvents.AddListener(UnsubscribeEvents);
    }

    protected void UnsubscribeEvents()
    {
        EventManagers.Instance.CanDraw -= CanDraw;
    }
    protected void Update()
    {
        if (_isDraw)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartDrawingLine(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                ContinueDrawingLine(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopDrawingLine();
            }
        }
    }

    protected void StartDrawingLine(Vector2 position)
    {
        //if (!CharacterMoving)
        //{
        //}
            linePoints.Clear();
            _isDrawing = true;
            _countOfTries -= 1;
            EventManagers.Instance.Drawing(_isDrawing);
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, new Vector3(position.x, position.y, 0));

    }

    protected void ContinueDrawingLine(Vector2 position)
    {
        if (_isDrawing)
        {
            // Check that the position is on the "Ground" plane
            Ray ray = Camera.main.ScreenPointToRay(position);

            int ignoreFinishLayer = LayerMask.NameToLayer("Finish");
            int layerMask = ~(1 << ignoreFinishLayer);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity,layerMask))
            {
                if ((_groundMask.value & (1 << hit.collider.gameObject.layer)) > 0)
                {
                    Vector3 hitPoint = ray.GetPoint(hit.distance) + new Vector3(0f, 0.2f, 0f);
                    linePoints.Add(hitPoint);

                    // Update the line in LineRenderer
                    lineRenderer.positionCount = linePoints.Count;
                    lineRenderer.SetPositions(linePoints.ToArray());
                }
            }
        }
    }

    protected void StopDrawingLine()
    {
        _isDraw = false;
        _isDrawing = false;

        if (linePoints.Count != 0)
        {
            EventManagers.Instance.MoveOnLine(linePoints, _playerIndex);
            EventManagers.Instance.Drawing(_isDrawing);

            PathDrawingCompleted?.Invoke();
        }
    }

    protected void CanDraw(bool canDraw, int index)
    {
        if(_playerIndex == index)
        {
            _isDraw = canDraw;
        }
    }

    public void Undo()
    {
        linePoints.Clear();
        _countOfTries = 1;
        _isDraw = false;
        lineRenderer.positionCount = 0;
    }
}
