using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    [SerializeField]
    private Image _hpBarFill;

    [SerializeField]
    private TextMeshProUGUI _hpBarText;

    [SerializeField]
    private TextMeshProUGUI _waveText;

    [SerializeField]
    private TextMeshProUGUI _enemyText;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private Image _expBarFill;

    [SerializeField]
    private TextMeshProUGUI _waveIncomingText;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private TextMeshProUGUI _statsText;

    private int _spawnedEnemiesCount;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        
    }

    public void SetHPBar(int hp, int maxHP)
    {
        _hpBarText.text = $"��������: {hp} / {maxHP}";
        _hpBarFill.fillAmount = (float)hp / maxHP;
    }

    public void SetWave(int wave, int spawnedEnemiesCount) 
    {
        _spawnedEnemiesCount = spawnedEnemiesCount;
        _waveText.text = $"�����: {wave}";
        _enemyText.text = $"�����: {spawnedEnemiesCount} / {spawnedEnemiesCount}";
    }

    public void SetEnemiesCount(int enemiesCount) 
    {
        _enemyText.text = $"�����: {enemiesCount} / {_spawnedEnemiesCount}";
    }

    public void SetExp(int exp, int maxExp) 
    {
        _expBarFill.fillAmount = (float)exp / maxExp;
    }

    public void SetLevel(int level)
    {
        _levelText.text = level.ToString();
    }

    public void ShowWaveText(string msg)
    {
        _waveIncomingText.text = msg;
    }

    public void ShowGameOver()
    {
        GameStats.TotalDefeated += GameStats.LocalStats.Defeated;
        if (GameStats.LocalStats.Defeated > GameStats.MaxDefeated)
            GameStats.MaxDefeated = GameStats.LocalStats.Defeated;
        if (GameStats.LocalStats.Wave > GameStats.MaxWave)
            GameStats.MaxWave = GameStats.LocalStats.Wave;
        if (GameStats.LocalStats.Level > GameStats.MaxLevel)
            GameStats.MaxLevel = GameStats.LocalStats.Level;
        _gameOverPanel.SetActive(true);
        _statsText.text = 
            $"������ �������: {GameStats.FormatTime(GameStats.LocalStats.Time)}\n" +
            $"��������� �������: {GameStats.LocalStats.Level}\n" +
            $"�������� ����: {GameStats.LocalStats.Wave}\n" +
            $"��������� ������: {GameStats.LocalStats.Defeated}";
    }

    public void Exit()
    {
        GameStats.LocalStats = new LocalStats();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        GameStats.LocalStats = new LocalStats();
        SceneManager.LoadScene("MainScene");
    }
}
