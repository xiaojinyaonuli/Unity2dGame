using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkSystem : MonoBehaviour
{
   [Header("ui")]
   public Text textlab;
   public Image face;

   [Header("文本")]
   public int index;
   public TextAsset textAsset;

   public bool isFinsh;

   List<string> textlist=new List<string>();

   private void Awake()
   {
       GetTextforTextAsset(textAsset);
       index=0;
   }
   
   private void OnEnable()
   {
    //    textlab.text=textlist[index];
    //    index++;
    isFinsh=true;
    Talk.Instance.Talk_with();
    StartCoroutine(starttext());
   }
   private void Update()
   {

       if(Input.GetKeyDown(KeyCode.R)&&textlist.Count==index)
       {
           Talk.Instance.Talk_with();
           gameObject.SetActive(false);
           index=0;
           return;
       }
       if(Input.GetKeyDown(KeyCode.R)&&isFinsh)
       {
        //    textlab.text=textlist[index];
        //    index++;
        Talk.Instance.Talk_with();
        StartCoroutine(starttext());
       }
   }

   public void GetTextforTextAsset(TextAsset t)
   {
       textlist.Clear();
       index=0;
       var linedata=t.text.Split('\n');

       foreach (var line in linedata)
       {
           textlist.Add(line);
       }
   }

   IEnumerator starttext()
   {
       isFinsh=false;
       textlab.text=" ";
       for (int i = 0; i < textlist[index].Length; i++)
       {
           textlab.text+=textlist[index][i];
           yield return new WaitForSecondsRealtime(0.1f);
       }
       isFinsh=true;
       index++;
   }
}
