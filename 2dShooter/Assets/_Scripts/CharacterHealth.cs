using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{


    //** There are properties to give information for healt status.
    //** Each character has to implement it's own dying pattern.

    [SerializeField]
    private float health;

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

    public float Health
    {
        get
        {
            return health;
        }
    }


    void Update ()
    {
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            isDead = true;
        }
    }

    public void TakeDamage(float amount)
    {
        float armorDefence = 1f - (int)armorType / 100f;
        health -= amount * armorDefence;
    }
}
