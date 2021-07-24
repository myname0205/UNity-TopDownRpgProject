using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Mover
{
    private SpriteRenderer playerSpriteRenderer;
    private bool isAlive = true;

    private bool doneWaiting;

    public GameObject healthBar;

    protected override void Start()
    {
        base.Start();
        healthBar.SetActive(true);
        doneWaiting = false;
        StartCoroutine(WaitFourSec());
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void RecieveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.RecieveDamage(dmg);
        GameManager.instance.OnHitPointChanged();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(isAlive)
            if(doneWaiting)
                UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinID)
    {
        playerSpriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp()
    {
        maxHitpoint ++;
        maxHitpoint ++;
        hitPoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount)
    {
        if(hitPoint == maxHitpoint)
        {
            return;
        }

        hitPoint += healingAmount;
        
        if(hitPoint > maxHitpoint)
        {
            hitPoint = maxHitpoint;
        }
        
        GameManager.instance.OnHitPointChanged();

        if(showText)
            GameManager.instance.ShowText("+" + healingAmount.ToString(), 25, Color.green, transform.position, Vector3.up * 50, 1f);

        showText = true;
    }

    public bool showText;

    protected override void Death()
    {
        GameManager.instance.deathMenuAnim.SetTrigger("show");
        isAlive = false;
    }

    public void Respawn()
    {
        showText = false;
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    IEnumerator WaitFourSec()
    {
        doneWaiting = false;
        yield return new WaitForSeconds(4.1f);
        doneWaiting = true;
    }
}
