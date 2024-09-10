using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtracciónPuntos : MonoBehaviour
{
    public float VelAtraccion;
    public float DisAtraccion;
    public Transform Player;

    public GameObject IconPrefab;
    private GameObject IconInstance;

    private bool esAtraida = false;

    private void Start()
    {
        if (IconPrefab != null)
        {
            IconInstance = Instantiate(IconPrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        }
    }

    private void Update()
    {
        float Distancia = Vector3.Distance(transform.position, Player.position);

        if (Distancia < DisAtraccion)
        {
            esAtraida = true;
        }

        if (esAtraida)
        {
            MoverAJugador();

            if (Distancia < 0.5f)
            {
                Recolectar();
            }
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.transform;
            esAtraida = true;
        }
    }

    private void MoverAJugador()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, VelAtraccion * Time.deltaTime);
    }

    public void Recolectar()
    {
        if (IconInstance != null)
        {
            Destroy(IconInstance);
        }
        Destroy(gameObject);
    }
}