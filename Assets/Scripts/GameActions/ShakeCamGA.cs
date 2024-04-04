using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamGA : GameAction
{
    public static Action Shake = delegate { };
    public override void Action()
    {
        Shake();
    }
}
