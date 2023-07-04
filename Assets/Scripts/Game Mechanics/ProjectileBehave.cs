using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehave : MonoBehaviour
{

    public float speed = 4;
    public Vector3 LaunchOffset;
    public bool Thrown;

    void Start()
    {
        if (Thrown)
        {
        var direction = -transform.right + Vector3.up;
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
    transform.Translate(LaunchOffset);

    Destroy(gameObject, 5);

    }

    void Update()
    {
        if(!Thrown)
        {
        transform.position += -transform.right * speed * Time.deltaTime; 
        }       
    }
}
