using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BunkerHealthUI : MonoBehaviour
{
    public BunkerHealth bunker;

    public Slider healthSlider;

    public TMP_Text healthText;

    private void Start()
    {
        healthSlider.minValue = 0;
    }

    private void Update()
    {
        healthSlider.maxValue =
            bunker.maxHealth;

        healthSlider.value =
            bunker.CurrentHealth;

        healthText.text =
            Mathf.CeilToInt(
                bunker.CurrentHealth
            )
            + " / " +
            Mathf.CeilToInt(
                bunker.maxHealth
            );
    }
}