﻿using System;
using UnityEngine;

public class ParametrisierteOberflächeScript2 : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector2[] _uvs;
    private Vector2 _subdivisions;
    private Vector2 _vertexSize;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        Build();
    }

    private void Build()
    {
        _subdivisions = new Vector2Int(50, 50);
        _vertexSize = _subdivisions + new Vector2Int(1, 1);
        _vertices = new Vector3[(int) (_vertexSize.x * _vertexSize.y)];
        _uvs = new Vector2[_vertices.Length];

        for (int y = 0; y < _vertexSize.y; y++)
        {
            // Das verändern
            float v = (float) (((2 * Math.PI) / _subdivisions.y) * y);

            for (int x = 0; x < _vertexSize.x; x++)
            {
                // Das verändern
                float u = (float) (((2 * Math.PI) / _subdivisions.x) * x);

                // Das verändern
                float newX = (float) ((2 + Mathf.Cos(u)) * Math.Pow(Mathf.Cos(v), 3) * Mathf.Sin(v));
                float newY = (float) ((2 + Mathf.Cos((float) (u + 2 * Math.PI / 3))) * Math.Pow(Mathf.Cos((float) (v + 2 * Math.PI / 3)), 2) * Math.Pow(Mathf.Sin((float) (v + 2 * Math.PI / 3)), 2));
                float newZ = (float) (-(2 + Mathf.Cos((float) (u - 2 * Math.PI / 3))) * Math.Pow(Mathf.Cos((float) (v + 2 * Math.PI / 3)), 2) * Math.Pow(Mathf.Sin((float) (v + 2 * Math.PI / 3)), 2));
                
                Vector3 vertex = new Vector3(newX, newY, newZ);
                
                Vector2 uv = new Vector2(u, v);
                
                int arrayIndex = (int) (x + y * _vertexSize.x);

                _vertices[arrayIndex] = vertex;
                _uvs[arrayIndex] = uv;
            }
        }

        _triangles = new int[(int) (_subdivisions.x * _subdivisions.y * 6)];

        int triangleIndex = 0;
        int indexer = 0;

        for (int y = 0; y < _subdivisions.y; y++)
        {
            for (int x = 0; x < _subdivisions.x; x++)
            {
                _triangles[indexer + 0] = triangleIndex;
                _triangles[indexer + 1] = (int) (triangleIndex + _subdivisions.x + 1);
                _triangles[indexer + 2] = triangleIndex + 1;
                
                _triangles[indexer + 3] = triangleIndex + 1;
                _triangles[indexer + 4] = (int) (triangleIndex + _subdivisions.x + 1);
                _triangles[indexer + 5] = (int) (triangleIndex + _subdivisions.x + 2);

                triangleIndex++;
                indexer += 6;
            }
            triangleIndex++;
        }

        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;
    }
}
