using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oskill : MonoBehaviour
{
     public static Oskill  O; //实例化iskill

    private  Transform  PlayerPosition;//获取玩家子物体Skillpoint的位置

    private Transform Player;//获取玩家位置
    
    public float  SkillexecuteTime; //I技能执行时间

    public float LastReleaseSkillTime;//最后一次释放O技能的时间
 
    private Rigidbody2D OSkillPrefab_rb;//OSkill预制体的刚体
    public float SkillSpeed;//i技能的速度

    public float CamreaShaketime;//相机摇晃的时间

    public float SkillStrenght;//摇晃的强度

    public int SkillSuspend;//顿帧相关

    public int CrashConut;

    private void Awake()
    {
        O=this;

    }
    
    private void OnEnable()
    {
       LastReleaseSkillTime=Time.time;
       PlayerPosition=GameObject.FindGameObjectWithTag("SkillPoint").GetComponent<Transform>();
       this.transform.position=PlayerPosition.position;
       Player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       OSkillPrefab_rb=GetComponent<Rigidbody2D>();
       JudgeSkillface();

    }
    
    private void Update()
    {
        Gameobject_o_Enque();
    }
    /// <summary>
    /// 在给定时间内回收对象进入skillmap
    /// </summary> 
    private void Gameobject_o_Enque()
    {
        if((LastReleaseSkillTime+SkillexecuteTime)<Time.time)
        {
            SkillMap.Instance.O_push_back(this.gameObject);
        }
    }
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy")
        {    CrashConut++;
             if(CrashConut>7)
             {
                 CrashConut=0;
             }
            Senceinfluence_.instance.CamerastartShake(CamreaShaketime,SkillStrenght);
            Senceinfluence_.instance.StartSuspend(SkillSuspend);
             if(this.transform.localScale.x>0)
             {
                 other.GetComponent<Enemy>().Enemy_I_SkillGetHit(Vector2.right);
             }
             if(this.transform.localScale.x<0)
             {
                 other.GetComponent<Enemy>().Enemy_I_SkillGetHit(Vector2.left);
             }
             
        }
        //   startcrash(CrashConut);
    }


    /// <summary>
    /// 判断技能的方向是否和玩家一致，并给预制体一个速度
    /// </summary>
     private void JudgeSkillface()
    {
       if(Player.localScale.x>0)
       {
           this.transform.localScale=new Vector3(Vector2.right.x*Mathf.Abs(this.transform.localScale.x),transform.localScale.y,transform.localScale.z);
           
           OSkillPrefab_rb.velocity=new Vector2(Vector2.right.x*SkillSpeed*Time.deltaTime,OSkillPrefab_rb.velocity.y);

       }
       if(Player.localScale.x<0)
       {
           this.transform.localScale=new Vector3(Vector2.left.x*Mathf.Abs(this.transform.localScale.x),transform.localScale.y,transform.localScale.z);

           OSkillPrefab_rb.velocity=new Vector2(Vector2.left.x*SkillSpeed*Time.deltaTime,OSkillPrefab_rb.velocity.y);
       }
    }

    IEnumerator skillgoback(int crash)
    {
        if(this.transform.localScale.x>0&&crash<7)
        {
            OSkillPrefab_rb.velocity=new Vector2(Vector2.right.x*SkillSpeed*Time.deltaTime,OSkillPrefab_rb.velocity.y);

            yield return new WaitForSecondsRealtime(0.5f);
            
            OSkillPrefab_rb.velocity=new Vector2(Vector2.left.x*SkillSpeed*Time.deltaTime,OSkillPrefab_rb.velocity.y);

        }
        if(this.transform.localScale.x<0&&crash<7)
        {
            OSkillPrefab_rb.velocity=new Vector2(Vector2.left.x*SkillSpeed*Time.deltaTime,OSkillPrefab_rb.velocity.y);

            yield return new WaitForSecondsRealtime(0.5f);
            
            OSkillPrefab_rb.velocity=new Vector2(Vector2.left.x*SkillSpeed*Time.deltaTime,OSkillPrefab_rb.velocity.y);

        }
    }

    public void startcrash(int crash)
    {
         StartCoroutine( skillgoback(crash) );
    }
}
