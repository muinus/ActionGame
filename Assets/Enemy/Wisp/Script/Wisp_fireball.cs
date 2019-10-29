using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp_fireball : MonoBehaviour
{
    float life_time = 1.0f;//生存時間
    float time = 0f;

    float speed = 0.1f;
    int drec;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        drec = System.Math.Sign(player.transform.position.x - this.transform.position.x);
        gameObject.SetActive(true);
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
            other.transform.tag != "Player")
            return;


        Destroy(this.transform.gameObject);

    }
}
