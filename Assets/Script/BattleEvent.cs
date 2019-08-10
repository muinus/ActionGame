using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent : MonoBehaviour
{
    bool isBattleEvent;
    bool isBattleFlag;
    private int enemyCounter;

    public GameObject enemy;
    GameObject maincamera;

    private void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        isBattleEvent = false;
        isBattleFlag = true;
        enemyCounter = 0;

        ResetPlace();
    }

    private void Update()
    {
        if(isBattleEvent)
            if (enemyCounter == 0)
            {
                ResetCamera();
                ResetPlace();
            }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        isBattleEvent = true;
        if (isBattleFlag)
        {
            isBattleFlag = false;
            SpwanEnemy();
            LockCamera();
            LockPlace();
        }
        
    }

    void SpwanEnemy()
    {
        Instantiate(enemy, this.transform.position, Quaternion.identity);
        IncreaseEnemyCounter();
    }

    void LockCamera()
    {
        maincamera.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, -1);
    }

    void ResetCamera()
    {
        isBattleEvent = false;
    }

    void LockPlace()
    {
        foreach(Transform childTransform in this.transform)
        {
            childTransform.gameObject.SetActive(true);
        }
    }

    void ResetPlace()
    {
        foreach (Transform childTransform in this.transform)
        {
            childTransform.gameObject.SetActive(false);
        }
    }

    public bool GetIsBattleEvent()
    {
        return isBattleEvent;
    }

    public void IncreaseEnemyCounter()
    {
        enemyCounter += 1;
    }

    public void DecreaseEnemyCounter()
    {
        enemyCounter -= 1;
    }
}
