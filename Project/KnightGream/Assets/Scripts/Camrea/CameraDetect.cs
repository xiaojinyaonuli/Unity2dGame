using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{
    public GameObject C1;

    public GameObject C2;
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag=="Player")
       {
           C1.SetActive(true);
           C2.SetActive(false);
           Destroy(this.gameObject);
       }
   }
}
