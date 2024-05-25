using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutCSharp.RandomSystem;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemyPrefab;

    [SerializeField]
    private List<EnemyTableElement> _enemiesList;

    [SerializeField]
    private Transform _spawnArea;

    private int _wave = 1;

    private RandomTable<EnemyData> enemiesTable;

    private float spawnAreaXMin;

    private float spawnAreaXMax;

    private float spawnAreaZMin;

    private float spawnAreaZMax;

    private List<Enemy> _enemySpawned = new List<Enemy>();

    private void Awake()
    {
        enemiesTable = new RandomTable<EnemyData>();
        foreach (var item in _enemiesList)
            enemiesTable.Add(item.enemy, item.capacity);
        spawnAreaXMin = _spawnArea.position.x - _spawnArea.localScale.x / 2;
        spawnAreaXMax = _spawnArea.position.x + _spawnArea.localScale.x / 2;
        spawnAreaZMin = _spawnArea.position.z - _spawnArea.localScale.z / 2;
        spawnAreaZMax = _spawnArea.position.z + _spawnArea.localScale.z / 2;
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        PlayerUI.instance.ShowWaveText("");
        int enemiesToSpawn = 3 + (int)(0.1f * _wave * _wave);
        PlayerUI.instance.SetWave(_wave, enemiesToSpawn);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnAreaXMin, spawnAreaXMax),
                0f,
                Random.Range(spawnAreaZMin, spawnAreaZMax));
            var en = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            en.SetEnemy(enemiesTable.Next());
            en.died += EnemyDied;
            _enemySpawned.Add(en);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _wave += 10;
            PlayerUI.instance.SetWave(_wave, _enemySpawned.Count);
        }
    }

    private void EnemyDied(Enemy enemy)
    {
        enemy.died -= EnemyDied;
        GameStats.LocalStats.Defeated++;
        _enemySpawned.Remove(enemy);
        PlayerUI.instance.SetEnemiesCount(_enemySpawned.Count);
        if (_enemySpawned.Count == 0)
        {            
            GameStats.LocalStats.Wave = _wave;
            if (_wave < 50)
                _wave++;
            PlayerUI.instance.ShowWaveText($"Далее Волна {_wave}");
            Invoke(nameof(Spawn), 2f);
        }
    }

    [System.Serializable]
    private class EnemyTableElement
    {
        public EnemyData enemy;
        public int capacity;
    }
}
