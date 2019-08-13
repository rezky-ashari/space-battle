using RTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Text status;
    public Text price;
    public GameButton button;
    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        price.text = GetPlural(SessionData.coins, "coin", "coins");
    }

    string GetPlural(int count, string single, string other)
    {
        return count + " " + (count == 1 ? single : other);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
