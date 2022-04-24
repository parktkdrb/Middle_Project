using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class NewBehaviourScript : MonoBehaviour
{
    //public float Speed = 0.5f; 
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    bool leftFlag = false;
    private void Start()
    {
        actions.Add("left", Left) ;
        actions.Add("right", Right);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }
    void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void Left()
    {
        transform.Translate(-1, 0, 0);
        leftFlag = true;

    }

    public void Right()
    {
        transform.Translate(0, -1, 0);
        leftFlag = false;

    }

    // 이 밑에부터는 음성 인식이 아닌 일반 키보드 입력으로 
    // 캐릭터의 움직임을 확인하려는 목적으로 짠 코드입니다.

    private void Update()
    {
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;
        if (Input.GetKey("a"))
        {
            //transform.Translate(-Speed, 0, 0);
            leftFlag = true;
        }
        if (Input.GetKey("d"))
        {
            //transform.Translate(Speed, 0, 0);
            leftFlag = false;
            
        }
    }
    
}

