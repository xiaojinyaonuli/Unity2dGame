using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
     public static ObjectPool instance;

     public int ShadowCount;//阴影数量

     public GameObject perfabobject; //预制体

     private Queue<GameObject> objectspool= new Queue<GameObject>();
    // Update is called once per frame
    private void Awake()
    {
        instance=this;
    }
    private void  InstancePool()
    {
        for(var i=0;i<ShadowCount;++i)
        {
            var newobject=Instantiate(perfabobject);

            push_backpool(newobject);//返回对象池

        }
    }
    public void push_backpool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        objectspool.Enqueue(gameObject);
    }

    public GameObject GetFormObjectPool()
    {
        if(objectspool.Count==0)
        {
            InstancePool();
        }
        var shadow=objectspool.Dequeue();
        shadow.SetActive(true);
        return shadow;
    }

}
