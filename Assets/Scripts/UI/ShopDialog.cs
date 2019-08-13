using RTools;
using UnityEngine.Events;

public class ShopDialog : PopupDialog
{
    public ShopButton addLife;
    public ShopButton rechargeRocket;
    public ShopButton upgradeRocket;
    public ShopButton upgradeShieldRechargeRate;
    public ShopButton upgradeShieldCapacity;

    GameData GameData => GameData.Instance;

    // Start is called before the first frame update
    void Start()
    {
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

    private void OnEnable()
    {
        SetInteractable(addLife, SessionData.lives < 3);
        SetInteractable(rechargeRocket, SessionData.rocket < GameData.rocketCapacity);
        SetInteractable(upgradeRocket, GameData.rocketCapacity < 10);
        SetInteractable(upgradeShieldRechargeRate, GameData.shieldRechargeRate > 1);
        SetInteractable(upgradeShieldCapacity, GameData.shieldCapacity < 100);
    }

    void SetInteractable(ShopButton shopButton, bool interactable)
    {
        shopButton.button.interactable = interactable;
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
            SetInteractable(upgradeShieldCapacity, false);
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
            SetInteractable(upgradeShieldRechargeRate, false);
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
            SetInteractable(upgradeRocket, false);
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
        SetInteractable(rechargeRocket, false);
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
            return true;
        }
        return false;
    }
}
