using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerMuerte : MonoBehaviour
{
    public Transform spawnPoint;
    public float DelaySpawn = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportAndPause(other));
        }
    }

    private IEnumerator TeleportAndPause(Collider player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        player.transform.position = spawnPoint.position;

        var characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        
        yield return new WaitForSeconds(DelaySpawn);

        characterController.enabled = true;
    }
}