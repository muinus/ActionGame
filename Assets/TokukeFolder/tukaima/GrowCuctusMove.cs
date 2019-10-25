using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCuctusMove : MonoBehaviour
{
    GameObject player;
    GameObject target;
    Vector3 startPosition;
    Vector3 currentPosition;
    Vector3 endPosition;
    float elapsedTime = 1;
    int Duration = 50;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3.0f);
        player = GameObject.Find("Player");
        
        target = SerchTag(gameObject, "Enemy");
        Debug.Log(target);
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y-10, target.transform.position.z);
        endPosition = target.transform.position;
        startPosition = this.transform.position;
        StartCoroutine("CuctusAttack");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CuctusAttack()
    {
        
        while (elapsedTime < Duration)
        {
            float timeRate = (float)elapsedTime / Duration;
            // timeRate = EaseOutQuart(timeRate);
            this.transform.position = Vector3.Lerp(startPosition, endPosition, timeRate);
            elapsedTime++;
            startPosition = this.transform.position;
            yield return new WaitForSeconds(0.01f);
        }

       

    }
    //指定されたタグの中で最も近いものを取得
    GameObject SerchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
    /*float EaseOutQuart(float t)
    {
        t -= 1f;
        return -1f * (t * t * t * t - 1);
    }*/
}
