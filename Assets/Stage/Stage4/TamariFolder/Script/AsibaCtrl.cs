using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsibaCtrl : MonoBehaviour
{

    SurfaceEffector2D surfaceEffector;
    //Rigidbody2D rb;

    Vector2 movingVector;//足場の移動ベクトルを代入するところ
    // Start is called before the first frame update
    void Start()
    {
        surfaceEffector = GetComponent<SurfaceEffector2D>();
        //rb= GetComponent<Rigidbody2D>();
        setMovingVector(new Vector2(0, 0));

        setMovingVector(new Vector2(0.01f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        surfaceEffector.speed = movingVector.magnitude;
        transform.Translate(movingVector);
    }

    public void setMovingVector(Vector2 v)
    {
        movingVector = v;
    }
    public Vector2 getMovingVector()
    {

        return movingVector;
    }

}
