using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatxue : MonoBehaviour
{
    public static Floatxue instance;

     public int Count;

     public GameObject perfabobject; //预制体

     private Queue<GameObject> objectspool= new Queue<GameObject>();

     private void Start()
     {
        
     }
    // Update is called once per frame
    private void Awake()
    {
        instance=this;
    }
    private void  InstancePool()
    {
        for(var i=0;i<Count;++i)
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
        var Floatprefab=objectspool.Dequeue();
        Floatprefab.SetActive(true);
        return Floatprefab;
    }
}
