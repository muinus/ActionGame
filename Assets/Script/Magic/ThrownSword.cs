using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrownSword : MonoBehaviour
{
    float life_time = 1.0f;//生存時間
    float time = 0f;

    float speed = 0.2f;
    int drec;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        drec = System.Math.Sign(this.transform.position.x - player.transform.position.x);
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

        if (other.transform.tag == "Enemy")
        {
            Vector3 pos = player.transform.position;
            pos.x = this.transform.position.x - 0.2f*drec;
            player.transform.position = pos;

            player.GetComponent<Animator>().SetBool("isThrowSword_E", true);
        }

        Destroy(gameObject);

    }
}
