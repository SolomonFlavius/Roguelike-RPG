using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerManager : MonoBehaviour
{
    [SerializeField]
    Gun theGun;
    [SerializeField]
    int startingHealth = 5;
    [SerializeField]
    int coins = 0;

    public static OldPlayerManager instance;
    
    private void Awake() 
    {
        instance = this;
    }

    public void SetTheGun(Gun gun)
    {
        theGun = gun;
    }

    public void SetStartingHealth(int health)
    {
        startingHealth = health;
    }

    public void SetStartingCoins()
    {
        coins = 0;
    }

    public void ResetPlayerManager()
    {
        PlayerHealthController.instance.ResetAnimationsAndColor();
        PlayerHealthController.instance.maxHealth = startingHealth;
        PlayerHealthController.instance.HealPlayer(startingHealth);
        PlayerController.instance.theGun = theGun;
        LevelManager.instance.currentCoins = 0;
    }
}
