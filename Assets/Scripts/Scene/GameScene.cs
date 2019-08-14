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

    public Text playerScore;
    public Text rocketCount;
    public Text killCount;
    public Text coinDisplay;
    public EnemyManager enemyManager;
    public CanvasGroup endingOverlay;
    public Text endingText;
    public PlayerShip playerShip;
    public GameObject UICanvas;
    public ShopDialog shopDialog;
    public HighscoreDialog highscoreDialog;
    public TutorialDialog tutorialDialog;
    public GameObject pauseMenu;
    public int roundDuration = 60;

    GameData GameData => GameData.Instance;

    RezTween.Timer roundTimer;
    int secondsElapsed;

    bool isShowingEndingUI = false;
    bool firstTimePlay = true;

	// Use this for initialization
	void Start () {
        enemyManager.spawnInterval = 11;
        SessionData.Initialize();
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
        isShowingEndingUI = false;

        secondsElapsed = 0;
        roundTimer = new RezTween.Timer(1, roundDuration)
        {
            onTick = LogSeconds,
            onComplete = OnTimeUp
        };        

        // Increase enemy linearly
        if (enemyManager.spawnInterval > 1)
        {
            enemyManager.spawnInterval -= 1;
        }        

        if (firstTimePlay)
        {
            tutorialDialog.onClose = () =>
            {
                firstTimePlay = false;
                enemyManager.StartSpawn();
                roundTimer.Start();
                Destroy(tutorialDialog.gameObject);
            };
            RezTween.DelayedCall(0.5f, () => tutorialDialog.Show());
        }
        else
        {
            roundTimer.Start();
            enemyManager.StartSpawn();
        }
    }

    private void LogSeconds()
    {
        secondsElapsed++;
        //Debug.Log("Seconds Elapsed: " + secondsElapsed);
    }

    // Update is called once per frame
    void Update () {
		if (!isShowingEndingUI && Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
	}

    public void Pause()
    {
        pauseMenu.SetActive(true);
        playerShip.allowInput = false;
        Time.timeScale = 0;
    }

	// Handle click event
	public override void HandleClick(string id, string value)
	{
        switch (id)
        {
            case "unpause":
                Time.timeScale = 1;
                playerShip.allowInput = true;
                pauseMenu.SetActive(false);
                break;
            case "mainmenu":
                Time.timeScale = 1;
                SceneSwitchR.To("MainMenu");
                break;
            case "continue":
                shopDialog.Hide();
                ResetState();
                break;
            default:
                break;
        }
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
                UpdateCoin();
                break;
            case "OnEnemyKilled":
                UpdateEnemiesKilled();
                break;
            case "OnGotHitByPlayer":
                UpdateScore();
                break;
            case "OnGameOver":
                if (!shopDialog.IsVisible)
                {
                    OnGameOver();
                }
                break;
            default:
                break;
        }
    }

    void ShowEnding(string text, Action onComplete)
    {
        if (!isShowingEndingUI)
        {
            roundTimer.Stop(17);
            endingText.text = text;
            playerShip.allowInput = false;
            RezTween.To(endingOverlay, 0.5f, "alpha:1").OnComplete = () =>
            {
                playerShip.transform.position = Vector2.zero;
                enemyManager.StopSpawnAndDestroyEnemies();
                BulletManager.Instance.ClearBullets();
                UICanvas.SetActive(false);

                RezTween.To(endingOverlay, 0.5f, "alpha:0", RezTweenOptions.Delay(2f)).OnStart = onComplete;
            };

            isShowingEndingUI = true;
        }
    }

    private void OnGameOver()
    {
        ShowEnding("GAME OVER", () =>
        {
            highscoreDialog.Show();
        });
    }

    private void OnTimeUp()
    {
        Debug.Log("Time up");
        ShowEnding("ROUND END", () =>
        {
            shopDialog.Show();
        });
    }

    private void UpdateRocketCount()
    {
        UpdateText(rocketCount, SessionData.rocket);
    }

    private void UpdateCoin()
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