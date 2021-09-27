using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class loadBack : MonoBehaviour
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
         asyncOperation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-3);

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
        if(asyncOperation.progress>=0.9f)
        {   
            slider.value=asyncOperation.progress+0.1f;;
            loadtext.text=100+"%";
            asyncOperation.allowSceneActivation=true;
        }

    }
}
