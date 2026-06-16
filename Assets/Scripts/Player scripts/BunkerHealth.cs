using UnityEngine;

public class BunkerHealth : MonoBehaviour
{
    public float maxHealth = 1000f;

    public float CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("Bunker took damage: " + damage);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            GameOverManager.Instance.ShowGameOver();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth =
            Mathf.Min(
                CurrentHealth + amount,
                maxHealth
            );
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;

        CurrentHealth += amount;
    }

}