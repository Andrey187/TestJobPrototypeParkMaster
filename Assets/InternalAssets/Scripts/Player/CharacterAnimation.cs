using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private IPlayer _player;
    private CharacterState _currentState;

    private void Start()
    {
        _player = GetComponent<IPlayer>();
    }

    private void Update()
    {
        if (_player.Rb.velocity.magnitude >= 0)
        {
            SetState(new RunState(_player));
        }

        if (_currentState is RunState && _player.Dancing)
        {
            SetState(new DanceState(_player));
        }
    }

    public void SetState(CharacterState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();
    }
}
