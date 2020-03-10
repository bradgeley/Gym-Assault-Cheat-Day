using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int defaultScorePerEnemyKill = 25;

    [Tooltip("Points earned per increment.")]
    [SerializeField] int scorePerIncrement = 5;

    [Tooltip("Time it takes to earn points.")]
    [SerializeField] float timeBetweenIncrements = 1;

    [Tooltip("Time it takes to multiply points gain per increment.")]
    [SerializeField] float timeRequiredForScoreAcceleration = 10;

    //Private variables
    private int score = 0;
    private Text scoreText;
    private float timeOfLastIncrement = 0; //used to increment score at fixed time intervals

    void Start()
    {
        scoreText = GetComponent<Text>();
        score = 0;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        HandleTimeBasedScore();
    }

    private void HandleTimeBasedScore()
    {
        if (Time.time - timeOfLastIncrement > timeBetweenIncrements)
        {
            //Accelerate score based on time without dying
            float numberOfScoreIncrements = Mathf.Floor(Time.time / timeRequiredForScoreAcceleration);
            //Figure out score for this increment and increase it
            float scoreThisIncrement = (numberOfScoreIncrements + 1) * scorePerIncrement;
            //Update
            IncrementScoreBy((int) scoreThisIncrement);
            timeOfLastIncrement = Time.time;
        }
    }

    public void ScoreHit(int value)
    {
        IncrementScoreBy(value);
    }

    public void ScoreHit()
    {
        IncrementScoreBy(defaultScorePerEnemyKill);
    }

    public void IncrementScoreBy(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
