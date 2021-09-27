using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectpotion : Collect
{
   public static int PotionCount;
    
   protected override void  Update()
   {
        if(Input.GetKeyDown(KeyCode.E)&&IsCollider)
         {
             keydown=true;
            
             PotionCount+=1;

            GameObject.FindWithTag("Player").GetComponent<PLayerControl>().PotionCount=PotionCount;
            Destroy(this.gameObject);
         }
   }

   protected override void  OnTriggerEnter2D(Collider2D other)
   {
       base.OnTriggerEnter2D(other);
   }
   
   protected override void OnTriggerExit2D(Collider2D other)
   {
       base.OnTriggerExit2D(other);
   }
}
