using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;

    UIBottun UB_up;
    UIBottun UB_down;
    UIBottun UB_left;
    UIBottun UB_right;
    UIBottun UB_gun;

    public GameObject railGun;
    public GameObject bullet;
    public GameObject Fannelbullet;

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存

    bool isComboing;

    GameObject nearEnemy;

    float longPressIntervalTime = 1.0f;//生存時間
    float pressTime = 0f;
    bool isPressed;
    bool isreload;

    // Start is called before the first frame update
    void Start()
    {
        Transform dodai = GameObject.Find("PlayerUI").transform.Find("dodai");
        UB_up = dodai.Find("upButton").GetComponent<UIBottun>();
        UB_down = dodai.Find("downButton").GetComponent<UIBottun>();
        UB_left = dodai.Find("leftButton").GetComponent<UIBottun>();
        UB_right = dodai.Find("rightButton").GetComponent<UIBottun>();
        UB_gun = dodai.Find("GunButton").GetComponent<UIBottun>();
        this.PC = GetComponent<PlayerController>();
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isComboing = false;
        isPressed = false;
        isreload = true;//銃ボタン長押しで無限にマシンガンが撃てるのを抑制するフラグ

        Debug.Log(UB_gun.transform.name);
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
        //Debug.Log(UB_gun.GetIsPressedDown());
        //Debug.Log(UB_gun.GetIsPressedUp());
        //Debug.Log(UB_gun.GetIsPressed());

        if (Input.GetKeyUp(KeyCode.X)||UB_gun.GetIsPressedUp())
            isreload = true;

        if (Input.GetKey(KeyCode.X) || UB_gun.GetIsPressed())
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
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("MachineGun") && (Input.GetKeyUp(KeyCode.X) || UB_gun.GetIsPressedUp()))
            {
                state = "IDLE";
            }
            // 銃長押し攻撃
            else if (isPressed && pressTime >= longPressIntervalTime)
            {
                

                    if (pressTime >= 3.0f)
                {
                    state = "IDLE";
                    return;
                }

                if (isreload)
                {
                    state = "MachineGun";
                    isreload = false;
                }
            }// 横銃(レールガン)
            else if (((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.LeftArrow)||UB_left.GetIsPressed())) ||
                ((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.RightArrow)||UB_right.GetIsPressed())))
            {
                state = "RailGun";
            }//上銃(ファンネル)
            else if ((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.UpArrow)||UB_up.GetIsPressed()))
            {
                if (!SkillLearned.GetSkillActive("Fannel"))
                    return;

                var gameobject = GameObject.Find("fannel(Clone)");
                if(gameobject==null)
                    InstanceFannel();
            }// 下銃(乱れうち)
            else if ((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.DownArrow)||UB_down.GetIsPressed()))
            {
                state = "Midareuti";
            }
            // 銃コンボ1
            else if (((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && !isComboing)||
                (animator.GetCurrentAnimatorStateInfo(0).IsName("GunAttack2") && (Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown())))
            {
                state = "GunATTACK1";
                isComboing = true;
                GunAttack();
            }// 銃コンボ2
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("GunAttack1") && (Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()))
            {
                state = "GunATTACK2";
                GunAttack();
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                state = "IDLE";
                isComboing = false;
            }

        }
        else//空中にいる場合
        {

            //空中横銃攻撃(ショットガン横)
            if (((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.LeftArrow)||UB_left.GetIsPressed())) ||
                ((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.RightArrow)||UB_right.GetIsPressed())))
            {
                state = "ShotGun";
            }
            //空中下銃攻撃(ショットガン下)
            else if ((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.DownArrow)||UB_down.GetIsPressed()))
            {
                if (!SkillLearned.GetSkillActive("ShotGun"))
                    return;

                state = "ShotGun_Down";
            }
            //空中上銃(ファンネル)
            else if ((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && (Input.GetKey(KeyCode.UpArrow)||UB_up.GetIsPressed()))
            {
                if (!SkillLearned.GetSkillActive("Fannel"))
                    return;

                var gameobject = GameObject.Find("fannel(Clone)");
                if (gameobject == null)
                    InstanceFannel();
            }
            // 空中銃1コンボ
            else if (((Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown()) && !isComboing)||
                (animator.GetCurrentAnimatorStateInfo(0).IsName("AirGunAttack2") && (Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown())))
            {
                state = "AirGunATTACK1";
                isComboing = true;
                rb.velocity = new Vector2(0, 1);
                GunAttack();
            }
            // 空中銃2コンボ
            else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("AirGunAttack1") && (Input.GetKeyDown(KeyCode.X) || UB_gun.GetIsPressedDown())))
            {
                state = "AirGunATTACK2";
                rb.velocity = new Vector2(0, 1);
                GunAttack();
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
                case "GunATTACK1":
                    animator.SetBool("isGunAttack1", true);
                    animator.SetBool("isGunAttack2", false);
                    break;
                case "GunATTACK2":
                    animator.SetBool("isGunAttack2", true);
                    animator.SetBool("isGunAttack1", false);
                    break;
                case "AirGunATTACK1":
                    animator.SetBool("isAGunAttack1", true);
                    animator.SetBool("isAGunAttack2", false);
                    animator.SetBool("isJump", false);
                    break;
                case "AirGunATTACK2":
                    animator.SetBool("isAGunAttack2", true);
                    animator.SetBool("isAGunAttack1", false);
                    animator.SetBool("isJump", false);
                    break;
                case "RailGun":
                    animator.SetBool("isRailGun", true);
                    break;
                case "MachineGun":
                    animator.SetBool("isMachineGun", true);
                    break;
                case "ShotGun":
                    animator.SetBool("isShotGun", true);
                    break;
                case "ShotGun_Down":
                    animator.SetBool("isShotGunD", true);
                    break;
                case "Midareuti":
                    animator.SetBool("isMidareuti", true);
                    break;
                default:
                    animator.SetBool("isGunAttack1", false);
                    animator.SetBool("isGunAttack2", false);
                    animator.SetBool("isAGunAttack1", false);
                    animator.SetBool("isAGunAttack2", false);
                    animator.SetBool("isRailGun", false);
                    animator.SetBool("isMachineGun", false);
                    animator.SetBool("isShotGun", false);
                    animator.SetBool("isShotGunD", false);
                    animator.SetBool("isMidareuti", false);
                    ResetPressTIme();
                    break;
            }
            //状態の変更を判定するために状態を保存しておく
            prevState = state;

        }
    }

    void GunAttack()
    {
        try
        {
            nearEnemy = serchTag(transform.gameObject, "Enemy");

            Slider HPbar = nearEnemy.GetComponentInChildren<Slider>();

            if (HPbar == null)
                return;

            if (HPbar.value > 0.0f)
                HPbar.value -= 5.0f * 1.0f;
            else
                nearEnemy.GetComponent<Animator>().SetBool("isDamaged", true);
        }catch{}
    }

    void RailGun()
    {
        Instantiate(railGun, this.transform.position + new Vector3(5.08f * PC.GetDrection(), -0.1f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 0));
    }

    void ShotGun()
    {
        rb.velocity = new Vector2(0, 2);
        transform.localScale = new Vector3(PC.GetDrection() * 3, 3, 3); // 向きに応じてキャラクターを反転
    }

    void MachineGun()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0.26f * PC.GetDrection(), -0.03f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 0));
        Instantiate(bullet, this.transform.position + new Vector3(0.26f * PC.GetDrection(), -0.03f), Quaternion.Euler(0, 90f - PC.GetDrection() * 90f, 0));
    }

    void InstanceFannel()
    {
        Vector3 pos = this.transform.position;
        Quaternion qua = Quaternion.Euler(0, 0, 90);

        for (int i = 0; i < 7; i++)
        {
            GameObject g = GameObject.Instantiate(Fannelbullet, pos, qua);//ファンネル生成

            float targetHight = (i + 1) * 0.6f;
            float riseTime = 0.6f;
            float bulletSpeed = 5f;

            g.GetComponent<FannelMoveContrl>().setInit(targetHight, riseTime, bulletSpeed);//ファンネルの初期値を与える

            if (targetHight < 0f)
            {

                Debug.Log("・。・" + targetHight);
            }
        }
    }

    //指定されたタグの中で最も近いものを取得
    GameObject serchTag(GameObject thisObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト


        Renderer _Renderer;


        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            _Renderer = obs.GetComponent<Renderer>();
            if (!_Renderer.isVisible)
                continue;

            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, thisObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

    public void ResetPressTIme()
    {
        pressTime = 0;
    }
}
