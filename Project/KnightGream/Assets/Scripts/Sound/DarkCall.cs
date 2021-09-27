using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCall : MonoBehaviour
{
    public static DarkCall Instance;

   private void Awake()
   {
       Instance=this;
   }

   public AudioSource audioSource;

   public AudioClip attackcall;

   public void enemycall()
   {
       audioSource.clip=attackcall;
       audioSource.Play();
   }
}
