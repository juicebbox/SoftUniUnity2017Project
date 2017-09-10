using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    //** There are properties to give information for healt status.
    //** Each character has to implement it's own dying pattern.

    private GameMaster gameMaster;

    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    private bool isDead;
    private PlayerItems items;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    public ArmorType ArmorType
    {
        get
        {
            return items.ArmorType;
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    private void Start()
    {
        items = gameObject.GetComponent<PlayerItems>();
        currentHealth = maxHealth;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void Update()
    {
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        float armorDefence = 1f - (int)items.ArmorType / 100f;
        currentHealth -= (amount * armorDefence) * gameMaster.damageMultiplier;
    }

    public void UpgradeHealth(float amount)
    {
        maxHealth += amount;
    }
}
