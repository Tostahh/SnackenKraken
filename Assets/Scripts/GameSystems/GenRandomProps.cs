using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenRandomProps : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private List<GameObject> PropPoints;
    [SerializeField] private List<GameObject> PropPrefabs;

    private void Start()
    {
        SpawnProps();
    }
    private void SpawnProps()
    {
        foreach(GameObject pp in PropPoints)
        {
            int Prop = Random.Range(0, PropPrefabs.Count);
            GameObject SpawnedProp = Instantiate(PropPrefabs[Prop],pp.transform.position, Quaternion.identity);
            SpawnedProp.transform.parent = pp.transform;
        }
    }
}
