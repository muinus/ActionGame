using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    EdgeCollider2D col;

    float life_time = 1.0f;//生存時間
    float time = 0f;

    private void Start()
    {
        col = GetComponent<EdgeCollider2D>();
        col.enabled = false;
    }

    void Update()
    {

        time += Time.deltaTime;
        if (time > life_time)
        {
            Destroy(gameObject);
        }
    }

    void AttackStart()
    {
        col.enabled = true;
    }

    void AttackEnd()
    {
        col.enabled = false;
    }
}
