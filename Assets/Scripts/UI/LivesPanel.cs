using RTools;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LivesPanel : MonoBehaviour
{
    const int MAX_LIVES = 3;

    [Range(0, MAX_LIVES)]
    public int live = 0;
    public List<ColorSwitcher> icons;

    int lastLive = -1;

    private void Reset()
    {
        icons = new List<ColorSwitcher>();
        for (int i = 0; i < transform.childCount; i++)
        {
            icons.Add(transform.GetChild(i).GetComponent<ColorSwitcher>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastLive != live)
        {
            UpdateLives();
            lastLive = live;
        }
    }

    private void UpdateLives()
    {
        for (int i = 0; i < MAX_LIVES; i++)
        {
            icons[i].SetCurrentColor(i + 1 <= live? 0 : 1);
        }
    }
}
