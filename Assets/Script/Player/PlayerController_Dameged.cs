using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_Dameged : MonoBehaviour
{
    Slider HPbar;//HPバーのオブジェクト
    GameObject playerUI;
    Animator animator;

    PlayerAttackAnime PAA;
    PlayerGunAttackAnime PGA;

    bool isdamage;
    string state;

    // Start is called before the first frame update
    void Start()
    {
        PAA = GetComponent<PlayerAttackAnime>();
        PGA = GetComponent<PlayerGunAttackAnime>();
        playerUI = GameObject.Find("PlayerUI");
        HPbar = playerUI.GetComponentInChildren<Slider>();
        HPbar.maxValue = 100.0f;
        HPbar.value = HPbar.maxValue;
        animator = transform.GetComponent<Animator>();

        isdamage = false;
    }

    void Update()
    {
        ChangeState();          // ① 状態を変更する
        ChangeAnimation();      // ② 状態に応じてアニメーションを変更する
    }

    void ChangeState()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            Destroy(this.gameObject);
        }
        else if (HPbar.value <= 0&& animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            state = "Die";
            isdamage = false;
        }
        else if (isdamage)
        {
            //剣溜め攻撃の時にダメージを食らった場合の処理
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slashing_R")||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Slashing"))
            {
                PAA.Slashing_E();
            }

            PAA.ResetPressTIme();
            PGA.ResetPressTIme();

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
                animator.SetBool("isDead", true);
                animator.SetBool("isDamaged", false);
                break;
            case "Damage":
                animator.SetBool("isDamaged", true);
                break;
            default:
                animator.SetBool("isDead", false);
                animator.SetBool("isDamaged", false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag != "Enemy_Attack")
            return;

        isdamage = true;
    }
}
