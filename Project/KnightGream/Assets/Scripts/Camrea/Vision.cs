using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public Transform camreaTransform;

    public float MoveArea;

    public float Startpoint;
    void Start()
    {
        Startpoint=transform.position.x;
    }

    
    void Update()
    {
        transform.position=new Vector2(Startpoint+camreaTransform.position.x*MoveArea,transform.position.y);
    }
}
