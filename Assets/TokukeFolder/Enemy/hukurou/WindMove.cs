using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour
{
    public float coefficient=3.0f;   // 空気抵抗係数
    public Vector3 velocity= new Vector3(3.0f, 0, 0);
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<Rigidbody2D>() == null)
        {
            return;
        }
        if (col.tag == "Player")
        {
           //Debug.Log("a");
            // 相対速度計算
            //var relativeVelocity = col.GetComponent<Rigidbody2D>().velocity- GetComponent<Rigidbody2D>().velocity;

            // 空気抵抗を与える
            col.GetComponent<Rigidbody2D>().AddForce(velocity);
        }
    }
}
