using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xunluo :StateBase
{
   
   public xunluo(StateSystem stateSystem,FSMStateID fSMStateID):base(stateSystem,fSMStateID){}
 
   private Transform Darkenemy,PlayerTrans;

   private float LeftPoint,RightPoint,PointY;

   float time=0;
    float Endtime=1;

    private float RunSpeed;

    private Rigidbody2D darkRB;

    private bool IsLeftPoint,IsRightPoint;

    private static int intid;

    public override void StartState(MonoBehaviour monoBehaviour)
    {
         
         LeftPoint=monoBehaviour.GetComponent<DarkEnemy>().LeftPoint;  
         RightPoint=monoBehaviour.GetComponent<DarkEnemy>().RightPoint;  
         PointY=monoBehaviour.GetComponent<DarkEnemy>().GamePoint.position.y;     
         RunSpeed=100f;
         Darkenemy=monoBehaviour.GetComponent<Transform>();
         darkRB=monoBehaviour.GetComponent<Rigidbody2D>();       
         IsLeftPoint=true;
         PlayerTrans=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //  Debug.Log("开始巡逻状态");
    }

    public override void EndState()
    {
        //  Debug.Log("结束巡逻状态");
         darkRB.velocity=new Vector2(0,0);
    }

    public override void StateUpdate()
    {
        time+=Time.deltaTime;
        if(time>Endtime)
        {
            time=0;
            // Debug.Log("巡逻状态在更新");
        }
        if(Darkenemy.position.x<=LeftPoint)
        {
            IsRightPoint=true;
            IsLeftPoint=false;
        }
        if(Darkenemy.position.x>=RightPoint)
        {
            IsLeftPoint=true;
            IsRightPoint=false;
        }
        
        xunluoRun();
    }

    public override void Transition_Condition()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            // Debug.Log("按下x");
            mySystem.TransitionState(FSMTransition.SawPlayer);
        }
        if(Vector2.Distance(Darkenemy.position,PlayerTrans.position)<15)
        {
            mySystem.TransitionState(FSMTransition.SawPlayer);
        }

    }

    protected void xunluoRun()
    {
        
        if(IsLeftPoint)
        {
            Darkenemy.localScale=new Vector3(1*Mathf.Abs(Darkenemy.localScale.x),Darkenemy.localScale.y,Darkenemy.localScale.z);
            if(Darkenemy.position.y<PointY)
            {
                darkRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,200f);
            }
            else
               darkRB.velocity=new Vector2(-RunSpeed*Time.deltaTime,0);
        }


        if(IsRightPoint)
        {
             Darkenemy.localScale=new Vector3(-1*Mathf.Abs(Darkenemy.localScale.x),Darkenemy.localScale.y,Darkenemy.localScale.z);
             
              if(Darkenemy.position.y<PointY)
                {
                    darkRB.velocity=new Vector2(RunSpeed*Time.deltaTime,200f);
                }
                else
                  darkRB.velocity=new Vector2(RunSpeed*Time.deltaTime,0f);
        }
        

    }

    public override void DelayFunction()
    {
       
    }
}
