using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAbility : MonoBehaviour
{
    public bool ablePickUp;
    public string mainPickUp;
    public int rapidCount;
    public int dashCount;
    public int speedCount;

    private void Awake()
    {
        mainPickUp = "nothing";
        ablePickUp = true;
        rapidCount = 0;
        dashCount = 0;
        speedCount = 0;
    }

    public void CanPickUp()
    {
        ablePickUp = true;
    }

    public void CannotPickUp()
    {
        ablePickUp = false;
    }
}
