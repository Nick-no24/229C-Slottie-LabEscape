using UnityEngine;

public class GetGem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.itemPickup);
            KeyItemManager.instance.CollectGem(); 
            Destroy(gameObject);
            UIManager.instance.AddGem();
        }
    }

}
