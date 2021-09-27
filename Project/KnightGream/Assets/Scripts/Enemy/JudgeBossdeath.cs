using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeBossdeath : MonoBehaviour
{
    // Start is called before the first frame update
    

    public GameObject ChoiceBar,BOSS;
   

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BossDeath());
    }
    IEnumerator BossDeath()
    {
        if(BOSS.activeSelf==false)
        {
            
            yield return new WaitForSecondsRealtime(1f);
            ChoiceBar.SetActive(true);
        }
    }
}
