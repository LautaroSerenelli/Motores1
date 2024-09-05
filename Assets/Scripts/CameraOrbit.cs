using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform Target;

    private float Angulo = -90 * Mathf.Deg2Rad;
    public float Distancia;
    public float Sensibilidad;
    public float Altura;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");

        if (hor != 0)
        {
            Angulo += hor * Sensibilidad  * Mathf.Deg2Rad;
        }
    }

    void LateUpdate()
    {
        Vector3 orbita = new Vector3(
            Mathf.Cos(Angulo),
            0,
            Mathf.Sin(Angulo));

        Vector3 alturaAjustada = new Vector3(0, Altura, 0);
        Vector3 posicionDeseada = Target.position + orbita * Distancia + alturaAjustada;

        transform.position = Vector3.Lerp(transform.position, posicionDeseada, Time.deltaTime * 10f);
        transform.LookAt(Target);
    }
}