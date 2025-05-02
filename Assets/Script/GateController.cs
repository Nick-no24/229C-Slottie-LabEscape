using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject[] barricade; 
    private bool onConsole = false;
    private bool acivated = false;

    void Update()
    {
        if (onConsole && Input.GetKeyDown(KeyCode.E) && !acivated)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.consoleOpen);
            foreach (GameObject obj in barricade)
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
