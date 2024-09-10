using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosSpawn : MonoBehaviour
{
    public GameObject PuntoPrefab;
    public Vector3[] spawnPositions;

    private void Start()
    {
        SpawnearPuntos();
    }

    private void SpawnearPuntos()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Instantiate(PuntoPrefab, spawnPositions[i], Quaternion.identity);
        }
    }
}