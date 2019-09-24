using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存

    

    bool isComboing;

    public GameObject fireball;
    public GameObject airFireball;

    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponent<PlayerController>();
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isComboing = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            // 魔法(火球)
            if (Input.GetKeyDown(KeyCode.C) && !isComboing)
            {
                state = "Fireball";
                isComboing = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                state = "IDLE";
                isComboing = false;
            }

        }
        else//空中にいる場合
        {
            // 空中1コンボ
            if (Input.GetKeyDown(KeyCode.C) && !isComboing)
            {
                state = "AirFireball";
                isComboing = true;
                
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                state = "FALL";
                isComboing = false;
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
                case "Fireball":
                    animator.SetBool("isFireball", true);
                    break;
                case "AirFireball":
                    animator.SetBool("isAFireball", true);
                    break;
                default:
                    animator.SetBool("isFireball", false);
                    animator.SetBool("isAFireball", false);
                    break;
            }
            //状態の変更を判定するために状態を保存しておく
            prevState = state;

        }
    }

    void FireBall()
    {
        Instantiate(fireball, this.transform.position+new Vector3(0.8f*PC.GetDrection(),0.1f), Quaternion.identity);
    }

    void AirFireBall()
    {
        Instantiate(airFireball, this.transform.position + new Vector3(0.6f* PC.GetDrection(), -0.5f), Quaternion.identity);
    }
}
