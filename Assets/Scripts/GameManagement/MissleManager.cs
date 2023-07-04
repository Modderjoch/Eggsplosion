using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleManager : MonoBehaviour
{
    public Transform[] targetPoints;
    public GameObject missle;
    private bool missleOn = true;
    public float timeStart;
    public Animator anim;
    public float timeAwake;
    public GameObject alertText;
    public Camera playerCam;
    private bool soundOn;
    [SerializeField]
    private float timeBetween = 20f;
    private float timeCount;

    private void Update()
    {
        
        timeStart -= Time.deltaTime;

        if ((timeStart + timeCount) < Time.deltaTime)
        {
            if (!soundOn)
            {
                StartCoroutine(MissleLaunch());
                anim.SetBool("Awake", true);
                SpawnMissle();
               // missleOn = false;
                timeCount = timeCount + timeBetween;
                soundOn = false;
            }

        }
    }


    IEnumerator MissleLaunch()
    {
        FindObjectOfType<AudioManager>().Play("MissleAnnouncement");
        soundOn = true;
        GameObject screenText = Instantiate(alertText, playerCam.transform);
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioManager>().Play("MissleLaunch");
        yield return new WaitForSeconds(6.5f);
        anim.SetBool("Awake", false);
    }

    private void Start()
    {
        anim.SetBool("Awake", false);
    }

    private void Awake()
    {
        soundOn = false;
        timeCount = 0;
    }

    public void SpawnMissle()
    {
        if (missleOn)
        {
            foreach (Transform t in targetPoints)
            {
                t.gameObject.SetActive(true);
                GameObject newMissile = Instantiate(missle, this.transform);
                newMissile.GetComponent<HomingMissle>().SetTarget(t.gameObject);
            }
        }  
    }
}
