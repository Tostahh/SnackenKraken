using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjGA : GameAction
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private bool BRange;
    public override void Action()
    {
        if (BRange)
        {
            Instantiate(Prefab, new Vector3(transform.position.x + Random.Range(-0.7f, 0.7f), transform.position.y + Random.Range(-0.7f, 0.7f), transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(Prefab, transform.position, Quaternion.identity);
        }
    }
}
