using UnityEngine;
using System.Collections;
public class FlipFlop : MonoBehaviour
{
    public GameObject Flip;
    
   
    public float Delay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (Flip != null)
        {
            StartCoroutine(StartFlipFlop());
        }
    }

    IEnumerator StartFlipFlop()
    {
        while (true)
        {
            // Toggle แบบสลับสถานะ
            bool isCurrentlyActive = Flip.activeSelf;
            Flip.SetActive(!isCurrentlyActive);

            yield return new WaitForSeconds(Delay);
        }
    }
}
