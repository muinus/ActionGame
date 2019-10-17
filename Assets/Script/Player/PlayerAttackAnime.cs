using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;

    public GameObject thrownSword;

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
        //Time.timeScale = 0.1f;
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
            //上攻撃
            if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.UpArrow)))
            {
                
                state = "HighSlash";
            }
            //横攻撃
            else if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftArrow))||
                (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.RightArrow)))
            {
                
                state = "ThrowSword";
            }
            else if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow)))
            {
                //state = "ThrowSword";
            }
            // 1コンボ
            else if (Input.GetKeyDown(KeyCode.Z)&&!isComboing)
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
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirRaid"))
            {
                state = "IDLE";
                rb.velocity = new Vector2(0,0);

            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                state = "IDLE";
                isComboing = false;
            }
            
        }
        else//空中にいる場合
        {
            //
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirHighSlash_loop") &&rb.velocity.y < 0.5f)
            {
                state = "AHighSlashE";
                return;
            }

            //空中上攻撃
            if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.UpArrow)))
            {
                state = "AirHighSlash";
            }
            //空中横攻撃
            else if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftArrow)) ||
                (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.RightArrow)))
            {
                state = "AirRaid";
                
            }
            //空中兜割り
            else if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow)) && !isAAttack3)
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
                case "AirHighSlash":
                    animator.SetBool("isAHighSlash", true);
                    break;
                case "AirRaid":
                    animator.SetBool("isARaid", true);
                    break;
                case "HighSlash":
                    animator.SetBool("isHighSlash", true);
                    break;
                case "AHighSlashE":
                    animator.SetBool("isAHighSlash_end", true);
                    break;
                case "Slashing_R":
                    break;
                case "Slashing":
                    break;
                case "ThrowSword":
                    animator.SetBool("isThrowSword", true);
                    break;
                case "Hamma":
                    break;
                case "Sickle":
                    break;
                default:
                    animator.SetBool("isAAttack3_S", false);
                    animator.SetBool("isAAttack3_E", false);
                    animator.SetBool("isAAttack1", false);
                    animator.SetBool("isAAttack2", false);
                    animator.SetBool("isAttack1", false);
                    animator.SetBool("isAttack2", false);
                    animator.SetBool("isAttack3", false);
                    animator.SetBool("isAHighSlash", false);
                    animator.SetBool("isHighSlash", false);
                    animator.SetBool("isAHighSlash_end", false);
                    animator.SetBool("isARaid", false);
                    animator.SetBool("isThrowSword", false);
                    animator.SetBool("isThrowSword_E", false);
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく
            prevState = state;

        }
    }

    void PlayerRise()
    {
        rb.velocity = new Vector2(0, 8);
    }

    void ThrowSword()
    {
        Instantiate(thrownSword, this.transform.position + new Vector3(0.45f * PC.GetDrection(), 0.08f), Quaternion.Euler(0, 90f-PC.GetDrection()*90f, 0));
    }
    
    void AirRaid()
    {
        transform.localScale = new Vector3(PC.GetDrection() * 3, 3, 3); // 向きに応じてキャラクターを反転
        rb.velocity = new Vector2(2.5f * PC.GetDrection(), -5);
    }
}
