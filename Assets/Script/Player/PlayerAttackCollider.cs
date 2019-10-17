using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            col.offset = new Vector2(0.1f, 0.03f);
            col.size = new Vector2(0.28f, 0.33f);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            col.offset = new Vector2(0.04f, -0.03f);
            col.size = new Vector2(0.37f, 0.3f);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            col.offset = new Vector2(0.01f, -0.05f);
            col.size = new Vector2(0.5f, 0.2f);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack1"))
        {
            col.offset = new Vector2(0.0f, 0.04f);
            col.size = new Vector2(0.47f, 0.23f);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack2"))
        {
            col.offset = new Vector2(0.085f, -0.01f);
            col.size = new Vector2(0.32f, 0.32f);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_loop"))
        {
            col.offset = new Vector2(0.05f, 0.0035f);
            col.size = new Vector2(0.1f, 0.3f);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirAttack3_end"))
        {
            col.offset = new Vector2(0.0f, -0.05f);
            col.size = new Vector2(0.5f, 0.25f);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirHighSlash_loop"))
        {
            col.offset = new Vector2(0.17f, -0.01f);
            col.size = new Vector2(0.14f, 0.3f);
        }
    }

    void AttackEnd()
    {
        col.enabled = false;
    }

    //　剣を振り終わった時のイベント
    void AAttack3End()
    {
        animator.SetBool("isAAttack3_E", false);
        col.enabled = false;
    }
}
