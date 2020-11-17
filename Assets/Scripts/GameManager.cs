using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public bool gameOver;
    [SerializeField] AudioClip theme;
    AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        gameOver = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        audioSource.Stop();
        UiManager.instance.GameStart();
        ScoreManager.instance.startScore();
        GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>().StartSpawningPlatforms();
    }
    public void GameOver()
    {
        if (!gameOver)
        {
            ScoreManager.instance.StopScore();
            //AdManager.instance.ShowInterstitialAd();
            UiManager.instance.GameOver();
            //UnityAdManager.instance.ShowAd();
            gameOver = true;

        }
    }
    
}
