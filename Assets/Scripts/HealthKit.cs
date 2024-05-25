using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour, IConsumable
{
    [SerializeField]
    private int _healAmount;

    [SerializeField]
    private float _lifetime;

    private float _timer;

    private void Start()
    {
        _timer = _lifetime;
    }

    private void Update()
    {
        if (_timer > 0f)
            _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Consume(Player player)
    {
        player.Heal(_healAmount + (int)(player.MaxHP * 0.05f));
        Destroy(gameObject);
    }
}
