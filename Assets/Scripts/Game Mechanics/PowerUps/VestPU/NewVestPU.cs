using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewVestPU : MonoBehaviour
{

    [SerializeField]
    private GameObject VestEffect;

    public GameObject PUEffect;
    private string currentPickUp;

    public float duration = 3f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentPickUp = other.GetComponent<PickUpAbility>().mainPickUp;
            Exceptions(other);

            if (other.GetComponent<PickUpAbility>().ablePickUp)
            {
                other.GetComponent<PlayerStats>().TurnOnCircle();
                StartCoroutine(PickUp(other));
                GameObject effect = Instantiate(PUEffect, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("Vest");
                FindObjectOfType<AudioManager>().Play("PickUp");
            }
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        PlayerMovement speed = player.GetComponent<PlayerMovement>();
        Shooting stats = player.GetComponent<Shooting>();
        Animator anim = player.GetComponentInChildren<Animator>();

        anim.SetBool("Vest", true);
        stats.shotType = "vest";
        speed.bonusSpeed = 3f;
        speed.vestOn = true;
        PlayerStats playerStats = player.GetComponentInParent<PlayerStats>();
        playerStats.uiInfo = "vest";
        player.GetComponent<PickUpAbility>().CannotPickUp();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(duration);
        if (stats.vestDeployed == false) // destroy in the if and make else as well
        {
            stats.SpawnSmallVest();
            anim.SetBool("Vest", false);
            speed.bonusSpeed = 0;
            speed.vestOn = false;
            player.GetComponent<PickUpAbility>().CanPickUp();
            FindObjectOfType<AudioManager>().Play("VestBoom");
            player.GetComponent<PlayerStats>().TurnOffCircle();
        }
        anim.SetBool("Vest", false);
        stats.shotType = "normal";
        speed.bonusSpeed = 0;
        stats.vestDeployed = false;
        speed.vestOn = false;
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
                player.GetComponent<PickUpAbility>().CannotPickUp();
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
                player.GetComponent<PickUpAbility>().CannotPickUp();
                break;
        }
    }


}
