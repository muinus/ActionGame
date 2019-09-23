﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_ground : MonoBehaviour
{

    GameObject Player;
    Animator animator;

    private void Start()
    {
        Player = transform.parent.gameObject;
        animator=Player.GetComponent<Animator>();
    }

    //着地判定
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            if (!animator.GetBool("isGround"))
            {
                animator.SetBool("isGround", true);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!animator.GetBool("isGround"))
            {
                animator.SetBool("isGround", true);
            }
        }
    }

}