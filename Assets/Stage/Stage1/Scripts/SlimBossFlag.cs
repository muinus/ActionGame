using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimBossFlag : MonoBehaviour
{
    GameObject boss;
    bool bossFlag=true;
    Animator animator;
    void Start()
    {
        boss = GameObject.Find("Boss(Clone)");
        animator = transform.root.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isDamaged", false);
        }
    }
}
