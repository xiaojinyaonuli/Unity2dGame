using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kanAttack : MonoBehaviour
{
   public static kanAttack Instance;

   private void Awake()
   {
       Instance=this;
   }

   public AudioSource audioSource;

   public AudioClip attackzhong;

   public void attackinzhong()
   {
       audioSource.clip=attackzhong;
       audioSource.Play();
   }

}
