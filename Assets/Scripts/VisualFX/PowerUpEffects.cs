using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffects : MonoBehaviour
{
    public GameObject speedTrail;
    public GameObject dashTrail;
    public GameObject rapidEffect;
    public void SpeedTrailOn()
    {
        speedTrail.SetActive(true);
    }

    public void SpeedTrailOff()
    {
        speedTrail.SetActive(false);
    }

    public void DashTrailOn()
    {
        dashTrail.SetActive(true);
    }

    public void DashTrailOff()
    {
        dashTrail.SetActive(false);
    }

    public void RapidEffectOn()
    {
        rapidEffect.SetActive(true);
    }

    public void RapidEffectOff()
    {
        rapidEffect.SetActive(false);
    }

}
