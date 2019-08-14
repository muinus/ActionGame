using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventMaster : MonoBehaviour
{
    private int enemyCounter;//敵の数
    private bool isBattleEvent;//イベント中かどうかのフラグ
    private bool eventEndFlag;//イベント終了フラグ


    

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        eventEndFlag = false;
        isBattleEvent = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetIsBattleEvent());
        Debug.Log("E;" + eventEndFlag);

        if (isBattleEvent)
            if (eventEndFlag)
            {
                
                ResetCamera();//カメラを追従に戻す
                
            }
    }

    void ResetCamera()
    {
        isBattleEvent = false;
    }

    void ResetPlace()
    {
        foreach (Transform childTransform in this.transform)
        {
            childTransform.gameObject.SetActive(false);
        }
    }

    public int GetEnemyCounter()
    {
        return enemyCounter;
    }

    public void IncreaseEnemyCounter()
    {
        enemyCounter += 1;
    }

    public void DecreaseEnemyCounter()
    {
        enemyCounter -= 1;
    }

    public bool GetIsBattleEvent()
    {
        return isBattleEvent;
    }

    public void SetIsBattleEvent(bool isBattleEvent)
    {
        this.isBattleEvent = isBattleEvent;
    }

    public bool GetEventEndFlag()
    {
        return eventEndFlag;
    }

    public void SetEventEndFlag(bool eventEndFlag)
    {
        this.eventEndFlag = eventEndFlag;
    }
}
