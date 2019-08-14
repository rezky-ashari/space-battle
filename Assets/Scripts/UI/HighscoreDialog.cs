using RTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDialog : PopupDialog
{
    public Text scoreDisplay;
    public InputField nameField;


    public override void Show()
    {
        scoreDisplay.text = SessionData.score.ToString();

        base.Show();
    }


    public void SaveRecord()
    {
        Debug.Log("Save record here");
    }
}
