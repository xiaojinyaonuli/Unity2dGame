using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectUI : MonoBehaviour
{
   public static collectUI instance;

   public GameObject collect_UI;
   GameObject UI;

   private void Start()
   {
       
      instance=this;
       //collect_UI=GameObject.FindGameObjectWithTag("objectcollect");
       UI=Instantiate(collect_UI);
   }
   
   private void Update()
   {
       
   }
   public void SetTrue()
   {
         //Instantiate(collect_UI);
         UI.SetActive(true);
   }
   public void SetFalse()
   {
       UI.SetActive(false);
   }
}
