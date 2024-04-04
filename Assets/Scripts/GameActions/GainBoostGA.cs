using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GainBoostGA : GameAction
{
    public static Action GiveBoost = delegate { };
    public override void Action()
    {
        GiveBoost();
    }
}
