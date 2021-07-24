using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    public string message;

    public float coolDown = 10f;
    private float lastShout;

    public float yOffset = 0.24f;

    protected override void Start()
    {
        base.Start();
        lastShout = -coolDown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            if (Time.time - lastShout > coolDown)
            {
                lastShout = Time.time;
                GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, yOffset, 0), Vector3.zero, coolDown);
            }
        }
    }
}
