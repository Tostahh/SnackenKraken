using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectGA : GameAction
{
    public override void Action()
    {
        Destroy(gameObject);
    }
}
