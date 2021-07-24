using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = 1;
    public float chaseLength = 5f;
    private bool isChasing;
    private bool collideWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    public enum typeOfEnemy{Skeleton, Zombie, crazyZombie};
    public typeOfEnemy TypeOfEnemy;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                isChasing = true;
                SetFirstTimes();
            }

            if (isChasing)
            {
                if (!collideWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
                else
                {
                    UpdateMotor(startingPosition - transform.position);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
                isChasing = false;
            }
        }

        //Check for Overlap
        collideWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collideWithPlayer = true;
            }

            //Clean Array
            hits[i] = null;
        }
    }

    void SetFirstTimes()
    {
        if (TypeOfEnemy == typeOfEnemy.Skeleton)
        {
            if (MonPadManager.instance.firstTimeSeenSkeleton == false)
            {
                MonPadManager.instance.firstTimeSeenSkeleton = true;
            }
        }

        if (TypeOfEnemy == typeOfEnemy.Zombie)
        {
            if (MonPadManager.instance.firstTimeSeenZombie == false)
            {
                MonPadManager.instance.firstTimeSeenZombie = true;
            }
        }

        if (TypeOfEnemy == typeOfEnemy.crazyZombie)
        {
            if (MonPadManager.instance.firstTimeSeenCrazyZombie == false)
            {
                MonPadManager.instance.firstTimeSeenCrazyZombie = true;
            }
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.yellow, transform.position, Vector3.up * 40, 1f);
    }
}
