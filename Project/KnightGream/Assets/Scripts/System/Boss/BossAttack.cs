using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : StateBase
{
   public BossAttack(StateSystem fSMSystem,FSMStateID fSMStateID):base(fSMSystem,fSMStateID){}

    float time=0;
    float Endtime=2f;
    
     private Transform PlayerTrans,BOSS;

     private Rigidbody2D BOSSRB;
     private Animator BOSSanimator;
     public bool Attack;

    public override void StartState(MonoBehaviour monoBehaviour)
    {
        
        if(monoBehaviour.name=="BOSS1")//(这又没有空格后面)
        {
            // JudgeHp=monoBehaviour.GetComponent<BOSS1>();
           
            Attack=monoBehaviour.GetComponent<BOSS1>().Attack;
            PlayerTrans=monoBehaviour.GetComponent<BOSS1>().Player;

        }
        
        BOSS=monoBehaviour.GetComponent<Transform>();
        BOSSRB=monoBehaviour.GetComponent<Rigidbody2D>();
        BOSSanimator=monoBehaviour.GetComponent<Animator>();
         
    }

    public override void EndState()
    {
        //  Debug.Log("结束BOsss攻击状态");
         BOSSRB.velocity=new Vector2(0f,0);
    }

    public override void StateUpdate()
    {
        
         
         time+=Time.deltaTime;
        if(time>Endtime)
        {
            BossAttackJudge(); 
           
           BOSSanimator.SetTrigger("Attack");
           
          
           time=0;
        }  
               
        
    }

    public override void Transition_Condition()
    {      
        if(Vector2.Distance(BOSS.position,PlayerTrans.position)>7)
         {
             mySystem.TransitionState(FSMTransition.BOSS_CasePlayer);
         }
        // if(Vector2.Distance(BOSS.position,PlayerTrans.position)<3)
        // {
        //     Debug.Log("即将进去");
        //    mySystem.TransitionState(FSMTransition.NearPlayer);
        // }else if(JudgeHp)
        // {
        //      mySystem.TransitionState(FSMTransition.EnemyGetHit);
        // }


        
    }

    public void BossAttackJudge()
    {
         if(PlayerTrans.position.x<=BOSS.position.x)
        {   

            BOSS.localScale=new Vector3(1*Mathf.Abs(BOSS.localScale.x),BOSS.localScale.y,BOSS.localScale.z);                   
        }

        if(PlayerTrans.position.x>=BOSS.position.x)
        {
            
            BOSS.localScale=new Vector3(-1*Mathf.Abs(BOSS.localScale.x),BOSS.localScale.y,BOSS.localScale.z);
        }
    }

  

    public override void DelayFunction()
    {
         
    }
}
