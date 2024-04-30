using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private GameObject Tracker;

    private void Awake()
    {
        GameObject t = Instantiate(Tracker);
    }
}
