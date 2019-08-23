using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsibaCtrl : MonoBehaviour
{

    //SurfaceEffector2D surfaceEffector;
    //Rigidbody2D rb;
    public GameObject moveBedMaster;

    private int debugCounter = 0;
    private int debugCounter2 = 0;
    Vector2 movingVector;//足場の移動ベクトルを代入するところ
    
    private MoveBedMaster moveBedMasterScr;
    // Start is called before the first frame update
    void Start()
    {
        //surfaceEffector = GetComponent<SurfaceEffector2D>();
        //rb= GetComponent<Rigidbody2D>();

        setMovingVector(new Vector2(0f,-0.025f));


        moveBedMasterScr = moveBedMaster.GetComponent<MoveBedMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        //urfaceEffector.speed = movingVector.magnitude;
        //setMovingVector(new Vector2(Mathf.Cos((Mathf.PI/300)*debugCounter)*0.1f, Mathf.Sin((Mathf.PI / 300) * debugCounter) * 0.1f));

        transform.Translate(movingVector);
        debugCounter += 1;
    }

    public void setMovingVector(Vector2 v)
    {
        movingVector = v;
    }
    public Vector2 getMovingVector()
    {

        return movingVector;
    }


    //足場の上に乗っているとき
    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionStay2D: " + collision.gameObject.name);

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag=="Enemy")
        {
            GameObject g = collision.gameObject;

            Vector2 move = moveBedMasterScr.getMoveVector(g,this.gameObject,movingVector);

            g.transform.position+=new Vector3(move.x,move.y,0f);
           

            //Debug.Log(""+debugCounter+"  :"+debugCounter2);
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            GameObject g = collision.gameObject;

            moveBedMasterScr.RemoveRideInfo(g);

        }

    }

}
