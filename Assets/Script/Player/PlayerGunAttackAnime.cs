using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunAttackAnime : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    PlayerController PC;

    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存

    bool isComboing;

    GameObject nearEnemy;

    // Start is called before the first frame update
    void Start()
    {
        this.PC = GetComponent<PlayerController>();
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
            // 銃コンボ1
            if ((Input.GetKeyDown(KeyCode.X) && !isComboing)||
                (animator.GetCurrentAnimatorStateInfo(0).IsName("GunAttack2") && Input.GetKeyDown(KeyCode.X)))
            {
                state = "GunATTACK1";
                isComboing = true;
                GunAttack();
            }// 銃コンボ2
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("GunAttack1") && Input.GetKeyDown(KeyCode.X))
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
            // 空中1コンボ
            if ((Input.GetKeyDown(KeyCode.X) && !isComboing)||
                (animator.GetCurrentAnimatorStateInfo(0).IsName("AirGunAttack2") && Input.GetKeyDown(KeyCode.X)))
            {
                state = "AirGunATTACK1";
                isComboing = true;
                rb.velocity = new Vector2(0, 1);
                GunAttack();
            }
            // 空中2コンボ
            else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("AirGunAttack1") && Input.GetKeyDown(KeyCode.X)))
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
        // 状態が変わった場合のみアニメーションを変更する
        //Debug.Log(state);
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
                default:
                    animator.SetBool("isGunAttack1", false);
                    animator.SetBool("isGunAttack2", false);
                    animator.SetBool("isAGunAttack1", false);
                    animator.SetBool("isAGunAttack2", false);
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
}
