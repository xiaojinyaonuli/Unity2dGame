using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pool : MonoBehaviour
{
    public static Enemy_Pool instance;

     public int ExCount;

     public GameObject Explosionperfabobject; //预制体

     private Queue<GameObject> objectspool= new Queue<GameObject>();
    // Update is called once per frame
    private void Awake()
    {
        instance=this;
    }
    
    private void  InstancePool()
    {
        for(var i=0;i<ExCount;++i)
        {
            var newobject=Instantiate(Explosionperfabobject);

            push_backExplosion_pool(newobject);

        }
    }
    public void push_backExplosion_pool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        objectspool.Enqueue(gameObject);
    }

    public GameObject GetFormObjectPool_Explosion()
    {
        if(objectspool.Count==0)
        {
            InstancePool();
        }
        var Explosion=objectspool.Dequeue();
        Explosion.SetActive(true);
        return Explosion;
    }

}
