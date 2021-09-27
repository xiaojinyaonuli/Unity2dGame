using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senceinfluence_ : MonoBehaviour
{
    // Start is called before the first frame update
    public static Senceinfluence_ instance;

    private bool isshake;
    void Start()
    {
        instance=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSuspend(int time)
    {
        StartCoroutine(Suspend(time));
    }
    IEnumerator Suspend(int time)
    {
        float suspendtime=time/60f;

        Time.timeScale=0;

        yield return new WaitForSecondsRealtime(suspendtime);
        
         Time.timeScale=1;
    }
    public void CamerastartShake(float time,float strenght)
    {
        if(!isshake)
        {
            StartCoroutine(CamreaShake( time,strenght));
        }
    }
    IEnumerator CamreaShake(float time,float strenght)
    {
        isshake=true;
        Transform camrea=Camera.main.transform;
        Vector3 startposition=camrea.position;

        while(time>0)
        {
            camrea.position=Random.insideUnitSphere*strenght+startposition;
            time-=Time.deltaTime;
            yield return null;
        }
       
        isshake=false;
    }
}
