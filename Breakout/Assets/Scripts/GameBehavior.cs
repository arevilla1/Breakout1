using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public int MaximumScore;

    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives)
    {
        //Update lives to either positive or negative
        lives += changeInLives;
        livesText.text = "Lives: " + lives;
        GameOver();
    }

    public void UpdateScore(int changeInPoints)
    {
        score += changeInPoints;
        scoreText.text = "Score: " + score;
        GameOver();
    }

    public void GameOver()
    {
        if(lives == 0 || score == MaximumScore)
        {
            SceneManager.LoadScene(2);
        }
    }
}
