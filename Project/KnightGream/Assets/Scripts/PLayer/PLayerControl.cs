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
public class PLayerControl : MonoBehaviour
{
    [Space]
    public Rigidbody2D Player_rb;//玩家刚体

    public float RunSpeed=5f,JumpSpeed;//跳跃速度，跑步速度

    public BoxCollider2D Player_foot;//玩家foot

    float horizontalmove ;//玩家水平移动速度

    private Animator animator;

    bool  isjump;//是否起挑

    int maxjumptimes;//最大起跳次数

    bool Jumpressed,isground;

    public Transform checkground;

    public LayerMask ground;
    public int FlashLightCount=0;//手电筒数量

    public TMP_Text pillstext,zuanshitext,potiontext;
    public  float DashSpeed;
    [Header("挑飞速度")]
    public float TopSpeed;

    [Header("打击感")]
    public float Shaketime;//振动时间

    public int lightsuspend;//轻攻击帧数
    public float lightstrenght;//轻攻击相机振动强度

    [Header("dash参数")]
    public float DashCool;//技能冷却时间
    private float LastTime;//最后一次释放技能时间
    private bool IsDash;
    private float SkillTimeResidue;//技能剩余时间
    public float SkillExecuteTime;//技能执行时间

    [Header("Attack参数(parameter)")]
    private int ComboCount;//combox数量
    public float AttackTime;//攻击时间
    public string comboxmode;//攻击模式

    public float AttackResidueTime=2f;//攻击剩余时间

    private bool IsAttack=true;//是否进行攻击
    public float LightAttackSpeed;//轻攻击速度

    [Header("血条")]
    private float health;

    public float maxHp=100;

    public float lerptime,LerptimeMp;

    public Image HpBack,Mpback;

    public float DelaySpeed=3f;

    public float Hp=100;

    public float Mp=100;
    public float CurrentHp;
  
    public float CurrentMp;
    public Image  PlayerHp;

    public Image PLayerMp;

    public TMP_Text Hptext,Mptext;
    
    [Header("技能图标")]
    
    public Image LSkillImage;

    public Image ISkillImage;

    public Image USkillImage;
    [Header("Pills物体数量")]
    public int PillsCount;

    public int PotionCount;

    public int ZuanCount;

    

    // Start is called before the first frame update
    void Start()
    {   
        CurrentHp=Hp;
        CurrentMp=Mp;
        Player_rb=GetComponent<Rigidbody2D>();
        Player_foot=GetComponent<BoxCollider2D>();
        animator=GetComponent<Animator>();
        pillstext.text=":"+PillsCount;
        potiontext.text=":"+PotionCount;
        zuanshitext.text=":"+ZuanCount;
        Debug.unityLogger.logEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetButtonDown("Jump")&&maxjumptimes>0)//Input.GetKeyDown(KeyCode.K)&&maxjumptimes>0)
        {
             Jumpressed=true;
        }
        animationjuedge();
        Attack();
        ControlBarHp();
        ControlBarMp();
        UpdateCollectCount();
        ConsumeGoods();
        UpdateHpMp();
        PlayerDeath();
        LSkillImage.fillAmount-=1.0f/DashCool*Time.deltaTime;
        ISkillImage.fillAmount-=1.0f/ComboAtk1Cool*Time.deltaTime;
        USkillImage.fillAmount-=1.0f/AirJAtkCool*Time.deltaTime;
    }
    private void FixedUpdate()
    {
        isground= Physics2D.OverlapCircle(checkground.position,0.1f,ground);
        Run();
        Jump();
        Dash();
        //Attack();
        testDash();
        TopAttackKey();
        
        AirjumpAtk();
        ReadyAirJUmpAttack();
         
        ComBoAtk1();
        ReadyComboAtk1();

        //ThumpAtk();
        //ReadyThumpAtk();

       
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        CanDamage canDamage=other.GetComponent<CanDamage>();
        if(canDamage!=null)
        {
                Senceinfluence_.instance.CamerastartShake(Shaketime,lightstrenght);
                Senceinfluence_.instance.StartSuspend(lightsuspend);
                
                if(comboxmode=="LightAttack")
                {
                   
                }
                //挑飞攻击代码处
                if(comboxmode=="TopAttack")
                {
                    
                    if(transform.localScale.x>0)
                    {
                        canDamage.EnemyTopGetHit(Vector2.right);
                    }
                    else if(transform.localScale.x<0)
                    {
                        canDamage.EnemyTopGetHit(Vector2.left);
                    }
                    
                }
                //空中攻击
                if(comboxmode=="Airjumpatk")
                {
                    
                    if(transform.localScale.x>0)
                    {
                        canDamage.EnemyTopGetHit(Vector2.right);
                    }
                    else if(transform.localScale.x<0)
                    {
                        canDamage.EnemyTopGetHit(Vector2.left);
                    }
                }
                //向前劈砍
                if(comboxmode=="Comboatk1")
                {
                    
                    if(transform.localScale.x>0)
                    {
                       canDamage.EnemyComboatk1GetHit(Vector2.right);
                        
                    }
                    else if(transform.localScale.x<0)
                    {
                       canDamage.EnemyComboatk1GetHit(Vector2.left);
                    
                    }
                }
                //判断Enemy朝向
                if(transform.localScale.x>0)
                {
                   canDamage.EnemyGetHit(Vector2.right);
                    
                }
                else if(transform.localScale.x<0)
                {          
                    canDamage.EnemyGetHit(Vector2.left);
                
                }

            
            
        }
        
        
    }

    


    void UpdateCollectCount()
    {
        pillstext.text=":"+PillsCount;
        potiontext.text=":"+PotionCount;
        zuanshitext.text=":"+ZuanCount;
    }
        
   


/// <summary>
/// 玩家跑步  
/// </summary>
    private void Run()
    {
        horizontalmove=Input.GetAxisRaw("Horizontal");

        if(IsAttack)
        {
            if(comboxmode=="TopAttack")
            {
                Vector2 playerrun= new Vector2 (transform.localScale.x* LightAttackSpeed*Time.deltaTime,TopSpeed*Time.deltaTime);
      
                 Player_rb.velocity=playerrun;
            }
            else
            {
                Vector2 playerrun= new Vector2 (horizontalmove*RunSpeed*Time.deltaTime,Player_rb.velocity.y);
              
                Player_rb.velocity=playerrun;
                 
            }

        }
        else 
        {
            
            if(comboxmode=="LightAttack")
            {
                   Vector2 playerrun= new Vector2 (transform.localScale.x* LightAttackSpeed*Time.deltaTime,Player_rb.velocity.y);
      
                    Player_rb.velocity=playerrun;
            }
            else
            {
                 Vector2 playerrun= new Vector2 (horizontalmove*RunSpeed*Time.deltaTime,Player_rb.velocity.y);
              
                  Player_rb.velocity=playerrun;
            }
        }
        

        PlayerFace(horizontalmove);

        animator.SetFloat("Speed",Mathf.Abs( horizontalmove));
       
       
    }


/// <summary>
/// 判断玩家的朝向
/// </summary>
    private void PlayerFace(float horizon )
    {     
        if(horizon!=0)
        {

             this.transform.localScale=new Vector3(horizon*Mathf.Abs(transform.localScale.x) ,Mathf.Abs(transform.localScale.y),Mathf.Abs(transform.localScale.z));

        }
       
      
       
    }
/// <summary>
/// 玩家跳跃
/// </summary>
    private void Jump()
    {

        if(isground)
        {
            isjump=false;
            maxjumptimes=2;

        }
        if(Jumpressed&&isground)
        {

             isjump=true;

            Player_rb.velocity=new Vector2(Player_rb.velocity.x,JumpSpeed*Time.deltaTime);
            
            maxjumptimes--;

            Jumpressed=false;

        }
        else if(Jumpressed && maxjumptimes>0 && isjump)
        {
            
            Player_rb.velocity=new Vector2(Player_rb.velocity.x,JumpSpeed*Time.deltaTime);

            maxjumptimes--;

            Jumpressed=false;
        }
                
    }
    public void animationjuedge()
    {
      if(!isground&&Player_rb.velocity.y>0)
      {
          animator.SetBool("booljump",true);
      }else if(isground)
      {
          animator.SetBool("booljump",false);
      }

    }

    private void ConsumeGoods()
    {
        if(PillsCount>0)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                PillsCount--;
                CurrentHp+=50;
            }
        }
        if(PotionCount>0)
        {
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                PotionCount--;
                CurrentMp+=30;
            }

        }

        
    }

    /// <summary>
    /// 开始尝试冲锋函数
    /// </summary>
    void testDash()
    {
       
        if(Input.GetKeyDown(KeyCode.L))
        {
            
            if(Time.time>(LastTime+DashCool))
            {       
                I_L.Instance.L_Play();
                   readyfinshDash();
            }
        }    
    }
    /// <summary>
    /// 准备好冲锋，定义各项参数
    /// </summary>
    private void readyfinshDash()
    {
       IsDash=true;  

       SkillTimeResidue=SkillExecuteTime;

       LastTime=Time.time;
       
       LSkillImage.fillAmount=1;
    }
    /// <summary>
    /// 冲锋
    /// </summary>
    private void Dash()
    {
        if(IsDash)
        {    
            if(SkillTimeResidue>0)
            {
                Player_rb.velocity=new Vector2(horizontalmove*DashSpeed*Time.deltaTime,Player_rb.velocity.y);
                SkillTimeResidue-=Time.deltaTime;
                
                ObjectPool.instance.GetFormObjectPool();
            }
            if(SkillTimeResidue<0)
            {
                IsDash=false;
            }
        }
    }

    /// <summary>
    /// 玩家攻击，设置动画
    /// </summary>
    /// 
    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J)&&IsAttack)
        {
            IsAttack=false;
            ComboCount++;
            if(ComboCount==1)
            {
            SoundManage.Instance.commatk1();
            }
            if(ComboCount==2)
            {
            SoundManage.Instance.commatk2();
            }
            if(ComboCount==3)
            {
                SoundManage.Instance.commatk3();
            }
            if(ComboCount>3)
            {
                ComboCount=1;
                SoundManage.Instance.commatk1();
            }
            comboxmode="LightAttack";
            animator.SetTrigger("CommonLightAttack");
            animator.SetInteger("CommonCombo",ComboCount);
            AttackTime=AttackResidueTime;
        }
        
        if(AttackTime!=0)
        {
            AttackTime-=Time.deltaTime;
            if(AttackTime<=0)
            {
                AttackTime=0;
                ComboCount=0;
                comboxmode="";
                IsAttack=true;
                
            }
        }
    }
    /// <summary>
    /// 添加动画帧事件，结束动画，预输入下一个攻击动画
    /// </summary>
    public void AttackOver()
    {
        IsAttack=true;
    }
    public void TestOver()
    {
        if(!IsAttack)
        {
            IsAttack=true;
        }
    }

    [Header("topAttack")]
    bool IsStart;//是否按下D或者A建
    private bool Keydown;//是否按下H建

    public float TopAttackTime=1f;//挑飞攻击进行时间

    public float TopAttackCurrentTime=0;//挑飞攻击当前时间
    public int TopAttackCount=0;//可按H键的次数
    public float EndCommbomode;//结束commbomode模式的时间

    public void TopAttackKey()
    {
        if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.A))
        {
            IsStart=true;
            TopAttackCurrentTime=TopAttackTime;
            TopAttackCount=1;
        }
        if(IsStart)
        {
           TopAttackCurrentTime-=Time.deltaTime;
           //Debug.Log("帧数"+KeyFrameCount);
           if(TopAttackCurrentTime>0)
           {
               if(Input.GetKeyDown(KeyCode.H)&&TopAttackCount>0&&isground&&IsAttack)
               {
                   TopAttackCount=0;
                   Keydown=true;                 
                   if(Keydown&&IsStart)
                   {           
                       comboxmode="TopAttack";
                       animator.SetTrigger("TopAttack");                     
                       
                        //ResetTopAttack();
                       Invoke("ResetAttackMode",EndCommbomode);
                   }
                   
               }


           }
           else
             ResetTopAttack();
        }
    }
   /// <summary>
    /// 重置挑飞攻击的某些参数
    /// </summary>
    public void ResetTopAttack()
    {
        TopAttackCurrentTime=0;
        IsStart=false;
        Keydown=false;
    }
    /// <summary>
    /// 重置AttackMode攻击模式
    /// </summary>
    public void ResetAttackMode()
    {
        comboxmode="";
    }

    [Header("AirAtk")]
    public float AirJUmpResidueTime;//当前技能剩余时间

    public float AirExecuteTime;//技能执行时间

    public float AirJAtkCool;//airatk的冷却时间

    public float LastAirAtk;//最后一次释放airatk的时间

    private  bool ISAirAtk;

    public float AirAtkSpeed;//Air攻击补偿速度
    
    /// <summary>
    /// 触发设置airjumpack
    /// </summary>
    private void  ReadyAirJUmpAttack()
    {
        if(Input.GetKeyDown(KeyCode.U)&&!isground)
        {
            if(Time.time>(LastAirAtk+AirJAtkCool))
            {
                
               SetAirJumpParameter();
            }
        }
    }
    /// <summary>
    /// 设置airatk具体的参数
    /// </summary>
    private void SetAirJumpParameter()
    {
        ISAirAtk=true;
        AirJUmpResidueTime=AirExecuteTime;
        LastAirAtk=Time.time;
        USkillImage.fillAmount=1;
        
    }
    /// <summary>
    /// airjump攻击函数
    /// </summary>
    private void AirjumpAtk()
    {
        if(ISAirAtk)
        {
            if(AirJUmpResidueTime>0)
            {
                Player_rb.velocity=new Vector2(transform.localScale.x*AirAtkSpeed*Time.deltaTime,Player_rb.velocity.y);
                AirJUmpResidueTime-=Time.deltaTime;
                animator.SetTrigger("AirJumpAttack");
                comboxmode="Airjumpatk";
                Invoke("ResetAttackMode",0.4f);
            }
            if(AirJUmpResidueTime<=0)
            {
                AirJUmpResidueTime=0;
                ISAirAtk=false;
                
            }
        }
    }
    
    [Header("向前劈砍")]
    public float ComboAtk1ResidueTime;//当前技能剩余时间

    public float ComboAtk1ExecuteTime;//技能执行时间

    public float ComboAtk1Cool;//ComboAtk的冷却时间

    public float LastComboAtk1;//最后一次释放ComboAtk的时间

    private  bool ISComboAtk1;

    public float ComboAtk1Speed;//Air攻击补偿速度

    

     
    private void ReadyComboAtk1()
    {
                                 
        if(Input.GetKeyDown(KeyCode.I)&&CurrentMp>5)
        {
                
            

            if(Time.time>(LastComboAtk1+ComboAtk1Cool))
            {
                LerptimeMp=0;
                ISComboAtk1=true;
                CurrentMp-=5;
                I_L.Instance.I_Play();
                ComboAtk1ResidueTime=ComboAtk1ExecuteTime;
                LastComboAtk1=Time.time;
                ISkillImage.fillAmount=1;
            }                      
                    
        }              
                                                         
    }


    private void ComBoAtk1()
    {
         if(ISComboAtk1)
         {
             ComboAtk1ResidueTime-=Time.deltaTime;
             if(ComboAtk1ResidueTime>0)
             {
               
                Player_rb.velocity=new Vector2(transform.localScale.x*ComboAtk1Speed*Time.deltaTime,Player_rb.velocity.y);
                // AirJUmpResidueTime-=Time.deltaTime;
                animator.SetTrigger("Comboatk1");
                comboxmode="Comboatk1";
                Invoke("ResetAttackMode",0.4f);
                SkillMap.Instance.GetFromSkillMap();
             }
             if(ComboAtk1ResidueTime<=0)
             {
                 ComboAtk1ResidueTime=0;
                 ISComboAtk1=false;
             }

         }
    }

    [Header("周围重击")]
    public float ThumpAtkResidueTime;//当前技能剩余时间

    public float ThumpAtkExecuteTime;//技能执行时间

    public float ThumpAtkCool;//ThumpAtk的冷却时间

    public float LastThumpAtk;//最后一次释放ThumpAtk的时间

    private  bool ISThumpAtk;

    public float ThumpAtkSpeed;//Thump攻击补偿速度


    private void ReadyThumpAtk()
    {
                                 
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(Time.time>(LastThumpAtk+ThumpAtkCool))
            {

                ISThumpAtk=true;
                ThumpAtkResidueTime=ThumpAtkExecuteTime;
                LastThumpAtk=Time.time;
            }                      
                    
        }              
                                                         
    }

    
    private void ThumpAtk()
    {
         if(ISThumpAtk)
         {
             ThumpAtkResidueTime-=Time.deltaTime;
             if(ThumpAtkResidueTime>0)
             {
                
                Player_rb.velocity=new Vector2(transform.localScale.x*ThumpAtkSpeed*Time.deltaTime,Player_rb.velocity.y);
                animator.SetTrigger("ThumpAtk");
                comboxmode="ThumpAtk";
                Invoke("ResetAttackMode",0.4f);
                SkillMap.Instance.O_GetFromSkillMap();
             }
             if(ThumpAtkResidueTime<=0)
             {
                 ThumpAtkResidueTime=0;
                 ISThumpAtk=false;
             }

         }
    }

    public void PlayerGetHit_FromGeBuLin()
    {
         lerptime=0;
         CurrentHp-=2;
        //  PlayerHp.fillAmount=CurrentHp/Hp;
        // ControlBarHp();
         SoundManage.Instance.PlayerGethit();
         animator.SetTrigger("SuatainAttack");
    }

    public void PlayerGetHit_FromDarkEnemy()
    {
        lerptime=0;
        CurrentHp-=6;
        //   PlayerHp.fillAmount=CurrentHp/Hp;
        // ControlBarHp();
        SoundManage.Instance.PlayerGethit();
        animator.SetTrigger("SuatainAttack");
    }
    
    public float FillHp,BackHp,FillMp,BackMp;
    public void ControlBarHp()
    {
        FillHp=PlayerHp.fillAmount;
        BackHp=HpBack.fillAmount;
        float Temp=CurrentHp/Hp;
        if(BackHp>Temp)
        {
            PlayerHp.fillAmount=CurrentHp/Hp;
            HpBack.color=Color.blue;
            lerptime+=Time.deltaTime;
            float lerppercent=lerptime/DelaySpeed;
            lerppercent*=lerppercent;
            HpBack.fillAmount=Mathf.Lerp(BackHp,Temp,lerppercent);
        }
        if(FillHp<Temp)
        {
            HpBack.color=Color.blue;
            HpBack.fillAmount=Temp;
             float lerppercent=lerptime/DelaySpeed;
            lerppercent*=lerppercent;
            PlayerHp.fillAmount=Mathf.Lerp(FillHp,HpBack.fillAmount,lerppercent);
        }
        // PlayerHp.fillAmount=CurrentHp/Hp;

    }

    public void ControlBarMp()
    {
        FillMp=PLayerMp.fillAmount;
        BackMp=Mpback.fillAmount;
        float Temp=CurrentMp/Mp;
        if(BackMp>Temp)
        {
            PLayerMp.fillAmount=CurrentMp/Mp;
            Mpback.color=Color.blue;
            LerptimeMp+=Time.deltaTime;
            float lerppercent=LerptimeMp/DelaySpeed;
            lerppercent*=lerppercent;
            Mpback.fillAmount=Mathf.Lerp(BackMp,Temp,lerppercent);
        }
        if(FillMp<Temp)
        {
            Mpback.color=Color.blue;
            Mpback.fillAmount=Temp;
             float lerppercent=lerptime/DelaySpeed;
            lerppercent*=lerppercent;
            PLayerMp.fillAmount=Mathf.Lerp(BackMp,Mpback.fillAmount,lerppercent);
        }
    }

    void UpdateHpMp()
    {
        if(CurrentHp>100)
        {
            CurrentHp=100;
        }else if(CurrentMp>100)
        {
            CurrentMp=100;
        }
        Hptext.text=CurrentHp+"/"+100;
        Mptext.text=CurrentMp+"/"+100;
    }

    public GameObject DeathUi;
    void PlayerDeath()
    {
        if(CurrentHp<=0)
        {
            CurrentHp=0;
            animator.SetFloat("Hp",CurrentHp);
            Time.timeScale=0;
           DeathUi.SetActive(true);
           
        }
    }

   
    





}
