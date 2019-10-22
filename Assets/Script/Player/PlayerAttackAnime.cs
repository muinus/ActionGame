using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;
    CameraController CC;
    

    public GameObject thrownSword;
    public GameObject potion;

    CapsuleCollider2D capCol;
    CircleCollider2D cirCol;
    BoxCollider2D boxCol;

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存
    
    bool isComboing;
    bool isAAttack3;
    bool isAttack3;
    bool isHamma;
    bool isPressed;

    float longPressIntervalTime = 1.0f;//生存時間
    float pressTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.CC = GameObject.Find("Main Camera").GetComponent<CameraController>();
        this.PC = GetComponent<PlayerController>();
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        capCol= GetComponent<CapsuleCollider2D>();
        cirCol = GetComponent<CircleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();
        isComboing = false;
        isAAttack3 = false;
        isPressed = false;
        isHamma = false;
        isAttack3 = false;
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
        if (Input.GetKey(KeyCode.Z))
            isPressed = true;
        else
            isPressed = false;

        if (isPressed)
            pressTime += Time.deltaTime;
        else
            pressTime = 0;
    }

    void ChangeState()
    {

        // 接地している場合
        if (animator.GetBool("isGround"))
        {
            //ため攻撃
            if ((state == "Slashing_R")&& Input.GetKeyUp(KeyCode.Z))
            {
                state = "Slashing";
                isPressed = false;
            }
            //ため攻撃(準備)
            else if (isPressed&&pressTime>=longPressIntervalTime)
            {
                state = "Slashing_R";
            }
            //ポーション投げ
            else if (Input.GetKeyDown(KeyCode.A))
            {
                state = "Potion";
            }
            //上攻撃
            else if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.UpArrow)))
            {
                state = "HighSlash";
            }
            //横攻撃
            else if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftArrow))||
                (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.RightArrow)))
            {
                
                state = "ThrowSword";
            }
            //下攻撃
            else if ((Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow)))
            {
                state = "Iai";
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
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && Input.GetKeyDown(KeyCode.Z)
                     && isAttack3)
            {
                state = "ATTACK3";
            }// 派生コンボ(ハンマー)
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && Input.GetKeyDown(KeyCode.Z)
                     && isHamma)
            {

                state = "Hamma";
            }// 派生コンボ(鎌)
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hamma") && Input.GetKeyDown(KeyCode.Z))
            {
                state = "Sickle";
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_loop"))
            {
                state = "AirATTACK3E";

                isAAttack3 = false;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirRaid"))
            {
                state = "IDLE";
                GetComponent<PlayerAttackCollider>().AttackEnd();
                boxCol.enabled = false;
                PC.UnFreezGravity();
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
                if (!SkillLearned.GetSkillActive("HighSlash"))
                    return;

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
                    Slashing_S();
                    animator.SetBool("isSlashing_R", true);
                    break;
                case "Slashing":
                    animator.SetBool("isSlashing", true);
                    break;
                case "ThrowSword":
                    animator.SetBool("isThrowSword", true);
                    break;
                case "Hamma":
                    animator.SetBool("isHamma", true); 
                    break;
                case "Sickle":
                    animator.SetBool("isSickle", true);
                    break;
                case "Iai":
                    animator.SetBool("isIai", true);
                    break;
                case "Potion":
                    animator.SetBool("isPotion", true);
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
                    animator.SetBool("isSlashing", false);
                    animator.SetBool("isSlashing_R", false);
                    animator.SetBool("isHamma", false);
                    animator.SetBool("isSickle", false);
                    animator.SetBool("isIai", false);
                    animator.SetBool("isPotion", false);
                    isAttack3 = false;
                    isHamma = false;
                    cirCol.enabled = true;
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
        rb.velocity = new Vector2(9f * PC.GetDrection(), -4.5f);
        boxCol.enabled = true;
    }

    public void Slashing_S()
    {
        CC.LockCamera();
        transform.position += new Vector3(0.06f, -0.09f);
        capCol.offset -= new Vector2(0.03f, -0.03f);
        cirCol.offset -= new Vector2(0.03f, -0.03f);
    }

    public void Slashing_E()
    {
        CC.UnLockCamera();
        transform.position -= new Vector3(0.06f, -0.09f);
        capCol.offset += new Vector2(0.03f, -0.03f);
        cirCol.offset += new Vector2(0.03f, -0.03f);
    }

    void Attack3Conbo()
    {
        isAttack3 = true;
    }

    void HammaConbo()
    {
        isAttack3 = false;
        isHamma = true;
    }

    void Hamma_and_Sickle()
    {
        rb.velocity = new Vector2(2 * PC.GetDrection(), 0);
    }

    void ThrowPotion()
    {
        Instantiate(potion, this.transform.position + new Vector3(0.4f * PC.GetDrection(), -0.2f), Quaternion.identity);
    }

    public void ResetPressTIme()
    {
        pressTime=0;
    }

    void CirColoff()
    {
        cirCol.enabled = false;
    }

    void CirColon()
    {
        cirCol.enabled = true;
    }
}
