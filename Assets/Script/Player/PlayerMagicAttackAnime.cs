using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;
    CameraController CC;
    UIBottun UB_up;
    UIBottun UB_down;
    UIBottun UB_left;
    UIBottun UB_right;
    UIBottun UB_magic;

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存

    
    

    public GameObject fireball;
    public GameObject airFireball;
    public GameObject waterMasic;
    public GameObject airwaterMasic;
    public GameObject firecircle;

    // Start is called before the first frame update
    void Start()
    {
        Transform dodai = GameObject.Find("PlayerUI").transform.Find("dodai");
        UB_up=dodai.Find("upButton").GetComponent<UIBottun>();
        UB_down = dodai.Find("downButton").GetComponent<UIBottun>();
        UB_left = dodai.Find("leftButton").GetComponent<UIBottun>();
        UB_right = dodai.Find("rightButton").GetComponent<UIBottun>();
        UB_magic = dodai.Find("MagicButton").GetComponent<UIBottun>();
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
        //Debug.Log(UB_magic.GetIsPressedDown());
        //Debug.Log(UB_magic.GetIsPressedUp());
        //Debug.Log(UB_magic.GetIsPressed());
    }

    void ChangeState()
    {

        // 接地している場合
        if (animator.GetBool("isGround"))
        {
            //下魔法(土)
            if ((Input.GetKeyDown(KeyCode.C)||UB_magic.GetIsPressedDown()) &&
                (Input.GetKey(KeyCode.DownArrow) || UB_down.GetIsPressed()))
            {
                state = "Tyoson";
            }
            //上魔法(水)
            else if ((Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown()) 
                && (Input.GetKey(KeyCode.UpArrow) || UB_up.GetIsPressed()))
            {
                state = "WaterMasic";
            }
            //横魔法(火柱)
            else if (((Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown()) 
                    && (Input.GetKey(KeyCode.LeftArrow) || UB_left.GetIsPressed()))||
                     ((Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown()) 
                     && (Input.GetKey(KeyCode.RightArrow) || UB_right.GetIsPressed())))
            {
                state = "FireTower";
            }
            // 魔法(火球)
            else if (Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown())
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
            if ((Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown())
                && (Input.GetKey(KeyCode.UpArrow) || UB_up.GetIsPressed()))
            {
                state = "AirWaterMasic";
            }//空中魔法(雷)
            else if ((Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown())
                && (Input.GetKey(KeyCode.DownArrow) || UB_down.GetIsPressed()))
            {
                state = "Lightning-Strike";

            }
            // 空中魔法(火球)
            else if (Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown())
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
        try
        {
            if (!SkillLearned.GetSkillActive(state))
                state = prevState;
        }
        catch { }

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
                case "FireTower":
                    animator.SetBool("isFiretower", true);
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
                case "Lightning-Strike":
                    animator.SetBool("isLightningstrike", true);
                    break;
                default:
                    animator.SetBool("isFireball", false);
                    animator.SetBool("isAFireball", false);
                    animator.SetBool("isFiretower", false);
                    animator.SetBool("isWaterMasic", false);
                    animator.SetBool("isAWaterMasic", false);
                    animator.SetBool("isTyoson", false);
                    animator.SetBool("isLightningstrike", false);
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

    void FireTower()
    {
        Instantiate(firecircle, this.transform.position + new Vector3(0.5f * PC.GetDrection(), -0.5f), Quaternion.identity);
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
