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
        if (nameField.text.Trim().Length == 0)
        {
            DialogBox.ShowDialog("Plase fill in your name!");
            return;
        }
        GameData.Instance.AddScore(nameField.text, SessionData.score);
        Scene.SendClickEvent("mainmenu");
    }
}
