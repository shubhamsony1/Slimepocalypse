using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TMP_Text coinText;

    private void Update()
    {
        coinText.text =
            "Coins: " +
            CurrencyManager.Instance.Coins;
    }
}