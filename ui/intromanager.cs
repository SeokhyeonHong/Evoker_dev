using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class intromanager : MonoBehaviour
{
    public GameObject intropanel;
    public GameObject Target;

    // Start is called before the first frame update
    void Start() /// 한 번만 실행
    {
        StartCoroutine(DelayTime(0));

    }

    IEnumerator DelayTime(float time)
    {
        /// start panel 틀고, 2초 뒤에 intro panel
        yield return new WaitForSeconds(time);
        intropanel.SetActive(true);

    }
    public void goglass() 
    {
        SceneManager.LoadScene(2);
        
    }

    public void gotutorial(){

        SceneManager.LoadScene(3);
    }

    public void goready(){

        SceneManager.LoadScene(4);
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y)){
            // Application.LoadLevel("SceneName");
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.X)){
            // Application.LoadLevel("SceneName");
            SceneManager.LoadScene(5);
        }
        if(Input.GetMouseButtonDown(0)){
            Target.SetActive(false);
        }
    }
}
