using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSMStateID
{
     NONE,
     WithPlayer,
     XunLuo,
     Attack,
     GetHit,
     BOSS_Case,

     BOSS_Attack,
     

}

public enum FSMTransition
{
     NONE,
     SawPlayer,

     LeavePlayer,

     NearPlayer,

     EnemyGetHit,

     BOSS_CasePlayer,

     BOSS_Near,
}

public class StateSystem 
{
    public FSMStateID CurrentStateId;

    public StateBase CurrentState;

    private List<StateBase> StateList;

    private MonoBehaviour mymonobehaxiour{get; set;}

   public static int intid=0;
    public StateSystem(MonoBehaviour monoBehaviour)
    {
        StateList=new List<StateBase>();
        intid+=1;
        // Debug.Log("静态"+intid);
        this.mymonobehaxiour=monoBehaviour;
    }

    public void AddState(StateBase state)
    {
        if(state==null)
        {
            // Debug.Log("状态为空!");
            return;
        }

        if(StateList.Count==0)
        {
            StateList.Add(state);
            // Debug.Log("添加默认状态");
            CurrentState=state;
            CurrentStateId=state.myStateID;
            return;
        }

        foreach (var s in StateList)
        {
            if(s.myStateID==state.myStateID)
            {
                // Debug.Log(s.myStateID.ToString()+"状态已经存在");
                return;
            }
            StateList.Add(state);
            break;
        }
    }
    public void TransitionState(FSMTransition fSMTransition)
    {
        // Debug.Log("进入状态转换");
        if(fSMTransition==FSMTransition.NONE)
        {
            // Debug.Log("转换状态为空");
            return;
        }
        FSMStateID stateID=CurrentState.GetFSMStateIdFromTransition(fSMTransition);
        if(FSMStateID.NONE==stateID)
        {
            // Debug.Log("状态为空");
            return;
        }
        CurrentStateId=stateID;
        foreach (var state in StateList)
        {
            if(state.myStateID==CurrentStateId)
            {
                // Debug.Log("开始状态转换");
                CurrentState.EndState();

                CurrentState=state;

                CurrentState.StartState(mymonobehaxiour);
                break;
            }
        }
    }

    public void UpdateSystem()
    {
         CurrentState.StartState(mymonobehaxiour);
        CurrentState.StateUpdate();
        CurrentState.Transition_Condition();
    }

    public void Fixupdate()
    {
        // CurrentState.StartState();
    }

    public void UpdateDelayFunction()
    {
        CurrentState.DelayFunction();
    }

}
