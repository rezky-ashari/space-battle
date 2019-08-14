using System;
using RTools;
using UnityEngine;

public class PlayerShip : Ship
{
    [Tooltip("Player ship's move speed")]
    public float moveSpeed = 10;

    [Tooltip("Cooldown before next shoot")]
    public float cooldownTime = 1;

    [Tooltip("Lives indicator for player ship")]
    public LivesPanel livesPanel;

    [Tooltip("Player ship's shield")]
    public SpriteRenderer shield;

    [Tooltip("Healthbar for shield")]
    public Healthbar shieldHealth;

    public bool allowInput = true;

    float time = 0;
    RezTween rechargeTween;

    protected override void Start()
    {
        base.Start();
        shieldHealth.regenerateHealth = false;
    }

    public void Initialize()
    {
        livesPanel.live = SessionData.lives;
        shieldHealth.maximumHealth = shieldHealth.health = GameData.Instance.shieldCapacity;
        shieldHealth.lowHealth = (int)(shieldHealth.maximumHealth * 0.25f);
        shieldHealth.highHealth = (int)(shieldHealth.maximumHealth * 0.75f);
        UpdateShield();
    }

    private void OnEnable()
    {
        UpdateShield();
    }

    // Update is called once per frame
    void Update()
    {
        if (!allowInput)
            return;

        // Move based on keyboard input
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Shoot with cooldown time
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                time = cooldownTime;
            }
            if (Input.GetMouseButtonDown(1))
            {
                LaunchRocket();
                time = cooldownTime;
            }
        }

        // Always facing mouse pointer
        spriteTransform.rotation = Util.GetRotationToMouse(transform.position);
    }

    private void LaunchRocket()
    {
        if (SessionData.rocket > 0)
        {
            SessionData.rocket--;
            BulletManager.Instance.GetRocket(this).ShootToMousePointer(transform.position);
            Scene.SendEvent("OnUsedRocket");
        }
    }

    protected override void OnGotShot(int damage, string source)
    {
        if (shieldHealth.health > 0)
        {
            shieldHealth.health -= damage;
            UpdateShield();

            if (shieldHealth.health <= 0) shield.gameObject.SetActive(false);

            RezTween.Destroy(ref rechargeTween);
            rechargeTween = RezTween.DelayedCall(GameData.Instance.shieldRechargeRate, ()=>
            {
                shield.gameObject.SetActive(true);
                rechargeTween = RezTween.ValueRange(shieldHealth.health, shieldHealth.maximumHealth, 0.5f, progress =>
                {
                    shieldHealth.health = progress;
                    UpdateShield();
                });
                rechargeTween.OnComplete = UpdateShield;
            });
        }
        else
        {
            livesPanel.live--;
            if (livesPanel.live == 0)
            {
                Scene.SendEvent("OnGameOver");
            }
        }        
    }

    void UpdateShield()
    {
        shieldHealth.UpdateHealth();

        Color shieldColor = shield.color;
        shieldColor.a = shieldHealth.health / shieldHealth.maximumHealth;
        shield.color = shieldColor;
    }

    protected override void OnCollideWith(GameObject other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other);
            SessionData.coins++;
            Scene.SendEvent("OnGotCoin");
        }
    }
}
