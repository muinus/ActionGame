using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuctusMove : MonoBehaviour
{
    public GameObject cuctus;
    int count =0;
    Vector2 growPos;
    // Start is called before the first frame update
    void Start()
    {
        growPos =new Vector2(this.transform.position.x, this.transform.position.y - 10);
        StartCoroutine("StartCuctusAttack");
    }
    IEnumerator StartCuctusAttack()
    {
        yield return new WaitForSeconds(3f);
        while (count<3)
        {
            Instantiate(cuctus, growPos, Quaternion.identity);
            yield return new WaitForSeconds(3f);
            count++;
        }
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
