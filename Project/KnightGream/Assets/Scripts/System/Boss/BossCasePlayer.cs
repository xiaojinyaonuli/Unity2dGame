using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCasePlayer :StateBase
{
     public BossCasePlayer(StateSystem fSMSystem,FSMStateID fSMStateID):base(fSMSystem,fSMStateID){}

    float time=0;
    float Endtime=1.2f;
    
     private Transform PlayerTrans,BOSS;
     private float RunSpeed,AirSpeed;

     private Rigidbody2D BOSSRB;
     private Animator BOSSanimator;
     

    public override void StartState(MonoBehaviour monoBehaviour)
    {
        
        if(monoBehaviour.name=="BOSS1")//(这又没有空格后面)
        {
           
            RunSpeed=monoBehaviour.GetComponent<BOSS1>().RandomSpeed;
        }
        // Debug.Log(monoBehaviour.name+"s");
        // Debug.Log("开始更新");
         PlayerTrans=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
         
         AirSpeed=100f;
         
         BOSS=monoBehaviour.GetComponent<Transform>();
        BOSSRB=monoBehaviour.GetComponent<Rigidbody2D>();
        BOSSanimator=monoBehaviour.GetComponent<Animator>();
    }

    public override void EndState()
    {
        //  Debug.Log("结束追踪状态");
         BOSSRB.velocity=new Vector2(0f,0);
    }

    public override void StateUpdate()
    {
        
        time+=Time.deltaTime;
        if(time>Endtime)
        {
            BOSSanimator.SetTrigger("NotAttack");
            time=0;
            // Debug.Log("追踪状态在更新");
            JudgeEnemyFace();
        }
        
        
        
    }

    public override void Transition_Condition()
    {      
        // if(Vector2.Distance(BOSS.position,PlayerTrans.position)>25)
        // {
        //     mySystem.TransitionState(FSMTransition.LeavePlayer);
        // }
         if(Vector2.Distance(BOSS.position,PlayerTrans.position)<4)
        {
            
            mySystem.TransitionState(FSMTransition.BOSS_Near);
         }
        //else if(JudgeHp)
        // {
        //      mySystem.TransitionState(FSMTransition.EnemyGetHit);
        // }


        
    }

    protected void JudgeEnemyFace()
    {
        
        if(PlayerTrans.position.x<=BOSS.position.x)
        {   

            if(PlayerTrans.position.y>BOSS.position.y)
            {
                BOSS.localScale=new Vector3(1*Mathf.Abs(BOSS.localScale.x),BOSS.localScale.y,BOSS.localScale.z);
            //  BOSSRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
                Debug.Log("速度");
                BOSSRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,80*Time.deltaTime);
            }else 
            {
                BOSS.localScale=new Vector3(1*Mathf.Abs(BOSS.localScale.x),BOSS.localScale.y,BOSS.localScale.z);
            //  BOSSRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
            
                BOSSRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,-RunSpeed*Time.deltaTime);
          
            }
            
                             
        }

        if(PlayerTrans.position.x>=BOSS.position.x)
        {
            if(PlayerTrans.position.y>BOSS.position.y)
            {
                BOSS.localScale=new Vector3(-1*Mathf.Abs(BOSS.localScale.x),BOSS.localScale.y,BOSS.localScale.z);
            //  BOSSRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
                Debug.Log("速度");
                BOSSRB.velocity=new Vector2(RunSpeed*Time.deltaTime,80*Time.deltaTime);
            }else 
            {
                BOSS.localScale=new Vector3(-1*Mathf.Abs(BOSS.localScale.x),BOSS.localScale.y,BOSS.localScale.z);
            //  BOSSRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
            
                BOSSRB.velocity=new Vector2(RunSpeed*Time.deltaTime,-RunSpeed*Time.deltaTime);
          
            }
                   
                //  BOSS.localScale=new Vector3(-1*Mathf.Abs(BOSS.localScale.x),BOSS.localScale.y,BOSS.localScale.z);
                // //  BOSSRB.velocity=new Vector2(RunSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
               
                // BOSSRB.velocity=new Vector2(RunSpeed*Time.deltaTime,0f);
                //  BOSSanimator.SetFloat("Speed",Mathf.Abs(BOSSRB.velocity.x));
                
            
        }

    }

    public override void DelayFunction()
    {
         
    }
}
