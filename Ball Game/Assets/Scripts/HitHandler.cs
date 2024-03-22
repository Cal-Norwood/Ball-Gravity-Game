using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");    
    }

    private void OnTriggerEnter(Collider other)
    {
        player.BroadcastMessage("HitObstacle");
    }
}
