using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public int TotalKills;
    public int NormalSlimeKills;
    public int BigSlimeKills;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetStats()
    {
        TotalKills = 0;
        NormalSlimeKills = 0;
        BigSlimeKills = 0;
    }

    public void NormalKilled()
    {
        TotalKills++;
        NormalSlimeKills++;
    }

    public void BigKilled()
    {
        TotalKills++;
        BigSlimeKills++;
    }
}