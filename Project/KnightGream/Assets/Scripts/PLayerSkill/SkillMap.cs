using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMap : MonoBehaviour
{
    public static SkillMap Instance; //实例化

    public GameObject I_SkillPrefab;//i 技能预制体

    public GameObject O_SkillPrefab;//o 技能预制体

    private Queue<GameObject> I_SkillQue= new Queue<GameObject>();

    private  Queue<GameObject> O_SkillQue= new Queue<GameObject>();
    private Dictionary<string,Queue<GameObject>> SkillDic= new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance=this;
    }
    private void Start()
    {
        SkillDic.Add("I_Skill",I_SkillQue);
        SkillDic.Add("O_Skill",O_SkillQue);
        InstanceI_SkillQue();
        InstanceO_SkillQue();
    }

    /// <summary>
    /// 给I技能队列添加元素
    /// </summary>
    private void InstanceI_SkillQue()
    {
        for(int i=0;i<1;i++)
        {
             var newISkill= Instantiate(I_SkillPrefab);
             push_back(newISkill);
        }
    }
    /// <summary>
    /// 给O技能队列添加元素
    /// </summary>
    private void InstanceO_SkillQue()
    {
        for(int i=0;i<1;i++)
        {
             var newOSkill= Instantiate(O_SkillPrefab);
             O_push_back(newOSkill);
        }
    }
    
    /// <summary>
    /// 将使用完的对象（预制体）放回pool中
    /// </summary>
    /// <param name="Skill">Skill 技能对象  </param>
    public void push_back(GameObject Skill)
    {
        Skill.SetActive(false);

        I_SkillQue.Enqueue(Skill);
    }


    public void O_push_back(GameObject Skill)
    {
        Skill.SetActive(false);

        O_SkillQue.Enqueue(Skill);
    }
    
    private  void FixedUpdate()
    {
        
    }
    /// <summary>
    /// 从i技能队列中得到预制体
    /// </summary>
    /// <returns></returns>
    public GameObject GetFromSkillMap()
    {
        if(SkillDic.ContainsKey("I_Skill"))
        {
            
            foreach(var value in SkillDic.Values)
            {
                // if(value.Count==0)
                // {
                //     InstanceI_SkillQue();
                // }
                
            }
        }
        if(I_SkillQue.Count>0)
        {
            var returnSkill= I_SkillQue.Dequeue();
            returnSkill.SetActive(true);
            return returnSkill;
        }
        
        return null ; 
    }
    

    public GameObject O_GetFromSkillMap()
    {
        
        var returnSkill= O_SkillQue.Dequeue();
        returnSkill.SetActive(true);
        return returnSkill;
    }

    public void teset()
    {
        
    }

}
