using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase 
{
    public FSMStateID myStateID{get;set;}

    public StateSystem mySystem{get;set;}
     
    public MonoBehaviour myMonoBehaviour;
    

    public Dictionary<FSMTransition,FSMStateID>Trans_ID=new Dictionary<FSMTransition, FSMStateID>();

    public StateBase(StateSystem statesystem,FSMStateID fSMStateID)
    {
        this.mySystem=statesystem;
        this.myStateID=fSMStateID;
        // this.myMonoBehaviour=monoBehaviour;
    }

    public void AddTransition(FSMTransition fSMTransition,FSMStateID fSMstateID)
    {
          if(Trans_ID.ContainsKey(fSMTransition))
          {
            //   Debug.Log("已经包含此转换状态");
              return;
          }
          Trans_ID.Add(fSMTransition,fSMstateID);
    }

    public FSMStateID GetFSMStateIdFromTransition(FSMTransition transition)
    {
        if(!Trans_ID.ContainsKey(transition))
        {
            // Debug.Log("字典中没有此转换条件");
            return FSMStateID.NONE ;
        }
        return Trans_ID[transition];
    }

     public abstract void StartState(MonoBehaviour monoBehaviour);

     public abstract void EndState();

     public abstract void StateUpdate();


     public abstract void Transition_Condition();

     public abstract void DelayFunction();

}
