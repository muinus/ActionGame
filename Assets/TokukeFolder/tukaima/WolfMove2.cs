using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMove2 : MonoBehaviour
{
    GameObject player;
    int muki;
    void Start()
    {
        player = GameObject.Find("Player");
        Destroy(this.gameObject, 10.0f);
        if (player.transform.localScale.x > 0)
        {
            muki = 1;
        }else
        {
            muki = -1;
        }
    }

    IEnumerator WolfMove()
    {
        while (true)
        {
            this.gameObject.transform.Translate(muki*0.1f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
