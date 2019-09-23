﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackProcess : MonoBehaviour
{
    Slider HPbar;//HPバーのオブジェクト
    GameObject enemy;//攻撃対象の敵
    EnemyAttackDamege attackTable;//アクションとダメージの対応テーブル
    List<AttackDamage_E> ADlist;//テーブルを格納するリスト

    Animator animator;

    int damage = 0;//与えるダメージ
    Vector2 force = new Vector2(0, 0);

    private void Start()
    {
        attackTable = Resources.Load<EnemyAttackDamege>("Data/CharacterStatusData_E");
        ADlist = attackTable.AttackDataList;
        animator = transform.parent.GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {


        enemy = other.gameObject;

        if (enemy.tag != "Player")
            return;


        if (other != enemy.GetComponent<CapsuleCollider2D>())
            return;

        HPbar = GameObject.Find("PlayerUI").GetComponentInChildren<Slider>();

        //敵から自分への向き
        int drec = System.Math.Sign(enemy.transform.position.x - this.transform.position.x);

        foreach (AttackDamage_E state in ADlist)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(state.AName))
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
            HPbar.value -= attack * 1.0f;
    }

    void AddForce(Vector2 force)
    {
        enemy.GetComponent<Rigidbody2D>().velocity = force;
    }
}