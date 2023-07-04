using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPickUp : MonoBehaviour
{
    public float duration = 3f;
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
                other.GetComponent<PickUpAbility>().dashCount++;
                StartCoroutine(PickUp(other));
                other.GetComponent<PickUpAbility>().mainPickUp = "dash";
                GameObject effect = Instantiate(PUEffect, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("PickUp");
            }
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        player.GetComponent<PowerUpEffects>().DashTrailOn();
        PlayerMovement stats = player.GetComponent<PlayerMovement>();
        stats.dashAllow = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        playerStats.TurnOff();
        playerStats.uiInfo = "walldash";
        yield return new WaitForSeconds(duration);
        if (player.GetComponent<PickUpAbility>().dashCount > 1)
        {
            player.GetComponent<PickUpAbility>().mainPickUp = "nothing";
            player.GetComponent<PickUpAbility>().CanPickUp();
            player.GetComponent<PickUpAbility>().dashCount--;
        }
        else
        {
            player.GetComponent<PowerUpEffects>().DashTrailOff();
            player.GetComponent<PickUpAbility>().CanPickUp();
            player.GetComponent<PickUpAbility>().dashCount--;
            stats.dashAllow = false;
            player.GetComponent<PickUpAbility>().mainPickUp = "nothing";
        }

        Destroy(gameObject);
    }

    void Exceptions(Collider2D player)
    {
        switch (currentPickUp)
        {
            case "grenade":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
            case "bounce":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
            case "dash":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
            case "rapid":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
            case "vest":
                //cannot
                player.GetComponent<PickUpAbility>().CannotPickUp();
                break;
            case "freeze":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
            case "speed":
                //can
                player.GetComponent<PickUpAbility>().CanPickUp();
                break;
        }
    }
}
