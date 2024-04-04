using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorGA : GameAction
{
    [Header("Refs")]
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer Sr;

    [SerializeField] private bool Bflash;
    private void Awake()
    {
        if (Sr == null)
        {
            Sr = FindObjectOfType<SeaCritterController>().gameObject.GetComponentInChildren<SpriteRenderer>();
        }
    }
    public override void Action()
    {
        SeaCritterController P = Sr.gameObject.GetComponentInParent<SeaCritterController>();
        if (!P.Power)
        {
            Sr.color = color;
            if (Bflash)
            {
                P.ResetColor();
            }
        }
    }
}
