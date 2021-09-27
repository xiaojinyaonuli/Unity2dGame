using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasePlayer :StateBase
{
   public CasePlayer(StateSystem fSMSystem,FSMStateID fSMStateID):base(fSMSystem,fSMStateID){}

    float time=0,JudgeTime;
    float Endtime=1.2f;
    
     private Transform PlayerTrans,Darkenemy;
     private float RunSpeed,AirSpeed;

     private Rigidbody2D darkRB;
     private Animator Darkanimator;
     public bool JudgeHp;

    public override void StartState(MonoBehaviour monoBehaviour)
    {
        //  Debug.Log("开始追踪状态");
         if(monoBehaviour.name=="DarkEnemy ")
        {
            JudgeHp=monoBehaviour.GetComponent<DarkEnemy>().HpChange;
        }
        if(monoBehaviour.name=="BOSS1 ")//类名加一个空格(tmd大坑)
        {
            JudgeHp=monoBehaviour.GetComponent<BOSS1>();
        }
         PlayerTrans=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
         RunSpeed=monoBehaviour.GetComponent<DarkEnemy>().RandomSpeed;
         AirSpeed=100f;
         JudgeTime=1f;
         Darkenemy=monoBehaviour.GetComponent<Transform>();
        darkRB=monoBehaviour.GetComponent<Rigidbody2D>();
        Darkanimator=monoBehaviour.GetComponent<Animator>();
    }

    public override void EndState()
    {
        //  Debug.Log("结束追踪状态");
         darkRB.velocity=new Vector2(0f,0);
    }

    public override void StateUpdate()
    {
         Darkanimator.SetTrigger("CancelAttack");
        time+=Time.deltaTime;
        if(time>Endtime)
        {
            time=0;
            // Debug.Log("追踪状态在更新");
            JudgeEnemyFace();
        }
        
        
    }

    public override void Transition_Condition()
    {      
        if(Vector2.Distance(Darkenemy.position,PlayerTrans.position)>20)
        {
            mySystem.TransitionState(FSMTransition.LeavePlayer);
        }
        if(Vector2.Distance(Darkenemy.position,PlayerTrans.position)<3)
        {
            
           mySystem.TransitionState(FSMTransition.NearPlayer);
        }else if(JudgeHp)
        {
             mySystem.TransitionState(FSMTransition.EnemyGetHit);
        }


        
    }

    protected void JudgeEnemyFace()
    {
        
        if(PlayerTrans.position.x<=Darkenemy.position.x)
        {    
            
            Darkenemy.localScale=new Vector3(1*Mathf.Abs(Darkenemy.localScale.x),Darkenemy.localScale.y,Darkenemy.localScale.z);
            //  darkRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
            if(Darkenemy.position.y<=PlayerTrans.position.y+3)
            {
                darkRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,0);
            }
            else{
                darkRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,-AirSpeed*Time.deltaTime);
            }
                      
        }

        if(PlayerTrans.position.x>=Darkenemy.position.x)
        {
                   
                 Darkenemy.localScale=new Vector3(-1*Mathf.Abs(Darkenemy.localScale.x),Darkenemy.localScale.y,Darkenemy.localScale.z);
                //  darkRB.velocity=new Vector2(RunSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
                if(Darkenemy.position.y<=PlayerTrans.position.y+3)
                {
                    darkRB.velocity=new Vector2(RunSpeed*Time.deltaTime,0f);
                }
                else{
                    darkRB.velocity=new Vector2(RunSpeed*Time.deltaTime,-AirSpeed*Time.deltaTime);
                }   
            
        }

    }

    public override void DelayFunction()
    {
         
    }

   
}
