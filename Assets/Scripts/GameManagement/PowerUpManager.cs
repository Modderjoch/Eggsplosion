using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float[] percentages;
    public List<GameObject> powerUps;

    public float timeBetweenPowerups;

    public float[] timers;
    private Transform[] emptyPoints;
    public float individualTimer = 2f;
    private bool waitingForSpawn = true;
    private bool setTimer;

    private int lastSpawnPoint;

    private float timer;
    private float amount;
    private void Awake()
    {
        //powerUps = new List<GameObject>();
        GameObject casualGameInfo = GameObject.FindGameObjectWithTag("CasualInfo");
        if (casualGameInfo != null)
        {
            int i = 0; // iterations
            int j = 0; // amount of falses
            foreach (bool b in casualGameInfo.GetComponent<CasualGameInfo>().disableList)
            {
                if (b == false)
                {
                   
                    powerUps.RemoveAt(i - j);
                    Debug.Log("power up " + " was removed");
                    j++;
                }
                //powerUps[i].gameObject.SetActive(b);
               // Debug.Log("power up " + i + "was set to " + b);
                i++;
            }
        }
    }
    private void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            timers[i] = individualTimer;
        }
        amount = 1;
    }
    public void WaitForSpawn()
    {
        if (waitingForSpawn)
        {
            int random = Random.Range(0, spawnPoints.Length);
            if (random != lastSpawnPoint)
            {
                if (spawnPoints[random].transform.childCount == 0)
                {
                    timers[random]--;
                    if (timers[random] == 0)
                    {
                        //Debug.Log("timer ran out!");
                        timers[random] = individualTimer;
                        SpawnRandomPowerUp(random, GetRandomSpawn());
                        lastSpawnPoint = random;
                        waitingForSpawn = false;
                        setTimer = true;
                    }
                }
                else if (!spawnPoints[random].transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    Destroy(spawnPoints[random].transform.GetChild(0).gameObject);

                    timers[random]--;
                    if (timers[random] == 0)
                    {
                        //Debug.Log("timer ran out!");
                        timers[random] = individualTimer;
                        SpawnRandomPowerUp(random, GetRandomSpawn());
                        lastSpawnPoint = random;
                        waitingForSpawn = false;
                        setTimer = true;
                    }
                }
            }
        }
    }
    public void SpawnRandomPowerUp(int index, int powerUpToSpawn)
    {
        Instantiate(powerUps[powerUpToSpawn], spawnPoints[index]);
    }
    private int GetRandomSpawn()
    {
        float random = Random.Range(0, 1f);
        float numforAdding = 0;
        float total = 0;
        for (int i = 0; i < percentages.Count(); i++)
        {
            total += percentages[i];
        }

        for (int i = 0; i < powerUps.Count(); i++)
        {
            if (percentages[i] / total + numforAdding >= random)
            {
                return i;
            }
            else
            {
                numforAdding += percentages[i] / total;
            }
        }
        return 0;
    }

    private void Update()
    {
        WaitForSpawn();
        if (setTimer)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenPowerups)
            {
                waitingForSpawn = true;
                timer = 0;
                setTimer = false;
            }
        }
    }
}

public class PowerUpSpawnPoint : MonoBehaviour
{

    public Transform spawnPoint;
    public float timer;

}