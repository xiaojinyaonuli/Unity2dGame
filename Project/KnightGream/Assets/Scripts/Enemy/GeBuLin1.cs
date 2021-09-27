using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GeBuLin1 : EnemyBase
{
   public GameObject DeathPerfab;

   public GameObject Goods1,Goods2,Goods3;

   public GameObject[]  GoodsPerfab;

   public GameObject explosion;

   private GameObject GoodsTemp;

   private SpriteRenderer gebulin;

   public GameObject DamageUi;

   private Color ememycolor;

   private float DamageHp=10,DamageForISkill=18;
    protected override void Start()
    {
       //GoodsPerfab= new GameObject[6]{Goods1,null,Goods2,Goods3,null,null};
       
        // ealthBar.SetMaxHealth(Hp);
        // a.SetMaxHealth(Hp);
        gebulin=GetComponent<SpriteRenderer>();
        ememycolor=GetComponent<SpriteRenderer>().color;
        base.Start();
    }

    protected override void Awake()
    {
       //this.enemyrb=this.GetComponent<Rigidbody2D>();
        base.Awake();
       
    }
    public void resetcolor()
    {
        
        gebulin.color=ememycolor;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }

    private void Changecolor()
    {
        gebulin.color=Color.red;

        Invoke("resetcolor",0.1f);
    }

    protected override void Update()
    {
        EnemyDeath();
        base.Update();
        
        
        animator.SetFloat("enemyspeed",Mathf.Abs( enemyrb.velocity.x));
        
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
    public int count=0;
    public void EnemyDeath()
    {
        if(CurrentHp<=0)
        {
            
                enemyrb.velocity=new Vector2(0,0);
                
                // DeathPerfab.SetActive(true);
                //geexplosion.Instance.enemyexplosion();
                Vector3 pos=transform.position;
                Instantiate(explosion,transform.position,Quaternion.identity);
                // Enemy_Pool.instance.GetFormObjectPool_Explosion();
                if(!JuegeGoods())
                {
                    Instantiate(GoodsPerfab[Random.Range(0,GoodsPerfab.Length)],transform.position,Quaternion.identity);
                    //Enemy_Pool.instance.GetFormObjectPool_Explosion();
                }
                //Instantiate(GoodsPerfab[Random.Range(0,GoodsPerfab.Length)],transform.position,Quaternion.identity);
                Destroy(this.gameObject);
               
           
                
            //animator.SetTrigger("deathimpact");
           
            
            
        }
    }
    protected override  void OnTriggerEnter2D(Collider2D other)
    {
            base.OnTriggerEnter2D(other);
    }
/// <summary>
/// 定义一个接口,
/// </summary>
/// <param name="direction"></param>
    public override void EnemyGetHit(Vector2 direction)
    {
        //  ealthBar.SetHealth(Hp);
        //     a.SetHealth(Hp);
         DamageHp=10;
         CurrentHp-=DamageHp;

         StartCoroutine(Randomtime());
         
         Changecolor();
         HealthBar.fillAmount=CurrentHp/Hp;
         kanAttack.Instance.attackinzhong();
         EnemyCall.Instance.enemycall();
         base.EnemyGetHit(direction);
    }

    public override void EnemyTopGetHit(Vector2 direction)
    {
        DamageHp=5;
        CurrentHp-=DamageHp; 
        StartCoroutine(Randomtime());
        HealthBar.fillAmount=CurrentHp/Hp;
        kanAttack.Instance.attackinzhong();
        EnemyCall.Instance.enemycall();
        base.EnemyTopGetHit(direction);
    }

    public override void EnemyComboatk1GetHit(Vector2 direction)
    {  

        CurrentHp-=DamageHp; 
        Changecolor();
        HealthBar.fillAmount=CurrentHp/Hp;
        kanAttack.Instance.attackinzhong();
        EnemyCall.Instance.enemycall();
        base.EnemyComboatk1GetHit(direction);
    }

    public override void Enemy_I_SkillGetHit(Vector2 direction)
    {
        DamageHp=18;
        StartCoroutine(Randomtime());
        CurrentHp-=DamageForISkill; 
        Changecolor();
        kanAttack.Instance.attackinzhong();
        HealthBar.fillAmount=CurrentHp/Hp;
        EnemyCall.Instance.enemycall();
        base.Enemy_I_SkillGetHit(direction);
    }

    public override void Enemy_O_SkillGetHit(Vector2 direction)
    {
        CurrentHp-=10; 
        HealthBar.fillAmount=CurrentHp/Hp;
        base.Enemy_O_SkillGetHit(direction);
    }


    protected override void Run()
    {
        base.Run();
       
    }
    protected override void JudgeFace()
    {
        base.JudgeFace();
    }

    protected override void WithPlayer()
    {
        base.WithPlayer();
    }
    
    private GameObject Uitemp;
    IEnumerator Randomtime()
    {
        float a=Random.Range(0,0.2f);
        yield return new WaitForSecondsRealtime(a);
        Uitemp= Instantiate(DamageUi,transform.position,Quaternion.identity)as GameObject;
        
        Uitemp.transform.GetChild(0).GetComponent<TMP_Text>().text="-"+DamageHp.ToString();
        

        
    }
}
