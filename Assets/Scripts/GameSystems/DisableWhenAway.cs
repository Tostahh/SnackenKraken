using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenAway : MonoBehaviour
{
    [SerializeField] private bool Ignore;
    private void Start()
    {
        SeaSpawner ss = FindObjectOfType<SeaSpawner>();
        ss.SeaObjects.Add(new Critter { gameObject = this.gameObject, position = transform.position, Ignore = Ignore });
    }
}
