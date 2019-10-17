using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    float jumpForce = 420.0f;       // ジャンプ時に加える力
    float jumpThreshold = 2.0f;    // ジャンプ中か判定するための閾値
    float runForce = 30.0f;       // 走り始めに加える力
    float runSpeed = 0.5f;       // 走っている間の速度
    float runThreshold = 2.0f;   // 速度切り替え判定のための閾値
    bool isDoubleJump = false;   // ダブルジャンプをしているか管理するフラグ
    int key = 0;                 // 左右の入力管理
    int drec = 1;                // 左右の管理
    int dJumpCount = 0;

    
    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存
    float stateEffect = 1.0f;       // 状態に応じて横移動速度を変えるための係数


    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        animator.SetBool("isGround",true);
    }

    void Update()
    {
        GetInputKey();          // ① 入力を取得
        ChangeState();          // ② 状態を変更する
        ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
        Move();                 // ④ 入力に応じて移動する
    }

    void GetInputKey()
    {
        key = 0;
        isDoubleJump = false;
        if (Input.GetKey(KeyCode.RightArrow))
            key = 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            key = -1;
        if (Input.GetKeyDown(KeyCode.Space)&&(!animator.GetBool("isGround")) &&(state != "DJUMP"))
            isDoubleJump = true;
        if (key != 0)
            drec = key;
    }

    void ChangeState()
    {
        // 空中にいるかどうかの判定。上下の速度(rigidbody.velocity)が一定の値を超えている場合、空中とみなす
        if (Mathf.Abs(rb.velocity.y) > jumpThreshold)
        {
            animator.SetBool("isGround", false);
        }

        // 接地している場合
        if (animator.GetBool("isGround"))
        {
            // 走行中
            if (key == 1)
            {
                state = "RUN";
                //待機状態
            }
            else if (key == -1)
            {
                state = "RUN2";
                //待機状態
            }
            else
            {
                state = "IDLE";
            }
            // 空中にいる場合
        }
        else
        {
            // ダブルジャンプか
            if (isDoubleJump&&dJumpCount == 0)
            {
                state = "DJUMP";

            }
            // 上昇中
            else if (rb.velocity.y > 0 && (!animator.GetBool("isHighSlash")))
            {                  
                state = "JUMP";

            }// 下降中
            else if (rb.velocity.y < 0)
            {
                state = "FALL";
            }
        }
    }

    void ChangeAnimation()
    {
        // 状態が変わった場合のみアニメーションを変更する
        if (prevState != state)
        {
            switch (state)
            {
                case "JUMP":
                    animator.SetBool("isJump", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isMove", false);
                    stateEffect = 0.5f;
                    break;
                case "DJUMP":
                    animator.SetBool("isDJump", true);
                    animator.SetBool("isJump", false);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isMove", false);
                    stateEffect = 0.5f;
                    break;
                case "FALL":
                    animator.SetBool("isDJump", false);
                    animator.SetBool("isFall", true);
                    animator.SetBool("isJump", false);
                    animator.SetBool("isMove", false);
                    //dJumpCount = 0;
                    stateEffect = 0.5f;
                    break;
                case "RUN":
                    animator.SetBool("isMove", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isJump", false);
                    stateEffect = 1f;
                    transform.localScale = new Vector3(key*3, 3, 3); // 向きに応じてキャラクターを反転
                    break;
                case "RUN2":
                    animator.SetBool("isMove", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isJump", false);
                    stateEffect = 1f;
                    transform.localScale = new Vector3(key * 3, 3, 3); // 向きに応じてキャラクターを反転
                    break;
                default:
                    animator.SetBool("isDJump",false);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isMove", false);
                    animator.SetBool("isJump", false);
                    stateEffect = 1f;
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく
            prevState = state;
            
        }
    }

    void Move()
    {
        


        if (state == "DJUMP"&&dJumpCount==0)
        {
            //if (rb.velocity.y <= 0)
                rb.velocity=new Vector2(0, 2);
            this.rb.AddForce(new Vector3(0, 1, 0) * this.jumpForce*0.5f);
            isDoubleJump = false;
            dJumpCount = 1;
        }
        // 接地している時にSpaceキー押下でジャンプ
        if (animator.GetBool("isGround"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_end"))
                return;

            dJumpCount = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.rb.AddForce(new Vector3(0, 1, 0) * this.jumpForce);
                animator.SetBool("isGround", false);
            }
        }

        //攻撃時は移動しない
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Move") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("DoubleJump")&&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            return;

        // 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
        float speedX = Mathf.Abs(this.rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            this.rb.AddForce(transform.right * key * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
        }
        else
        {
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
        }

    }

    public void SetRb(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public Rigidbody2D GetRb()
    {
        return rb;
    }

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetState(string state)
    {
        this.state = state;
    }

    public string GetState()
    {
        return state;
    }

    public void OffRb()
    {
        rb.simulated = false;
    }

    public void OnRb()
    {
        rb.simulated = true;
        rb.velocity = new Vector2(0, 0);
    }

    public int GetDrection()
    {
        return drec;
    }
}
