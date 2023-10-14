using UnityEngine;

public class CoinController : MonoBehaviour, IUndo, IReward
{
    private ICoin coin;

    private void Start()
    {
        coin = gameObject.GetComponentInParent<ICoin>();
        CoinUndoManager.Instance.RegisterController(this);
    }

    private void DisableObject()
    {
        coin.ParticleSystem.Play();
        coin.MeshRenderer.enabled = false;
        coin.MeshCollider.enabled = false;
    }

    public void AddReward()
    {
        DisableObject();
        EventManagers.Instance.TakeCoins();
    }

    public void Undo()
    {
        Invoke(nameof(ResetComponents), 0.2f);
    }

    private void ResetComponents()
    {
        coin.MeshRenderer.enabled = true;
        coin.MeshCollider.enabled = true;
    }
}
