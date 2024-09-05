using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position;
        }
    }
}