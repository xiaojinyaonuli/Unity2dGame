using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public static int PillsCount;
    public GameObject Panel;

    protected bool keydown;

    protected bool IsCollider;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        //sPanel=GameObject.FindGameObjectWithTag("objectcollect");
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual  void Update()
    {
         if(Input.GetKeyDown(KeyCode.Space)&&IsCollider)
         {
             keydown=true;
            
             PillsCount+=1;

            GameObject.FindWithTag("Player").GetComponent<PLayerControl>().PillsCount=PillsCount;
            Destroy(this.gameObject);
         }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            
             //Panel.SetActive(true);
            collectUI.instance.SetTrue();
            
            IsCollider=true;
            
        }
        
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            //Panel.SetActive(false);
            collectUI.instance.SetFalse();
            IsCollider=false;
        }
    }
}
