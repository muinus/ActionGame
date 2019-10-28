using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BlackHole : MonoBehaviour
{

    public float suikomiPower;//吸引力（0.0f～）
    //public float suikomiBurePower;//大きくするとより地形を迂回して吸い込ませられる
    public float suikomiHanni_x;//吸い込みの範囲x
    public float suikomiHanni_y;//吸い込みの範囲y

    public float suikomiMaxSpeed;//吸い込んでいる敵のMAXの速さ
    public float suikomiKasokuSpeed;//吸い込み加速度
    public float suikomiHanni_Hanni_kakutei;//範囲内の敵が完全に吸い込まれる状態


    private GameObject[] EnemyObjects;
    private List<GameObject> targetObjects = new List<GameObject>();
    private List<Rigidbody2D> targetRbObjects = new List<Rigidbody2D>();
    private float counter = 0.0f;
    private float c_kasokuSpeed = 0.0f;
    private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        
        Choice_Enemy();
        c_kasokuSpeed = 0.0f;

        playerObject = GameObject.Find("Player");//デバッグ用。消しても大丈夫

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerObject.transform.position.x+2, playerObject.transform.position.y);

        if (Convert.ToInt32(counter*100)%500==0) {
            //5秒に一回呼び出される
            Choice_Enemy();
        }
        

        EnemyMovedByThis();

        c_kasokuSpeed += suikomiKasokuSpeed;
        if (c_kasokuSpeed > suikomiMaxSpeed)
        {
            c_kasokuSpeed = suikomiMaxSpeed;
        }
        counter += Time.deltaTime;
    }

    private void Choice_Enemy()
    {
        Debug.Log("・。・");
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

            if (rb == null)
                continue;
            Vector2 force = Vector3
                .Scale(//z成分を殺す
                transform.position - rb.transform.position//これで敵からブラックホールへのベクトルができる
                ,new Vector3(1,1,0))//z成分を殺す
                .normalized;//正規化

            /*
            rb.bodyType = RigidbodyType2D.Kinematic;

            rb.MovePosition(force);

            rb.se
            rb.bodyType = RigidbodyType2D.Dynamic;
            */
            if (transform.position.x - suikomiHanni_Hanni_kakutei <= rb.transform.position.x
                && rb.transform.position.x <= transform.position.x + suikomiHanni_Hanni_kakutei
                && transform.position.y - suikomiHanni_Hanni_kakutei <= rb.transform.position.y
                && rb.transform.position.y <= transform.position.y + suikomiHanni_Hanni_kakutei)
            {
                //ブラックホール固定
                rb.transform.position = transform.position;
            }
            else
            {
                Vector2 res = force * suikomiPower * (c_kasokuSpeed) * Time.deltaTime;

                if (suikomiMaxSpeed <= res.magnitude)
                {
                    res -= res*(res.magnitude - suikomiMaxSpeed);
                }
                rb.transform.Translate(res);
                //Debug.Log(res.x + "" + res.y);
            }
           
        }
        
    }
    
    public void DestoryThisObject()
    {
        Destroy(gameObject);
    }
}
