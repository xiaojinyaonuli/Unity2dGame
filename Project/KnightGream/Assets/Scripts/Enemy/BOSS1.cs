using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BOSS1 : MonoBehaviour,CanDamage
{
    public StateSystem stateSystem;   
    public Transform Player;
    Rigidbody2D R_b;
    public  float RandomSpeed;

    public bool Attack;
    public float BOSSHp=800;

    public float CurrentHp=0;

    private SpriteRenderer Boss_sprite;
    private Color ememycolor;

    public HealthBar healthBar;

    private float Deamagehp;
    public int Damage{get;set;}
    

   private void Start()
   {
       
       CurrentHp=BOSSHp;
       Deamagehp=10;
       healthBar.SetMaxHealth(BOSSHp);
       ememycolor=GetComponent<SpriteRenderer>().color;
       Boss_sprite=GetComponent<SpriteRenderer>();
       stateSystem=new StateSystem(this);
        RandomSpeed=Random.Range(140,160);
        R_b=GetComponent<Rigidbody2D>();
        Player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StateBase BOSSCase=new BossCasePlayer(stateSystem,FSMStateID.BOSS_Case);
        BOSSCase.AddTransition(FSMTransition.BOSS_Near,FSMStateID.BOSS_Attack);
        stateSystem.AddState(BOSSCase);

        StateBase BOSSAttack=new BossAttack(stateSystem,FSMStateID.BOSS_Attack);
        BOSSAttack.AddTransition(FSMTransition.BOSS_CasePlayer,FSMStateID.BOSS_Case);
        stateSystem.AddState(BOSSAttack);
   }

   private void Update()
   {
       stateSystem.UpdateSystem();
       StartCoroutine(BossDeath());
   }

   public void JudgeAttack()
   {
       if(Vector2.Distance(transform.position,Player.position)<5)
       {
           Attack=true;
       }else{
           Attack=false;
       }
   }
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
             other.GetComponent<PLayerControl>().PlayerGetHit_FromDarkEnemy();
        }
    }


    public void resetcolor()
    {
        
        Boss_sprite.color=ememycolor;
    }
    private void Changecolor()
    {
        Boss_sprite.color=Color.blue;

        Invoke("resetcolor",0.1f);
    }
    public void EnemyGetHit(Vector2 vector)
    {
        Deamagehp=10;
        CurrentHp-=Deamagehp;
        healthBar.SetHealth(CurrentHp);
        StartCoroutine(Randomtime());
        kanAttack.Instance.attackinzhong();  
        Changecolor();
        
        
        
    }

    public void EnemyTopGetHit(Vector2 vector)
    {
        Deamagehp=15;
        CurrentHp-=Deamagehp;
        healthBar.SetHealth(CurrentHp);
        StartCoroutine(Randomtime());
        kanAttack.Instance.attackinzhong();  
        Changecolor();
    }

    public void Enemy_I_SkillGetHit(Vector2 vector)
    {
        Deamagehp=20;
        CurrentHp-=Deamagehp;
        healthBar.SetHealth(CurrentHp);
        StartCoroutine(Randomtime());
        kanAttack.Instance.attackinzhong();  
        Changecolor();
    }

     public void EnemyComboatk1GetHit(Vector2 vector)
    {
        Deamagehp=10;
        CurrentHp-=Deamagehp;
        healthBar.SetHealth(CurrentHp);
        StartCoroutine(Randomtime());
        kanAttack.Instance.attackinzhong();  
        Changecolor();
    }

    public GameObject DamageUi;
    private GameObject Uitemp;
    IEnumerator Randomtime()
    {
        float a=Random.Range(0,0.2f);
        yield return new WaitForSecondsRealtime(a);
        Uitemp= Instantiate(DamageUi,transform.position,Quaternion.identity)as GameObject;
        
        Uitemp.transform.GetChild(0).GetComponent<TMP_Text>().text="-"+Deamagehp.ToString();
        

        
    }
    public GameObject Hpbar,ChoiceBar,HintText;


    IEnumerator BossDeath()
    {
        if(CurrentHp<=0)
        {
            Hpbar.SetActive(false);
            HintText.SetActive(true);
            gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(1f);
            ChoiceBar.SetActive(true);
        }
    }
}
