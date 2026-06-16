using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public static int AliveEnemies = 0;

    public static void ResetCounter()
    {
        AliveEnemies = 0;
    }

    public static void EnemySpawned()
    {
        AliveEnemies++;
    }

    public static void EnemyDied()
    {
        AliveEnemies--;

        if (AliveEnemies < 0)
        {
            AliveEnemies = 0;
        }
    }
}