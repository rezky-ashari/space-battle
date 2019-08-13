using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTools;
using UnityEngine.UI;
using System;

/// <summary>
/// Gameplay scene.
/// </summary>
public class GameScene : Scene {

    public Text roundDisplay;
    public Text playerScore;
    public Text rocketCount;
    public Text killCount;
    public Text coinDisplay;
    public EnemyManager enemyManager;
    public CanvasGroup gameOverUI;
    public PlayerShip playerShip;
    public GameObject UICanvas;

    GameData GameData => GameData.Instance;

	// Use this for initialization
	void Start () {
        ResetState();
	}

    void ResetState()
    {
        SessionData.Reset();

        playerScore.text = SessionData.score.ToString();
        killCount.text = SessionData.enemiesKilled.ToString();
        rocketCount.text = SessionData.rocket.ToString();
        coinDisplay.text = SessionData.coins.ToString();

        playerShip.Initialize();
        playerShip.allowInput = true;

        UICanvas.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	// Handle click event
	public override void HandleClick(string id, string value)
	{

	}

	// Listener for custom event
	public override void OnEvent(string name)
    {
        switch (name)
        {
            case "OnUsedRocket":
                UpdateRocketCount();
                break;
            case "OnGotCoin":
                AddCoin();
                break;
            case "OnEnemyKilled":
                UpdateEnemiesKilled();
                break;
            case "OnGotHitByPlayer":
                UpdateScore();
                break;
            case "OnGameOver":
                OnGameOver();
                break;
            default:
                break;
        }
    }

    private void OnGameOver()
    {
        playerShip.allowInput = false;
        RezTween.To(gameOverUI, 0.5f, "alpha:1").OnComplete = () =>
        {
            playerShip.transform.position = Vector2.zero;
            enemyManager.StopSpawn();
            UICanvas.SetActive(false);

            RezTween.To(gameOverUI, 0.5f, "alpha:0", RezTweenOptions.Delay(2f)).OnComplete = () =>
            ShowShopUI();
        };
        
    }

    void ShowShopUI()
    {

    }

    private void UpdateRocketCount()
    {
        UpdateText(rocketCount, SessionData.rocket);
    }

    private void AddCoin()
    {
        UpdateText(coinDisplay, SessionData.coins);
    }

    private void UpdateEnemiesKilled()
    {
        UpdateText(killCount, SessionData.enemiesKilled);
    }

    private void UpdateScore()
    {
        UpdateText(playerScore, SessionData.score);
    }

    void UpdateText(Text text, int value)
    {
        text.text = value.ToString();
        RezTween.ScaleFromTo(text.gameObject, 0.5f, 0.5f, 1f, RezTweenEase.BACK_OUT);
    }
}