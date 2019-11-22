using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class PlayableBehaviourTest : PlayableBehaviour
{
    public PlayableDirector playableDirector;

    MovieEventMaster MEM;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        //同じゲームオブジェクトにあるPlayableDirectorを取得する
        MEM = GameObject.Find("MovieEventMaster").GetComponent<MovieEventMaster>();
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // Called when the state of the playable is set to Play(Timeline上で呼ばれたとき)
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        MEM.SetIsMovieEvent(false);
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

    // Called each frame while the state is set to Play(Timeline上で呼ばれている間ずっと)
    public override void PrepareFrame(Playable playable, FrameData info)
    {

    }
}
