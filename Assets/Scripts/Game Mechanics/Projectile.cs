using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private GameObject EggStrikeEffect;

    public GameObject tower;
    public GameObject target;
    public GameObject target1;
    public GameObject target2;

    public float speed = 10f;

    private float towerX;
    private float targetX;
    private float target1X;
    private float target2X;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalcPosition();
        CalcPosition1();
        CalcPosition2();
    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }

    public void CalcPosition()
    {
        towerX = tower.transform.position.x;
        targetX = target.transform.position.x;

        dist = targetX - towerX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(tower.transform.position.y, target.transform.position.y, (nextX - towerX) / dist);

        height = 2 * (nextX - towerX) * (nextX - targetX) /  (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;
    }

    public void CalcPosition1()
    {
        towerX = tower.transform.position.x;
        target1X = target1.transform.position.x;

        dist = target1X - towerX;
        nextX = Mathf.MoveTowards(transform.position.x, target1X, speed * Time.deltaTime);
        baseY = Mathf.Lerp(tower.transform.position.y, target1.transform.position.y, (nextX - towerX) / dist);

        height = 2 * (nextX - towerX) * (nextX - target1X) /  (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;
    }

     public void CalcPosition2()
    {
        towerX = tower.transform.position.x;
        target2X = target2.transform.position.x;

        dist = target2X - towerX;
        nextX = Mathf.MoveTowards(transform.position.x, target2X, speed * Time.deltaTime);
        baseY = Mathf.Lerp(tower.transform.position.y, target2.transform.position.y, (nextX - towerX) / dist);

        height = 2 * (nextX - towerX) * (nextX - target2X) /  (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject EggStrike = Instantiate(EggStrikeEffect, transform.position, Quaternion.identity);
            //Destroy(EggStrike);
            Destroy(gameObject);
        }
    }
}
