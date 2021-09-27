using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{

    public Transform leftpoint,rightpoint;

    bool face,isground;
    
    public float speed;
    

    public Transform player;

    public LayerMask ground;
    
    
    void Start()
    {
       
        face=true;//向左
        transform.DetachChildren();
    }


   private void FixedUpdate()
   {
       if(face)
        {
            transform.position=Vector2.MoveTowards(transform.position,leftpoint.position,speed*Time.deltaTime);
            if(transform.position.x<=leftpoint.position.x)
            {
                Debug.Log("进入左");
                face=false;
            }
        }
        else{
            
            
            transform.position=Vector2.MoveTowards(transform.position,rightpoint.position,speed*Time.deltaTime);
            if(transform.position.x>=rightpoint.position.x)
            {
                face=true;
            }
        }
      
   }
}
