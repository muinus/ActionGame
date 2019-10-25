using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col.gameObject.SetActive(false);
    }
    void AttackJudge()
    {
        if (col.gameObject.activeSelf == true)
        {
            col.gameObject.SetActive(false);
        }
        else if (col.gameObject.activeSelf == false)
        {
            col.gameObject.SetActive(true);
        }
    }
    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
