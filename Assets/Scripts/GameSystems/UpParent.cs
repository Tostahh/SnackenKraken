using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpParent : MonoBehaviour
{
    private void Awake()
    {
        gameObject.transform.parent = null;
    }
}
