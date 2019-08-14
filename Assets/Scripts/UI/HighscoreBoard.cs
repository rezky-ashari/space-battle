using RTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreBoard : PopupDialog
{
    public GameObject itemPrefab;
    public Transform itemContainer;

    protected override void Start()
    {
        base.Start();

        List<ScoreData> scores = GameData.Instance.scores;

        for (int i = 0; i < scores.Count; i++)
        {
            ScoreData data = scores[i];
            HighscoreItem item = Instantiate(itemPrefab, itemContainer, false).GetComponent<HighscoreItem>();
            item.SetValues(data.name, data.score);
        }
    }
}
