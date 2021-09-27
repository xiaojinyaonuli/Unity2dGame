using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCall : MonoBehaviour
{
    public static EnemyCall Instance;

   private void Awake()
   {
       Instance=this;
   }

   public AudioSource audioSource;

   public AudioClip attackcall;

   public AudioClip BossFirl;

   public void enemycall()
   {
       audioSource.clip=attackcall;
       audioSource.Play();
   }

   public void BossFire()
   {
       audioSource.clip=BossFirl;
       audioSource.Play();
   }

   
}
