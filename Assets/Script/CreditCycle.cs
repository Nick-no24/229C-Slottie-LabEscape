using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditCycle : MonoBehaviour
{
    public Image creditImage;
    public Sprite[] creditSprites;
    public float switchDelay = 3f;

    void OnEnable()
    {
        StartCoroutine(SwitchCredits());
    }

    IEnumerator SwitchCredits()
    {
        foreach (var sprite in creditSprites)
        {
            creditImage.sprite = sprite;
            yield return new WaitForSecondsRealtime(switchDelay);
        }
    }
}