using UnityEngine;

public class BulletAOE : BulletBase
{
    [Header("Bullet")]
    public float speed = 1f;
    public float damage = 50f;

    [Header("AOE")]
    public float splashRadius = 1.5f;
    public float explosionScale = 0.5f;

    [Header("Effects")]
    public GameObject explosionPrefab;

    private Transform target;

    public override void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        Explode();
    }

    private void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, splashRadius);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            if (!enemyCollider.CompareTag("Enemy"))
                continue;

            EnemyHealth enemy = enemyCollider.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab,transform.position,Quaternion.identity);

            explosion.transform.localScale = Vector3.one * splashRadius * explosionScale;

            Destroy(explosion, 0.2f);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashRadius);
    }
}