
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_1 : MonoBehaviour
{


    public GameObject enemy;//敵のプレハブを入れる変数
    public GameObject enemy2;
    int wave;//ウェーブの状態
    bool isThisBattleEvent;//イベントの箇所の判定
    Vector3 enemyPosition;
    GameObject objBattleEventMaster;

    GameObject maincamera;
    //GameObject battleEventMaster;
    BattleEventMaster battleEventMaster;

    private void Start()
    {
        objBattleEventMaster = transform.parent.gameObject;
        enemyPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, 0);
        wave = 1;//初期ウェーブは1
        isThisBattleEvent = false;

        battleEventMaster = transform.parent.gameObject.GetComponent<BattleEventMaster>();
        maincamera = GameObject.Find("Main Camera");

        ResetPlace();//障壁の消去

    }

    private void Update()
    {
       // Debug.Log(battleEventMasterStage.GetEnemyCounter());

        //敵の数がゼロになる度にウェーブが進行する
        if ((battleEventMaster.GetEnemyCounter() == 0) && isThisBattleEvent)
        {
            wave += 1;//ウェーブ進行
            ChangeWave();
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
                SpwanEnemy(enemy, enemyPosition + new Vector3(1, 1, 1));
                break;
            default:
                battleEventMaster.SetEventEndFlag(true);
                break;
        }
    }

    void LockCamera()
    {
        //イベントオブジェクトに対する相対座標で指定
        maincamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-2);
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

    void SpwanEnemy(GameObject enemy, Vector3 position)
    {
        Instantiate(enemy, position, Quaternion.identity, objBattleEventMaster.transform);
        battleEventMaster.IncreaseEnemyCounter();
    }


}
