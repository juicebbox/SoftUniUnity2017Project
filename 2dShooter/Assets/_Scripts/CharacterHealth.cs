using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{


    //** There are properties to give information for healt status.
    //** Each character has to implement it's own dying pattern.

    private GameMaster gameMaster;

    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    [SerializeField]
    private ArmorType armorType;

    private bool isDead;

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
            return armorType;
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
        currentHealth = maxHealth;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void Update ()
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

    public void TakeDamage(float amount)
    {
        float armorDefence = 1f - (int)armorType / 100f;
        currentHealth -= (amount * armorDefence) * gameMaster.enemyDamageMultiplier;
    }
}
