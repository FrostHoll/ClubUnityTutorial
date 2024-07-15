using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh _mesh;

    private Vector3[] _vertices;
    private int[] _triangles;

    [SerializeField]
    private int _xSize;

    [SerializeField]
    private int _zSize;

    void Start()
    {
        
    }

    public void Generate(int[,] arr)
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        CreateShape(arr);
        UpdateMesh();
        Vector2[] uvs = new Vector2[_vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);
        }
        _mesh.uv = uvs;
    }

    private void CreateShape(int[,] arr)
    {

        _vertices = new Vector3[(_xSize + 1) * (_zSize + 1)];

        for (int z = 0, i = 0; z <= _zSize; z++)
        {
            for (int x = 0; x <= _xSize; x++)
            {
                float y = arr[z, x] * 20;

                _vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        _triangles = new int[_zSize * _xSize * 6];

        int vert = 0, tris = 0;

        for (int z = 0; z < _zSize; z++)
        {
            for (int x = 0; x < _xSize; x++, vert++, tris += 6)
            {
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + _xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + _xSize + 1;
                _triangles[tris + 5] = vert + _xSize + 2;
            }
            vert++;
        }
    }

    private void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (_vertices == null)
            return;

        for (int i = 0; i < _vertices.Length; i++)
        {
            Gizmos.DrawSphere(_vertices[i], .1f);
        }
    }
}
