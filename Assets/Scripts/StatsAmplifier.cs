using UnityEngine;

[System.Serializable]
public class StatsAmplifier
{
    public float maxHPAmp = 1f;

    public float damageAmp = 1f;

    public float atkSpdAmp = 1f;

    public int GetMaxHP(int baseValue, int level)
    {
        return (int)(baseValue * Mathf.Pow(maxHPAmp, level - 1)); 
    }

    public int GetDamage(int baseValue, int level)
    {
        return (int)(baseValue * Mathf.Pow(damageAmp, level - 1));
    }

    public float GetAtkSpd(float baseValue, int level)
    {
        return baseValue * Mathf.Pow(atkSpdAmp, level - 1);
    }
}
