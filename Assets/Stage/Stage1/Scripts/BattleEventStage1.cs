using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventStage1 : MonoBehaviour
{
    

    public GameObject enemy;//敵のプレハブを入れる変数
    int wave;//ウェーブの状態
    bool isThisBattleEvent;//イベントの箇所の判定
    Vector3 enemyPosition;

    GameObject maincamera;
    //GameObject battleEventMaster;
    BattleEventMasterStage1 battleEventMasterStage1;

    private void Start()
    {
        enemyPosition = new Vector3(this.transform.position.x, this.transform.position.y+1.0f,0);
        wave = 1;//初期ウェーブは1
        isThisBattleEvent = false;

        battleEventMasterStage1 = transform.parent.gameObject.GetComponent<BattleEventMasterStage1>();
        maincamera = GameObject.Find("Main Camera");

        ResetPlace();//障壁の消去

    }

    private void Update()
    {
        

        //敵の数がゼロになる度にウェーブが進行する
        if((battleEventMasterStage1.GetEnemyCounter()==0)&&isThisBattleEvent)
        {
            wave += 1;//ウェーブ進行
            ChangeWave();
        }

        

        //イベント終了時の処理
        if (!battleEventMasterStage1.GetIsBattleEvent())
        {
            isThisBattleEvent = false;
            battleEventMasterStage1.SetEventEndFlag(false);
            ResetPlace();//障壁の消去
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        battleEventMasterStage1.SetIsBattleEvent(true);
        isThisBattleEvent = true;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        ChangeWave();//敵の発生
        LockCamera();//カメラを固定にする
        LockPlace();//障壁の出現
        
    }

    void ChangeWave()
    {
        //スポーン位置はイベントオブジェクトに対する相対座標で指定

        //ウェーブの状態によって敵のスポーンを変えることができる
        switch (wave)
        {
            case 1:
                //SpwanEnemy(enemy, this.transform.position);
                SpwanEnemy(enemy, enemyPosition);
                break;
            case 2:
                //SpwanEnemy(enemy, this.transform.position);
                //SpwanEnemy(enemy, this.transform.position+new Vector3(1,1,1));
                SpwanEnemy(enemy, enemyPosition);
                SpwanEnemy(enemy, enemyPosition+new Vector3(1,1,1));
                break;
            default:
                battleEventMasterStage1.SetEventEndFlag(true);
                break;
        }
    }

    void LockCamera()
    {
        //イベントオブジェクトに対する相対座標で指定
        maincamera.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.z);
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

    void SpwanEnemy(GameObject enemy,Vector3 position) {
        Instantiate(enemy, position, Quaternion.identity);
        battleEventMasterStage1.IncreaseEnemyCounter();
    }


}
