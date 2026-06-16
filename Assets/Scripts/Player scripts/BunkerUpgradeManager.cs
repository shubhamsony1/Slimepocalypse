using UnityEngine;
using TMPro;

public class BunkerUpgradeManager : MonoBehaviour
{
    public BunkerHealth bunker;

    [Header("Heal")]

    public int healAmount = 100;
    public int healCost = 50;

    [Header("Max HP Upgrade")]

    public int maxHealthIncrease = 100;
    public int maxHealthUpgradeCost = 100;

    [Header("UI")]

    public TMP_Text healCostText;
    public TMP_Text maxHealthCostText;


    private void Update()
    {
        maxHealthCostText.text = maxHealthUpgradeCost.ToString();
    }

    public void HealBunker()
    {
        if (bunker.CurrentHealth >= bunker.maxHealth)
        {
            Debug.Log("Bunker already full HP");
            return;
        }

        if (!CurrencyManager.Instance.SpendCoins(healCost))
        {
            Debug.Log("Not enough coins");
            return;
        }

        bunker.Heal(healAmount);
    }

    public void UpgradeMaxHealth()
    {
        if (!CurrencyManager.Instance.SpendCoins(
                maxHealthUpgradeCost))
        {
            Debug.Log("Not enough coins");
            return;
        }

        bunker.IncreaseMaxHealth(
            maxHealthIncrease
        );

        maxHealthUpgradeCost *= 2;
    }

}