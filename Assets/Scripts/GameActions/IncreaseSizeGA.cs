using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSizeGA : GameAction
{
    [SerializeField] private Transform target;
    private GameSystems Us;
    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<SeaCritterController>().gameObject.transform;
            Us = FindFirstObjectByType<GameSystems>();
        }
    }
    public override void Action()
    {
        if(Us.SizeNumb+1 != 11)
        {
            target.localScale += new Vector3(0.15f, 0.15f, 0.15f);
        }
    }
}
