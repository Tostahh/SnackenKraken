using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUIManger : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    private void OnEnable()
    {
        DirectionArrow.BossSpawn += BossSpawned;
    }

    private void OnDisable()
    {
        DirectionArrow.BossSpawn -= BossSpawned;
    }

    public void Reset()
    {
        Animator.SetTrigger("Reset");
    }

    public void BossSpawned()
    {
        Animator.SetTrigger("Spawn");
    }
}
