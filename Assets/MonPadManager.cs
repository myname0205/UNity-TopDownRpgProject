using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonPadManager : MonoBehaviour
{
    public static MonPadManager instance;

    public Animator anim;

    [Header("Skeleton")]
    public bool firstTimeSeenSkeleton;
    public Image skeletonIcon;
    public Image skeletonHideDesc;

    [Header("Zombie")]
    public bool firstTimeSeenZombie;
    public Image zombieIcon;
    public Image zombieHideDesc;

    [Header("Crazy Zombie")]
    public bool firstTimeSeenCrazyZombie;
    public Image crazyZombieIcon;
    public Image crazyZombieHideDesc;

    private void Awake()
    {
        instance = this;
        firstTimeSeenSkeleton = false;
        firstTimeSeenZombie = false;
        firstTimeSeenCrazyZombie = false;
    }

    private void Start()
    {
        skeletonIcon.color = Color.black;
        skeletonHideDesc.enabled = true;

        zombieIcon.color = Color.black;
        zombieHideDesc.enabled = true;

        crazyZombieIcon.color = Color.black;
        crazyZombieHideDesc.enabled = true;
    }

    private void Update()
    {
        if(firstTimeSeenSkeleton == true)
        {
            skeletonIcon.color = Color.white;
            skeletonHideDesc.enabled = false;
        }

        if (firstTimeSeenZombie == true)
        {
            zombieIcon.color = Color.white;
            zombieHideDesc.enabled = false;
        }

        if (firstTimeSeenCrazyZombie == true)
        {
            crazyZombieIcon.color = Color.white;
            crazyZombieHideDesc.enabled = false;
        }
    }

    public void Show()
    {
        anim.SetBool("isOpen", true);
    }

    public void Hide()
    {
        anim.SetBool("isOpen", false);
    }

}
