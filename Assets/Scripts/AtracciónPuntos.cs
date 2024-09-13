using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtracciónPuntos : MonoBehaviour
{
    public float VelAtraccion;
    public float DisAtraccion;
    public Transform Player;
    private bool esAtraida = false;

    //Icono
    public GameObject IconPrefab;
    private GameObject IconInstance;
    public float DistanciaVisibilidad;

    //Oscilacion vertical
    public float AlturaOscilacion;
    public float VelocidadOscilacion;
    private float TiempoOscilacion;
    private Vector3 PuntoInicial;
    private float DesfaseOscilacion;

    public int ValorPunto;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (IconPrefab != null)
        {
            IconInstance = Instantiate(IconPrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        }

        PuntoInicial = transform.position;
        
        DesfaseOscilacion = Random.Range(0f, 2f);
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
        else
        {
            OscilarVerticalmente();
        }

        if (IconInstance != null && mainCamera != null)
        {
            bool iconoVisible = Distancia > DistanciaVisibilidad;
            IconInstance.SetActive(iconoVisible);
            
            OrientarHaciaCamara();
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

    public void OscilarVerticalmente()
    {
        TiempoOscilacion += Time.deltaTime * VelocidadOscilacion;
        float alturaOscilacion = Mathf.PingPong(TiempoOscilacion + DesfaseOscilacion, AlturaOscilacion);
        transform.position = new Vector3(transform.position.x, PuntoInicial.y + alturaOscilacion, transform.position.z);
    }

    public void Recolectar()
    {
        if (IconInstance != null)
        {
            Destroy(IconInstance);
        }
        GameManager.Instance.AddScore(ValorPunto);
        Destroy(gameObject);
    }

    //Icono
    //Lo que renegué para hacer esto
    public void OrientarHaciaCamara()
    {
        Vector3 direccionCamara = mainCamera.transform.forward;
        float Angulo = Mathf.Atan2(direccionCamara.x, direccionCamara.z) * Mathf.Rad2Deg;
        
        if (Angulo < 0)
        {
            Angulo += 360;
        }

        IconInstance.transform.rotation = Quaternion.Euler(0, Angulo, 0);
    }
}