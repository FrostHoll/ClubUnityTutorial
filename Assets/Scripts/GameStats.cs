using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    public static float TotalTimeElapsed = 0f;

    public static float MaxTimeSurvived = 0f;

    public static int TotalDefeated = 0;

    public static int MaxDefeated = 0;

    public static int MaxLevel = 0;

    public static int MaxWave = 0;

    public static LocalStats LocalStats = new LocalStats();

    static GameStats()
    {
        if (PlayerPrefs.HasKey("TotalTimeElapsed"))
            TotalTimeElapsed = PlayerPrefs.GetFloat("TotalTimeElapsed");
        if (PlayerPrefs.HasKey("MaxTimeSurvived"))
            MaxTimeSurvived = PlayerPrefs.GetFloat("MaxTimeSurvived");
        if (PlayerPrefs.HasKey("TotalDefeated"))
            TotalDefeated = PlayerPrefs.GetInt("TotalDefeated");
        if (PlayerPrefs.HasKey("MaxDefeated"))
            MaxDefeated = PlayerPrefs.GetInt("MaxDefeated");
        if (PlayerPrefs.HasKey("MaxLevel"))
            MaxLevel = PlayerPrefs.GetInt("MaxLevel");
        if (PlayerPrefs.HasKey("MaxWave"))
            MaxWave = PlayerPrefs.GetInt("MaxWave");
    }

    public static string FormatTime(float seconds)
    {
        int minutes = (int)seconds / 60;
        int remainingSeconds = (int)seconds % 60;
        return string.Format("{0:D2}:{1:D2}", minutes, remainingSeconds);
    }

    public static void SaveStats()
    {
        PlayerPrefs.SetFloat("TotalTimeElapsed", TotalTimeElapsed);
        PlayerPrefs.SetFloat("MaxTimeSurvived", MaxTimeSurvived);
        PlayerPrefs.SetInt("TotalDefeated", TotalDefeated);
        PlayerPrefs.SetInt("MaxDefeated", MaxDefeated);
        PlayerPrefs.SetInt("MaxLevel", MaxLevel);
        PlayerPrefs.SetInt("MaxWave", MaxWave);
        PlayerPrefs.Save();
    }
}

public class LocalStats
{
    public float Time;

    public int Defeated;

    public int Level;

    public int Wave;
}
