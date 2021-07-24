using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Public Variables
    public int hitPoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = .2f;

    //Immunity
    protected float immuneTime = 1f;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;

    protected virtual void RecieveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 28, Color.red, transform.position, Vector3.up * 40, .5f);

            if(hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
