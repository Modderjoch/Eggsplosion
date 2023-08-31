using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{

    public Bullet(bool _isBlue, bool _isTeams)
    {
        isBlue = _isBlue;
        isTeams = _isTeams;
    }
    
    public Rigidbody2D rb;
    Vector3 lastVel;
    [SerializeField]
    private GameObject hitEffect;
    [SerializeField]
    private GameObject killPlayer;

    public int damage = 25;

    public bool isTeams = false;
    public bool isBlue;

    //public string shotFrom { get; set; }

    public PlayerConfiguration WhoShotMe;

    private void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastVel = rb.velocity;
    }
  
    // 2 Methods for preventing code repition
    public void PlayerTakeDmg(Collision2D playerCollider)
    {
        playerCollider.gameObject.GetComponent<PlayerStats>().TakeDamage(damage,WhoShotMe);
        Debug.Log("shot other player");
        FindObjectOfType<AudioManager>().Play("Hurt");
    }

    public void PlayParticleFX(GameObject prefabToPlay)
    {
        GameObject effect = Instantiate(prefabToPlay, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isTeams)
            {
                PlayerTakeDmg(collision);
                PlayParticleFX(killPlayer);
            }
            else if (isTeams)
            {
                if (collision.gameObject.GetComponent<PlayerStats>().isBlue && isBlue)
                {
                    Destroy(gameObject);
                    Debug.Log("shot teammate");
                }
                else if (!collision.gameObject.GetComponent<PlayerStats>().isBlue && isBlue)
                {
                    PlayerTakeDmg(collision);
                    PlayParticleFX(killPlayer);
                }
                else if (!collision.gameObject.GetComponent<PlayerStats>().isBlue && !isBlue)
                {
                    Destroy(gameObject);
                    Debug.Log("shot teammate");
                }
                else if (collision.gameObject.GetComponent<PlayerStats>().isBlue && !isBlue)
                {
                    PlayerTakeDmg(collision);
                    PlayParticleFX(killPlayer);
                }
            }
        }

        else if(collision.gameObject.tag == "BulletWall" || collision.gameObject.tag == "Wall")
        {       
            var speed = lastVel.magnitude;
            var direction = Vector2.Reflect(lastVel.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);

            PlayParticleFX(hitEffect);
            FindObjectOfType<AudioManager>().Play("CrackEgg");
        }


        else if (collision.gameObject.tag == "Bomba")
        {
            PlayParticleFX(hitEffect);
            FindObjectOfType<AudioManager>().Play("CrackEgg");
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            //Unlock achievement for shooting other players bullet
            AchievementManager.instance.UnlockAchi(1);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("EggHit");
        }
    }
}
