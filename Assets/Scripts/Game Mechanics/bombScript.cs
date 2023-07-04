using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public float fieldofImpact;

    public float force;

    public LayerMask LayerToHit;
    public LayerMask PlayerToHit;
    public GameObject explodeEffect;
    PlayerStats stats;


    public bool isTeams = false;
    public bool isBlue;

    void explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position,fieldofImpact,LayerToHit);
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, PlayerToHit);
              
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
        foreach(CircleCollider2D obj2 in player)
        {
            obj2.gameObject.GetComponent<PlayerStats>().TakeDamage(100);
        }
        GameObject effect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!isTeams)
            {
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(100);
                explode();
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("Eggsplotion");
            }
            else if (isTeams)
            {
                if (collision.gameObject.GetComponent<PlayerStats>().isBlue && isBlue)
                {
                    Destroy(gameObject);
                    FindObjectOfType<AudioManager>().Play("Eggsplotion");
                    Debug.Log("shot Teammates");
                }
                else if (!collision.gameObject.GetComponent<PlayerStats>().isBlue && !isBlue)
                {
                    Destroy(gameObject);
                    FindObjectOfType<AudioManager>().Play("Eggsplotion");
                    Debug.Log("shot Teammates");
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerStats>().TakeDamage(100);
                    explode();
                    Destroy(gameObject);
                    FindObjectOfType<AudioManager>().Play("Eggsplotion");
                }
            }
        }
        else if(collision.gameObject.tag == "BulletWall" || collision.gameObject.tag == "Wall")
        {
            explode();
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Eggsplotion");
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,fieldofImpact);
    }
}
