using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fannel : MonoBehaviour
{
    float life_time = 5.0f;//生存時間
    float time = 0f;

    void Update()
    {

        time += Time.deltaTime;
        if (time > life_time)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag != "Untagged" &&
            other.transform.tag != "Ground" &&
            other.transform.tag != "Wall" &&
            other.transform.tag != "Enemy")
            return;


        Destroy(this.transform.gameObject);

    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.transform.tag != "Untagged" &&
            other.transform.tag != "Ground" &&
            other.transform.tag != "Wall" &&
            other.transform.tag != "Enemy")
            return;


        Destroy(this.transform.gameObject);

    }
}
