using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
   public static Talk Instance;

   private void Awake()
   {
       Instance=this;
   }

   public AudioSource audioSource;

   public AudioClip Talkl;

   public void Talk_with()
   {
       audioSource.clip=Talkl;
       audioSource.Play();
   }
}
