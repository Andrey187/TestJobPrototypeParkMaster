using UnityEngine;

public class Smash : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private int _smashForce;
    private IPlayer _player;

    private void Start()
    {
        _player = GetComponent<IPlayer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((_collisionMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            BreakDown();
        }
    }

    private void BreakDown()
    {
        _player.IsMoving = false;
        _player.Rb.velocity = Vector3.zero;
        _player.Rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

        _player.Rb.AddForce((_smashForce * Vector3.up) + (-transform.forward * _smashForce / 2),ForceMode.VelocityChange);
       
    }
}
