using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyBase : MonoBehaviour,CanDamage
{
    [Space]

    public float RetreatSpeed;//敌人后退速度

    public float AirSpeed;//敌人被挑飞的速度

    public Vector2 Direction;// 敌人的方向

    public bool IsGetAttack;

    protected bool IsTopGetAttack;

    protected Animator animator;//获得敌人的动画

    protected Animator Impactanimator;//获得敌人子对象的动画

    protected Rigidbody2D enemyrb;//敌人的刚体
    protected AnimatorStateInfo animatorStateInfo;

    protected bool IsI_Skill_attack;//当前状态是否为 i技能攻击状态
    public float I_SkillRrtreatSpeed; //i 技能将敌人击退距离

    protected bool Comboatk1;//当前状态是否为comboatk1

    protected bool IsO_Skill_attack;////o 

     public float O_SkillRrtreatSpeed;

     public float O_Skill_TopSpeed;
    public float comboatk1Speed;//comboatk1将敌人击退的速度
     
      
     private float MaxHealth=100;
    public  float Hp=100;

    public Image HealthBar;
    public float CurrentHp;

    public int Damage{get;set;}

   

 
    protected virtual void Awake()
    {
       
        Hp=100;
        animator=this.GetComponent<Animator>();
        enemyrb=this.GetComponent<Rigidbody2D>();
        
        Impactanimator=transform.GetChild(0).GetComponent<Animator>();
        
        
       
    }
    
    protected virtual void Start()
    {
        
    //    foreach (Canvas canvas in FindObjectsOfType<Canvas>())
    //    {
    //        if(canvas.renderMode==RenderMode.WorldSpace)
    //        {
    //            Instantiate(barui,canvas.transform);
    //        }
    //    }
         CurrentHp=Hp;
        playerpoint=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        leftpoint=transform.position.x-5;
        rightpoint=transform.position.x+10;

        
        

        CurrenRunState=((int)EnemyRunState.ArriveRightPoint);

        RunSpeed=Random.Range(110,140);//随机速度防止人物重叠
        comewithspeed=Random.Range(130,150);

        RetreatSpeed=Random.Range(200,230);

        AirSpeed=Random.Range(100,150);
        
      
      
    } 

    protected virtual void Update()
    {
       startJudgeRunState();

       StartCoroutine(GetPoint());
       if(CurrenRunState==((int)EnemyRunState.GetAttack))
       {
          
       }
    //   if(CurrentHp<=0)
    //   {
    //       //animator.SetTrigger("deathimpact");
    //       enemyrb.velocity=new Vector2(0,0);
    //       Destroy(this.gameObject,1f);
    //   }
      
    }
    protected virtual void FixedUpdate()
    {
        if(IsGetAttack)
       {    
           //CurrenRunState=((int)EnemyRunState.GetAttack);
           enemyrb.velocity=new Vector2(Direction.x*RetreatSpeed*Time.deltaTime,enemyrb.velocity.y);
           StartCoroutine(delaytime());
        //    IsGetAttack=false;
       }
       if(IsTopGetAttack)
       {   
           //CurrenRunState=((int)EnemyRunState.GetAttack);
           enemyrb.velocity=new Vector2(Direction.x*RetreatSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
           StartCoroutine(delaytimeTop());
           //IsTopGetAttack=false;
       }
       if(IsI_Skill_attack)
       {
          
           //CurrenRunState=((int)EnemyRunState.GetAttack);
           enemyrb.velocity=new Vector2(Direction.x*I_SkillRrtreatSpeed*Time.deltaTime,enemyrb.velocity.y);
           StartCoroutine(delaytime());
           //IsI_Skill_attack=false;
           
       }
       if(Comboatk1)
       {
           
           //CurrenRunState=((int)EnemyRunState.GetAttack);
           enemyrb.velocity=new Vector2(Direction.x*comboatk1Speed*Time.deltaTime,enemyrb.velocity.y);
           //Comboatk1=false;
           StartCoroutine(delaytime());
       }
       if(IsO_Skill_attack)
       {
           
           //CurrenRunState=((int)EnemyRunState.GetAttack);
           enemyrb.velocity=new Vector2(Direction.x*O_SkillRrtreatSpeed*Time.deltaTime,O_Skill_TopSpeed*Time.deltaTime);
           //IsO_Skill_attack=false;
           StartCoroutine(delaytime());
       }
       
       Run();
       
    }

    
    /// <summary>
    /// 玩家普通攻击调用
    /// </summary>
    /// <param name="direction">玩家的方向 </param>
    /// 
    //
   
    public virtual void EnemyGetHit(Vector2 direction)
    {
        
        CurrenRunState=((int)EnemyRunState.GetAttack);
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsGetAttack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");

    }
    /// <summary>
    /// 玩家挑飞攻击调用
    /// </summary>
    /// <param name="direction"> 玩家的方向</param>
    public virtual void EnemyTopGetHit(Vector2 direction)
    {
        
        CurrenRunState=((int)EnemyRunState.GetAttack);
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsTopGetAttack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");

    }
    /// <summary>
    /// 玩家使用i技能时调用
    /// </summary>
    /// <param name="direction">玩家的方向</param>
    public virtual void Enemy_I_SkillGetHit(Vector2 direction)
    {
        
        
         
        CurrenRunState=((int)EnemyRunState.GetAttack);
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsI_Skill_attack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");
    }
    
    /// <summary>
    /// 玩家使用combox1技能时调用
    /// </summary>
    /// <param name="direction">玩家的方向</param>
    public virtual void EnemyComboatk1GetHit(Vector2 direction)
    {
        
        CurrenRunState=((int)EnemyRunState.GetAttack);
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        Comboatk1=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");
    }
   
    public virtual void Enemy_O_SkillGetHit(Vector2 direction)
    {
        
        CurrenRunState=((int)EnemyRunState.GetAttack);
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsI_Skill_attack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");
        
    }
    

    [Header("Run")]
    public float RunSpeed;//跑步状态下跑步速度

    public float comewithspeed;//跟随玩家状态下的跟随速度

    protected bool Isnormalrun=true;//正常run

    public bool comewithplayer;//走向玩家

    protected float leftpoint,rightpoint;//正常状态下 左右移动最大点

    protected Transform playerpoint;//玩家的位置



    protected GameObject leftobject,rightobject;//左右定点

    public float Area;

    public Stack<float> Ppoint= new Stack<float>();//实时获取玩家的位置，加入栈，如果栈顶的距离已经小于10那么已经到达

    public Stack<int> Statestack=new Stack<int>();
    
    enum EnemyRunState
    {
        None=0,
        ArriveLeftPoint,
        ArriveRightPoint,

        ComeWith,

        GetAttack,
        Enemyattack
    }

    public int CurrenRunState=((int)EnemyRunState.ArriveRightPoint);

    public int BeforeRunState;//之前的状态
    
    public bool iscome;//是否跟随

    /// <summary>
    /// 得到玩家的位置进行计算并判断玩家一部分跑步状态
    /// </summary>
    /// <returns></returns>
   public IEnumerator GetPoint()
   {
       for(int i=0;i<10;++i)
       {
           Ppoint.Push(playerpoint.position.x);

       }
       Area=Ppoint.Peek();
       if(Mathf.Abs(transform.position.x-Ppoint.Peek())<=10)
       {
                iscome=true;
                //BeforeRunState=CurrenRunState;
                if(CurrenRunState!=((int)EnemyRunState.GetAttack))
                {
                    
                    CurrenRunState=((int)EnemyRunState.ComeWith);
                }
                else{
                    
                    StartCoroutine(recovercomestate());
                }
                //CurrenRunState=((int)EnemyRunState.ComeWith);
       }else 
       {
           CurrenRunState=BeforeRunState;
           iscome=false;             
       }
    // if(CurrenRunState!=((int)EnemyRunState.GetAttack))
    // {
    //     if(Mathf.Abs(transform.position.x-Ppoint.Peek())<=10)
    //    {
    //             iscome=true;
    //             //BeforeRunState=CurrenRunState;
    //             CurrenRunState=((int)EnemyRunState.ComeWith);
    //    }else
    //    {
    //        CurrenRunState=BeforeRunState;
    //        iscome=false;
    //    }
    // }
    // else{

    // }
       yield return null;
       Ppoint.Clear();
       
   } 
   /// <summary>
   /// 
   /// </summary>
   /// <returns></returns>
   public IEnumerator recovercomestate()
   {
        yield return new WaitForSecondsRealtime(2.5f);
        CurrenRunState=((int)EnemyRunState.ComeWith);
        iscome=false;
   }
  /// <summary>
  /// 延时将isget等参数变成false,让current变成getattack
  /// </summary>
  /// <returns></returns>
   public IEnumerator delaytime()
   {
       
       yield return new WaitForSecondsRealtime(0.5f);
       IsGetAttack=false;
    //    IsTopGetAttack=false;
       IsI_Skill_attack=false;
       Comboatk1=false;
      
       
   }
   /// <summary>
   /// 单独把top拿出来
   /// </summary>
   /// <returns></returns>
   public IEnumerator delaytimeTop()
   {
       yield return new WaitForSecondsRealtime(0.5f);
       IsTopGetAttack=false;
       
      
   }
   /// <summary>
   /// 调用判断
   /// </summary>
    protected virtual void startJudgeRunState()
    {
        
        StartCoroutine(JudgeRunState());
    }
    /// <summary>
    /// 判断玩家的run状态是正常还是跟随玩家
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator JudgeRunState()
    {
        if(iscome&&CurrenRunState==((int)EnemyRunState.ComeWith))
        {
            comewithplayer=true;
            
            //yield return StartCoroutine(WithPlayer());
            WithPlayer();
        }
        else
        {
            yield return new WaitForSecondsRealtime(2f);
           
            comewithplayer=false;
        }
        if(!iscome)
        {
            if(this.transform.position.x<=leftpoint)
            {
            
                CurrenRunState=((int)EnemyRunState.ArriveLeftPoint);
                
            }
            if(this.transform.position.x>=rightpoint)
            {
            
            // arriveleftpoint=false;
             CurrenRunState=((int)EnemyRunState.ArriveRightPoint);
                
            }
           
        }
        
        
        
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            
            other.GetComponent<PLayerControl>().PlayerGetHit_FromGeBuLin();
        }
    }
    /// <summary>
    /// 敌人跑步
    /// </summary>
    protected virtual void Run()
    {
        
        if(Isnormalrun&&!comewithplayer)
        {
          
           
            if(CurrenRunState==((int)EnemyRunState.ArriveRightPoint))
            {
                BeforeRunState=CurrenRunState;
                transform.localScale=new Vector3(Vector2.left.x* Mathf.Abs( transform.localScale.x),transform.localScale.y,transform.localScale.z);
                enemyrb.velocity=new Vector2(-RunSpeed*Time.deltaTime,enemyrb.velocity.y);
            }
            if(CurrenRunState==((int)EnemyRunState.ArriveLeftPoint))
            {
                BeforeRunState=CurrenRunState;
                transform.localScale=new Vector3(Vector2.right.x*Mathf.Abs( transform.localScale.x),transform.localScale.y,transform.localScale.z);
                enemyrb.velocity=new Vector2(RunSpeed*Time.deltaTime,enemyrb.velocity.y);
            }
            
        }
        else
        {
             
        }
       
    }
    /// <summary>
    /// 跟随玩家状态下
    /// </summary>
    protected  virtual void WithPlayer()
    {
        
        if(playerpoint.position.x<transform.position.x&&CurrenRunState!=((int)EnemyRunState.GetAttack))
        {     
           
            enemyrb.velocity=new Vector2(-comewithspeed*Time.deltaTime,enemyrb.velocity.y);
            transform.localScale=new Vector3(Vector2.left.x* Mathf.Abs(transform.localScale.x),transform.localScale.y ,transform.localScale.z);
            
            if(Vector2.Distance(this.transform.position,playerpoint.position)<2&&CurrenRunState!=((int)EnemyRunState.GetAttack))
            {
                 //enemyrb.velocity=new Vector2(0,0);
                 StartCoroutine(DelayAttack());
                  //animator.SetTrigger("attack");
                  
            }
            
            //yield return null;
        }
        if(playerpoint.position.x>transform.position.x&&CurrenRunState!=((int)EnemyRunState.GetAttack))
        {
            
            enemyrb.velocity=new Vector2(comewithspeed*Time.deltaTime,enemyrb.velocity.y);
            transform.localScale=new Vector3(Vector2.right.x* Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            
            if(Vector2.Distance(this.transform.position,playerpoint.position)<2&&CurrenRunState!=((int)EnemyRunState.GetAttack))
            {
               
                //enemyrb.velocity=new Vector2(0,0);
                 // animator.SetTrigger("attack");
                 StartAttack();

                 //不同敌人攻击代码处

            }
            //yield return null;
        }


    }
    
    public virtual void StartAttack()
    {
        StartCoroutine(DelayAttack());
    }
    public IEnumerator DelayAttack()
    {
         enemyrb.velocity=new Vector2(0,0);
         yield return new WaitForSecondsRealtime(1f);
         animator.SetTrigger("attack");
    }
    protected virtual void JudgeFace()
    {
        
        
    }
     

}
