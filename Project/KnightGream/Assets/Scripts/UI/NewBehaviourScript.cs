using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public float speed;

    private float ReTime;
    private Transform Gebulint;

    private void OnEnable()
    {
        ReTime=Time.time;
       
    }
    private void Start()
    {
        
    }
    
    private void Update()
    {
        if((ReTime+speed)<Time.time)
        {
            Floatxue.instance.push_backpool(this.gameObject);
        }
    }
    public void Destroyobject()
    {
        Destroy(gameObject,speed);
    }
}
