using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Vector3 Direccion;
    public float Velocidad;
    private Vector3 VelocidadObjetivo = Vector3.zero;

    public float Aceleracion;
    public float Desaceleracion;

    public float FuerzaSalto;
    public bool EnSuelo = false;

    public LayerMask CapaSuelo;

    public float VelocidadDash;
    public float DuracionDash;
    private bool enDash = false;
    private float TiempoDash;
    private Vector3 DireccionDash; 
    
    private Rigidbody rb;
    public CinemachineVirtualCamera VirtualCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movimiento();
        CheckSuelo();

        if (Input.GetKeyDown(KeyCode.LeftShift) && EnSuelo && !enDash)
        {
            IniciarDash();
        }

        if (enDash && Time.time >= TiempoDash)
        {
            TerminarDash();
        }
    }

    private void FixedUpdate()
    {
        float VelocidadActual = Velocidad;

        if (enDash)
        {
            VelocidadActual += VelocidadDash;
        }
        
        if (Direccion != Vector3.zero)
        {
            //Aceleracion
            VelocidadObjetivo = new Vector3(Direccion.x * VelocidadActual, rb.velocity.y, Direccion.z * VelocidadActual);
            rb.velocity = Vector3.Lerp(rb.velocity, VelocidadObjetivo, Time.deltaTime * Aceleracion);
        }
        else if (enDash)
        {
            VelocidadObjetivo = DireccionDash * VelocidadActual;
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
    }

    private void Movimiento()
    {
        Transform camTransform = VirtualCamera.transform;
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Direccion = (forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal")).normalized;

        if (Input.GetButtonDown("Jump") && EnSuelo)
        {
            Salto();
        }
    }

    private void Salto()
    {
        rb.velocity += Vector3.up * FuerzaSalto;
    }

    private void CheckSuelo()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, CapaSuelo))
        {
            EnSuelo = true;
        }
        else
        {
            EnSuelo = false;
        }

        // Debug.DrawRay(transform.position, Vector3.down * 1f, EnSuelo ? Color.green : Color.red);
    }

    //Dash
    private void IniciarDash()
    {
        enDash = true;
        TiempoDash = Time.time + DuracionDash;

        DireccionDash = Direccion == Vector3.zero ? VirtualCamera.transform.forward : Direccion;
    }

    private void TerminarDash()
    {
        enDash = false;
    }
}