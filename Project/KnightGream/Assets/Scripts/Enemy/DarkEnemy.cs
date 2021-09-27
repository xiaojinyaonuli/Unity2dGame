/* ==================================================================
* Copyright (c) 2021,@laochai
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions
* are met:
*
* 1. Redistributions of source code must retain the above copyright
* 2. Redistributions in binary form must reproduce the above copyright
* 3.This project can only be used for study and exchange
* and can not be used for commercial purposes without my permission
* You can contact me on the blog:https://blog.csdn.net/m0_46472878
* ===================================================================
* Author:@laochai
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DarkEnemy : MonoBehaviour,CanDamage
{
    public StateSystem stateSystem;

    public float LeftPoint,RightPoint;

    public  float RandomSpeed;

    public float DarkEnemyHp=50;

    public float CurrentHp=0;

    public float JudegeHp;

    public Image HealthBar;

    public bool HpChange;

    private float DamageHp=10;

    public float comboAttack_Retreat_Speed,ISkill_Restreat_Speed;

    Rigidbody2D R_b;
    private Color ememycolor;
    private SpriteRenderer Dark;

    public Vector2 PlayerVector; 

    public bool IsiSkill,IsTopSkill,IsAirJump;

    public Transform GamePoint;

    public Animator animator;

    public GameObject[]  GoodsPerfab;


    public int Damage{get;set;}//接口属性是公有，设置为private有错误

    public GameObject explosion;

    private GameObject GoodsTemp;

    void Start()
    {
        CurrentHp=DarkEnemyHp;
        JudegeHp=DarkEnemyHp;
        GamePoint=transform;
        LeftPoint=transform.position.x-10;
        RightPoint=transform.position.x+10;
        animator=GetComponent<Animator>();
        RandomSpeed=Random.Range(140,160);
        R_b=GetComponent<Rigidbody2D>();
        ememycolor=GetComponent<SpriteRenderer>().color;
        Dark=GetComponent<SpriteRenderer>();
        stateSystem=new StateSystem(this);
   
        StateBase Idel_XunLuo=new xunluo(stateSystem,FSMStateID.XunLuo);
        Idel_XunLuo.AddTransition(FSMTransition.SawPlayer,FSMStateID.WithPlayer);
        stateSystem.AddState(Idel_XunLuo);

        StateBase PlayerWith=new CasePlayer(stateSystem,FSMStateID.WithPlayer);
        PlayerWith.AddTransition(FSMTransition.LeavePlayer,FSMStateID.XunLuo);
        PlayerWith.AddTransition(FSMTransition.NearPlayer,FSMStateID.Attack);
        PlayerWith.AddTransition(FSMTransition.EnemyGetHit,FSMStateID.GetHit);
        stateSystem.AddState(PlayerWith);
        
        StateBase Attack= new AttackState(stateSystem,FSMStateID.Attack);
         Attack.AddTransition(FSMTransition.SawPlayer,FSMStateID.WithPlayer);
         Attack.AddTransition(FSMTransition.EnemyGetHit,FSMStateID.GetHit);
         stateSystem.AddState(Attack);

         StateBase GetHit =new GetHitState(stateSystem,FSMStateID.GetHit);
         GetHit.AddTransition(FSMTransition.SawPlayer,FSMStateID.WithPlayer);
         GetHit.AddTransition(FSMTransition.NearPlayer,FSMStateID.Attack);
         stateSystem.AddState(GetHit);
        
        
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
        stateSystem.UpdateSystem();
        
        EnemyDeath();
       
        
    }
    private void FixedUpdate()
    {
        stateSystem.Fixupdate();
       

        
        

    }

     private bool JuegeGoods()
    {
        GoodsTemp=GoodsPerfab[Random.Range(0,GoodsPerfab.Length)];
        if(GoodsTemp==null)
        {
            
            return true;
        }
        else 
            return false; 
    }

    void Delay()
    {
        
         //yield return new WaitForSecondsRealtime(3f);
        stateSystem.UpdateDelayFunction();
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
             other.GetComponent<PLayerControl>().PlayerGetHit_FromDarkEnemy();
        }
    }

    public void EnemyGetHit(Vector2 vector)
    {
        DamageHp=8;
        CurrentHp-=DamageHp;   
        StartCoroutine(Randomtime());
        HealthBar.fillAmount=CurrentHp/DarkEnemyHp;
        JudegeHp=CurrentHp;
        kanAttack.Instance.attackinzhong();
        DarkCall.Instance.enemycall();
        HpChange=true;
        Changecolor();
        PlayerVector=vector;
        StartCoroutine(ReleaseHpchange());
        
        
    }

    public void EnemyTopGetHit(Vector2 vector)
    {
        DamageHp=10;
        CurrentHp-=DamageHp;   
        StartCoroutine(Randomtime());
        HealthBar.fillAmount=CurrentHp/DarkEnemyHp;
        JudegeHp=CurrentHp;
        kanAttack.Instance.attackinzhong();
        DarkCall.Instance.enemycall();
        IsAirJump=true;
        Changecolor();
        PlayerVector=vector;
        StartCoroutine(ReleaseHpchange());
    }

    public void Enemy_I_SkillGetHit(Vector2 vector)
    {
        DamageHp=16;
        CurrentHp-=DamageHp;
        StartCoroutine(Randomtime());
        HealthBar.fillAmount=CurrentHp/DarkEnemyHp;
        DarkCall.Instance.enemycall();
        kanAttack.Instance.attackinzhong();
        Changecolor();
        IsiSkill=true;
        PlayerVector=vector;
    }

    public void EnemyComboatk1GetHit(Vector2 vector)
    {
            
    }

    
    IEnumerator ReleaseHpchange()
    {
        yield return new WaitForSecondsRealtime(1f);
        HpChange=false;
        IsAirJump=false;
        IsiSkill=false;
        
    }

    public void resetcolor()
    {
        
        Dark.color=ememycolor;
    }
    private void Changecolor()
    {
        Dark.color=Color.blue;

        Invoke("resetcolor",0.1f);
    }

    private void EnemyDeath()
    {
        if(CurrentHp<=0)
        {
            Instantiate(explosion,transform.position,Quaternion.identity);
            if(!JuegeGoods())
            {
                Instantiate(GoodsPerfab[Random.Range(0,GoodsPerfab.Length)],transform.position,Quaternion.identity);
                //Enemy_Pool.instance.GetFormObjectPool_Explosion();
            }
            Destroy(this.gameObject);
        }
    }

    public GameObject DamageUi;
    private GameObject Uitemp;
    IEnumerator Randomtime()
    {
        float a=Random.Range(0,0.2f);
        yield return new WaitForSecondsRealtime(a);
        Uitemp= Instantiate(DamageUi,transform.position,Quaternion.identity)as GameObject;
        
        Uitemp.transform.GetChild(0).GetComponent<TMP_Text>().text="-"+DamageHp.ToString();
        

        
    }


}
