using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsControal : MonoBehaviour
{
    
    public float speed;

    private Rigidbody2D goodsrb;

    
    private void Awake()
    {
        goodsrb=GetComponent<Rigidbody2D>();

        float angle=Random.Range(-30f,30);

        goodsrb.velocity=Quaternion.AngleAxis(angle,Vector3.forward)*Vector3.up*speed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
