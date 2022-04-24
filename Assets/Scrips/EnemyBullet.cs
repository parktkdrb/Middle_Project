using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class EnemyBullet : MonoBehaviour
{
    public float Speed = 10.0f;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    void Start()
    {
        actions.Add("left", Left);
        actions.Add("right", Right);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }
    public void Update()
    {
        float BulletSpeed = Speed * Time.deltaTime;
        transform.Translate(Vector3.down * BulletSpeed);
        if (transform.position.y < 2.5f)
        {
            InitPosition();
            SceneManager.LoadScene("Start");
        }
        if (transform.position.y < 9.5f && transform.position.y > 5.5f)
        {
            if (Input.GetKey("a"))
            {
                InitPosition();
                Speed = Speed + 1;
            }
        }

    }
    void InitPosition()
    {
        int range = (int)UnityEngine.Random.Range(0.0f, 1.0f);
        transform.position = new Vector3(range, 23, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            InitPosition();
        }
    }


    private void OnDestroy()
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
        if (transform.position.y < 9.5f && transform.position.y > 5.5f)
        {
            InitPosition();
            Speed = Speed + 1;
        }

    }
    public void Right()
    {
        if (transform.position.y < -9.5f && transform.position.y > -5.5f)
        {
            InitPosition();
            Speed = Speed + 1;
        }


    }

}


