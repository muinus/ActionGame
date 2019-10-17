using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackProcess : MonoBehaviour
{
    Slider HPbar;//HPバーのオブジェクト
    GameObject enemy;//攻撃対象の敵
    PlayerAttackDamage attackTable;//アクションとダメージの対応テーブル
    List<AttackDamage> ADlist;//テーブルを格納するリスト
    GameObject player;

    Animator animator;

    int damage = 0;//与えるダメージ
    Vector2 force = new Vector2(0, 0);

    private void Start()
    {
        attackTable = Resources.Load<PlayerAttackDamage>("Data/CharacterStatusData");
        ADlist = attackTable.AttackDataList;
        animator = transform.root.GetComponent<Animator>();
        player = GameObject.Find("Player");
    }


    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {

        if (transform.tag != "Player_Attack")
            return;

        if (other.gameObject.tag != "Enemy")
            return;

        enemy = other.gameObject;
        
        damage = 0;
        force = Vector2.zero;

        HPbar = enemy.GetComponentInChildren<Slider>();

        //敵から自分への向き
        int drec = System.Math.Sign(enemy.transform.position.x - player.transform.position.x);
        

        foreach (AttackDamage state in ADlist)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(state.Name))
            {
                damage = state.Atk;
                force = new Vector2(state.Force.x * drec, state.Force.y);
                break;
            }
        }

        TakeDamage(damage);
        AddForce(force);

    }

    void TakeDamage(int attack)
    {
        
        if (HPbar == null)
            return;

        if (HPbar.value > 0.0f)
            HPbar.value -= attack*1.0f;
    }

    void AddForce(Vector2 force)
    {
        enemy.GetComponent<Rigidbody2D>().velocity = force;
    }
}
