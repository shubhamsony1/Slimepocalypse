using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject normalSlimePrefab;
    public GameObject bigSlimePrefab;

    [Header("References")]
    public SpawnArea spawnArea;

    [Header("Wave Settings")]
    public int currentWave = 0;
    public float nextWaveDelay = 3f;

    [Header("Group Settings")]
    public int groupSize = 10;
    public float groupRadius = 1.5f;

    [Header("Group Delay")]
    public float minGroupDelay = 1f;
    public float maxGroupDelay = 2f;

    private void Start()
    {
        EnemyTracker.ResetCounter();

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            currentWave++;

            //Debug.Log("WAVE " + currentWave);


            yield return new WaitForSeconds(2f);

            yield return StartCoroutine(SpawnWave(GetWaveEnemyCount()));

            yield return new WaitUntil(() => EnemyTracker.AliveEnemies <= 0);

            //Debug.Log("Wave " + currentWave +" Completed");

            yield return new WaitForSeconds(nextWaveDelay);
        }
    }

    int GetWaveEnemyCount()
    {
        int count =
            Mathf.RoundToInt(
                10 * Mathf.Pow(
                    1.15f,
                    currentWave - 1
                )
            );

        return Mathf.Min(count, 500);
    }

    float GetMinBigPercent()
    {
        return Mathf.Min(((currentWave - 1) / 5) * 5,50);
    }

    float GetMaxBigPercent()
    {
        if (currentWave <= 1)
            return 0;

        return Mathf.Min(Mathf.Pow(2, currentWave - 2),100);
    }

    IEnumerator SpawnWave(int totalEnemies)
    {
        float minPercent = GetMinBigPercent();
        float maxPercent = GetMaxBigPercent();

        int minBig = Mathf.FloorToInt(totalEnemies * minPercent / 100f);

        int maxBig = Mathf.CeilToInt(totalEnemies * maxPercent / 100f);

        int bigRemaining = Random.Range(minBig, maxBig + 1);

        int normalRemaining = totalEnemies - bigRemaining;

        //Debug.Log($"Wave {currentWave} | " + $"Total:{totalEnemies} | " + $"Big:{bigRemaining} | " + $"Normal:{normalRemaining}");

        int remaining = totalEnemies;

        while (remaining > 0)
        {
            int currentGroup = Mathf.Min(groupSize, remaining);

            Vector3 groupCenter = spawnArea.GetRandomSpawnPosition();

            for (int i = 0; i < currentGroup; i++)
            {
                Vector2 offset = Random.insideUnitCircle * groupRadius;

                Vector3 spawnPos = groupCenter + (Vector3)offset;

                bool spawnBig = false;

                int enemiesLeft = normalRemaining + bigRemaining;

                if (bigRemaining > 0)
                {
                    float chance = (float)bigRemaining /enemiesLeft;

                    spawnBig = Random.value < chance;
                }

                if (spawnBig)
                {
                    Instantiate(bigSlimePrefab,spawnPos,Quaternion.identity);

                    bigRemaining--;
                }
                else
                {
                    Instantiate(normalSlimePrefab,spawnPos,Quaternion.identity);

                    normalRemaining--;
                }

                remaining--;
            }

            if (remaining > 0)
            {
                float delay =Random.Range(minGroupDelay,maxGroupDelay);

                yield return new WaitForSeconds(delay);
            }
        }
    }
}