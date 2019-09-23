using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag != "Untagged" &&
            other.transform.tag != "Ground" &&
            other.transform.tag != "Enemy")
            return;

        Destroy(this.transform.gameObject);

    }
}
