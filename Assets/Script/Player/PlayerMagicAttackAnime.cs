using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAttackAnime : PlayerAttack
{

    public GameObject fireball;
    public GameObject airFireball;
    public GameObject waterMasic;
    public GameObject airwaterMasic;
    public GameObject firecircle;
    public GameObject blackhole;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void GetInputKey()
    {

        if (Input.GetKeyDown(KeyCode.C) || UB_magic.GetIsPressedDown())
            isreroad = true;

        if ((Input.GetKey(KeyCode.C) || UB_magic.GetIsPressed())&&isreroad)
            isPressed = true;
        else
            isPressed = false;

        if (isPressed)
            pressTime += Time.deltaTime;
        else
            pressTime = 0;
    }

    public override void ChangeState()
    {

        // 接地している場合
        if (animator.GetBool("isGround"))
        {
            // 魔法長押し攻撃
            if (isPressed && pressTime >= longPressIntervalTime)
            {
                state = "BlackHole";
                isPressed = false;
                isreroad = false;
            }
            //下魔法(土)
            else if ((Input.GetKeyDown(KeyCode.C)||UB_magic.GetIsPressedDown()) &&
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

    public override void ChangeAnimation()
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
                case "BlackHole":
                    animator.SetBool("isBlackhole", true);
                    break;
                default:
                    animator.SetBool("isFireball", false);
                    animator.SetBool("isAFireball", false);
                    animator.SetBool("isFiretower", false);
                    animator.SetBool("isWaterMasic", false);
                    animator.SetBool("isAWaterMasic", false);
                    animator.SetBool("isTyoson", false);
                    animator.SetBool("isLightningstrike", false);
                    animator.SetBool("isBlackhole", false);
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

    void BlackHole()
    {
        Instantiate(blackhole, this.transform.position + new Vector3(-0.03f * PC.GetDrection(), 1.06f), Quaternion.identity);
    }
}
