using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Cell _cellPrefab;

    [SerializeField]
    private int _size;

    [SerializeField]
    [Range(0f, 1f)]
    private float _spawnBoundFactor;

    private void Start()
    {
        List<Cell> cells = new List<Cell>();
        var noise = PerlinNoise.GetPerlinNoise(0f, 0f);
        var matrix = NoiseToMatrix(noise);
        
        StartCoroutine(CellSpawn(matrix));
    }

    private int[,] NoiseToMatrix(float[,] arr)
    {
        if (_size > arr.GetLength(0))
            throw new Exception("PIZDEC");
        int size = _size;
        bool IndexInRange(int x) => x >= 0 && x < size;
        int[,] ans = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float sum = arr[i, j];
                sum += IndexInRange(i - 1) && IndexInRange(j) ? arr[i - 1, j] : 0;
                sum += IndexInRange(i - 2) && IndexInRange(j) ? arr[i - 2, j] : 0;
                sum += IndexInRange(i + 1) && IndexInRange(j) ? arr[i + 1, j] : 0;
                sum += IndexInRange(i + 2) && IndexInRange(j) ? arr[i + 2, j] : 0;
                sum += IndexInRange(i - 1) && IndexInRange(j - 1) ? arr[i - 1, j - 1] : 0;
                sum += IndexInRange(i + 1) && IndexInRange(j - 1) ? arr[i + 1, j - 1] : 0;
                sum /= 7;
                ans[i, j] = sum >= _spawnBoundFactor ? 1 : 0;
            }
        }
        return ans;
    }

    private IEnumerator CellSpawn(int[,] matrix)
    {
        float cellRadius = Cell.CellRadius;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != 0)
                {
                    SpawnCell(new Vector3(2 * i * cellRadius, 0f, 2 * j * cellRadius));
                }                   
            }
        }
        yield return null;
    }

    private void SpawnCell(Vector3 offset)
    {
        Instantiate(_cellPrefab, _spawnPoint.position + offset, Quaternion.identity);
    }
}
