using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieEventMaster : MonoBehaviour
{
    bool isMovieEvent;

    // Start is called before the first frame update
    void Start()
    {
        isMovieEvent = false;
    }

    private void Update()
    {
        
    }

    public bool GetIsMovieEvent()
    {
        return isMovieEvent;
    }

    public void SetIsMovieEvent(bool isMovieEvent)
    {
        
        this.isMovieEvent = isMovieEvent;
    }
}
