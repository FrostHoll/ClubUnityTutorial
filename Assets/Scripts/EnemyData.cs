using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New EnemyData", fileName = "NewEnemyData")]
public class EnemyData : ScriptableObject
{
    public string Name;

    public int MaxHP;

    public int Experience;

    public float MoveSpeed;

    public AttackType AttackType;

    public int Damage;

    public float AttackSpeed;

    public float AttackRange;

    public StatsAmplifier Amplifier;

    public Bullet BulletPrefab;

    public float BulletSpeed;

    public Material Sprite;

    public float SpriteSizeX;

    public float SpriteSizeY;
}

public enum AttackType
{
    Melee,
    Ranged
}
