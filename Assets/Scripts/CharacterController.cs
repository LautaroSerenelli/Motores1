using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float Velocidad;
    public float VelocidadMin;
    public float VelocidadMax;
    public float Aceleracion;
    public float Desaceleracion;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movHorizontal, 0.0f, movVertical);
        
        //Aceleracion
        if (movHorizontal != 0 || movVertical != 0)
        {
            rb.AddForce(movimiento * Aceleracion);

            //Limitar la velocidad max
            if (rb.velocity.magnitude > VelocidadMax)
            {
                rb.velocity = rb.velocity.normalized * VelocidadMax;
            }
        }
        else
        {
            if (rb.velocity.magnitude >= VelocidadMin)
            {
                Vector3 desacelerar = rb.velocity.normalized * Mathf.Max(rb.velocity.magnitude - Desaceleracion * Time.fixedDeltaTime, VelocidadMin);
                rb.velocity = Vector3.Lerp(rb.velocity, desacelerar, Time.fixedDeltaTime * Desaceleracion);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

        //Visualizar en el inspector la velocidad
        Velocidad = rb.velocity.magnitude;
    }
}