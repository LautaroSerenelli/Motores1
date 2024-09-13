using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosSpawn : MonoBehaviour
{
    public GameObject PuntoPrefab;
    public Vector3[] spawnPositions;
    public int[] valoresPuntos;

    private void Start()
    {
        SpawnearPuntos();

        int TotalPoints = CalcularPuntosTotales();
        GameManager.Instance.AddSpawnerPoints(TotalPoints);
    }

    private void SpawnearPuntos()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GameObject punto = Instantiate(PuntoPrefab, spawnPositions[i], Quaternion.identity);
            punto.SetActive(true);

            AtracciónPuntos atracciónPuntos = punto.GetComponent<AtracciónPuntos>();
            atracciónPuntos.ValorPunto = valoresPuntos[i];
        }
    }

    private int CalcularPuntosTotales()
    {
        int TotalPoints = 0;
        
        for (int i = 0; i < valoresPuntos.Length; i++)
        {
            TotalPoints += valoresPuntos[i];
        }
        return TotalPoints;
    }
}