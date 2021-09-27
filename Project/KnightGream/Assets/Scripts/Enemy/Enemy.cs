using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Space]

    public float RetreatSpeed;//敌人后退速度

    public float AirSpeed;//敌人被挑飞的速度

    public Vector2 Direction;// 敌人的方向

    public bool IsGetAttack;

    private bool IsTopGetAttack;

    private Animator animator;//获得敌人的动画

    private Animator Impactanimator;//获得敌人子对象的动画
    private Rigidbody2D enemyrb;//敌人的刚体
    private AnimatorStateInfo animatorStateInfo;

    private bool IsI_Skill_attack;//当前状态是否为 i技能攻击状态
    public float I_SkillRrtreatSpeed; //i 技能将敌人击退距离

    private bool Comboatk1;//当前状态是否为comboatk1

    private bool IsO_Skill_attack;////o 

     public float O_SkillRrtreatSpeed;

     public float O_Skill_TopSpeed;
    public float comboatk1Speed;//comboatk1将敌人击退的速度

    public float Hp;

    private void Awake()
    {
        animator=this.GetComponent<Animator>();
        enemyrb=this.GetComponent<Rigidbody2D>();
        Impactanimator=transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        playerpoint=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        if(IsGetAttack)
       {    Hp-=1;
           enemyrb.velocity=new Vector2(Direction.x*RetreatSpeed*Time.deltaTime,enemyrb.velocity.y);
           IsGetAttack=false;
       }
       if(IsTopGetAttack)
       {
           enemyrb.velocity=new Vector2(Direction.x*RetreatSpeed*Time.deltaTime,AirSpeed*Time.deltaTime);
           IsTopGetAttack=false;
       }
       if(IsI_Skill_attack)
       {
           enemyrb.velocity=new Vector2(Direction.x*I_SkillRrtreatSpeed*Time.deltaTime,enemyrb.velocity.y);
           IsI_Skill_attack=false;
           
       }
       if(Comboatk1)
       {
           enemyrb.velocity=new Vector2(Direction.x*comboatk1Speed*Time.deltaTime,enemyrb.velocity.y);
           Comboatk1=false;
       }
       if(IsO_Skill_attack)
       {
           enemyrb.velocity=new Vector2(Direction.x*O_SkillRrtreatSpeed*Time.deltaTime,O_Skill_TopSpeed*Time.deltaTime);
           IsO_Skill_attack=false;
       }
    }
    /// <summary>
    /// 玩家普通攻击调用
    /// </summary>
    /// <param name="direction">玩家的方向 </param>
    public void EnemyGetHit(Vector2 direction)
    {
       
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsGetAttack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");

    }
    /// <summary>
    /// 玩家挑飞攻击调用
    /// </summary>
    /// <param name="direction"> 玩家的方向</param>
    public void EnemyTopGetHit(Vector2 direction)
    {
       
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsTopGetAttack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");

    }
    /// <summary>
    /// 玩家使用i技能时调用
    /// </summary>
    /// <param name="direction">玩家的方向</param>
    public void Enemy_I_SkillGetHit(Vector2 direction)
    {
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsI_Skill_attack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");
    }
    
    /// <summary>
    /// 玩家使用combox1技能时调用
    /// </summary>
    /// <param name="direction">玩家的方向</param>
    public void EnemyComboatk1GetHit(Vector2 direction)
    {
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        Comboatk1=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");
    }
   
    public void Enemy_O_SkillGetHit(Vector2 direction)
    {
        transform.localScale=new Vector3(-1*direction.x*Mathf.Abs(((float)this.transform.localScale.x)),((float)this.transform.localScale.y),((float)this.transform.localScale.z));
        IsI_Skill_attack=true;
        this.Direction=direction;
        Impactanimator.SetTrigger("Isimpact");
    }
    
    [Header("Run")]
    public float RunSpeed;

    private bool Isnormalrun;//正常run

    private bool comewithplayer;//走向玩家

    private Transform leftpoint,rightpoint;//正常状态下 左右移动最大点

    private Transform playerpoint;

    private void JudgeRunState()
    {
        
    }
    private void Run()
    {
        
    }



}
