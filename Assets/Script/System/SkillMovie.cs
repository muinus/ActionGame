using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkillMovie : MonoBehaviour
{
    VideoPlayer videoPlayer;
    RawImage raw;




    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;

        raw = GetComponent<RawImage>();
        raw.enabled = false;

    }

    // Update is called once per frame
    public void SetMovie(string buttonSelected)
    {
        videoPlayer.url = "Assets/Movies/" + buttonSelected + ".mp4";
        StartCoroutine(playVideo());        
    }

    IEnumerator playVideo()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();
        raw.texture = videoPlayer.texture;
        raw.enabled = true;
    }
}
