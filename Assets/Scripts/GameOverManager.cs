using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject panel;

    public TMP_Text waveText;
    public TMP_Text totalKillsText;
    public TMP_Text normalKillsText;
    public TMP_Text bigKillsText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowGameOver()
    {
        panel.SetActive(true);

        waveText.text =
            "Wave Reached: " +
            FindFirstObjectByType<WaveManager>()
            .currentWave;

        totalKillsText.text =
            "Total Kills: " +
            GameStats.Instance.TotalKills;

        normalKillsText.text =
            "Normal Slimes: " +
            GameStats.Instance.NormalSlimeKills;

        bigKillsText.text =
            "Big Slimes: " +
            GameStats.Instance.BigSlimeKills;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        EnemyTracker.ResetCounter();

        if (GameStats.Instance != null)
        {
            GameStats.Instance.ResetStats();
        }

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}