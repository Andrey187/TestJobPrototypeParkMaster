using UnityEngine;

public interface IPlayer
{
    public int PlayerIndex { get; }
    public bool Dancing { get; set; }
    public bool IsMoving { get; set; }
    public Rigidbody Rb { get; }
    public Animator Animator { get; }

    public void FollowPath();

    public void ResetPosition();
}
