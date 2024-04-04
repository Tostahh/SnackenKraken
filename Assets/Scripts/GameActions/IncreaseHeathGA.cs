using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHeathGA : GameAction
{
    public static Action Heal = delegate { };
    public override void Action()
    {
        Heal();
    }
}
