using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetSelect : MonoBehaviour, ICancelHandler
{
    public void OnCancel(BaseEventData data)
    {
        Debug.Log("OnCancel called.");
    }
}
