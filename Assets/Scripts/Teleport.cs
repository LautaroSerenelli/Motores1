using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Collider TriggerDestino;

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            Vector3 PosicionDestino = TriggerDestino.transform.position;
            PosicionDestino.y += 10f;
            other.transform.position = PosicionDestino;

            Vector3 DireccionHorizontal = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            rb.velocity = Vector3.zero;
            rb.AddForce(DireccionHorizontal * 5f, ForceMode.VelocityChange);
        }
    }
}