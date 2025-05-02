using UnityEngine;

public class Liftconsole : MonoBehaviour
{
    public FloatPlatFormMove[] platformScripts;
    private bool onConsole = false;
    private bool acivated = false;
    void Update()
    {
        if (onConsole && Input.GetKeyDown(KeyCode.E))
        { 
            AudioManager.Instance.PlaySFX(AudioManager.Instance.consoleOpen);
            foreach (var platform in platformScripts)
            {
                platform.ActivatePlatform();
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

