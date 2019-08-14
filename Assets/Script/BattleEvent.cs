using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent : MonoBehaviour
{
    

    public GameObject enemy;//敵のプレハブを入れる変数
    int wave;//ウェーブの状態
    bool isThisBattleEvent;//イベントの箇所の判定

    GameObject maincamera;
    //GameObject battleEventMaster;
    BattleEventMaster battleEventMaster;

    private void Start()
    {
        wave = 1;//初期ウェーブは1
        isThisBattleEvent = false;

        battleEventMaster = transform.parent.gameObject.GetComponent<BattleEventMaster>();
        maincamera = GameObject.Find("Main Camera");

        ResetPlace();//障壁の消去

    }

    private void Update()
    {
        

        //敵の数がゼロになる度にウェーブが進行する
        if((battleEventMaster.GetEnemyCounter()==0)&&isThisBattleEvent)
        {
            wave += 1;//ウェーブ進行
            SpwanEnemy();
        }

        

        //イベント終了時の処理
        if (!battleEventMaster.GetIsBattleEvent())
        {
            isThisBattleEvent = false;
            battleEventMaster.SetEventEndFlag(false);
            ResetPlace();//障壁の消去
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        battleEventMaster.SetIsBattleEvent(true);
        isThisBattleEvent = true;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        SpwanEnemy();//敵の発生
        LockCamera();//カメラを固定にする
        LockPlace();//障壁の出現
        
    }

    void SpwanEnemy()
    {
        //スポーン位置はイベントオブジェクトに対する相対座標で指定

        //ウェーブの状態によって敵のスポーンを変えることができる
        switch (wave)
        {
            case 1:
                Instantiate(enemy, this.transform.position, Quaternion.identity);
                battleEventMaster.IncreaseEnemyCounter();
                break;
            case 2:
                Instantiate(enemy, this.transform.position, Quaternion.identity);
                battleEventMaster.IncreaseEnemyCounter();
                Instantiate(enemy, this.transform.position, Quaternion.identity);
                battleEventMaster.IncreaseEnemyCounter();
                break;
            default:
                battleEventMaster.SetEventEndFlag(true);
                break;
        }
    }

    void LockCamera()
    {
        //イベントオブジェクトに対する相対座標で指定
        maincamera.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, -1);
    }

    void LockPlace()
    {
        //あらかじめ設定した障害物をアクティブに
        foreach (Transform childTransform in this.transform)
        {
            childTransform.gameObject.SetActive(true);
        }
    }

    void ResetPlace()
    {
        //設定した障害物を非アクティブに
        foreach (Transform childTransform in this.transform)
        {
            childTransform.gameObject.SetActive(false);
        }
    }




}
