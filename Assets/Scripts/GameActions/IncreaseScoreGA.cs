using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IncreaseScoreGA : GameAction
{
    public static Action IncreaseScore = delegate { };
    public override void Action()
    {
        IncreaseScore();
    }
}
