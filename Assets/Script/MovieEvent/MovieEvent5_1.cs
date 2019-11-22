using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovieEvent5_1 : MonoBehaviour
{
    public PlayableDirector playableDirector;

    MovieEventMaster MEM;

    void Start()
    {
        //同じゲームオブジェクトにあるPlayableDirectorを取得する
        playableDirector = GetComponent<PlayableDirector>();
        MEM = transform.parent.GetComponent<MovieEventMaster>();
        PlayTimeline();
    }

    //再生する
    void PlayTimeline()
    {
        MEM.SetIsMovieEvent(true);
        playableDirector.Play();
    }

    //一時停止する
    void PauseTimeline()
    {
        MEM.SetIsMovieEvent(false);
        playableDirector.Pause();
    }

    //一時停止を再開する
    void ResumeTimeline()
    {
        MEM.SetIsMovieEvent(true);
        playableDirector.Resume();
    }

    //停止する
    void StopTimeline()
    {
        MEM.SetIsMovieEvent(false);
        playableDirector.Stop();
    }

}
