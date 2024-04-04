using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float Dtime;
    private void Start()
    {
        StartCoroutine(Dday());
    }
    private IEnumerator Dday()
    {
        yield return new WaitForSeconds(Dtime);
    }
}
