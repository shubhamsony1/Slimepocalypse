using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public int coinReward = 10;

    private float currentHealth;
    private EnemyMovement enemyMovement;
    private bool isDead = false;
    public bool isBigSlime = false;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;

            if (isBigSlime)
            {
                GameStats.Instance.BigKilled();
            }
            else
            {
                GameStats.Instance.NormalKilled();
            }

            CurrencyManager.Instance.AddCoins(
                coinReward
            );

            if (enemyMovement != null)
            {
                enemyMovement.Die();
            }
        }
    }
}