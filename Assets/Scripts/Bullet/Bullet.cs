using UnityEngine;

public class Bullet : BulletBase
{
    public float speed = 1f;
    public float damage = 25f;
    public float explosionScale = 0.5f;

    private Transform target;

    public GameObject explosionPrefab;

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

        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab,transform.position,Quaternion.identity);

            explosion.transform.localScale = Vector3.one * explosionScale;

            Destroy(explosion, 0.2f);
        }

        Destroy(gameObject);
    }
}