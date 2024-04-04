using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackGA : GameAction
{
    public override void Action()
    {
        Transform obj = FindObjectOfType<SeaCritterController>().gameObject.transform;
        SeaCritterController SC = obj.GetComponent<SeaCritterController>();
        SC.KnockedBack = true;
        StartCoroutine(Knockback(0.3f, 75f, obj, SC));
    }
    public IEnumerator Knockback(float duration, float power, Transform obj, SeaCritterController SC)
    {
        float timer = 0;
        while (timer <= duration)
        {
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.AddForce(direction.normalized * power);
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
        }
        SC.KnockedBack=false;
    }
}
