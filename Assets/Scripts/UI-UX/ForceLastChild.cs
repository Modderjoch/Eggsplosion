using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLastChild : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.SetAsLastSibling();
    }
}
