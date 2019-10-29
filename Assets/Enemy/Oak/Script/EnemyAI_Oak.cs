using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Oak : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        senseDis = 8.0f;
        attackDis = 1.0f;
        attackProb = 0.5f;

        runForce = 30.0f;      // 走り始めに加える力
        runSpeed = 0.3f;       // 走っている間の速度
        runThreshold = 1.0f;   // 速度切り替え判定のための閾値

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        animator = transform.GetComponent<Animator>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する
        Move();                 // ③ 状態に応じて移動する       
    }
}
