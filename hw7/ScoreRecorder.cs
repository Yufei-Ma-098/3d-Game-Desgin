using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecorder : MonoBehaviour
{
    public Text ScoreText;
    int Score = 0;
    void GetScore()
    {
        Score++;
    }
    // Use this for initialization  
    void Start()
    {
        Gate.addScore += GetScore;
    }

    // Update is called once per frame  
    void Update()
    {

    }
}