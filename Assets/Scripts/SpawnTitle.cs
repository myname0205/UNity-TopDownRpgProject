using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnTitle : Collidable
{
    public TextMeshProUGUI dungeonText;

    public string currentDungeonLevel;

    public Animator textAnim;

    public bool firstTime;

    protected override void Start()
    {
        base.Start();
        dungeonText.enabled = false;
        firstTime = true;
        //StartCoroutine(WaitSec());
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            if(firstTime == true)
            {
                firstTime = false;
                dungeonText.text = currentDungeonLevel;
                dungeonText.enabled = true;
                textAnim.SetBool("isShow", true);
                StartCoroutine(WaitTwoSec());
            }
        }
    }

    IEnumerator WaitTwoSec()
    {
        yield return new WaitForSeconds(4f);
        textAnim.SetBool("isShow", false);
        dungeonText.enabled = false;

    }
}
