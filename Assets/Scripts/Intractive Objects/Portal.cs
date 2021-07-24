using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public static Portal instance;

    public string scenes;

    public Scene currentScene;

    protected void Awake()
    {
        instance = this;    
    }

    protected override void Start()
    {
        base.Start();

        currentScene = SceneManager.GetActiveScene();

        Chest.instance.HideChests();
        Chest.instance.ShowChests();

    }

    protected override void Update()
    {
        base.Update();
        GameManager.instance.SetArrays();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            StartCoroutine(Wait());

            SceneManager.LoadScene(scenes);
            GameManager.instance.SaveState();

            GameManager.instance.SetArrays();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        Chest.instance.HideChests();
    }
}
