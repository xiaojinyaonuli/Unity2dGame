using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerawith : MonoBehaviour
{
    
    public Transform player;//玩家位置

    Vector3 player_x_y;
    [Header("相机的最高最低位置")]
    public Vector2 MaxPoint;

    public Vector2 MinPoint;

    
     void Start()
    {
         
    }
    private void Update()
    {
        if(player.position.x>126)
        {
            Vector2 a=new Vector2(transform.position.x,1.8f);
            MinPoint=Vector2.Lerp(transform.position,a,1f) ;
        }
    }
    
    void LateUpdate()
    {
       
        Vector3 playerpoint=new Vector3(player.transform.position.x,player.transform.position.y,-15f);

        Vector3 Mpoint=player.position;

        if(playerpoint!=null)
        {

           if(transform.position!=playerpoint)
           {
                  playerpoint.x=Mathf.Clamp(playerpoint.x,MinPoint.x,MaxPoint.x);


                  playerpoint.y=Mathf.Clamp(playerpoint.y,MinPoint.y,MaxPoint.y);

                  transform.position=Vector3.Lerp(transform.position,playerpoint,1f);

           }

        }
    }
}
