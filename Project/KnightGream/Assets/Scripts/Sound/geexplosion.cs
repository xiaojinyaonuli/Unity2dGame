using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geexplosion : MonoBehaviour
{
     public static geexplosion Instance;

   private void Awake()
   {
       Instance=this;
   }

   public AudioSource audioSource;

   public AudioClip Explosion;

   public void enemyexplosion()
   {
      audioSource.clip=Explosion;
       audioSource.Play();
   }

}
