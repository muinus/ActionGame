using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存
    
    bool isComboing;
    bool isAAttack3;

    // Start is called before the first frame update
    void Start()
    {
        this.PC = GetComponent<PlayerController>();
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isComboing = false;
        isAAttack3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Time.timeScale = 0.3f;
        GetInputKey();          // ① 入力を取得
        ChangeState();          // ② 状態を変更する
        ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
    }

    void GetInputKey()
    {

    }

    void ChangeState()
    {

        // 接地している場合
        if (animator.GetBool("isGround"))
        {
            // 1コンボ
            if (Input.GetKeyDown(KeyCode.Z)&&!isComboing)
            {
                state = "ATTACK1";
                isComboing = true;

            }// 2コンボ
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")&&Input.GetKeyDown(KeyCode.Z))
            {
                state = "ATTACK2";

            }// 3コンボ
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")&&Input.GetKeyDown(KeyCode.Z))
            {
                state = "ATTACK3";
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_loop"))
            {
                state = "AirATTACK3E";

                isAAttack3 = false;
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                state = "IDLE";
                isComboing = false;
            }
            
        }
        else//空中にいる場合
        {
            //空中兜割り
            if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow)) && !isAAttack3)
            {
                state = "AirATTACK3S";
                rb.velocity = new Vector2(0, 2);
                isAAttack3 = true;

            }
            // 空中兜割り(落下)
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_loop"))
            {
                rb.AddForce(new Vector2(0, -50));
            }// 空中1コンボ
            else if(Input.GetKeyDown(KeyCode.Z) && !isComboing)
            {
                state = "AirATTACK1";
                isComboing = true;
                rb.velocity = new Vector2(0,2);
                

            }
            // 空中2コンボ
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack1") && Input.GetKeyDown(KeyCode.Z))
            {
                state = "AirATTACK2";
                rb.velocity = new Vector2(0,2);

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                state = "FALL";
                isComboing = false;
                isAAttack3 = false;
            }
        }
    }

    void ChangeAnimation()
    {
        // 状態が変わった場合のみアニメーションを変更する
        //Debug.Log(state);
        if (prevState != state)
        {
            switch (state)
            {
                case "ATTACK1":
                    animator.SetBool("isAttack1", true);
                    animator.SetBool("isAttack2", false);
                    animator.SetBool("isAttack3", false);
                    break;
                case "ATTACK2":
                    animator.SetBool("isAttack2", true);
                    animator.SetBool("isAttack1", false);
                    animator.SetBool("isAttack3", false);
                    break;
                case "ATTACK3":
                    animator.SetBool("isAttack3", true);
                    animator.SetBool("isAttack1", false);
                    animator.SetBool("isAttack2", false);
                    break;
                case "AirATTACK1":
                    animator.SetBool("isAAttack1", true);
                    animator.SetBool("isAAttack2", false);
                    break;
                case "AirATTACK2":
                    animator.SetBool("isAAttack2", true);
                    animator.SetBool("isAAttack1", false);
                    break;
                case "AirATTACK3S":
                    animator.SetBool("isAAttack3_S", true);
                    break;
                case "AirATTACK3E":
                    animator.SetBool("isAAttack3_E", true);
                    animator.SetBool("isAAttack3_S", false);
                    break;
                default:
                    animator.SetBool("isAAttack3_S", false);
                    animator.SetBool("isAAttack3_E", false);
                    animator.SetBool("isAAttack1", false);
                    animator.SetBool("isAAttack2", false);
                    animator.SetBool("isAttack1", false);
                    animator.SetBool("isAttack2", false);
                    animator.SetBool("isAttack3", false);
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく
            prevState = state;

        }
    }
}
