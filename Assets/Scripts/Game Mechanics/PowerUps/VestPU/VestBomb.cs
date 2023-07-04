using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VestBomb : MonoBehaviour
{
    public float fieldofImpact;
    public LayerMask PlayerToHit;
    public GameObject explodeEffect;
    public int damagePlayer = 10;
    public int damageEnemy = 100;

    public bool isTeams = false;
    public bool isBlue;

    private void Awake()
    {
         ExplodeBig();
    }
    void ExplodeBig()
    {
        
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, PlayerToHit);

        foreach (CircleCollider2D obj2 in player)
        {         
            Shooting shooting = obj2.GetComponent<Shooting>();
            if (!isTeams)
            {
                if (shooting.vestDeployed)
                {
                    obj2.gameObject.GetComponent<PlayerStats>().TakeDamage(damagePlayer);
                }
                else
                {
                    obj2.gameObject.GetComponent<PlayerStats>().TakeDamage(damageEnemy);
                }
            }
            else if (isTeams)
            {
                if (shooting.vestDeployed)
                {
                    obj2.gameObject.GetComponent<PlayerStats>().TakeDamage(damagePlayer);
                }
                else
                {
                    if (obj2.gameObject.GetComponent<PlayerStats>().isBlue && isBlue)
                    {
                        Destroy(gameObject);
                    }
                    else if (!obj2.gameObject.GetComponent<PlayerStats>().isBlue && !isBlue)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        obj2.gameObject.GetComponent<PlayerStats>().TakeDamage(damageEnemy);
                    }
                }
            }
        }
        FindObjectOfType<AudioManager>().Play("VestBoom");
        GameObject effect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofImpact);
    }
}
