using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidPickUp : MonoBehaviour
{

    public float duration = 3f;
    [SerializeField]
    private float newFireRate;
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
                other.GetComponent<PickUpAbility>().rapidCount++;
                other.GetComponent<PickUpAbility>().mainPickUp = "rapid";
                StartCoroutine(PickUp(other)); 
                GameObject effect = Instantiate(PUEffect, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("PickUp");
            }
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        player.GetComponent<PowerUpEffects>().RapidEffectOn();
        Shooting shooting = player.GetComponent<Shooting>();
        shooting.fireRate = newFireRate;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        playerStats.uiInfo = "rapidfire";
        yield return new WaitForSeconds(duration);
        if(player.GetComponent<PickUpAbility>().rapidCount > 1)
        {
            player.GetComponent<PickUpAbility>().mainPickUp = "nothing";
            player.GetComponent<PickUpAbility>().CanPickUp();
            player.GetComponent<PickUpAbility>().rapidCount--;
        }
        else
        {
            player.GetComponent<PowerUpEffects>().RapidEffectOff();
            player.GetComponent<PickUpAbility>().CanPickUp();
            player.GetComponent<PickUpAbility>().rapidCount--;
            shooting.fireRate = 1f;
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
                player.GetComponent<PickUpAbility>().CanPickUp();
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
