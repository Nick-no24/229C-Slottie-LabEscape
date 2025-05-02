using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public enum Phase { Phase1, Phase2,  }
    public Phase currentPhase = Phase.Phase1;

    public float maxHP = 100f;
    private float currentHP;
    public GameObject bossHpUi;
    public Slider bossHealthBar;
    public TMP_Text bossHpText;
    public Animator anim;
    
    public GameObject fireballPrefab;
    public Transform[] fireSpawnPoints;

    public float phase2Threshold = 50;
    

    private bool isActivated = false;

    private bool isInAction = false;

    void Start()
    {
        currentHP = maxHP;

        if (bossHealthBar != null)
        {
            bossHealthBar.maxValue = maxHP;
            bossHealthBar.value = currentHP;
        }

        UpdateHpUI();
    }

    void Update()
    {
        Debug.Log(currentHP);
        if (!isActivated) return;

        HandlePhases();

        if (!isInAction)
        {
            StartCoroutine(DoAttackPattern());
        }
    }

    void HandlePhases()
    {
        if (currentHP <= phase2Threshold && currentPhase != Phase.Phase2)
        {
            currentPhase = Phase.Phase2;
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Max(currentHP, 0);

        UpdateHpUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }
    void UpdateHpUI()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.value = currentHP;
        }

        if (bossHpText != null)
        {
            bossHpText.text = $"FireLizard HP : {Mathf.CeilToInt(currentHP)} / {Mathf.CeilToInt(maxHP)}";
        }
    }

    IEnumerator DoAttackPattern()
    {
        isInAction = true;

        switch (currentPhase)
        {
            case Phase.Phase1:
                for (int i = 0; i < 3; i++)
                {
                    anim.SetTrigger("FireBreath");
                    int randIndex = Random.Range(0, fireSpawnPoints.Length);
                    SpawnFireball(fireSpawnPoints[randIndex]);
                    yield return new WaitForSeconds(0.75f);
                }
                break;

            case Phase.Phase2:
                anim.SetTrigger("FireBreath");
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(FireballBurst());
                yield return new WaitForSeconds(5f);
                break;
        }

        isInAction = false;
    }
    IEnumerator FireballBurst()
    {
        for (int i = 0; i < 6; i++)
        {
            int randIndex = Random.Range(0, fireSpawnPoints.Length);
            int burstCount = Random.Range(2, 4); 
            for (int j = 0; j < burstCount; j++)
            {
                anim.SetTrigger("FireBreath");
                SpawnFireball(fireSpawnPoints[randIndex]);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }


    void SpawnFireball(Transform point)
    {
        GameObject fireball = Instantiate(fireballPrefab, point.position, Quaternion.identity);
        Vector2 direction = (Vector2)(point.right);
        fireball.GetComponent<FireBall>().SetDirection(direction);
    }




    void Die()
    {
        bossHpUi.SetActive(false);
        anim.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }
    public void ActivateBoss()
    {
        isActivated = true;
        bossHpUi.SetActive(true);
       
    }
}