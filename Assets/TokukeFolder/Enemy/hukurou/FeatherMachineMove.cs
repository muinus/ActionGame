using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherMachineMove : MonoBehaviour
{
    public GameObject feather;
    float time = 5.0f;
    float stopTime=0.1f;
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
        while (seconds<time) {
            rand=Random.Range(-8.2f,8.2f);
            Instantiate(feather, new Vector3(this.transform.position.x+rand, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f, 0f, 180.0f));
            yield return new WaitForSeconds(stopTime);
        }
        Destroy(this.gameObject);
    }

}
