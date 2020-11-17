using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {
    public static UiManager instance;
    public GameObject zigzagPanel;
    public GameObject gameOverPanel;
    public GameObject tapText;
    public Text score;
    public Text highScore1;
    public Text highScore2;
    public Text ScoreText;
    bool GamerOvers;
    int Score;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start () {
        GamerOvers = false;
        Score = 0;
        InvokeRepeating("incrementScore", 1f,5f);
        highScore1.text = "High Score: " + PlayerPrefs.GetInt("highScore");
    }

    public void GameStart()
    {
        tapText.SetActive(false);
        zigzagPanel.GetComponent<Animator>().Play("panelUp");
    }
    public void GameOver()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
         highScore2.text = PlayerPrefs.GetInt("highScore").ToString();
        gameOverPanel.SetActive(true);
    }
    void scoreUpdate()
    {
        if (GamerOvers == false)
            Score += 1;
    }
    public void gameover()
    {
        GamerOvers = true;
    }
    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update() {
        ScoreText.text = ScoreManager.instance.score.ToString();
        //ScoreText.text = "Score: " + Score;


    }     

    public void Exit()
    {
        Application.Quit();
    }
  
        
 
    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}
