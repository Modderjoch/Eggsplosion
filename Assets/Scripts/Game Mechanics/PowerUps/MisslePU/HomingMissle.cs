using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissle : MonoBehaviour
{
    
    public float speed = 10f;
    public float rotateSpeed = 100f;
    [HideInInspector]
    public bool missleOn = false;

    public GameObject HomingEggsplosion;
    public GameObject targetGameObject;
    private Rigidbody2D rb;

    public float fieldOfImpact;
    public LayerMask PlayerToHit;
    public int damage = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {   
        Launch();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomba"))
        {
            FindObjectOfType<AudioManager>().Play("MissleExplode");
            Explode();
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, PlayerToHit);

        foreach (CircleCollider2D obj2 in player)
        {
            obj2.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
        GameObject effect = Instantiate(HomingEggsplosion, transform.position, transform.rotation);
        Destroy(effect, 1f);
    }

    public void Launch()
    {    
        Vector2 direction = (Vector2)targetGameObject.transform.position - rb.position;  

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed; 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
