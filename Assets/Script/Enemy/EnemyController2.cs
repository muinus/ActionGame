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

    public GameObject healPotion_s;
    public GameObject healPotion_l;

    bool isdamage;
    bool isDead;
    bool isPotioned;
    string state;

    float dropLate;

    GameObject BattleEvent;

    // Start is called before the first frame update
    void Start()
    {
        HPbar = GetComponentInChildren<Slider>();
        player = GameObject.Find("Player");
        animator = transform.GetComponent<Animator>();
        isdamage = false;
        isDead = false;
        isPotioned = false;
        dropLate = 0.5f;
        col = gameObject.GetComponent<CapsuleCollider2D>();

        healPotion_s = (GameObject)Resources.Load("Item/HealPotion_S");
        healPotion_l = (GameObject)Resources.Load("Item/HealPotion_L");

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
            
            if ((transform.root.gameObject == BattleEvent)
                && BattleEvent.GetComponent<BattleEventMaster>().GetIsBattleEvent())
            {
                BattleEvent.GetComponent<BattleEventMaster>().DecreaseEnemyCounter();
            }
            DropItem();
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
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool GetIsPotioned()
    {
        return isPotioned;
    }

    public void SetIsPotioned()
    {
        isPotioned = true;
        dropLate = 1;
    }

    public void SetDropLate(float dropLate)
    {
        this.dropLate = dropLate;
    }

    void DropItem()
    {
        float dropProf = Random.Range(0f, 1f);
        if (dropProf <= dropLate-0.2)
            Instantiate(healPotion_l, this.transform.position , Quaternion.identity);
        else if(dropProf <= dropLate)
            Instantiate(healPotion_s, this.transform.position, Quaternion.identity);
    }

    void Dead()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    void Deadend()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
