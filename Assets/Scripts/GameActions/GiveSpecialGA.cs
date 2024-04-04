using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSpecialGA : GameAction
{
    public override void Action()
    {
        SeaCritterController Sc = FindObjectOfType<SeaCritterController>();
        Sc.SetSpecial(Random.Range(1,4));
    }
}
