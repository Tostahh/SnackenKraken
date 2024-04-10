using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBoss : MonoBehaviour
{
    [SerializeField] public string BossName;
    [SerializeField] private int Phase;

    private EnemyHeath BossHeath;

    private void Awake()
    {
        BossIntro();
        Phase = 1;
    }
    private void Update()
    {
        CheckHeath();
    }


    private void CheckHeath()
    {
        if(BossHeath.Heath < (BossHeath.MaxHeath/2f))
        {
            Phase = 2;
        }
    }

    private void BossIntro()
    {

    }
}
