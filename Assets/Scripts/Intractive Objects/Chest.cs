using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : Collectable
{
    public Sprite emptyChest;

    public int moneyAmount = 5;

    public string uniqueName => SceneManager.GetActiveScene().name + "/" + gameObject.name;

    protected override void Start()
    {
        base.Start();

        collected = GameManager.instance.openedChests.Contains(uniqueName);

        if (collected == true)
        {
            GetComponent<SpriteRenderer>().sprite = emptyChest;
        }
    }

    protected override void OnCollect()
    {
        GameManager.instance.openedChests.Add(uniqueName);
        GetComponent<SpriteRenderer>().sprite = emptyChest;
        GameManager.instance.ShowText($"+{moneyAmount} coins!", 25, Color.yellow, transform.position, Vector3.up * 50, 1f);
        GameManager.instance.coinGrab.Play();
        GameManager.instance.totalMoney += moneyAmount;
    }
}
