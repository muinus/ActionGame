using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FannelMoveContrl : MonoBehaviour
{

    public float targetHight;//どのくらいの高さまで上昇するか
    public float riseTime;//何秒かけて上昇するか
    public float bulletSpeed;//敵に向かう時の速度

    private float bulledRad;//弾の進行ベクトルの向き

    private int state=0;//0:上昇中 1:静止中 2:敵に向けて発射中
    private float riseTimeCounter = 0f;
    private Vector3 startPositon;
    private float ray_distance = 100f;

    private float targetTimeCounter = 0.0f;
    private float targetRandomDicideTime = 0.0f;
    private GameObject targetEnemyObject=null;

    private float addDrawRotateZ=0.0f;

    private int counter=0;

    // Start is called before the first frame update
    void Start()
    {
        startPositon = gameObject.transform.position;
        riseTimeCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (counter == 2)
        {
            //rayはおもそうなので一フレームだけ使用
            //ファンネル弾の真上にrayを飛ばして高さ制限をする
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, 1), ray_distance);
            // 上に距離(ray_distance)以内になんかある
            if (hit.transform != null & hit.transform.gameObject.tag=="Ground")
            {
                float ofset = 0.5f;
                if (hit.point.y - startPositon.y < targetHight + ofset)
                {
                    //ファンネルの上昇する程の高さがないので高さを抑える
                    targetHight = hit.point.y - startPositon.y - ofset - Random.Range(0f,1f);
                }
            }


        }

        

        if (state == 0)
        {
            //一度生成された場所からtargetHight分まで上昇
            gameObject.transform.position = new Vector3(
                                            startPositon.x, 
                                            startPositon.y + (((float)riseTimeCounter)/riseTime)*targetHight,
                                            0
                                            );
            if (riseTimeCounter > riseTime)
            {
                targetRandomDicideTime = Random.Range(0.0f, 0.5f);//0～1秒ランダムGET
                state = 1;
            }
            riseTimeCounter += Time.deltaTime;
        }
        else if (state == 2)
        {
            //Debug.Log(bulledRad);
            
            transform.Translate(bulletSpeed*Time.deltaTime,0,0);

            if (counter > 1000)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
        if (state == 1)
        {
            //空中に静止中
            //この間に狙う敵を選ぶ
            if (targetTimeCounter > targetRandomDicideTime)//重い処理なのでタイミングを分散させる
            {
                
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                float minDistance = 999999f;
                
                foreach (GameObject g in enemies)
                {
                    float D = Vector2.Distance(g.transform.position, transform.position);
                    if (D < minDistance + Random.Range(-4, 4))//ある程度近い敵ならばそっちもターゲットにしたい
                    {
                        float rad = Mathf.Atan2(g.transform.position.y - transform.position.y, g.transform.position.x - transform.position.x);
                        //ファンネルから敵に向かってRayをうつ
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(rad),Mathf.Sin(rad)), ray_distance);

                        if (hit.transform != null)
                        {
                            if (hit.transform.gameObject.tag == "Ground")
                            {
                                //敵との間に壁がある
                                Debug.DrawRay(transform.position, hit.point - new Vector2(transform.position.x, transform.position.y), Color.red, 7, false);

                            }
                            else
                            {
                                Debug.DrawRay(transform.position, new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)), Color.green, 7, false);
                                minDistance = D;
                                targetEnemyObject = g;
                            }

                        }
                        else
                        {
                            //間に何もない
                            minDistance = D;
                            targetEnemyObject = g;
                        }


                    }

                }
                

                    
                
                targetRandomDicideTime = 999999f;//二度とこのくそ重い処理に入らないようにしておく

            }

            //ファンネルの狙いを発射直前まで更新
            if (targetEnemyObject!=null)
            {
                bulledRad = Mathf.Atan2(targetEnemyObject.transform.position.y - transform.position.y, targetEnemyObject.transform.position.x - transform.position.x);

                float diffY = Mathf.DeltaAngle(transform.localEulerAngles.z, bulledRad * 180 / Mathf.PI);

                //見た目も更新

                if (diffY<0)
                {
                    addDrawRotateZ -= 1f;
                    if (addDrawRotateZ < -10f)
                        addDrawRotateZ = -10f;
                }
                else if (diffY > 0)
                {
                    addDrawRotateZ += 0.9f;
                    if (addDrawRotateZ > 9f)
                        addDrawRotateZ = 9f;
                }
                else
                {
                    addDrawRotateZ = 0;
                }

                transform.rotation = Quaternion.Euler(0, 0, transform.localEulerAngles.z+addDrawRotateZ);
                
            }
            else
            {
                //敵を見つけられなかったとき
                bulledRad = 0;
            }

            if (targetTimeCounter > 1.5f)
            {
                transform.rotation = Quaternion.Euler(0, 0, bulledRad * 180 / Mathf.PI);

                //静止終了
                state = 2;
                
            }
            targetTimeCounter += Time.deltaTime;
        }

            
        counter++;
    }

    public void setInit(float targetHight,float riseTime,float bulletSpeed)
    {
        this.targetHight = targetHight;
        this.riseTime = riseTime;
        this.bulletSpeed = bulletSpeed;

    }
}
