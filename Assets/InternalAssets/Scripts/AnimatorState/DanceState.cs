public class DanceState : CharacterState
{
    private IPlayer _character;

    public DanceState(IPlayer character)
    {
        _character = character;
    }

    public void Enter()
    {
        _character.Animator.SetBool("Dancing", true);
    }

    public void Exit()
    {
        _character.Animator.SetBool("Dancing", false);
    }
}
