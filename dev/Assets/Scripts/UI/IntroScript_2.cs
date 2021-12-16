using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroScript_2 : MonoBehaviour
{
    private VideoPlayer vid;
    void Start()
    {
        vid = GetComponent<VideoPlayer>();
        vid.loopPointReached += CheckOver;
    }

    void Update()
    {
    }

    void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene("Intro_3");
    }
}
