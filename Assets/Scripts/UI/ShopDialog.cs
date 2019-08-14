using System;
using RTools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopDialog : PopupDialog
{
    public Text coinDisplay;

    [Header("Shop Buttons")]
    public ShopButton addLife;
    public ShopButton rechargeRocket;
    public ShopButton upgradeRocket;
    public ShopButton upgradeShieldRechargeRate;
    public ShopButton upgradeShieldCapacity;

    GameData GameData => GameData.Instance;

    protected override void Start()
    {
        base.Start();

        AddClickListener(addLife, AddLife);
        AddClickListener(rechargeRocket, RechargeRocket);
        AddClickListener(upgradeRocket, UpgradeRocket);
        AddClickListener(upgradeShieldRechargeRate, UpgradeShieldRechargeRate);
        AddClickListener(upgradeShieldCapacity, UpgradeShieldCapacity);
    }

    void AddClickListener(ShopButton shopButton, UnityAction<string> listener)
    {
        shopButton.button.onClick.AddListener(listener);
    }

    public override void Show()
    {
        UpdateCoinDisplay();

        UpdateState(addLife, SessionData.lives < 3, SessionData.lives, 3);
        UpdateState(rechargeRocket, SessionData.rocket < GameData.rocketCapacity, SessionData.rocket, GameData.rocketCapacity);
        UpdateState(upgradeRocket, GameData.rocketCapacity < 10, GameData.rocketCapacity, 10);
        UpdateState(upgradeShieldRechargeRate, GameData.shieldRechargeRate > 1, GameData.shieldRechargeRate + " second(s)");
        UpdateState(upgradeShieldCapacity, GameData.shieldCapacity < 100, GameData.shieldCapacity, 100);

        base.Show();
    }

    private void UpdateCoinDisplay()
    {
        coinDisplay.text = SessionData.coins.ToString();
    }

    void UpdateState(ShopButton shopButton, bool interactable, string status)
    {
        shopButton.button.interactable = interactable;
        shopButton.status.text = status;
    }

    void UpdateState(ShopButton shopButton, bool interactable, int current, int max)
    {
        shopButton.button.interactable = interactable;
        shopButton.status.text = current + "/" + max;
    }

    /// <summary>
    /// Plus 1 life. Maximum is 1 per round, not more than 3.
    /// </summary>
    /// <param name="sender"></param>
    private void AddLife(string sender)
    {
        if (!TryPurchase(addLife)) return;

        SessionData.lives++;
        addLife.button.interactable = false;
    }

    /// <summary>
    /// Upgrade shield capacity. 10 each buy. Maximum is 100.
    /// </summary>
    /// <param name="sender"></param>
    private void UpgradeShieldCapacity(string sender)
    {
        if (!TryPurchase(upgradeShieldCapacity)) return;

        if (GameData.shieldCapacity >= 100)
        {
            DialogBox.ShowDialog("Shield capacity has reached it's maximum.");
            upgradeShieldCapacity.button.interactable = false;
            return;
        }

        GameData.shieldCapacity += 10;
    }

    /// <summary>
    /// Reduces the waiting time by 0.5 seconds.
    /// </summary>
    /// <param name="sender"></param>
    private void UpgradeShieldRechargeRate(string sender)
    {
        if (!TryPurchase(upgradeShieldRechargeRate)) return;

        if (GameData.shieldRechargeRate <= 1)
        {
            DialogBox.ShowDialog("Can not decrease recharge rate below 1 second.");
            upgradeShieldRechargeRate.button.interactable = false;
            return;
        }

        GameData.shieldRechargeRate -= 0.5f;
    }

    /// <summary>
    /// Increase rocket capacity. 1 each buy. Maximum is 10.
    /// </summary>
    /// <param name="sender"></param>
    private void UpgradeRocket(string sender)
    {
        if (!TryPurchase(upgradeRocket)) return;

        if (GameData.rocketCapacity >= 10)
        {
            DialogBox.ShowDialog("You have reached the maximum rocket capacity.");
            upgradeRocket.button.interactable = false;
            return;
        }

        GameData.rocketCapacity++;
    }

    /// <summary>
    /// Recharge rocket to it's maximum.
    /// </summary>
    /// <param name="sender"></param>
    private void RechargeRocket(string sender)
    {
        if (!TryPurchase(rechargeRocket)) return;

        SessionData.rocket = GameData.rocketCapacity;
        upgradeRocket.button.interactable = false;
    }

    /// <summary>
    /// Try to purchase a shop item. Coins will be automatically decreased if purchase is possible.
    /// </summary>
    /// <param name="shopButton">Shop button</param>
    /// <returns></returns>
    bool TryPurchase(ShopButton shopButton)
    {
        if (SessionData.coins >= shopButton.cost)
        {
            SessionData.coins -= shopButton.cost;
            UpdateCoinDisplay();
            return true;
        }
        else
        {
            DialogBox.ShowDialog("Not enough coins");
        }
        return false;
    }
}
