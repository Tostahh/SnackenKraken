using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayGA : GameAction
{
    [SerializeField] private ParticleSystem ps;
    public override void Action()
    {
        ps.Play();
    }
}
