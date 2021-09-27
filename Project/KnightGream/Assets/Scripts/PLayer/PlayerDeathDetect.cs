using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDetect : MonoBehaviour
{
    public GameObject Ccamrea,deathUi; 
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag=="Player")
       {
           Ccamrea.SetActive(true);
           deathUi.SetActive(true);
       }
   }
}
