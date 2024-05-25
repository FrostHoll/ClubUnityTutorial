using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    private static Player s_player;

    private static Transform s_playerPos;

    [SerializeField]
    private EnemyData _enemyData;

    [SerializeField]
    private GameObject _sprite;

    [SerializeField]
    private Image _hpBarFill;

    [SerializeField]
    private Transform _hpBarCanvas;

    [SerializeField]
    private HealthKit _healthKit;

    private Rigidbody _rb;

    private float _attackRange;

    private bool _isInRange = false;

    private float _atkCD = 4f;

    private int _maxHP = 100;

    private int _hp = 0;

    private int _damage = 10;

    private float _atkSpeed = 1f;

    private DamageInfo _damageInfo;

    private Bullet _bulletPrefab = null;

    private bool IsReadyToAttack => _atkCD <= 0f;

    public UnityAction<Enemy> died;

    public void TakeDamage(DamageInfo damageInfo)
    {
        _hp = Mathf.Clamp(_hp - damageInfo.damage, 0, _maxHP);
        _hpBarFill.fillAmount = (float)_hp / _maxHP;
        if (_hp <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        s_player.GainEXP(_enemyData.Experience);
        if (Random.Range(0, 100) <= 10)
            Instantiate(_healthKit, transform.position, Quaternion.identity);
        died?.Invoke(this);
        Destroy(gameObject);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    public void SetEnemy(EnemyData enemyData)
    {
        if (s_player == null)
        {
            s_player = FindObjectOfType<Player>();
            s_playerPos = s_player.transform;
        }

        _enemyData = enemyData;

        _sprite.GetComponent<MeshRenderer>().material = _enemyData.Sprite;
        _sprite.transform.localScale = new Vector3(_enemyData.SpriteSizeX, _enemyData.SpriteSizeY, 1f);

        _hpBarCanvas.Translate(new Vector3(0f, _enemyData.SpriteSizeY / 2.0f + 0.2f, 0f));

        gameObject.GetComponent<BoxCollider>().size = new Vector3(_enemyData.SpriteSizeX, 1.4f, _enemyData.SpriteSizeY);

        var statsAmp = _enemyData.Amplifier;

        _attackRange = _enemyData.AttackRange + Random.Range(0f, 0.2f);
        _maxHP = statsAmp.GetMaxHP(_enemyData.MaxHP, s_player.Level);
        _hp = _maxHP;
        _damage = statsAmp.GetDamage(_enemyData.Damage, s_player.Level);
        _atkSpeed = statsAmp.GetAtkSpd(_enemyData.AttackSpeed, s_player.Level);
        _hpBarFill.fillAmount = 1f;

        _damageInfo = new DamageInfo(_damage, this);
        if (_enemyData.AttackType == AttackType.Ranged)
            _bulletPrefab = _enemyData.BulletPrefab;
    }

    private void FixedUpdate()
    {
        Vector3 distanceToPlayer = s_playerPos.position - transform.position;

        _isInRange = distanceToPlayer.magnitude <= _attackRange;

        if (!_isInRange)
        {
            Move(distanceToPlayer.normalized * _enemyData.MoveSpeed);
        }
        
    }

    private void Update()
    {
        if (_atkCD > 0f)
            _atkCD -= Time.deltaTime;

        if (_isInRange && IsReadyToAttack)
        {
            if (_enemyData.AttackType == AttackType.Melee)
                MeleeAttack();
            else if (_enemyData.AttackType == AttackType.Ranged)
                Shoot();
            _atkCD = _atkSpeed + Random.Range(0f, 0.1f);
        }
    }

    private void MeleeAttack()
    {
        s_player.TakeDamage(_damageInfo);
    }

    private void Shoot()
    {
        Vector3 velocity = (s_playerPos.position - transform.position).normalized;
        velocity.y = 0;
        Vector3 randSpread = new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
        Vector3 spawnPos = transform.position + Vector3.up + velocity;
        Instantiate(_bulletPrefab, spawnPos, Quaternion.identity)
            .SetIsFromPlayer(false)
            .SetBulletSpeed(_enemyData.BulletSpeed)
            .SetDamageInfo(_damageInfo)
            .StartMoving((velocity + randSpread).normalized);
    }

    private void Move(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            _rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        }
    }
}
