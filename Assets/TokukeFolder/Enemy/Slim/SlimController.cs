﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimController : MonoBehaviour
{

    Slider HPbar;//HPバーのオブジェクト
    GameObject player;
    public BoxCollider2D col;
    Animator animator;
    

    bool isdamage;
    string state;
    float dis;

    public GameObject BattleEvent;

    float slimDis=8.0f;
    float attackDis = 4.0f;
    float movePower=200.0f;//移動力
    int moveJudge;//移動方向
    bool attackJudge = false;
    Rigidbody2D rb;
   

    // Start is called before the first frame update
    void Start()
    {
        //this.animator = GetComponent<Animator>();
        col.gameObject.SetActive(false);
        HPbar = GetComponentInChildren<Slider>();
        player = GameObject.Find("Player");
        animator = transform.root.GetComponent<Animator>();
        isdamage = false;
        BattleEvent = GameObject.Find("BattleEventMaster");

        rb = this.GetComponent<Rigidbody2D>();
        animator.SetBool("isMove", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Apos = player.transform.position;
        Vector3 Bpos = this.transform.position;
        dis = Vector3.Distance(Apos, Bpos);

        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する

    }

    void ChangeState()
    {
        //敵から自分への向き
        int drec = System.Math.Sign(this.transform.position.x - player.transform.position.x);
        this.transform.rotation = new Quaternion(0, 90.0f * drec + 90.0f, 0, 0);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("slim_dead"))
        {
            Destroy(this.gameObject);
            if (BattleEvent.GetComponent<BattleEventMasterStage1>().GetIsBattleEvent())
            {
                BattleEvent.GetComponent<BattleEventMasterStage1>().DecreaseEnemyCounter();
            }
        }
        else if (isdamage)
        {
            state = "Damage";
            isdamage = false;
            //Debug.Log(isdamage);
        }
        else if (HPbar.value <= 0)
        {
            state = "Die";
        }
        else if (attackJudge==true)
        {
            state = "Move";
            attackJudge = false;
        }
        else if (dis <= attackDis)
        {
            state = "Attack";
        }
        else if (dis<=slimDis)
        {
            state = "Move";
        }
        else if (dis > slimDis)
        {
            state = "Idle";
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
            case "Move":
                animator.SetBool("isAttack", false);
                animator.SetBool("isMove", true);
                
                break;
            case "Idle":
                animator.SetBool("isMove", false);
                break;
            case "Attack":
                animator.SetBool("isCancel", true);
                animator.SetBool("isAttack", true);
                break;
            default:
                animator.SetBool("isDead", false);
                animator.SetBool("isDamaged", false);
                
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player_Attack") {
            isdamage = true;
            
        }
    }
    void SlimMovement()
    {
        if (player.transform.position.x>= this.transform.position.x)
        {
            moveJudge = 1;
        }else if(player.transform.position.x < this.transform.position.x)
        {
            moveJudge = -1;
        }
        Vector2 force = new Vector2(movePower*moveJudge, 0.0f);
        //Debug.Log(force);
        rb.AddForce(force);
        
    }
    void SlimAttack()
    {
        
        if (player.transform.position.x >= this.transform.position.x)
        {
            moveJudge = 1;
        }
        else if (player.transform.position.x < this.transform.position.x)
        {
            moveJudge = -1;
        }
        Vector2 force = new Vector2(400f * moveJudge, 200f);
        
        rb.AddForce(force);
       // Debug.Log(force);
        attackJudge = true;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("isAttack", false);
            animator.SetBool("isCancel", false);
            col.gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
        }

    }
    void Dead()
    {
        Destroy(this.gameObject);
    }
    void AttackActivateTrue()
    {
        col.gameObject.SetActive(true);
    }
   void Yarare()
    {
        isdamage = false;
        animator.SetBool("isDamaged", false);
    }
}
