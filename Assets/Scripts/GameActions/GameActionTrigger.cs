using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionTrigger : MonoBehaviour
{
    [SerializeField] private List<GameAction> Actions;
    [SerializeField] private bool aToggle;
    private bool aActive, aDeActive;

    [SerializeField] private bool Ignore;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (aActive)
            {
                return;
            }
            aActive = true;
            SeaCritterController Sc = collision.transform.GetComponent<SeaCritterController>();
            if (Sc.Power && !Ignore)
            {
                return;
            }
            else
            {
                StartCoroutine(nameof(ExecuteActions));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (aActive)
            {
                return;
            }
            if (aToggle)
            {
                aActive = true;
                aDeActive = true;
                SeaCritterController Sc = collision.transform.GetComponent<SeaCritterController>();
                if (Sc.Power)
                {
                    return;
                }
                else
                {
                    StartCoroutine(nameof(ExecuteActions));
                }
            }
        }
    }
    IEnumerator ExecuteActions()
    {
        foreach (GameAction action in Actions)
        {
            yield return new WaitForSeconds(action.delay);

            if (aDeActive)
            {
                action.DeAction();
            }
            else
            {
                action.Action();
            }
        }
        aActive = false;
        aDeActive = false;
    }
}
