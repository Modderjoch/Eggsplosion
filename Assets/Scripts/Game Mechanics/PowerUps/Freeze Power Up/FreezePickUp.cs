using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePickUp : MonoBehaviour
{
    private string currentPickUp;
    public GameObject PUEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentPickUp = other.GetComponent<PickUpAbility>().mainPickUp;
            Exceptions(other);
            if (other.GetComponent<PickUpAbility>().ablePickUp)
            {
                other.GetComponent<PickUpAbility>().mainPickUp = "freeze";
                PickUp(other);
                FindObjectOfType<AudioManager>().Play("PickUp");
            }
        }
    }

    void PickUp(Collider2D player)
    {
       
        Shooting stats = player.GetComponent<Shooting>();
        stats.shotType = "freeze";
        GameObject effect = Instantiate(PUEffect, transform.position, Quaternion.identity);
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        playerStats.TurnOff();
        playerStats.uiInfo = "freeze";
        Destroy(gameObject);
    }

    void Exceptions(Collider2D player)
    {
        switch (currentPickUp)
        {
            case "grenade":
                //can
                player.GetComponent<PickUpAbility>().CannotPickUp();
                break;
            case "bounce":
                //can
                player.GetComponent<PickUpAbility>().CannotPickUp();
                break;
            case "dash":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
            case "rapid":
                //can
                player.GetComponent<PickUpAbility>().CannotPickUp();
                break;
            case "vest":
                //cannot
                player.GetComponent<PickUpAbility>().CannotPickUp();
                break;
            case "freeze":
                //can
                player.GetComponent<PickUpAbility>().CannotPickUp();
                break;
            case "speed":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
        }
    }
}
