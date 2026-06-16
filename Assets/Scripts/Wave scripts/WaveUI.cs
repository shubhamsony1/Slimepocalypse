using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public WaveManager waveManager;

    public TMP_Text waveText;

    private void Update()
    {
        waveText.text =
            "Wave: " +
            waveManager.currentWave +
            "\nEnemies: " +
            EnemyTracker.AliveEnemies;
    }
}