using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float damageInvincLength = 1f;
    private float invincCount;

    private float hurtCount = 0.3f;

    private bool gotHit = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;

        ResetHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if(invincCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(1f, 1f, 1f, 1f);

            }
        }
        if(gotHit)
        {
            PlayerController.instance.bodySR.color = new Color(1f, 0f, 0f, 1f);
            hurtCount -= Time.deltaTime;
            if(hurtCount <= 0f)
            {
                hurtCount = 0.3f;
                PlayerController.instance.bodySR.color = new Color(1f, 1f, 1f, 1f);
                gotHit = false;
            }
        }
    }

    public bool IsFullHP()
    {
        if(currentHealth == maxHealth)
            return true;
        return false;
    }

    private void ResetHealthUI()
    {
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void InitializePlayerHealth()
    {
        maxHealth = 5;//trb schimbat mai tz
        currentHealth = maxHealth;
    }

    public void DamagePlayer()
    {
        
        if (invincCount <= 0)
        {
            gotHit = true;
            AudioManager.instance.PlaySFX(11);
            currentHealth--;

            invincCount = damageInvincLength;

            PlayerController.instance.bodySR.color = new Color(1f, 1f, 1f, .5f);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);

                AudioManager.instance.PlayGameOver();
                AudioManager.instance.PlaySFX(8);
            }


            ResetHealthUI();
        }
    }

    public void MakeInvincible(float length)
    {
        invincCount = length;
        PlayerController.instance.bodySR.color = new Color(1f, 1f, 1f, .5f);

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        ResetHealthUI();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;

        ResetHealthUI();
    }

    public void ResetAnimationsAndColor()
    {
        PlayerController.instance.bodySR.color = new Color(1f, 1f, 1f, 1f);
        PlayerController.instance.anim.SetTrigger("reset");
    }
}
