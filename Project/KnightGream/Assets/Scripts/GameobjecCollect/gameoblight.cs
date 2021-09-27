using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class gameoblight : MonoBehaviour
{
    
    private Light2D glight;
    public float lighttime=0f;
    public float speed=0.5f;

    public float LIghtContinue;
    void Start()
    {
        //glight=GetComponent<Light2D>();
        glight =transform.GetChild(0).GetComponent<Light2D>();
    }

    private void FixedUpdate()
    {
   
        if(lighttime<=Time.time)
        {
            if(lighttime>1.6f)
            {
                lighttime=0.3f;
                return;
            }
            lighttime+=Time.deltaTime*(1/speed);
            
                      
        } 
        glight.intensity=lighttime;
       
        
    }
    void Update()
    {
        
    }
}
