using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    private CharacterHealth characterHealth;

    [SerializeField]
    private GameObject enemy;

	// Use this for initialization
	void Start ()
    {
        characterHealth = enemy.GetComponent<CharacterHealth>();

        healthSlider.maxValue = characterHealth.MaxHealth;
        healthSlider.value = characterHealth.CurrentHealth;
        healthSlider.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(characterHealth.CurrentHealth < characterHealth.MaxHealth)
        {
            healthSlider.gameObject.SetActive(true);
        }
        healthSlider.value = characterHealth.CurrentHealth;
    }
}
