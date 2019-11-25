using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_ground : MonoBehaviour
{

    GameObject Player;
    Animator animator;

    private void Start()
    {
        Player = transform.parent.gameObject;
        animator=Player.GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = transform.parent.position;
    }

    //着地判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirLanding") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("AirLanding_end"))
            return;

        if (col.gameObject.tag == "Ground")
        {
            if (!animator.GetBool("isGround"))
            {
                Debug.Log(col.name);
                animator.SetBool("isGround", true);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AirLanding") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("AirLanding_end"))
            return;

        if (col.gameObject.tag == "Ground")
        {
            if (!animator.GetBool("isGround"))
            {
                animator.SetBool("isGround", true);
            }
        }
    }

}
