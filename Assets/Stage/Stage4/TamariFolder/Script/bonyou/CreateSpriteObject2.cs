//ブラックホールを生成する例


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpriteObject2 : MonoBehaviour
{


    public GameObject instanceTargetObject;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
        {
            InstanceBlackHole();
        }
    }


    void InstanceBlackHole()
    {
        Vector3 pos = this.transform.position;
        Quaternion qua = Quaternion.Euler(0,0,90);

        for (int i = 0; i < 7; i++)
        {
            GameObject g = GameObject.Instantiate(instanceTargetObject, pos, qua);//ファンネル生成

            float targetHight = (i+1)*0.6f;
            float riseTime = 0.6f;
            float bulletSpeed = 5f;

            g.GetComponent<FannelMoveContrl>().setInit(targetHight, riseTime, bulletSpeed);//ファンネルの初期値を与える

            if (targetHight < 0f)
            {

                Debug.Log("・。・" + targetHight);
            }
        }
    }
}
