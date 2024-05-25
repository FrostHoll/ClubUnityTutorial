using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static int enemyLayer = 0;

    private static int playerLayer = 0;

    private float _lifetime = 5f;

    private Rigidbody _rb;

    private float _bulletSpeed = 20f;

    private DamageInfo _damageInfo = new DamageInfo(1, null);

    private bool _isFromPlayer = false;

    private void Awake()
    {
        if (enemyLayer == 0 || playerLayer == 0)
        {
            enemyLayer = LayerMask.NameToLayer("Enemy");
            playerLayer = LayerMask.NameToLayer("Player");
        }
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_lifetime > 0) 
            _lifetime -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            if (_isFromPlayer && other.gameObject.layer == enemyLayer)
            {
                damageable.TakeDamage(_damageInfo);
                Destroy(gameObject);
            }
            if (!_isFromPlayer && other.gameObject.layer == playerLayer)
            {
                damageable.TakeDamage(_damageInfo);
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }

    public Bullet SetIsFromPlayer(bool isFromPlayer)
    {
        _isFromPlayer = isFromPlayer;
        return this;
    }

    public Bullet StartMoving(Vector3 velocity)
    {
        //_rb.velocity = velocity * _bulletSpeed;
        _rb.AddForce(velocity * _bulletSpeed);
        return this;
    }

    public Bullet SetLifetime(float lifetime) 
    {
        _lifetime = lifetime;
        return this;
    }

    public Bullet SetDamageInfo(DamageInfo damageInfo) 
    {
        _damageInfo = damageInfo;
        return this;
    }

    public Bullet SetBulletSpeed(float bulletSpeed) 
    {
        _bulletSpeed = bulletSpeed;
        return this;
    }
}
