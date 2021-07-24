using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    public int coins;

    public float randomNum;

    public AudioSource woodenCrateBreak;

    private void Start()
    {
        woodenCrateBreak = GameObject.Find("WoodenCrateBreak").GetComponent<AudioSource>();    
    }

    
    protected override void Death()
    {
        randomNum = Random.Range(1, 8);

        woodenCrateBreak.Play();

        if(randomNum <= 3)
        {
            GameManager.instance.totalMoney += coins;
            GameManager.instance.ShowText("+" + coins.ToString() + " coins", 25, Color.yellow, transform.position, Vector3.up * 40, 1);
            GameManager.instance.coinGrab.Play();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
