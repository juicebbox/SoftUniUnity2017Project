using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    private int coins = 0;

    [SerializeField]
    private ArmorType armorType;
    private int armorPieces = 0;
    private int armorPiecesToUpgrade;

    private int coinsToAdd;

    public int Coins
    {
        get
        {
            return coins;
        }

    }

    public ArmorType ArmorType
    {
        get
        {
            return armorType;
        }
    }

    public int ArmorPieces
    {
        get
        {
            return armorPieces;
        }
    }

    public int ArmorPiecesToUpgrade
    {
        get
        {
            return armorPiecesToUpgrade;
        }

        set
        {
            armorPiecesToUpgrade = value;
        }
    }

    private void Start()
    {

    }


    private void Update()
    {


    }


    public void AddCoins(int amount)
    {

        coins += amount;

    }

    public void UpgradeArmorPiece(int amount)
    {
        armorPieces += amount;
        if(armorPieces == armorPiecesToUpgrade)
        {
            switch((int)armorType)
            {
                case 0:
                    armorType = ArmorType.ClothNumber;
                    break;
                case 5:
                    armorType = ArmorType.LightArmor;
                    break;
                case 10:
                    armorType = ArmorType.MediumArmor;
                    break;
                case 20:
                    armorType = ArmorType.HeavyArmor;
                    break;
                case 30:
                    // maxed out
                    break;
                default:
                    break;
                
            }

        }
    }
}
