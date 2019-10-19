using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBullet : MonoBehaviour
{
    BoxCollider2D col;

    private void Start()
    {
        col = transform.GetComponent<BoxCollider2D>();
        col.size = new Vector2(3f, 0.04f);
    }

    void AttackStart2()
    {
        col.size = new Vector2(3f, 0.08f);
    }

    void AttackEnd()
    {
        col.enabled = false;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
