public class RunState : CharacterState
{
    private IPlayer _character;

    public RunState(IPlayer character)
    {
        _character = character;
    }

    public void Enter()
    {
        _character.Animator.SetFloat("MoveSpeed", _character.Rb.velocity.magnitude);
    }

    public void Exit()
    {
        _character.Animator.SetFloat("MoveSpeed", 0);
    }
}
