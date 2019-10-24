using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairlyMove : MonoBehaviour
{
    private float time;
    private const float LEAP_TIME = 2f;
    private Vector3 startPosition;
    private Vector3 endPosition; 
    private int switchJudge;
    Slider HPbar;
    private float idoukyori=0.6f;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("PlayerUI");
        HPbar = player.GetComponentInChildren<Slider>();
        startPosition = this.transform.position;
        endPosition= this.transform.position;
        endPosition.y += idoukyori;
        switchJudge = 1;
        StartCoroutine("Recover");
        StartCoroutine("DestroyTime");

    }
    void Update()
    {
        if (transform.position==endPosition)
        {
            
            startPosition = this.transform.position;
            endPosition = this.transform.position;
            switchJudge *= -1;
            endPosition.y += idoukyori * switchJudge;
            time = 0;
            
        }
        float t = Mathf.Min(time / LEAP_TIME, 1f) ;
        float leapt = (t * t) * (3f - (2f * t));
        transform.position = Vector3.Lerp(startPosition, endPosition, leapt);
        time += Time.deltaTime;
        
    }
    IEnumerator Recover()
    {
        while (true)
        {
            HPbar.value += 0.5f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
    
}
