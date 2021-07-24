using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> openedChests;

    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            Destroy(healthBar);

            return;
        }

        instance = this;
        coinGrab = GameObject.Find("CoinGrab").GetComponent<AudioSource>();
        currWeaponDamage = weapon.damagePoint[weapon.weaponLevel];
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //Chests
    public GameObject[] chest1;
    public GameObject[] chest2;
    public GameObject[] chest3;
    public GameObject[] chest4;

    public int numOfChest1;
    public int numOfChest2;
    public int numOfChest3;
    public int numOfChest4;

    //Refrences
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public GameObject healthBar;
    public Animator deathMenuAnim;
    public CharactherMenu cm;

    //Logic
    public int totalMoney;
    public int totalExperience;
    public int currWeaponDamage;

    //SFX
    public AudioSource coinGrab;

    private void Start()
    {
        cm.enabled = true;
    }

    public void SetArrays()
    {
        Debug.Log("SetArrays");

        chest1 = GameObject.FindGameObjectsWithTag("Chests1");
        chest2 = GameObject.FindGameObjectsWithTag("Chests2");
        chest3 = GameObject.FindGameObjectsWithTag("Chests3");
        chest4 = GameObject.FindGameObjectsWithTag("Chests4");
    }

    public void SetDamagePoint()
    {
        if (weapon.weaponLevel <= weapon.damagePoint.Length)
        {
            currWeaponDamage = weapon.damagePoint[weapon.weaponLevel];
        }
        else
        {
            currWeaponDamage = weapon.damagePoint.Length;
        }
    }

    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        //Is weapon max level?
        if(weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if(totalMoney >= weaponPrices[weapon.weaponLevel])
        {
            totalMoney -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Health Bar
    public void OnHitPointChanged()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1 , 1);
    }

    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (totalExperience >= add)
        {
            add += xpTable[r];
            r++;

            //Check MAX Level
            if(r == xpTable.Count)
            {
                return r;
            }
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        totalExperience += xp;
        Debug.Log(currLevel);
        if(currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitPointChanged();
    }

    //On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("Spawn").transform.position;
    }

    //Save States
    /*
     * INT preferedSkin
     * INT money
     * INT experience
     * INT weaponLevel
     */

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += totalMoney.ToString() + "|";
        s += totalExperience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log(s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');


        //Change Player Skin

        //Change player stats
        totalMoney = int.Parse(data[1]);
        totalExperience = int.Parse(data[2]);
        
        //GameManager.instance.player.SetLevel(GetCurrentLevel());

        //Chage weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        PlayerPrefs.DeleteAll();
    }

    //Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("hide");
        SceneManager.LoadScene("BasicDungeon");
        player.Respawn();
    }
}
