using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected GameObject player;
    protected Animator animator;

    protected string state;
    protected float dis;
    protected float senseDis;
    protected float attackDis;
    protected float attackProb;
    protected int drec;                     //キャラが向いている方向

    protected float runForce;       // 走り始めに加える力
    protected float runSpeed;       // 走っている間の速度
    protected float runThreshold;   // 速度切り替え判定のための閾値
    protected float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数

    protected float time;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        animator = transform.root.GetComponent<Animator>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する
        Move();                 // ③ 状態に応じて移動する
    }

    protected void ChangeState()
    {
        dis = Vector3.Distance(player.transform.position, this.transform.position);
        time += Time.deltaTime;

        if (dis <= attackDis && state == "Idle" && time>3.0f)
        {
            
            if(Random.Range(0.0f, 1.0f)> attackProb)
            state = "Attack";
            time = 0.0f;
        }
        else if (senseDis < dis || dis <= attackDis)
        {
            state = "Idle";
        }
        else if (dis <= senseDis && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            state = "Move";
        }
        else
        {
            state = "Default";
        }

    }

    protected void ChangeAnimation()
    {
        switch (state)
        {
            case "Attack":
                animator.SetBool("isAttack", true);
                break;
            case "Move":
                animator.SetBool("isMove", true);
                break;
            case "Idle":
                animator.SetBool("isMove", false);
                animator.SetBool("isAttack", false);
                break;
            default:
                animator.SetBool("isMove", false);
                animator.SetBool("isAttack", false);
                break;
        }
    }

    protected void Move()
    {
        
        if (state != "Move")
            return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt")||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            return;

        // 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
        float speedX = Mathf.Abs(this.rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            this.rb.AddForce(-transform.right * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
        }
        else
        {
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * -drec * stateEffect, 0, 0);
        }

    }
}
