using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    public GameObject Player;

    public bool magnetActive = false;
    public bool inMagnetRadius = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (magnetActive == true && inMagnetRadius == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, 0.5f);
        }
    }

    public void ActivateBool()
    {
        magnetActive = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetType() == typeof(SphereCollider))
        {
            inMagnetRadius = true;
        }

        if (other.GetType() == typeof(MeshCollider))
        {
            Player.BroadcastMessage("CoinPickup");
            Destroy(gameObject);
        }
    }
}
