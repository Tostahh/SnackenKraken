using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AiBoss : MonoBehaviour
{
    public string BossName;
    public int Phase;

    public EnemyHeath BossHeath;
    public Transform target;

    public virtual void Awake()
    {
        BossHeath = GetComponentInChildren<EnemyHeath>();
        target = FindObjectOfType<SeaCritterController>().gameObject.transform;
        Phase = 1;
    }

    public virtual void CheckHeath()
    {
        if (BossHeath.Heath < (BossHeath.MaxHeath / 4f) && Phase != 2)
        {
            BossHeath.Heath = BossHeath.MaxHeath;
            Phase = 2;
        }
    }

    public virtual IEnumerator BossIntro()
    {
        yield return null;
    }
}
