using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
   public static SoundManage Instance;

   private void Awake()
   {
       Instance=this;
   }

   public AudioSource audioSource;

   public AudioClip commonattack1,commonattack2,commonattack3,playergethit;

   public void commatk1()
   {
       audioSource.clip=commonattack1;
       audioSource.Play();
   }
    public void commatk2()
   {
       audioSource.clip=commonattack2;
       audioSource.Play();
   }
   public void commatk3()
   {
       audioSource.clip=commonattack3;
       audioSource.Play();
   }
   public void PlayerGethit()
   {
       audioSource.clip=playergethit;
       audioSource.Play();
   }
}
