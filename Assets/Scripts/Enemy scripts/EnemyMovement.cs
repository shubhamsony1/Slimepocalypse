using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Combat")]
    public float attackRange = 1.2f;
    public float attackDamage = 10f;
    public float attackRate = 1f;

    [Header("References")]
    public Animator animator;

    [Header("Target Selection")]
    public bool targetTurrets = true;
    public bool targetBunkers = true;

    private Transform target;
    private float attackCooldown;
    private bool isDead = false;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        EnemyTracker.EnemySpawned();

        EnemyRegistry.Register(transform);
    }

    private void OnDestroy()
    {
        EnemyRegistry.Unregister(transform);
    }

    private void Update()
    {
        if (isDead)
            return;

        FindClosestTarget();

        if (target == null)
        {
            animator.SetBool("Run", false);
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            target.position
        );

        if (distance > attackRange)
        {
            MoveToTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    void FindClosestTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        if (targetTurrets)
        {
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Player_Turret");

            foreach (GameObject turret in turrets)
            {
                float distance = Vector2.Distance(
                    transform.position,
                    turret.transform.position
                );

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestTarget = turret.transform;
                }
            }
        }

        if (targetBunkers)
        {
            GameObject[] bunkers = GameObject.FindGameObjectsWithTag("Bunker");

            foreach (GameObject bunker in bunkers)
            {
                float distance = Vector2.Distance(transform.position,bunker.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestTarget = bunker.transform;
                }
            }
        }

        target = closestTarget;
    }

    void MoveToTarget()
    {
        animator.SetBool("Run", true);

        Vector2 direction = (target.position - transform.position).normalized;

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        // Flip while keeping original size
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x),originalScale.y,originalScale.z);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x),originalScale.y,originalScale.z);
        }
    }

    void AttackTarget()
    {
        animator.SetBool("Run", false);

        if (attackCooldown <= 0f)
        {
            animator.SetTrigger("Attack");

            Damageable damageable =
                target.GetComponent<Damageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }

            BunkerHealth bunker =
                target.GetComponent<BunkerHealth>();

            if (bunker != null)
            {
                //Debug.Log("Found bunker");
                bunker.TakeDamage(attackDamage);
            }

            attackCooldown = 1f / attackRate;
        }

        attackCooldown -= Time.deltaTime;
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        EnemyTracker.EnemyDied();

        EnemyRegistry.Unregister(transform);

        animator.SetBool("Run", false);
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Die");

        Destroy(gameObject, 2f);
    }

    // Called by Animation Event at the end of death animation
    public void SpawnChildren()
    {
        //Debug.Log("ANIMATION EVENT CALLED");

        BigSlimeSpawner spawner = GetComponent<BigSlimeSpawner>();

        if (spawner != null)
        {
            spawner.SpawnSmallSlimes();
        }
    }
}