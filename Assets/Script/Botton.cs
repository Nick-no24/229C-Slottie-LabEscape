using UnityEngine;

public class Botton : MonoBehaviour
{
    public FloatPlatFormMove[] platformScripts;
    private bool isPressed = false;
    public GameObject On;
    public GameObject Off;

    private void OnTriggerEnter2D(Collider2D other)
    {
        On.SetActive(false);
        Off.SetActive(true);
        if (!isPressed && !other.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
            foreach (var platform in platformScripts)
            {
                platform.ActivatePlatform(); 
            }
            isPressed = true;
        }
    }
}
