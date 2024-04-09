using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAreana : MonoBehaviour
{
    public static Action Startboss = delegate { };
    public static Action StopBoss = delegate { };

    [SerializeField] private GameObject BossPrefab;
    [SerializeField] private Transform SpawnPos;

    public GameObject Boss;

    private void Update()
    {
        if(!Boss)
        {
            BossStop();
        }
    }

    private void Start()
    {
        BossGo();
    }

    private void BossGo()
    {
        Boss = Instantiate(BossPrefab, SpawnPos);
        Startboss();
    }

    private void BossStop()
    {
        Destroy(gameObject);
        StopBoss();
    }
}
