using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMove2 : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 10.0f);
    }

    IEnumerator WolfMove()
    {
        while (true)
        {
            this.gameObject.transform.Translate(-0.015f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
