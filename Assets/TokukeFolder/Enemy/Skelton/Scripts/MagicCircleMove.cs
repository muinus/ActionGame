using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleMove : MonoBehaviour
{
    public GameObject thunder;
    void Start()
    {
        

    }
    void Thunder()
    {
        Instantiate(thunder, new Vector2(this.transform.position.x, this.transform.position.y-1.9f), Quaternion.identity);
    }
    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }


}
