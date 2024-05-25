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
            "����������:\n\n" +
            $"��������� �����: {GameStats.FormatTime(GameStats.TotalTimeElapsed)}\n" +
            $"������������ �����: {GameStats.FormatTime(GameStats.MaxTimeSurvived)}\n" +
            $"������������ �������: {GameStats.MaxLevel}\n" +
            $"��������� ���������� �����: {GameStats.MaxWave}\n" +
            $"�������� ��������� ������: {GameStats.TotalDefeated}\n" +
            $"����������� ��������� ������: {GameStats.MaxDefeated}";
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
