using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downup_Stone : MonoBehaviour
{
    public Transform uppoint,downpoint;

    bool up;
    
    public float Speed;
    void Start()
    {
        up=true;
        transform.DetachChildren();
    }

    // Update is called once per frame
   private void FixedUpdate()
   {
       if(up)
       {
           transform.position=Vector2.MoveTowards(transform.position,downpoint.position,Speed*Time.deltaTime );
            if(transform.position.y<=downpoint.position.y)
            {
                up=false;
            }
       }else{

            transform.position=Vector2.MoveTowards(transform.position,uppoint.position,Speed*Time.deltaTime );
            if(transform.position.y>=uppoint.position.y)
            {
                up=true;
            }
       }
   }
}
