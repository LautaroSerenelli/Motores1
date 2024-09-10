using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float FuerzaRebote;

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            
            rb.velocity = new Vector3(rb.velocity.x, FuerzaRebote, rb.velocity.z);
        }
    }
}