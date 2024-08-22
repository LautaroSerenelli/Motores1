using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Vector3 Direccion;
    public float Velocidad;
    private Vector3 VelocidadObjetivo = Vector3.zero;
    //test
    public float VelocidadActualX;
    public float VelocidadActualZ;

    public float Aceleracion;
    public float Desaceleracion;

    public float FuerzaSalto;
    public bool EnSuelo = false;
    // public float FuerzaGravedad;

    public LayerMask CapaSuelo;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.useGravity = false; //Desactiva la gravedad global para este rb
    }

    private void Update()
    {
        Movimiento();
    }

    private void FixedUpdate()
    {
        if (Direccion != Vector3.zero)
        {
            //Aceleracion
            VelocidadObjetivo = new Vector3(Direccion.x * Velocidad, rb.velocity.y, Direccion.z * Velocidad);
            rb.velocity = Vector3.Lerp(rb.velocity, VelocidadObjetivo, Time.deltaTime * Aceleracion);
        }
        else
        {
            //Desaceleracion
            float xVel = rb.velocity.x;
            float zVel = rb.velocity.z;

            if (Direccion.x == 0)
            {
                xVel = Mathf.Lerp(xVel, 0, Time.deltaTime * Desaceleracion);
            }
            if (Direccion.z == 0)
            {
                zVel = Mathf.Lerp(zVel, 0, Time.deltaTime * Desaceleracion);
            }

            if (xVel < 0.7f && xVel > -0.5f)
            {
                xVel = 0f;
            }
            if (zVel < 0.7f && zVel > -0.5f)
            {
                zVel = 0f;
            }

            VelocidadObjetivo = new Vector3(xVel, rb.velocity.y, zVel);
            rb.velocity = VelocidadObjetivo;
        }

        //test
        VelocidadActualX = rb.velocity.x;
        VelocidadActualZ = rb.velocity.z;
    }

    private void Movimiento()
    {
        Direccion = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Direccion = Direccion.normalized;

        if (Input.GetButtonDown("Jump") && EnSuelo)
        {
            Salto();
        }
    }

    private void Salto()
    {
        rb.velocity += Vector3.up * FuerzaSalto;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            EnSuelo = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            EnSuelo = false;
        }
    }
}