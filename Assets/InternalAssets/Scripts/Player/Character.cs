using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IPlayer, IUndo
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _minDistance = 0.1f;
    [SerializeField] protected int _playerIndex;
  
    protected bool _isMoving;
    protected int _moveIndex;
    protected Rigidbody _rb;
    protected List<Vector3> _linePoints = new List<Vector3>();
    protected Animator _animator;
    protected Vector3 _startPosition;
    protected Quaternion _startRotation;

    public int PlayerIndex => _playerIndex;
    public Rigidbody Rb => _rb;
    public Animator Animator => _animator;
    public bool Dancing { get; set; }

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }

    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _startPosition = gameObject.transform.position;
        _startRotation = gameObject.transform.rotation;

        PlayerUndoManager.Instance.RegisterController(this);
        EventManagers.Instance.MovePlayer += MovePlayer;
        SceneReloadEvent.Instance.UnsubscribeEvents.AddListener(UnsubscribeEvents);
    }

    protected void UnsubscribeEvents()
    {
        EventManagers.Instance.MovePlayer -= MovePlayer;
    }

    protected void Update()
    {
        if (_isMoving && _linePoints.Count != 0)
        {

            FollowPath();
        }
    }

    public void FollowPath()
    {
        if (_isMoving && _linePoints.Count != 0)
        {
            Vector3 targetPosition = _linePoints[_moveIndex];

            Vector3 direction = (targetPosition - transform.position).normalized;

            _rb.velocity = direction * _speed;

            // Check that the target point does not coincide with the current position
            if (targetPosition != transform.position)
            {
                // Calculate the target rotation towards the next point
                Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

                // Interpolate between the current rotation and the target rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _speed);
            }

            // Check if the current point has been reached
            if (Vector3.Distance(targetPosition, transform.position) < _minDistance)
            {
                _moveIndex++;
            }

            if (_moveIndex > _linePoints.Count - 1)
            {
                _rb.velocity = Vector3.zero;
                _isMoving = false;
            }
        }
    }

    protected void MovePlayer(List<Vector3> linePoints, int index)
    {
        if (_playerIndex == index)
        {
            _isMoving = true;
            _linePoints = linePoints;
            _moveIndex = 0;
            transform.position = _linePoints[_moveIndex];
        }
    }

    public void Undo()
    {
        _linePoints.Clear();
        _rb.constraints |= RigidbodyConstraints.FreezePositionY;
        _rb.velocity = Vector3.zero;
        _isMoving = false;
        Dancing = false;
        _moveIndex = 0;
        gameObject.transform.position = _startPosition;
        gameObject.transform.rotation = _startRotation;
    }

    public void ResetPosition()
    {
        _isMoving = true;
        Dancing = false;
        _moveIndex = 0;
        gameObject.transform.position = _startPosition;
        gameObject.transform.rotation = _startRotation;
        transform.position = _linePoints[_moveIndex];
    }
}
