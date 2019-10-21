using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KazeMove : MonoBehaviour
{
    float speed = 3.0f;
    GameObject player;
    void Start()
    {
        
        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
