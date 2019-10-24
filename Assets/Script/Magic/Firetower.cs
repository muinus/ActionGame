using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetower : MonoBehaviour
{
    public BoxCollider2D col; //　炎のコライダー

    float life_time = 5.0f;//生存時間
    float time = 0f;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>(); 
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
        col.offset = new Vector2(0f, -0.17f);
        col.size = new Vector2(0.61f, 0.94f);
    }

    void AttackStart2()
    {
        col.offset = new Vector2(0f, 0f);
        col.size = new Vector2(0.61f, 1.28f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
