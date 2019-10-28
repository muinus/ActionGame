using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider_Skelton : MonoBehaviour
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skelton_w_Attack1"))
        {
            col.offset = new Vector2(-0.54f, 0.02f);
            col.size = new Vector2(0.3f, 0.52f);
        }
    }

    void AttackEnd()
    {
        col.enabled = false;
    }
}
