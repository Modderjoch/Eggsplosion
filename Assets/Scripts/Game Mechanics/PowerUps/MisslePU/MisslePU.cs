using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePU : MonoBehaviour
{
    public GameObject missle;

    public GameObject missle1;

    public Transform misslePoint;

    public Transform misslePoint1;

    public float duration = 3f;

    public Transform target;

    public Transform target1;


    public float speed = 5f;
    public float rotateSpeed = 100f;
   

    public GameObject HomingEggsplosion;

    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        GameObject sendMissle = Instantiate(missle, misslePoint.position, misslePoint.rotation);

        GameObject sendMissle1 = Instantiate(missle1, misslePoint1.position, misslePoint1.rotation);


        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    //  void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Bomba"))
    //     {
    //         Destroy(gameObject);
    //         Instantiate(HomingEggsplosion, transform.position, transform.rotation);       
    //     }
    // }
}
