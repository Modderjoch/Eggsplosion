using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class PlayerStats : MonoBehaviour
{
    //Damage Thresholds
    [SerializeField]
    private int slightDamageThreshold;
    [SerializeField]
    private int heavyDamageThreshold;
    //Animator
    [SerializeField]
    private Animator animator;
    //HealthState Animator
    [SerializeField]
    private AnimatorOverrideController animatorOverrideController;
    [SerializeField]
    private SpriteAnimationManager animationManager;
    [SerializeField]
    private int HP = 100;
    [SerializeField]
    private int MaxHP = 100;
    [SerializeField]
    private int currentHealth;

    public HealthBar healthBar;

    public SpriteRenderer sprite;
    public TextMeshPro playerNameText;
    public int score { get; set; }
    public int ID;
    private int colourID;
    public string controlscheme;
   
    public GameObject player;
    public Animator anim;
    public GameObject myPrefab;

    public AudioClip EggSploded;
    public int num= 100;

    public bool isBlue = true;
    public bool isTeams = false;
    public bool isCasual = false;
    Camera cam;

    LevelManagerScript level;

    public PlayerConfiguration playerConfig; //Player Config that will be assigned to this individual playerstats instance (player)

    public Vector3 scale;
    public Vector3 spawnPos;
    private bool scored = false;

    //UI Icons PowerUps
    public GameObject eggsplosivePU;
    public GameObject bouncePU;
    public GameObject TimerVest;
    public GameObject TimerVestSpeed;
    public GameObject TimerVestWallDash;
    public GameObject TimerVestRapidFire;
    public GameObject freezePU;
    public GameObject cirlclePrefab;
    public GameObject speedPU;
    public GameObject walldashPU;
    public GameObject rapidfirePU;

    //Bools for turning on powerups
    public string uiInfo = "nothing";
    
    public GameObject[] KillEffects;

    public bool activateTimer = false;

    //Who is the last player that hit me? (for killscore)
    public PlayerConfiguration LastPlayerThatHitMe;

    public string lastBulletTypeThatHitMe;

    private void Awake()
    {
        level = FindObjectOfType<LevelManagerScript>();
        cam = FindObjectOfType<Camera>();
        
    }

    void Start()
    {
        currentHealth = MaxHP;
        healthBar.SetMaxHealth(MaxHP);
        colourID = PlayerSpriteColour();
    }

    public void TakeDamage(int damage, PlayerConfiguration playerThatHitMe)
    {        
        HP = HP - damage; 
        currentHealth -= damage; 
        healthBar.SetHealth(HP);
        LastPlayerThatHitMe = playerThatHitMe;
        StartCoroutine(FlashRed());
    }

    public int PlayerSpriteColour()
    {
        int id;
        switch (playerConfig.playerColour)
        {
                case 0://Blue
                    id = 0;
                break;
                case 1://Red
                    id = 1;
                break;
                case 2://Purple
                    id = 2;
                break;
                case 3://Green
                    id = 3;
                break;
                default:
                    id = 0; 
                break;
        }

        return id;
    }

    public void GetHealth(int health)
    {
            HP = HP + health;
            currentHealth += health;
            if (HP > 100)
            {
                currentHealth = 100;
                HP = 100;
            }
            healthBar.SetHealth(HP);
    }
    public void VictoryDance(PlayerConfiguration playerConfig)
    {
        //playerConfig.playerInput.DeactivateInput();
        anim.SetBool("Dancing", true);
    }

  


    void FixedUpdate()
    {
        if(HP > slightDamageThreshold)
        {
            animator.runtimeAnimatorController = animationManager.animatorOverrideControllerNoDamage[colourID];
        }
        else if(HP < slightDamageThreshold && HP > heavyDamageThreshold)
        {
            animator.runtimeAnimatorController = animationManager.animatorOverrideControllerSlightlyDmg[colourID];
        }
        else if (HP < heavyDamageThreshold)
        {
            animator.runtimeAnimatorController = animationManager.animatorOverrideControllersHeavyDmg[colourID];
        }
        if (HP <= 0)
        {
            Instantiate(KillEffects[UnityEngine.Random.Range(0,6)], new Vector3(player.transform.position.x, player.transform.position.y, -1), Quaternion.identity);
            level.UpdateAmountOfPlayers(1);
            Die();
            
        }
        else if (level.GetAmountOfPlayers() == 1 && scored == false && !isTeams)
        {
            score+=1;
            playerConfig.playerScore = score;
            scored = true;
        }
        else if (HP >= 0)
        {
            playerConfig.isAlive=true;
        }


        switch (uiInfo)
        {
            case "nothing":
                break;
            case "nade":
                eggsplosivePU.SetActive(true);
                uiInfo = "nothing";
                break;
            case "bounce":
                bouncePU.SetActive(true);
                uiInfo = "nothing";
                break;
            case "freeze":
                freezePU.SetActive(true);
                uiInfo = "nothing";
                break;
            case "vest":
                TimerVest.SetActive(true);
                uiInfo = "nothing";
                break;
            case "speed":
                //speedPU.SetActive(true);
                uiInfo = "nothing";
                break;
            case "walldash":
                //walldashPU.SetActive(true);
                uiInfo = "nothing";
                break;
            case "rapidfire":
                //rapidfirePU.SetActive(true);
                uiInfo = "nothing";
                break;

        }
    }

    public void TurnOff()
    {
        TimerVest.SetActive(false);
        freezePU.SetActive(false);
        bouncePU.SetActive(false);
        eggsplosivePU.SetActive(false);
        speedPU.SetActive(false);
        rapidfirePU.SetActive(false);
        walldashPU.SetActive(false);
        TimerVestSpeed.SetActive(false);
        TimerVestWallDash.SetActive(false);
        TimerVestRapidFire.SetActive(false);
    }

    public void Die()
    {

        // Instantiate(myPrefab, new Vector3(player.transform.position.x, player.transform.position.y, -1), Quaternion.identity);
        Invoke("KillPopUp", 5);
        AudioSource.PlayClipAtPoint(EggSploded, transform.position);
        anim.SetBool("Death", true);
        playerConfig.isAlive = false;
        if (LastPlayerThatHitMe != null && LastPlayerThatHitMe != playerConfig)
        {
            LastPlayerThatHitMe.killAmount = LastPlayerThatHitMe.killAmount + 1;
            if (lastBulletTypeThatHitMe == "bomb")
            {
             // give other player a special bomb point to count towards achi has to be 3 to get it in a single scene (round)
            }
        }
        else if(LastPlayerThatHitMe != null && LastPlayerThatHitMe == playerConfig)
        {
            //Achievement for killing yourself with bounce egg
            if (lastBulletTypeThatHitMe == "bounce" && playerConfig.playerIndex == 0)
            {
                AchievementManager.instance.UnlockAchi(2);
            }
        }
        if (isCasual)
        {
            Respawn();
        }
        else
        {
            cam.GetComponent<MultiplePlayerCamera>().targets.Remove(this.transform);
            Destroy(player);
        }    
        
    }

    public void Respawn()
    {
        HP = MaxHP;
        playerConfig.isAlive = true;
        player.transform.position = spawnPos;
        GetHealth(HP);
    }

    public void KillPopUp()
    {
        Debug.Log("Perfect");
        //Destroy(myPrefab);
    }
    
    public void AssignPlayerConfig(PlayerConfiguration config)
    {
        playerConfig = config;
        playerNameText.text = config.playerName;
        ID = playerConfig.playerIndex;
        score = playerConfig.playerScore;
        controlscheme = config.playerInput.currentControlScheme;
    }
    public IEnumerator FlashRed()
    {
        Color hitColor = new Vector4(1f, 0f, 0f, 0.6f);
        sprite.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    public void TurnOnCircle()
    {
        cirlclePrefab.transform.localScale = scale;
        cirlclePrefab.SetActive(true);
    }

    public void TurnOffCircle()
    {
        cirlclePrefab.SetActive(false);
    }
    
}
