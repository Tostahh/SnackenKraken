using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DealDamageGA : GameAction
{
    public static Action DealDmg = delegate { };
    public override void Action()
    {
        DealDmg();
    }
}
