using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Combat")]
    public float range = 6f;
    public float fireRate = 1f;

    [Header("References")]
    public Transform[] firePoints;
    
    public GameObject bulletPrefab;
    public Animator gunAnimator;

    private float fireCountdown;
    private float searchTimer;
    private Transform target;

    private void Update()
    {
        searchTimer -= Time.deltaTime;

        if (searchTimer <= 0f)
        {
            FindNearestEnemy();
            searchTimer = 0.25f;
        }

        if (target == null)
            return;

        AimAtTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindNearestEnemy()
    {
        float shortestDistance = Mathf.Infinity;

        Transform nearestEnemy = null;

        foreach (Transform enemy in EnemyRegistry.Enemies)
        {
            if (enemy == null)
                continue;

            float distance =Vector2.Distance(transform.position,enemy.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null &&shortestDistance <= range)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void AimAtTarget()
    {
        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        //Debug.Log("FIRE TRIGGER");
        
        if (gunAnimator != null)
        {
            gunAnimator.SetTrigger("Fire");
        }
        else
        {
            FireBullet();
        }
    }

    // Called by Animation Event
    public void FireBullet()
    {
        if (target == null || firePoints.Length == 0)
            return;

        foreach (Transform firePoint in firePoints)
        {
            GameObject bulletGO = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);

            BulletBase bullet = bulletGO.GetComponent<BulletBase>();

            if (bullet != null)
            {
                bullet.SetTarget(target);
            }
            else
            {
                //Debug.LogWarning(bulletGO.name + " does not contain a BulletBase component!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        if (target != null)
        {
            //Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}