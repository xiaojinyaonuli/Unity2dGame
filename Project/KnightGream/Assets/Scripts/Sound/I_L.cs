using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_L : MonoBehaviour
{
    public static I_L Instance;

   private void Awake()
   {
       Instance=this;
   }

   public AudioSource audioSource;

   public AudioClip LSound,Isound;

   public void L_Play()
   {
       audioSource.clip=LSound;
       audioSource.Play();
   }
   public void I_Play()
   {
       audioSource.clip=Isound;
       audioSource.Play();
   }

}
