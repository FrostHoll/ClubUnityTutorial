using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _statsText;

    private void Start()
    {
        _statsText.text =
            "Статистика:\n\n" +
            $"Суммарное время: {GameStats.FormatTime(GameStats.TotalTimeElapsed)}\n" +
            $"Максимальное время: {GameStats.FormatTime(GameStats.MaxTimeSurvived)}\n" +
            $"Максимальный уровень: {GameStats.MaxLevel}\n" +
            $"Последняя пройденная волна: {GameStats.MaxWave}\n" +
            $"Суммарно побеждено врагов: {GameStats.TotalDefeated}\n" +
            $"Максимально побеждено врагов: {GameStats.MaxDefeated}";
        GameStats.SaveStats();
    }

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Exit()
    {      
        Application.Quit();
    }
}
