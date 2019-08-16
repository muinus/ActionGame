using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairlyMove : MonoBehaviour
{
    private float time;
    private const float LEAP_TIME = 2f;
    private Vector3 startPosition;
    private Vector3 endPosition; 
    private int switchJudge;
    private float idoukyori=0.6f;
    private void Start()
    {
        startPosition = this.transform.position;
        endPosition= this.transform.position;
        endPosition.y += idoukyori;
        switchJudge = 1;
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
}
