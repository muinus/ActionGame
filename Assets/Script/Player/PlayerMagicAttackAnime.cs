using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;
    CameraController CC;

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存

    
    

    public GameObject fireball;
    public GameObject airFireball;
    public GameObject waterMasic;
    public GameObject airwaterMasic;

    // Start is called before the first frame update
    void Start()
    {
        CC = GameObject.Find("Main Camera").GetComponent<CameraController>();
        PC = GetComponent<PlayerController>();
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
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
            //魔法(土)
            if ((Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.DownArrow)))
            {
                state = "Tyoson";
            }
            //魔法(水)
            else if ((Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.UpArrow)))
            {
                state = "WaterMasic";
            }
            // 魔法(火球)
            else if (Input.GetKeyDown(KeyCode.C))
            {
                state = "Fireball";
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                state = "IDLE";
            }

        }
        else//空中にいる場合
        {
            //空中魔法(水)
            if ((Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.UpArrow)))
            {
                state = "AirWaterMasic";
            }
            // 空中魔法(火球)
            else if (Input.GetKeyDown(KeyCode.C))
            {
                state = "AirFireball";
                
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                state = "FALL";
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
                case "WaterMasic":
                    animator.SetBool("isWaterMasic", true);
                    break;
                case "AirWaterMasic":
                    animator.SetBool("isAWaterMasic", true);
                    break;
                case "Tyoson":
                    animator.SetBool("isTyoson", true);
                    break;
                default:
                    animator.SetBool("isFireball", false);
                    animator.SetBool("isAFireball", false);
                    animator.SetBool("isWaterMasic", false);
                    animator.SetBool("isAWaterMasic", false);
                    animator.SetBool("isTyoson", false);
                    break;
            }
            //状態の変更を判定するために状態を保存しておく
            prevState = state;

        }
    }

    void FireBall()
    {
        Instantiate(fireball, this.transform.position+new Vector3(0.8f*PC.GetDrection(),0.1f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 0));
    }

    void AirFireBall()
    {
        Instantiate(airFireball, this.transform.position + new Vector3(0.6f* PC.GetDrection(), -0.5f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 0));
    }

    void WaterMasic()
    {
        Instantiate(waterMasic, this.transform.position + new Vector3(1.1f * PC.GetDrection(), 0.9f), Quaternion.Euler(180, 90f - PC.GetDrection() * 90f, 0));
    }

    void AirWaterMasic()
    {
        Instantiate(airwaterMasic, this.transform.position + new Vector3(-1.14f * PC.GetDrection(), 0.08f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 180f));
        Instantiate(airwaterMasic, this.transform.position + new Vector3(1.03f * PC.GetDrection(), 0.08f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 0));
        Instantiate(airwaterMasic, this.transform.position + new Vector3(-0.03f * PC.GetDrection(), -1.06f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, -90f));
        Instantiate(airwaterMasic, this.transform.position + new Vector3(-0.03f * PC.GetDrection(), 1.06f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 90f));
    }

    void Tyoson_S()
    {
        CC.LockCamera();
        transform.position += new Vector3(0, 0.5f);
    }

    void Tyoson_E()
    {
        CC.UnLockCamera();
        transform.position -= new Vector3(0, 0.5f);
    }
}
