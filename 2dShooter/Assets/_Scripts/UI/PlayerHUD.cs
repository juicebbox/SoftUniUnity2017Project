using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    public GameObject player;
    public PlayerItems items;
    public PlayerHealth health;

    public Slider healthSlider;
    public Text coinsText;
    public RectTransform deadPanel;
    public RectTransform upgradePanel;

    private GameMaster gameMaster;
    public Text upgradePanelText;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        items = player.GetComponent<PlayerItems>();
        health = player.GetComponent<PlayerHealth>();
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = health.CurrentHealth;
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        deadPanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = health.CurrentHealth;
        gameObject.SetActive(gameMaster.gameStarted);
        coinsText.text = items.Coins.ToString();

        if (health.IsDead)
        {
            deadPanel.gameObject.SetActive(true);
        }
        if(gameMaster.upgradeMenu)
        {
            if (upgradePanel != null)
            {
                upgradePanel.gameObject.SetActive(true);
            }
            if (upgradePanelText != null)
            {

                upgradePanelText.text = coinsText.text;
            }
           
        }
        else if (upgradePanel != null)
        {
            upgradePanel.gameObject.SetActive(false);
        }
    }

    public void UpgradeSword()
    {
        if (items.Coins >= 50)
        {
            items.AddCoins(-50);
            gameMaster.enemyDamageMultiplier += 0.05f;
        }
    }

    public void UpgradeArmor()
    {
        if (items.Coins >= 50)
        {
            items.AddCoins(-50);
            gameMaster.damageMultiplier -= 0.05f;
        }
    }

    public void UpgradeHealth()
    {
        if (items.Coins >= 50)
        {
            items.AddCoins(-50);
            health.UpgradeHealth(10);
        }
    }

    public void LoadNextScene()
    {
        gameMaster.LoadNextScene();
        upgradePanel.gameObject.SetActive(false);
        gameMaster.upgradeMenu = false;
    }
}
