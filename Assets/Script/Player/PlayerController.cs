using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    UIBottun UB_up;
    UIBottun UB_down;
    UIBottun UB_left;
    UIBottun UB_right;
    UIBottun UB_move;


    public GameObject driftMagicCircle;

    float jumpForce = 420.0f;       // ジャンプ時に加える力
    float jumpThreshold = 2.0f;    // ジャンプ中か判定するための閾値
    float runForce = 30.0f;       // 走り始めに加える力
    float runSpeed = 0.5f;       // 走っている間の速度
    float runThreshold = 2.0f;   // 速度切り替え判定のための閾値
    bool isDoubleJump = false;   // ダブルジャンプをしているか管理するフラグ
    int key = 0;                 // 左右の入力管理
    int drec = 1;                // 左右の管理
    int dJumpCount = 0;
    int aDriftCount = 0;


    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存
    float stateEffect = 1.0f;       // 状態に応じて横移動速度を変えるための係数
    float MasicEffect = 1.0f;    // スキルにに応じて横移動速度を変えるための係数

    float pressedTime = 0; //2回押しを判断するための時間計測の変数

    KeyCode pressedKey;
    UIBottun UB_clone;
    bool isPress = false;
    bool isDublePress = false;

    void Start()
    {
        Transform dodai = GameObject.Find("PlayerUI").transform.Find("dodai");
        UB_up = dodai.Find("upButton").GetComponent<UIBottun>();
        UB_down = dodai.Find("downButton").GetComponent<UIBottun>();
        UB_left = dodai.Find("leftButton").GetComponent<UIBottun>();
        UB_right = dodai.Find("rightButton").GetComponent<UIBottun>();
        UB_move = dodai.Find("MoveButton").GetComponent<UIBottun>();
        UB_clone = new UIBottun();
        this.rb = GetComponent<Rigidbody2D>();
        rb.sleepMode=RigidbodySleepMode2D.NeverSleep;
        this.animator = GetComponent<Animator>();
        animator.SetBool("isGround", true);
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


        //Debug.Log(UB_move.GetIsPressedDown());
        //Debug.Log(UB_move.GetIsPressedUp());
        //Debug.Log(UB_move.GetIsPressed());

        key = 0;
        isDoubleJump = false;
        if (Input.GetKey(KeyCode.RightArrow) || UB_right.GetIsPressed()) //右
            key = 1;
        if (Input.GetKey(KeyCode.LeftArrow) || UB_left.GetIsPressed()) //左
            key = -1;
        if ((Input.GetKeyDown(KeyCode.Space) || UB_move.GetIsPressedDown()) && (!animator.GetBool("isGround")) && (state != "DJUMP"))//ジャンプボタン
            isDoubleJump = true;
        if (key != 0)
            drec = key;


        isDublePress = false;

        if ((Input.GetKeyDown(pressedKey)||UB_clone.GetIsPressedDown()) && pressedTime < 0.5f && isPress)
        {
            isDublePress = true;
            isPress = false;
            pressedTime = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)|| UB_down.GetIsPressedDown()||
                 Input.GetKeyDown(KeyCode.RightArrow) || UB_right.GetIsPressedDown()||
                 Input.GetKeyDown(KeyCode.LeftArrow) || UB_left.GetIsPressedDown())
        {
            if (UB_up.GetIsPressed())
            {
                UB_clone = UB_up;
                isPress = true;
            }
            else if (UB_down.GetIsPressed())
            {
                UB_clone = UB_down;
                isPress = true;
            }
            else if (UB_left.GetIsPressed())
            {
                UB_clone = UB_left;
                isPress = true;
            }
            else if (UB_right.GetIsPressed())
            {
                UB_clone = UB_right;
                isPress = true;
            }
            else
            {
                foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(code))
                    {
                        pressedKey = code;
                        isPress = true;
                        
                    }
                }
            }
            
            pressedTime = 0;
        }


        if (isPress)
            pressedTime += Time.deltaTime;

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
                state = "RUN_right";
                //待機状態
            }
            else if (key == -1)
            {
                state = "RUN_left";
                //待機状態
            }
            else
            {
                state = "IDLE";
            }

        } // 空中にいる場合
        else
        {

            if (isDublePress && (pressedKey == KeyCode.DownArrow || UB_clone.Equals(UB_down)))
            {
                isDublePress = false;
                state = "ALanding";
            }
            else if ((isDublePress && (pressedKey == KeyCode.RightArrow || UB_clone.Equals(UB_right)
                      || pressedKey == KeyCode.LeftArrow || UB_clone.Equals(UB_left))) &&
                      aDriftCount == 0)
            {
                if (!SkillLearned.GetSkillActive("ADrift"))
                    return;

                AirDrift();
                isDublePress = false;
                state = "ADrift";
                aDriftCount++;
            }
            // ダブルジャンプか
            else if (isDoubleJump && dJumpCount == 0)
            {
                state = "DJUMP";

            }
            // 上昇中
            else if (rb.velocity.y > 0 && !animator.GetBool("isHighSlash") && !animator.GetBool("isShotGun"))
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
        try
        {
            if (!SkillLearned.GetSkillActive(state))
                state = prevState;
        }
        catch { }

        // 状態が変わった場合のみアニメーションを変更する
        if (prevState != state)
        {
            switch (state)
            {
                case "JUMP":
                    animator.SetBool("isJump", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isMove", false);
                    animator.SetBool("isALanding_E", false);
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
                    animator.SetBool("isADrift", false);
                    //dJumpCount = 0;
                    stateEffect = 0.5f;
                    break;
                case "RUN_right":
                    animator.SetBool("isMove", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isJump", false);
                    stateEffect = 1f;
                    transform.localScale = new Vector3(key * 3, 3, 3); // 向きに応じてキャラクターを反転
                    break;
                case "RUN_left":
                    animator.SetBool("isMove", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isJump", false);
                    stateEffect = 1f;
                    transform.localScale = new Vector3(key * 3, 3, 3); // 向きに応じてキャラクターを反転
                    break;
                case "ALanding":
                    animator.SetBool("isALanding_S", true);
                    break;
                case "ADrift":
                    animator.SetBool("isADrift", true);
                    transform.localScale = new Vector3(key * 3, 3, 3); // 向きに応じてキャラクターを反転
                    break;
                default:
                    animator.SetBool("isDJump", false);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isMove", false);
                    animator.SetBool("isJump", false);
                    animator.SetBool("isALanding_S", false);
                    animator.SetBool("isALanding_E", false);
                    animator.SetBool("isADrift", false);
                    stateEffect = 1f;
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく
            prevState = state;

        }
    }

    void Move()
    {

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Move") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("DoubleJump") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            return;

        if (state == "DJUMP" && dJumpCount == 0)
        {
            rb.velocity = new Vector2(0, 2);
            this.rb.AddForce(new Vector3(0, 1, 0) * this.jumpForce * 0.5f);
            isDoubleJump = false;
            dJumpCount = 1;
        }
        // 接地している時にSpaceキー押下でジャンプ
        if (animator.GetBool("isGround"))
        {

            aDriftCount = 0;
            dJumpCount = 0;
            if (Input.GetKeyDown(KeyCode.Space)||UB_move.GetIsPressedDown())
            {
                this.rb.AddForce(new Vector3(0, 1, 0) * this.jumpForce);
                animator.SetBool("isGround", false);
            }
        }

        // 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
        float speedX = Mathf.Abs(this.rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            this.rb.AddForce(transform.right * key * this.runForce * stateEffect * MasicEffect); //未入力の場合は key の値が0になるため移動しない
        }
        else
        {
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * key * stateEffect * MasicEffect, 0, 0);
        }

    }

    void AirDrift()
    {

        rb.velocity = new Vector2(4 * GetDrection(), 0);
        Instantiate(driftMagicCircle, this.transform.position + new Vector3(0.15f * GetDrection(), -0.2f), Quaternion.Euler(0, 90f - GetDrection() * 90f, 0));

    }

    void Ray()
    {
        Ray2D ray = new Ray2D(transform.position, Vector2.down);

        int raydistance = 10;
        int layerMask = 1 << 11;

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, raydistance, layerMask);

        if (hit.collider)
        {
            transform.position -=new Vector3(0, hit.distance-0.65f);
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("AirLanding"))
                animator.SetBool("isALanding_E", true);
        }
        animator.SetBool("isALanding_S", false);
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

    public void FreezPos()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnFreezPos()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void FreezGravity()
    {
        rb.gravityScale = 0;
    }

    public void UnFreezGravity()
    {
        rb.gravityScale = 1.5f;
    }

    public int GetDrection()
    {
        return drec;
    }

    public float GetMasicEffect()
    {
        return MasicEffect;
    }

    public void SetMasicEffect(float MasicEffect)
    {
        this.MasicEffect = MasicEffect;
    }
}
