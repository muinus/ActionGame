﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    Slider HPbar;//HPバーのオブジェクト
    GameObject player;
    Animator animator;

    bool isdamage;
    string state;

    public GameObject BattleEvent;

    // Start is called before the first frame update
    void Start()
    {
        HPbar = GetComponentInChildren<Slider>();
        player = GameObject.Find("Player");
        animator = transform.GetComponent<Animator>();
        isdamage = false;

        BattleEvent = GameObject.Find("BattleEventMaster");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する
    }

    void ChangeState()
    {
        //敵から自分への向き
        int drec = System.Math.Sign(this.transform.position.x - player.transform.position.x);
        this.transform.rotation = new Quaternion(0, 90.0f * drec + 90.0f, 0, 0);

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            if (BattleEvent.GetComponent<BattleEventMaster>().GetIsBattleEvent() && (transform.root.gameObject == BattleEvent))
            {
                BattleEvent.GetComponent<BattleEventMaster>().DecreaseEnemyCounter();
            }
            Destroy(this.gameObject);
        }
        else if (isdamage)
        {
            state = "Damage";
            isdamage = false;
        }
        else if (HPbar.value <= 0)
        {
            state = "Die";
        }
        else
        {
            state = "Default";
        }
        
    }

    void ChangeAnimation()
    {
        switch (state)
        {
            case "Die":
                animator.SetBool("isDead", true);
                animator.SetBool("isDamaged", false);
                break;
            case "Damage":
                animator.SetBool("isDamaged", true);
                break;
            default:
                animator.SetBool("isDead", false);
                animator.SetBool("isDamaged", false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player_Attack")
            isdamage = true;
    }

}
