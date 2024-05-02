using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrackenBossAI : AiBoss
{
    [SerializeField] private GameObject TenticleRightPrefab;
    [SerializeField] private GameObject TenticleLeftPrefab;

    [SerializeField] private TenticleAIManager tenticleR;
    [SerializeField] private TenticleAIManager tenticleL;
    private bool Done;
    public override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        CheckHeath();
        if(BossHeath == null)
        {
            Destroy(gameObject);
        }
    }

    public override void CheckHeath()
    {
        base.CheckHeath();
        if(Phase == 2 && !Done)
        {
            Done = true;
            if(tenticleR == null)
            {
                Instantiate(TenticleRightPrefab, transform.position, Quaternion.identity);
            }
            if (tenticleL == null)
            {
                Instantiate(TenticleLeftPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
