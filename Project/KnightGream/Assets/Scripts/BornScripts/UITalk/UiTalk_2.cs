using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTalk_2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tiejiangplane,maoxianjia;

    
    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            Talk.Instance.Talk_with();
            tiejiangplane.SetActive(true); 
        
        
             maoxianjia.SetActive(true);       
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         tiejiangplane.SetActive(false); 
        
        
             maoxianjia.SetActive(false);      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
