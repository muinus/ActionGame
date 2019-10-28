using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Wisp : EnemyAI
{
    public GameObject fireball;

    // Start is called before the first frame update
    void Start()
    {
        senseDis = 12.0f;
        attackDis = 8.0f;
        attackProb = 1.0f;

        runForce = 30.0f;      // 走り始めに加える力
        runSpeed = 0.4f;       // 走っている間の速度
        runThreshold = 1.0f;   // 速度切り替え判定のための閾値

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        animator = transform.root.GetComponent<Animator>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する
        Move();                 // ③ 状態に応じて移動する       
    }

    void WispAttack()
    {
        Debug.Log(drec);
        Instantiate(fireball, this.transform.position + new Vector3(-0.7f * drec, -0.1f), Quaternion.Euler(0, 90f - drec * 90f, 0));
    }
}
