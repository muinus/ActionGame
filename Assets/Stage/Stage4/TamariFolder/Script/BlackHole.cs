using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BlackHole : MonoBehaviour
{

    public float suikomiPower;//吸引力（0.0f～）
    public float suikomiBurePower;//大きくするとより地形を迂回して吸い込ませられる
    public float suikomiHanni_x;//吸い込みの範囲x
    public float suikomiHanni_y;//吸い込みの範囲y

    public float suikomiMaxSpeed;//吸い込んでいる敵のMAXの速さ
    public float suikomiKasokuSpeed;//吸い込み加速度
    public float suikomiHanni_eikyoudo;//範囲内の敵が受ける吸い込まれ影響度


    private GameObject[] EnemyObjects;
    private List<GameObject> targetObjects = new List<GameObject>();
    private List<Rigidbody2D> targetRbObjects = new List<Rigidbody2D>();
    private float counter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        Choice_Enemy();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Convert.ToInt32(counter*100)%100==0) {
            //1秒に一回呼び出される
            Choice_Enemy();
        }
        

        EnemyMovedByThis();
        counter += Time.deltaTime;
    }

    private void Choice_Enemy()
    {
        EnemyObjects = GameObject.FindGameObjectsWithTag("Enemy");//100000回実行しても0.02秒で終わるそうなので採用
        targetObjects.Clear();
        foreach (GameObject g in EnemyObjects){
            if (transform.position.x - suikomiHanni_x <= g.transform.position.x 
                && g.transform.position.x <= transform.position.x + suikomiHanni_x 
                && transform.position.y - suikomiHanni_y <= g.transform.position.y
                && g.transform.position.y <= transform.position.y + suikomiHanni_y)
            {
                //ブラックホールの効果を受ける敵オブジェクトを代入
                targetObjects.Add(g);
            }
        }
        foreach (GameObject g in targetObjects)
        {
            
            targetRbObjects.Add(g.GetComponent<Rigidbody2D>());//こいつが激重になる可能性がある
            
        }

    }

    private void EnemyMovedByThis()
    {
        //実際に敵を引き寄せる.
        foreach (Rigidbody2D rb in targetRbObjects) {

            Vector2 force = Vector3
                .Scale(transform.position - rb.transform.position//これで敵からブラックホールへのベクトルができる
                ,new Vector3(1,1,0))//z成分を殺す
                .normalized;//正規化


            rb.AddForce(new Vector2(1f,0));
            Debug.Log("・。・３");
        }
        
    }
    /*
    public void DestoryThisObject()
    {


    }*/
}
