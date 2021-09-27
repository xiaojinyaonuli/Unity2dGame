using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBase
{
     public AttackState(StateSystem stateSystem,FSMStateID fSMStateID):base(stateSystem,fSMStateID){}
 
    private Transform LeftPoint,RightPoint,Darkenemy,PlayerTrans;

    float time=0;
    float Endtime=0.6f;

    private float RunSpeed;

    private Rigidbody2D darkRB;

    private bool IsLeftPoint,IsRightPoint;

    private Animator Darkanimator;
     
    public bool JudgeHp;

    public float CurrentHp;
    
    
    


    public override void StartState(MonoBehaviour monoBehaviour)
    {
        
        if(monoBehaviour.name=="DarkEnemy ")//类名加一个空格(tmd大坑)
        {
            JudgeHp=monoBehaviour.GetComponent<DarkEnemy>().HpChange;
        }
        if(monoBehaviour.name=="BOSS1 ")//类名加一个空格(tmd大坑)
        {
            JudgeHp=monoBehaviour.GetComponent<BOSS1>();
        }
        

        PlayerTrans=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();         
        // Debug.Log("开始攻击状态");      
         Darkenemy=monoBehaviour.GetComponent<Transform>();
        darkRB=monoBehaviour.GetComponent<Rigidbody2D>();
        Darkanimator=monoBehaviour.GetComponent<Animator>();
        
        
    }

    public override void EndState()
    {
        //  Debug.Log("结束攻击状态");
         
         darkRB.velocity=new Vector2(0,0);
    }

    public override void StateUpdate()
    {
        time+=Time.deltaTime;
        if(time>Endtime)
        {
            time=0;
            // Debug.Log("攻击状态在更新");
            Darkanimator.SetTrigger("DarkAttack");
        }
        
        
    }

    public override void Transition_Condition()
    {
        if(Vector2.Distance(Darkenemy.position,PlayerTrans.position)>6)
        {
           
            mySystem.TransitionState(FSMTransition.SawPlayer);
        }else if(JudgeHp)
        {
            // Debug.Log("要受击");
            mySystem.TransitionState(FSMTransition.EnemyGetHit);
        }
        
        
        

    }

   
    public override void DelayFunction()
    {
       
    }
}
