using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string scenes;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            SceneManager.LoadScene(scenes);
            GameManager.instance.SaveState();
        }
    }
}
