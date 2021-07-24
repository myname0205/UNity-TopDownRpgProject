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
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            SceneManager.LoadScene(scenes);
            GameManager.instance.SaveState();
            GameManager.instance.SetArrays();
        }
    }
}
