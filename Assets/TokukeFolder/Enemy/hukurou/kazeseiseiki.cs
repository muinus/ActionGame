using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kazeseiseiki : MonoBehaviour
{
    public GameObject kaze;
    float time = 5.0f;
    float stopTime = 3.0f;
    float rand;
    float seconds;
    void Start()
    {
        seconds = 0f;
        StartCoroutine("FeatherFall");
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
    }
    IEnumerator FeatherFall()
    {
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            rand = Random.Range(0,5.0f);
            Instantiate(kaze, new Vector3(this.transform.position.x+rand, this.transform.position.y+rand, this.transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(stopTime);
        }
    }
}
