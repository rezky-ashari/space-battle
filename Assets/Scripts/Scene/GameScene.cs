using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTools;

/// <summary>
/// Gameplay scene.
/// </summary>
public class GameScene : Scene {

	// Use this for initialization
	void Start () {
		
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
            case "OnGameOver":
                Debug.Log("Game Over");
                break;
            default:
                break;
        }
    }
}