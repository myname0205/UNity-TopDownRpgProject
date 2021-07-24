using UnityEngine;

public abstract class Collectable : Collidable
{
    public bool collected; 

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player" && !collected)
        {
            collected = true;
            OnCollect();
        }
    }

    protected abstract void OnCollect();
}
