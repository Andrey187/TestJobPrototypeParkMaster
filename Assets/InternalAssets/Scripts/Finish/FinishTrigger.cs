using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private int _playerIndex;
    private bool _isFinished = false;

    public bool IsFinished { get => _isFinished; set => _isFinished = value; }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IPlayer player))
        {
            if (player.PlayerIndex == _playerIndex)
            {
                _isFinished = true;
                player.IsMoving = false;
                player.Rb.velocity = Vector3.zero;
                player.Dancing = true;
                EventManagers.Instance.CanFinish();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IPlayer player))
        {
            if (player.PlayerIndex == _playerIndex)
            {
                _isFinished = false;
                player.Dancing = false;
            }
        }
    }
}
