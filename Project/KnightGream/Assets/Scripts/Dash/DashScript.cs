using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private Transform Player;

    private SpriteRenderer thisperfabSprite;//预制体精灵

    private SpriteRenderer PlayerSprite;//玩家精灵
  
    [Header("Das参数")]
    public float StartTime;//开始时间
    public float execute;//执行时间

    [Header("颜色参数")]
    private Color color;
    private float alpha;//透明度
    public float alpha_Start_Value;

    public float ChangeAlpha;

    private void OnEnable()
    {

        alpha=alpha_Start_Value;

        StartTime=Time.time;
    
        Player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform;

        thisperfabSprite=this.GetComponent<SpriteRenderer>();

        PlayerSprite=GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();

        thisperfabSprite.sprite=PlayerSprite.sprite;

        transform.position=Player.position;

        transform.rotation=Player.rotation;

        transform.localScale=Player.localScale;
        
    }
    private void Update()
    {
        alpha*=ChangeAlpha;
        
        color=new Color(0.5f,0.5f,1f,alpha);

         thisperfabSprite.color=color;

        if( (StartTime+execute)<Time.time )
        {
           ObjectPool.instance.push_backpool(this.gameObject);
        }
    }
    


}
