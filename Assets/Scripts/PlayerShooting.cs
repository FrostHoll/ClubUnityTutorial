using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private float _atkSpeed;

    [SerializeField]
    private float _bulletSpeed;

    private float _atkCD = 0;

    private bool IsReadyToShoot => _atkCD <= 0;

    [SerializeField]
    private Bullet _bulletPrefab;

    private GameObject _hand;

    private DamageInfo _damageInfo;

    private void Update()
    {
        if (_atkCD > 0)
            _atkCD -= Time.deltaTime;

        if (Input.GetAxisRaw("Fire1") > 0f && IsReadyToShoot)
        {
            Vector3 velocity = (_hand.transform.position - transform.position).normalized;
            velocity.y = 0;
            Vector3 spawnPos = _hand.transform.position + velocity;
            Instantiate(_bulletPrefab, spawnPos, Quaternion.identity)
                .SetIsFromPlayer(true)
                .SetBulletSpeed(_bulletSpeed)
                .SetDamageInfo(_damageInfo)
                .StartMoving(velocity);
            _atkCD = _atkSpeed;
        }
    }

    public void Init(DamageInfo damageInfo, float atkSpeed, GameObject hand)
    {
        _damageInfo = damageInfo;
        _atkSpeed = atkSpeed;
        _hand = hand;
    }
}
