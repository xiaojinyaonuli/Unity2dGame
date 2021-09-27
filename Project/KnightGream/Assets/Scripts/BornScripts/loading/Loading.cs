using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider slider;

    public Text loadtext;

    public Text ttext;
    AsyncOperation asyncOperation;

    public float Slidervalue;

    float TextValue;

    public float SlidervalueTemp;
    

    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(loadProgress());
    }
    void Update()
    {
        // StartCoroutine(loadProgress());
        load();
    }

    private void FixedUpdate()
    {
        // load();
    }

    IEnumerator loadProgress()
    {
         asyncOperation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);

        asyncOperation.allowSceneActivation=false;

        if(asyncOperation.progress<0.9f)
        {
            slider.value=asyncOperation.progress;
            SlidervalueTemp=asyncOperation.progress;

            loadtext.text=asyncOperation.progress*100+"%";
            yield return null;
        }
    }
    public void load()
    {
        if(asyncOperation.progress>=0.9f&&slider.value<1f)
        {   
            
            slider.value+=0.01f;
            TextValue=slider.value;
            loadtext.text=slider.value*100+"%";
            
        }else if(slider.value>=1f)
        {
            
            ttext.text="按下任意按键继续";
            if(Input.anyKeyDown)
            {
                 asyncOperation.allowSceneActivation=true;

            }
        }


    }
}
