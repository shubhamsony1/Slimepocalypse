using UnityEngine;
using System.Collections;

public class BigSlimeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject smallSlimePrefab;
    public int spawnCount = 10;

    [Header("Spawn Area")]
    public float spawnRadius = 1.5f;

    [Header("Burst Force")]
    public float burstForce = 3f;

    private bool hasSpawned = false;

    public void SpawnSmallSlimes()
    {
        if (hasSpawned)
            return;

        hasSpawned = true;

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        // Wait for death animation
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 offset =
                Random.insideUnitCircle * spawnRadius;

            GameObject slime = Instantiate(smallSlimePrefab,transform.position + (Vector3)offset,Quaternion.identity);

            Rigidbody2D rb =
                slime.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(offset.normalized * burstForce, ForceMode2D.Impulse);
            }
        }
    }
}