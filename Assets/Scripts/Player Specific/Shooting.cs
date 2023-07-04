using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public GameObject bouncePrefab;
    public GameObject Bombprefab;
    public GameObject vestPrefab;
    public GameObject smallVestPrefab;
    public GameObject freezePrefab;
    public Animator anima;
    public string shotType;
    public PlayerMovement playerMovement;

    [HideInInspector]
    public bool vestDeployed = false;

    public float bulletForce = 15f;
    [SerializeField]
    public float fireRate = 1f;
    private float lastShot = 0.0f;
    public float freezeDuration;
    public bool isTeams;
    public bool allowShoot;
    private PlayerStats playerStats;

    //public string shooter { get; set; }
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        shotType = "normal";
        allowShoot = true;
    }
    public void Fire1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time > fireRate + lastShot)
            {
                if (allowShoot)
                {
                    DiffShooting();
                }
            }
        }
    }
        
    public void DiffShooting()
    {

        PlayerStats playerStats = GetComponent<PlayerStats>();
        
            switch (shotType)
            {

            case "normal":
                SpawnBullet();
                anima.SetTrigger("Shoot1");
                lastShot = Time.time;
                FindObjectOfType<AudioManager>().Play("Throw");
                break;
            case "grenade":
                SpawnBomb();
                anima.SetTrigger("Shoot1");
                shotType = "normal";
                lastShot = Time.time;
                GetComponent<PickUpAbility>().mainPickUp = "nothing";
                GetComponent<PickUpAbility>().CanPickUp();
                playerStats.TurnOff();
                break;
            case "vest":
                if (GetComponentInChildren<PlayerCircle>().numPlayers > 0)
                {
                    SpawnVest();
                    playerMovement.bonusSpeed = 0;
                    shotType = "normal";
                    lastShot = Time.time;
                    anima.SetBool("Vest", false);
                    playerMovement.vestOn = false;
                    TimerUI vestTimer = GetComponentInChildren<TimerUI>();
                    vestTimer.DisableTimer();
                    playerStats.TurnOff();
                    GetComponent<PickUpAbility>().mainPickUp = "nothing";
                    GetComponent<PickUpAbility>().CanPickUp();
                    GetComponent<PlayerStats>().TurnOffCircle();
                    FindObjectOfType<AudioManager>().Stop("Vest");
                }
                break;
            case "freeze":
                SpawnFreeze();
                anima.SetTrigger("Shoot1");
                shotType = "normal";
                lastShot = Time.time;
                //AudioSource.PlayClipAtPoint(shootSound, transform.position);
                GetComponent<PickUpAbility>().mainPickUp = "nothing";
                GetComponent<PickUpAbility>().CanPickUp();
                playerStats.TurnOff();
                break;
            case "bounce":
                SpawnBounce();
                anima.SetTrigger("Shoot1");
                lastShot = Time.time;
                shotType = "normal";
                GetComponent<PickUpAbility>().mainPickUp = "nothing";
                GetComponent<PickUpAbility>().CanPickUp();
                playerStats.TurnOff();
                break;
        }
    }

    
    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        bullet.GetComponent<Bullet>().isBlue = gameObject.GetComponent<PlayerStats>().isBlue;
        bullet.GetComponent<Bullet>().isTeams = isTeams;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);   
    }

    void SpawnBomb()
    {
        GameObject Bomb = Instantiate(Bombprefab, FirePoint.position, FirePoint.rotation);
        Bomb.GetComponent<bombScript>().isBlue = gameObject.GetComponent<PlayerStats>().isBlue;
        Bomb.GetComponent<bombScript>().isTeams = isTeams;
        Rigidbody2D rb = Bomb.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
        PlayerStats playerStats = GetComponent<PlayerStats>();
        
    }
    public void SpawnVest()
    {
    vestDeployed = true;
    GameObject vest = Instantiate(vestPrefab, this.transform.position, this.transform.rotation);
    vest.GetComponent<VestBomb>().isBlue = gameObject.GetComponent<PlayerStats>().isBlue;
    vest.GetComponent<VestBomb>().isTeams = isTeams;
    }

    public void SpawnSmallVest()
    {
        vestDeployed = true;
        GameObject smallVest = Instantiate(smallVestPrefab, this.transform.position, this.transform.rotation);
        smallVest.GetComponent<SmallVestBomb>().isBlue = gameObject.GetComponent<PlayerStats>().isBlue;
        smallVest.GetComponent<SmallVestBomb>().isTeams = isTeams;
    }

    public void SpawnFreeze()
    {
        GameObject freeze = Instantiate(freezePrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rb = freeze.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
        PlayerStats playerStats = GetComponent<PlayerStats>();
     
    }

    void SpawnBounce()
    {
        GameObject bounce = Instantiate(bouncePrefab, FirePoint.position, FirePoint.rotation);
        bounce.GetComponent<BouncingBullet>().isBlue = gameObject.GetComponent<PlayerStats>().isBlue;
        bounce.GetComponent<BouncingBullet>().isTeams = isTeams;
        Rigidbody2D rb = bounce.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
        PlayerStats playerStats = GetComponent<PlayerStats>();  
    }

    IEnumerator Frozen()
    {
        allowShoot = false;
        yield return new WaitForSeconds(freezeDuration);
        allowShoot = true;
    }

    public void StartFreeze()
    {
        StartCoroutine(Frozen());
    }
}
