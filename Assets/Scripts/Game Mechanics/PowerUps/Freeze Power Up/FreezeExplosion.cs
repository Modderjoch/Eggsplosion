using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeExplosion : MonoBehaviour
{
    public float fieldOfImpact;
    public LayerMask PlayerToHit;
    public GameObject explodeEffect;
    public float percentage;


    void explode()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, PlayerToHit);

        foreach (CircleCollider2D obj2 in player)
        {
            obj2.gameObject.GetComponent<PlayerMovement>().StartFreeze(percentage);
            obj2.gameObject.GetComponent<Shooting>().StartFreeze();
        }
        GameObject effect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Freeze");
        Destroy(effect, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            explode();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "BulletWall" || collision.gameObject.tag == "Wall")
        {
            explode();
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
