using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldScript : MonoBehaviour
{
    bool setOff;
    BoxCollider2D colliderOfGround;

    void Start()
    {
        colliderOfGround = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (setOff)
        {
            colliderOfGround.enabled = false;
            colliderOfGround.tag = "Untagged";
        }
        if (!setOff)
        {
            colliderOfGround.enabled = true;
            colliderOfGround.tag = "Ground";
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            setOff = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            setOff = false;
        }
    }
}
