using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState :StateBase
{
     public GetHitState(StateSystem stateSystem,FSMStateID fSMStateID):base(stateSystem,fSMStateID){}
 
    private Transform LeftPoint,RightPoint,Darkenemy,PlayerTrans;

    float time=0;
    float Endtime=0.6f;

    private float RunSpeed;

    private Rigidbody2D darkRB;

    private bool IsLeftPoint,IsRightPoint;

    private Animator Darkanimator;

     public bool JudgeHp;

     public Vector2 PlayerVector;
     public bool IsiSkill,IsTopSkill,IsAirJump;



    public override void StartState(MonoBehaviour monoBehaviour)
    {
        
        if(monoBehaviour.name=="DarkEnemy ")
        {
            IsiSkill=monoBehaviour.GetComponent<DarkEnemy>().IsiSkill;
            IsTopSkill=monoBehaviour.GetComponent<DarkEnemy>().IsTopSkill;
            IsAirJump=monoBehaviour.GetComponent<DarkEnemy>().IsAirJump;
        }
        
        JudgeHp=monoBehaviour.GetComponent<DarkEnemy>().HpChange;
        PlayerVector=monoBehaviour.GetComponent<DarkEnemy>().PlayerVector;
        PlayerTrans=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();         
        // Debug.Log("开始受击状态");      
         Darkenemy=monoBehaviour.GetComponent<Transform>();
        darkRB=monoBehaviour.GetComponent<Rigidbody2D>();
        Darkanimator=monoBehaviour.GetComponent<Animator>();
        
    }

    public override void EndState()
    {
        //  Debug.Log("结束受击状态");
         
         darkRB.velocity=new Vector2(0,0);
    }

    public override void StateUpdate()
    {
        time+=Time.deltaTime;
        if(time>Endtime)
        {
            time=0;
            // Debug.Log("受击状态在更新");
            
        }
        Retreat();
        
        
        
    }
    public void  Retreat()
    {
        if(IsiSkill)
        {
            
            darkRB.velocity=new Vector2(PlayerVector.x* 500*Time.deltaTime,0);
        }else if(IsAirJump)
        {
            darkRB.velocity=new Vector2(PlayerVector.x* 180*Time.deltaTime,160*Time.deltaTime);
        }else 
        {
            darkRB.velocity=new Vector2(PlayerVector.x* 180*Time.deltaTime,0);
        }
       
    }

    public override void Transition_Condition()
    {
        if(Vector2.Distance(Darkenemy.position,PlayerTrans.position)>6)
        {
           
            mySystem.TransitionState(FSMTransition.SawPlayer);
        }else if(!JudgeHp)
        {
             mySystem.TransitionState(FSMTransition.SawPlayer);
        }

        
        

    }

   
    public override void DelayFunction()
    {
       
    }
}
