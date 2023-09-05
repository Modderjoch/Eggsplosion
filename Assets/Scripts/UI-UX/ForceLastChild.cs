using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLastChild : MonoBehaviour
{
    public Transform parent;

    // Update is called once per frame
    void Update()
    {
        transform.SetAsLastSibling();

        if (parent.childCount == 5)
        {
            Destroy(gameObject);
        }
    }
}
