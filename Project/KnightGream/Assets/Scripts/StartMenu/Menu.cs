using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private VideoPlayer  videoPlayer;

    private RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = this.GetComponent <VideoPlayer> ();

        rawImage = this.GetComponent <RawImage> ();
    }

    // Update is called once per frame
    void Update()
    {
         //如果videoPlayer没有对应的视频texture，则返回

        if(videoPlayer.texture == null){

            return;

        }

        //把VideoPlayerd的视频渲染到UGUI的RawImage

        rawImage.texture = videoPlayer.texture;
    }
}
