using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreItem : MonoBehaviour
{
    public Text nameDisplay;
    public Text scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues(string name, int score)
    {
        nameDisplay.text = name;
        scoreDisplay.text = score.ToString();
    }
}
