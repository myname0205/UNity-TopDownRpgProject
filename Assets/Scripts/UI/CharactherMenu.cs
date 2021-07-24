using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharactherMenu : MonoBehaviour
{
    //Text feilds
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hitpointText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI weaponText;

    //Logic
    private int currentCharactherSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    private void Awake()
    {
        weaponText = GameObject.Find("Sword Text").GetComponent<TextMeshProUGUI>();    
    }

    private void Update()
    {
        UpdateMenu();    
    }

    //Chracter Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharactherSelection++;

            if(currentCharactherSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharactherSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            currentCharactherSelection--;

            if (currentCharactherSelection < 0)
            {
                currentCharactherSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharactherSelection];
        GameManager.instance.player.SwapSprite(currentCharactherSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
            GameManager.instance.SetDamagePoint();
        }
    }

    //Upgrade character information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
        upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }

        //Stats
        hitpointText.text = GameManager.instance.player.hitPoint.ToString() + " / " + GameManager.instance.player.maxHitpoint;
        moneyText.text = GameManager.instance.totalMoney.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        weaponText.text = GameManager.instance.currWeaponDamage.ToString();                          

        //XP
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.totalExperience.ToString() + "total experience points"; //Display Total XP
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currentXpToLevel = GameManager.instance.totalExperience - prevLevelXp;

            float completionRatio = (float)currentXpToLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXpToLevel.ToString() + " / " + diff;
        }

        xpBar.localScale = new Vector3(.5f, 0, 0);

    }
}
