using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISkill : MonoBehaviour
{

    
    public static ISkill  I; //实例化iskill

    private  Transform  PlayerPosition;//获取玩家子物体Skillpoint的位置

    private Transform Player;//获取玩家位置
    
    public float  SkillexecuteTime; //I技能执行时间

    public float LastReleaseSkillTime;//最后一次释放I技能的时间
 
    private Rigidbody2D ISkillPrefab_rb;//ISkill预制体的刚体
    public float SkillSpeed;//i技能的速度

    public float CamreaShaketime;//相机摇晃的时间

    public float SkillStrenght;//摇晃的强度

    public int SkillSuspend;//顿帧相关

    private void Awake()
    {
        I=this;

    }
    
    private void OnEnable()
    {
       LastReleaseSkillTime=Time.time;
       PlayerPosition=GameObject.FindGameObjectWithTag("SkillPoint").GetComponent<Transform>();
       this.transform.position=PlayerPosition.position;
       Player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       ISkillPrefab_rb=GetComponent<Rigidbody2D>();
       JudgeSkillface();
       Debug.unityLogger.logEnabled = false;

    }
    private void Update()
    {
        GameobjectEnque();

    }


    /// <summary>
    /// 在给定时间内回收对象进入skillmap
    /// </summary> 
    private void GameobjectEnque()
    {
        if((LastReleaseSkillTime+SkillexecuteTime)<Time.time)
        {
            SkillMap.Instance.push_back(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
         CanDamage canDamage=other.GetComponent<CanDamage>();
        if(canDamage!=null)
        {
             Senceinfluence_.instance.CamerastartShake(CamreaShaketime,SkillStrenght);
             Senceinfluence_.instance.StartSuspend(SkillSuspend);
             if(this.transform.localScale.x>0)
             {
                canDamage.Enemy_I_SkillGetHit(Vector2.right);
             }
             if(this.transform.localScale.x<0)
             {
                 canDamage.Enemy_I_SkillGetHit(Vector2.left);
             }
        }
        
    }

    /// <summary>
    /// 判断技能的方向是否和玩家一致，并给预制体一个速度
    /// </summary>
    private void JudgeSkillface()
    {
       if(Player.localScale.x>0)
       {
           this.transform.localScale=new Vector3(Vector2.right.x*Mathf.Abs(this.transform.localScale.x),transform.localScale.y,transform.localScale.z);
           
           ISkillPrefab_rb.velocity=new Vector2(Vector2.right.x*SkillSpeed*Time.deltaTime,ISkillPrefab_rb.velocity.y);

       }
       if(Player.localScale.x<0)
       {
           this.transform.localScale=new Vector3(Vector2.left.x*Mathf.Abs(this.transform.localScale.x),transform.localScale.y,transform.localScale.z);

           ISkillPrefab_rb.velocity=new Vector2(Vector2.left.x*SkillSpeed*Time.deltaTime,ISkillPrefab_rb.velocity.y);
       }
    }

    
}
