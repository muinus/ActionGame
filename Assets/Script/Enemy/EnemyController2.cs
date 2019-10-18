using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    Slider HPbar;//HPバーのオブジェクト
    GameObject player;
    Animator animator;
    Collider2D col;
    
    bool isdamage;
    bool isDead;
    string state;

    GameObject BattleEvent;

    // Start is called before the first frame update
    void Start()
    {
        HPbar = GetComponentInChildren<Slider>();
        player = GameObject.Find("Player");
        animator = transform.GetComponent<Animator>();
        isdamage = false;
        isDead = false;
        col = gameObject.GetComponent<CapsuleCollider2D>();

        BattleEvent = GameObject.Find("BattleEventMaster");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する
    }

    void ChangeState()
    {
        //敵から自分への向き
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            int drec = System.Math.Sign(this.transform.position.x - player.transform.position.x);
            this.transform.rotation = new Quaternion(0, 90.0f * drec * -1.0f + 90.0f, 0, 0);
            HPbar.transform.rotation = new Quaternion(0, 180.0f, 0, 0);
        } 

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            Debug.Log(BattleEvent);
            if ((transform.root.gameObject == BattleEvent)
                && BattleEvent.GetComponent<BattleEventMaster>().GetIsBattleEvent())
            {
                BattleEvent.GetComponent<BattleEventMaster>().DecreaseEnemyCounter();
            }
            Destroy(this.gameObject);
        }
        else if (HPbar.value <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            isDead = true;
            state = "Die";
            isdamage = false;
        }
        else if (isdamage)
        {
            state = "Damage";
            isdamage = false;
        }
        else
        {
            state = "Default";
        }

    }

    void ChangeAnimation()
    {
        switch (state)
        {
            case "Die":
                animator.SetBool("isDie", true);
                animator.SetBool("isDamaged", false);
                break;
            case "Damage":
                animator.SetBool("isDamaged", true);
                break;
            default:
                animator.SetBool("isDie", false);
                animator.SetBool("isDamaged", false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "Player_Attack" && !isDead)
        {
            isdamage = true;
            Debug.Log(other.transform.name);
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }


}
