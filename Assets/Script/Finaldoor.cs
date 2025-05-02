using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public GameObject[] door;
     private bool onConsole = false;
    private bool acivated = false;
    void Update()
    {
        if (onConsole && Input.GetKeyDown(KeyCode.E) && KeyItemManager.instance != null && KeyItemManager.instance.GetGemCount() >= KeyItemManager.instance.gemRequired && !acivated)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.consoleOpen);
            foreach (GameObject obj in door)
            {
                obj.SetActive(false);
            }
             acivated = true;
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onConsole = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onConsole = false;
        }
    }
}
