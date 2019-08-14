using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTools;

public class MainMenu : Scene {

    public GameButton highscoreButton;
    public HighscoreBoard highscoreBoard;

	// Use this for initialization
	void Start () {
        GameData.Instance.Load();

        highscoreButton.gameObject.SetActive(GameData.Instance.scores.Count > 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Handle click event
	public override void HandleClick(string id, string value)
	{
        switch (id)
        {
            case "play":
                SceneSwitchR.To("GameScene");
                break;
            case "highscore":
                highscoreBoard.Show();
                break;
            default:
                break;
        }
    }

	// Listener for custom event
	public override void OnEvent(string name)
    {

    }
}