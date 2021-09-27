using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MyKey
{
    public static MyKey Instance;
    private static MyKey instance()
    {
         if(Instance==null)
         {
              Instance=new MyKey();

         }
         return Instance;
    }
    
    public  static int SkillGroupCount=2;
     
    public enum MyKeyGroup
    {
        Up=1,
        Down=2,
        Right=3,
        Left=4,
        TopAttack=5      
    }

    public int[,] SkillGroup={ {(int)MyKeyGroup.Right,(int)MyKeyGroup.TopAttack}, };
    
    


}
