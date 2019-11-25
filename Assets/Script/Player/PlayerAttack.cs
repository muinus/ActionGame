using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public PlayerController PC;
    public CameraController CC;
    public MovieEventMaster MEM;

    public UIBottun UB_up;
    public UIBottun UB_down;
    public UIBottun UB_left;
    public UIBottun UB_right;
    public UIBottun UB_magic;
    public UIBottun UB_sword;
    public UIBottun UB_gun;
    public UIBottun UB_move;

    public string state;                // プレイヤーの状態管理
    public string prevState;            // 前の状態を保存

    public float longPressIntervalTime = 1.0f;//長押しと判定される時間
    public float pressTime = 0f;
    public bool isPressed;
    public bool isreroad;

    // Start is called before the first frame update
    void Awake()
    {
        CC = GameObject.Find("Main Camera").GetComponent<CameraController>();
        PC = GetComponent<PlayerController>();
        MEM= GameObject.Find("MovieEventMaster").GetComponent<MovieEventMaster>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Transform dodai = GameObject.Find("PlayerUI").transform.Find("dodai");
        UB_up = dodai.Find("upButton").GetComponent<UIBottun>();
        UB_down = dodai.Find("downButton").GetComponent<UIBottun>();
        UB_left = dodai.Find("leftButton").GetComponent<UIBottun>();
        UB_right = dodai.Find("rightButton").GetComponent<UIBottun>();
        UB_sword = dodai.Find("SwordButton").GetComponent<UIBottun>();
        UB_magic = dodai.Find("MagicButton").GetComponent<UIBottun>();
        UB_gun = dodai.Find("GunButton").GetComponent<UIBottun>();
        UB_move = dodai.Find("MoveButton").GetComponent<UIBottun>();
        isPressed = false;
        isreroad = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (MEM.GetIsMovieEvent())
            return;

        GetInputKey();          // ① 入力を取得
        ChangeState();          // ② 状態を変更する
        ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
    }

    public virtual void GetInputKey()
    {

    }

    public virtual void ChangeState()
    {

    }

    public virtual void ChangeAnimation()
    {

    }
}
