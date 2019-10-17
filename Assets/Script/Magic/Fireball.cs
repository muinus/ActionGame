using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    float life_time = 1.0f;//生存時間
    float time = 0f;

    float speed=0.2f;
    int drec;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        drec= System.Math.Sign(this.transform.position.x- player.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * drec, 0);

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
}
