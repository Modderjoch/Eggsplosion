using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggStrikePickUp : MonoBehaviour
{
    public float duration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    void PickUp(Collider2D player)
    {
        HomingMissle Fire = player.GetComponent<HomingMissle>();
        //Fire.FireMissile = true;  
        Destroy(gameObject);
    }
}
