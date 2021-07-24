using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class Chest : Collectable
{
    public Sprite emptyChest;

    public int moneyAmount = 5;

    public Scene currentScene;

    public static Chest instance;

    private static GameObject instanceChest;
    
    public static HashSet<Chest> openedChests;

  

    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();

        currentScene = SceneManager.GetActiveScene();

        ShowChests();

        if (collected == true)
        {
            GetComponent<SpriteRenderer>().sprite = emptyChest;
        }
        DeleteDuplicates();
    }

    protected override void Update()
    {
        base.Update();
        
        if(GameManager.instance.openedChests.Contains(this.gameObject) && collected == false)
        {
            collected = true;
        }
        
        if(!GameManager.instance.openedChests.Contains(this.gameObject) && collected == true)
        {
            GameManager.instance.openedChests.Add(this.gameObject);
        }

        print(GameObject.FindGameObjectsWithTag("Chests1").Length);

    }

    public void DeleteDuplicates()
    {
        if(currentScene.name == "BasicDungeon")
        {
            int numOfChestInScene1 = GameObject.FindGameObjectsWithTag("Chests1").Length;
            if(numOfChestInScene1 != GameManager.instance.numOfChest1)
            {      
                Destroy(this.gameObject);
            }
        }
    }
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GameManager.instance.openedChests.Add(this.gameObject);
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+" + moneyAmount + " coins!", 25, Color.yellow, transform.position, Vector3.up * 50, 1f);
            GameManager.instance.coinGrab.Play();
            GameManager.instance.totalMoney += moneyAmount;
        }
    }

    public void HideChests()
    {
   
        print("hide1");
        if (currentScene.name == "Dungeon1" || currentScene.name == "Dungeon2" || currentScene.name == "Dungeon3" || currentScene.name == "Dungeon4")
        {
            print("hide2");
            if (GameManager.instance.chest1.Length > 0)
            {
                print("hide3");
                for (int i = 0; i < GameManager.instance.chest1.Length; i++)
                {
                    print("hide4");
                    GameManager.instance.chest1[i].SetActive(false);
                }
            }
        }

        if (currentScene.name == "BasicDungeon" || currentScene.name == "Dungeon2" || currentScene.name == "Dungeon3" || currentScene.name == "Dungeon4")
        {
            if (GameManager.instance.chest2.Length > 0)
            {
                for (int i = 0; i < GameManager.instance.chest2.Length; i++)
                {
                    GameManager.instance.chest2[i].SetActive(false);
                }
            }
        }

        if (currentScene.name == "BasicDungeon" || currentScene.name == "Dungeon1" || currentScene.name == "Dungeon3" || currentScene.name == "Dungeon4")
        {
            if (GameManager.instance.chest3.Length > 0)
            {
                for (int i = 0; i < GameManager.instance.chest3.Length; i++)
                {
                    GameManager.instance.chest3[i].SetActive(false);
                }
            }
        }

        if (currentScene.name == "BasicDungeon" || currentScene.name == "Dungeon1" || currentScene.name == "Dungeon2" || currentScene.name == "Dungeon4")
        {
            if (GameManager.instance.chest4.Length > 0)
            {
                for (int i = 0; i < GameManager.instance.chest4.Length; i++)
                {
                    GameManager.instance.chest4[i].SetActive(false);
                }
            }
        }

        StartCoroutine(Wait());

    }

    public void ShowChests()
    {
        if (currentScene.name == "BasicDungeon")
        {
            for (int i = 0; i < GameManager.instance.chest1.Length; i++)
            {
                GameManager.instance.chest1[i].SetActive(true);
                print("lol");
            }
        }

        if (currentScene.name == "Dungeon1")
        {
            for (int i = 0; i < GameManager.instance.chest2.Length; i++)
            {
                GameManager.instance.chest2[i].SetActive(true);
            }
        }

        if (currentScene.name == "Dungeon2")
        {
            for (int i = 0; i < GameManager.instance.chest3.Length; i++)
            {
                GameManager.instance.chest3[i].SetActive(true);
            }
        }

        if (currentScene.name == "Dungeon3")
        {
            for (int i = 0; i < GameManager.instance.chest4.Length; i++)
            {
                GameManager.instance.chest4[i].SetActive(true);
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        ShowChests();
    }
}
