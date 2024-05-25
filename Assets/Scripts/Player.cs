using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerShooting), typeof(PlayerMovement))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _maxHP = 100;

    [SerializeField] 
    private float _atkSpeed = 1.0f;

    [SerializeField]
    private int _damage = 10;

    private int _hp = 0;

    private int _level = 1;

    private int _currentExp = 0;

    private int _maxExp = 1000;

    private PlayerMovement _movement;

    private PlayerShooting _shooting;

    [SerializeField]
    private GameObject _hand;

    [SerializeField]
    private StatsAmplifier _statsAmplifier;

    public int MaxHP { get; private set; }

    public int Damage { get; private set; }

    public float AttackSpeed { get; private set; }

    public int Level => _level;

    private void Awake()
    {
        GameStats.LocalStats.Time = Time.time;

        _movement = GetComponent<PlayerMovement>();
        _shooting = GetComponent<PlayerShooting>();
        _shooting.Init(new DamageInfo(_damage, this), _atkSpeed, _hand);
        _movement.Init(_hand);
        _hp = _maxHP;
        PlayerUI.instance.SetHPBar(_hp, _maxHP);
        MaxHP = _maxHP;
        Damage = _damage;
        AttackSpeed = _atkSpeed;
        PlayerUI.instance.SetLevel(_level);
        PlayerUI.instance.SetExp(_currentExp, _maxExp);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        if (_hp <= 0) return;
        _hp = Mathf.Clamp(_hp - damageInfo.damage, 0, MaxHP);
        PlayerUI.instance.SetHPBar(_hp, MaxHP);
        if (_hp <= 0)
        {
            HandleDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IConsumable>(out var item))
        {
            item.Consume(this);
        }
    }

    public void Heal(int healAmount)
    {
        _hp = Mathf.Clamp(_hp + healAmount, 0, MaxHP);
        PlayerUI.instance.SetHPBar(_hp, MaxHP);
    }

    public void GainEXP(int amount)
    {
        _currentExp += amount;
        PlayerUI.instance.SetExp(_currentExp, _maxExp);
        if (_currentExp >= _maxExp) 
        {
            _currentExp -= _maxExp;
            PlayerUI.instance.SetExp(_currentExp, _maxExp);
            NewLevel();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < 10; i++)
                NewLevel();            
        }
    }

    private void NewLevel()
    {
        if (_level == 100)
            return;
        _level++;
        PlayerUI.instance.SetLevel(_level);
        //_maxExp = (int)(_maxExp * Mathf.Pow(1.05f, _level));
        _maxExp = 1000 + _level * 150;

        MaxHP = _statsAmplifier.GetMaxHP(_maxHP, _level);
        int addedHP = MaxHP - _statsAmplifier.GetMaxHP(_maxHP, _level - 1);
        Heal(addedHP);
        Damage = _statsAmplifier.GetDamage(_damage, _level);
        AttackSpeed = _statsAmplifier.GetAtkSpd(_atkSpeed, _level);
        _shooting.Init(new DamageInfo(Damage, this), AttackSpeed, _hand);
    }

    private void HandleDeath()
    {
        float survived = Time.time - GameStats.LocalStats.Time;
        GameStats.LocalStats.Time = survived;
        GameStats.TotalTimeElapsed += survived;
        GameStats.LocalStats.Level = _level;
        if (survived > GameStats.MaxTimeSurvived)
            GameStats.MaxTimeSurvived = survived;

        _movement.enabled = false;
        _shooting.enabled = false;

        PlayerUI.instance.ShowGameOver();
    }
}
