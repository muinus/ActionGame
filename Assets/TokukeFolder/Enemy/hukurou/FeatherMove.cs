using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherMove : MonoBehaviour
{
    float speed = 5.0f;
    GameObject player;
    void Start()
    {
        player= GameObject.Find("Player");
        
        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
