using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IReward coin))
        {
            coin.AddReward();
        }
    }
}
