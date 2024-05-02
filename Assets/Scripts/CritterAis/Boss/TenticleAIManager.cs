using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleAIManager : MonoBehaviour
{
    private TenticleAi tenticle;
    private KrackenBossAI krackenBoss;
    private void Awake()
    {
        tenticle = GetComponentInChildren<TenticleAi>();
        krackenBoss = FindObjectOfType<KrackenBossAI>();
    }

    private void Update()
    {
        if(tenticle == null)
        {
            Destroy(gameObject);
        }
        if(krackenBoss == null)
        {
            Destroy(gameObject);
        }
    }
}
