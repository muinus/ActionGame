using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_air : MonoBehaviour
{
    float speed = 0.14f;
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
        transform.position += new Vector3(speed * drec, -speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        Debug.Log(other.transform.gameObject.name);

        if (other.transform.tag != "Untagged" &&
            other.transform.tag != "Ground" &&
            other.transform.tag != "Enemy")
            return;

        Destroy(this.transform.gameObject);

    }
}
