using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FinalDoor : MonoBehaviour
{
    public GameObject[] door;
     private bool onConsole = false;
    private bool acivated = false;
    public GameObject InterfaceUi;
    public GameObject Interract;
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
        if (onConsole && Input.GetKeyDown(KeyCode.E) && KeyItemManager.instance != null && KeyItemManager.instance.GetGemCount() < KeyItemManager.instance.gemRequired )
        {
            InterfaceUi.SetActive(true);
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !acivated)
        {
            Interract.SetActive(true);
            onConsole = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onConsole = false;
            Interract.SetActive(false);
             InterfaceUi.SetActive(false);
        }
    }
}
