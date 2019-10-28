using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider_Oak : MonoBehaviour
{
    public BoxCollider2D col; //　剣のコライダー
    Animator animator;


    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    //　剣を振りおろす時のイベント
    void AttackStart()
    {
        col.enabled = true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Oak_Attack"))
        {
            col.offset = new Vector2(-0.16f, 0.0f);
            col.size = new Vector2(0.19f, 0.53f);
        }
    }

    void AttackEnd()
    {
        col.enabled = false;
    }
}
